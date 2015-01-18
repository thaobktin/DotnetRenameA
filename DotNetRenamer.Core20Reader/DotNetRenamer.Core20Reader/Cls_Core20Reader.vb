Imports System.Runtime.InteropServices
Imports System.IO

' Building a dotnet Disassembler                              : http://codingwithspike.wordpress.com/2012/07/20/building-a-net-disassembler-part-1/
' x86 Disassembly-Windows Executable Files                    : http://en.wikibooks.org/wiki/X86_Disassembly/Windows_Executable_Files
' .NET File Format                                            : http://www.codeproject.com/KB/dotnet/dotnetformat.aspx
' .NET File Format                                            : http://ntcore.com/files/dotnetformat.htm
' The Microsoft papers (the Bible)                            : http://msdn.microsoft.com/en-us/library/gg463119.aspx
Public Class Cls_Core20Reader
    Implements ICore20Reader

#Region " CONSTANTS "
    Private Structure ImageConstants
        ' MZ
        Const IMAGE_DOS_SIGNATURE As UShort = 23117
        ' PE00
        Const IMAGE_NT_SIGNATURE As UInteger = 17744
        ' Intel 386
        Const IMAGE_FILE_MACHINE_I386 As UShort = 332
        ' Intel 64
        Const IMAGE_FILE_MACHINE_IA64 As UShort = 512
        ' AMD64
        Const IMAGE_FILE_MACHINE_AMD64 As UShort = 34404
        ' PE32
        Const IMAGE_NT_OPTIONAL_HDR32_MAGIC As UShort = 267
        ' PE32+
        Const IMAGE_NT_OPTIONAL_HDR64_MAGIC As UShort = 523
        ' DLL or EXE
        Const IMAGE_FILE_DLL As UShort = 8192
        ' 32 BITS HEADER
        Const IMAGE_FILE_32BIT_MACHINE As UShort = 256
    End Structure
#End Region

#Region " PRIVATE MEMBERS "
    ' The selected file
    Private m_filePath As String = String.Empty
    ' The DOS header
    Private m_DosHeader As IMAGE_DOS_HEADER
    ' The Section header
    Private m_SectionHeader As IMAGE_SECTION_HEADER
    ' The NT Header 32 Bits
    Private m_NTHeader32 As IMAGE_NT_HEADERS32
    ' The NT Header 64 Bits
    Private m_NTHeader64 As IMAGE_NT_HEADERS64
    ' The file header
    Private Shared m_FileHeader As IMAGE_FILE_HEADER
    ' The Cli Header
    Private Shared m_CoreHeader As IMAGE_COR20_HEADER
    ' Data directories
    Private Shared m_dataDirectories As Dictionary(Of Integer, Cls_DataDirectory)
    ' Sections
    Private Shared m_sections As Dictionary(Of Integer, IMAGE_SECTION_HEADER)
    ' The Metadata header
    Private Shared m_metadata As METADATA_HEADER
    ' The target Runtime
    Private Shared m_TargetFramework As String
    ' The signature
    Private Shared m_PlatformSignature As String
    ' The entrypoint of the file
    Private Shared m_entryPoint As String
#End Region

#Region " PUBLIC PROPERTIES "
    ''' <summary>
    ''' Gets the selected file
    ''' </summary>
    Public ReadOnly Property FilePath() As String
        Get
            Return m_filePath
        End Get
    End Property

    ''' <summary>
    ''' Gets the AddressOfEntryPoint from optionalHeader
    ''' </summary>
    Public ReadOnly Property GetAddressOfEntryPoint() As String
        Get
            Return m_entryPoint
        End Get
    End Property

    ''' <summary>
    ''' Gets the target systemType : Forms or Gui only ! Modify it as your own !
    ''' </summary>
    Public ReadOnly Property GetSystemType As String Implements ICore20Reader.GetSystemType
        Get
            If m_PlatformSignature = "x64" Then
                If m_NTHeader64.OptionalHeader.Subsystem = SubSystemTypes.IMAGE_SUBSYSTEM_WINDOWS_GUI Then
                    Return "Forms"
                ElseIf m_NTHeader64.OptionalHeader.Subsystem = SubSystemTypes.IMAGE_SUBSYSTEM_WINDOWS_CUI Then
                    Return "Console"
                End If
            Else
                If m_NTHeader32.OptionalHeader.Subsystem = SubSystemTypes.IMAGE_SUBSYSTEM_WINDOWS_GUI Then
                    Return "Forms"
                ElseIf m_NTHeader32.OptionalHeader.Subsystem = SubSystemTypes.IMAGE_SUBSYSTEM_WINDOWS_CUI Then
                    Return "Console"
                End If
            End If
            Return "Unknown"
        End Get
    End Property

    ''' <summary>
    ''' Gets the targetplatform of the file : (x64, x32, AnyCPU, Itanium)
    ''' </summary>
    Public ReadOnly Property GetTargetPlatform As String Implements ICore20Reader.GetTargetPlatform
        Get
            If m_FileHeader.Machine = MachineType.x64 Then
                Return "x64"
            ElseIf m_FileHeader.Machine = MachineType.I386 Then
                If CBool(m_CoreHeader.Flags = Cls_Core20Reader.RuntimeFlags.Required32Bit) = True Then
                    Return "x32"
                Else
                    Return "AnyCPU"
                End If
            ElseIf m_FileHeader.Machine = MachineType.Itanium Then
                Return "Itanium"
            End If
            Return ""
        End Get
    End Property

    ''' <summary>
    ''' Gets the target Runtime of the file (Framework version)
    ''' </summary>
    Public ReadOnly Property GetTargetRuntime As String Implements ICore20Reader.GetTargetRuntime
        Get
            Return If(m_TargetFramework = String.Empty, "Unknown", m_TargetFramework)
        End Get
    End Property

    ' ''' <summary>
    ' ''' Verifying executable flag from IMAGE_FILE_HEADER Characteristics 
    ' ''' </summary>
    Public Function isExecutable() As Boolean Implements ICore20Reader.isExecutable
        If (m_FileHeader.Characteristics And ImageConstants.IMAGE_FILE_DLL) <> 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Checks if the file is DotNet testing the existence of the DataDirectory 14 and its size
    ''' </summary>
    Public ReadOnly Property isManagedFile() As Boolean Implements ICore20Reader.isManagedFile
        Get
            Return (If((m_dataDirectories.Count >= 14 And m_dataDirectories.Item(14).Size > 0) = True, True, False))
        End Get
    End Property

    ''' <summary>
    ''' Gets the file header
    ''' </summary>
    Friend ReadOnly Property FileHeader() As IMAGE_FILE_HEADER
        Get
            Return m_FileHeader
        End Get
    End Property

    ''' <summary>
    ''' Gets the 64 Bits Image_NT_header 
    ''' </summary>
    Friend ReadOnly Property ImageNtHeaders64() As IMAGE_NT_HEADERS64
        Get
            Return m_NTHeader64
        End Get
    End Property

    ''' <summary>
    ''' Gets the 32 Bits Image_NT_header 
    ''' </summary>
    Friend ReadOnly Property ImageNtHeaders32() As IMAGE_NT_HEADERS32
        Get
            Return m_NTHeader32
        End Get
    End Property

    ''' <summary>
    ''' Gets the Core20_Header. Allow to get the flag "Required32Bits" to determine the AnyCPU file capability.
    ''' </summary>
    Friend ReadOnly Property Core20Header() As IMAGE_COR20_HEADER
        Get
            Return m_CoreHeader
        End Get
    End Property

    ''' <summary>
    ''' Gets the Metadata_Header. Allow to get the target Runtime of the file (Framework version)
    ''' </summary>
    Friend ReadOnly Property MetadataHeader() As METADATA_HEADER
        Get
            Return m_metadata
        End Get
    End Property

    ''' <summary>
    ''' Gets if the file header is 32 bit or not. Seems to be the same as "Required32Bits" flag from the CliHeader !
    ''' </summary>
    Public ReadOnly Property Is32BitHeader() As Boolean
        Get
            Return (m_FileHeader.Characteristics And ImageConstants.IMAGE_FILE_32BIT_MACHINE) = ImageConstants.IMAGE_FILE_32BIT_MACHINE
        End Get
    End Property
#End Region

#Region " PRIVATE METHODS "
    ''' <summary>
    ''' Convert numeric value to ASCII chars. 
    ''' </summary>
    Private Shared Function ConvertToString(str As Object) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(BitConverter.GetBytes(str))
    End Function

    ''' <summary>
    ''' Reading DataDirectories and assign to a dictionary. Usually 16 entries !
    ''' </summary>
    Private Sub ReadDataDirectories(reader As BinaryReader, numberOfRva%)
        m_dataDirectories = New Dictionary(Of Integer, Cls_DataDirectory)
        'MsgBox(numberOfRva.ToString)
        For i As Integer = 0 To If(numberOfRva < 16, 16, numberOfRva - 1)
            m_dataDirectories.Add(i, New Cls_DataDirectory() With { _
                 .Address = reader.ReadUInt32(), _
                 .Size = reader.ReadUInt32() _
            })
        Next
    End Sub

    ''' <summary>
    ''' Looking for which section points the DataDirectory !
    ''' </summary>
    ''' <returns>the start Offset</returns>
    Private Function ReadVirtualDirectory(dataDirectory As Cls_DataDirectory) As Cls_DataDirectory
        For Each s In m_sections
            ' Find the section whose virtual address range contains the data directory's RVA.
            If s.Value.VirtualAddress <= dataDirectory.Address AndAlso dataDirectory.Address <= s.Value.VirtualAddress + s.Value.VirtualSize Then
                ' Calculate the offset into the file.
                dataDirectory.SectionStartOffset = s.Value.PointerToRawData + (dataDirectory.Address - s.Value.VirtualAddress)
                ' Assign the current section
                dataDirectory.Section = s.Value
                Exit For
            End If
        Next
        Return dataDirectory
    End Function

    ''' <summary>
    ''' Reading sections and assign to a dictionary
    ''' </summary>
    Private Sub ReadSections(reader As BinaryReader)
        m_sections = New Dictionary(Of Integer, IMAGE_SECTION_HEADER)
        For i As Integer = 0 To m_FileHeader.NumberOfSections - 1
            m_SectionHeader = FromBinaryReader(Of IMAGE_SECTION_HEADER)(reader)
            m_sections.Add(i, m_SectionHeader)
        Next
    End Sub

    ''' <summary>
    ''' Reading Cli and Metadata Header.
    ''' </summary>
    Private Sub ReadClrAndMetadataHeader(reader As BinaryReader)
        If m_dataDirectories(14).Size > 0 Then
            reader.BaseStream.Position = ReadVirtualDirectory(m_dataDirectories(14)).SectionStartOffset
            m_CoreHeader = FromBinaryReader(Of IMAGE_COR20_HEADER)(reader)
            Dim m_metadatRoot = New Cls_DataDirectory() With { _
                                                             .Address = m_CoreHeader.MetaData.VirtualAddress, _
                                                             .Size = m_CoreHeader.MetaData.Size}
            reader.BaseStream.Position = ReadVirtualDirectory(m_metadatRoot).SectionStartOffset
            m_metadata = FromBinaryReader(Of METADATA_HEADER)(reader)
        End If
    End Sub

    ''' <summary>
    ''' Reads in a block from a file and converts it to the structure
    ''' </summary>
    ''' <typeparam name="T">type of the struct to read</typeparam>
    ''' <param name="reader">reader</param>
    ''' <returns>a instance of the struct T cast from the data in the reader</returns>
    Private Function FromBinaryReader(Of T)(reader As BinaryReader) As T
        Dim bytes As Byte() = reader.ReadBytes(Marshal.SizeOf(GetType(T)))
        Dim gch As GCHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned)
        Dim struct As T = DirectCast(Marshal.PtrToStructure(gch.AddrOfPinnedObject(), GetType(T)), T)
        gch.Free()
        Return struct
    End Function
#End Region

#Region " ####################### IMAGE FILE CONTENT ####################### "

#Region " IMAGE_DOS_HEADER "
    ''' <summary>
    ''' DOS .EXE Header
    ''' </summary>
    Private Structure IMAGE_DOS_HEADER
        ''' <summary>
        ''' Magic number
        ''' </summary>
        Friend MagicNumber As UShort
        ''' <summary>
        ''' Bytes on last page of file
        ''' </summary>
        Friend BytesOnLastPage As UShort
        ''' <summary>
        ''' Pages in file
        ''' </summary>
        Friend PagesInFile As UShort
        ''' <summary>
        ''' Relocations
        ''' </summary>
        Friend Relocations As UShort
        ''' <summary>
        ''' Size of header in paragraphs
        ''' </summary>
        Friend SizeOfHeader As UShort
        ''' <summary>
        ''' Minimum extra paragraphs needed
        ''' </summary>
        Friend MinExtraAlloc As UShort
        ''' <summary>
        ''' Maximum extra paragraphs needed
        ''' </summary>
        Friend MaxExtraAlloc As UShort
        ''' <summary>
        ''' Initial (relative) SS value
        ''' </summary>
        Friend SS As UShort
        ''' <summary>
        ''' Initial SP value
        ''' </summary>
        Friend SP As UShort
        ''' <summary>
        ''' Checksum
        ''' </summary>
        Friend Checksum As UShort
        ''' <summary>
        ''' Initial IP value
        ''' </summary>
        Friend IP As UShort
        ''' <summary>
        ''' Initial (relative) CS value
        ''' </summary>
        Friend CS As UShort
        ''' <summary>
        ''' File address of relocation table
        ''' </summary>
        Friend RelocationTableAddress As UShort
        ''' <summary>
        ''' Overlay number
        ''' </summary>
        Friend OverlayNumber As UShort
        ''' <summary>
        ''' Reserved words
        ''' </summary>
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Friend ReservedWords As UShort()
        ''' <summary>
        ''' OEM identifier (for OEMInformation)
        ''' </summary>
        Friend OEMIdentifier As UShort
        ''' <summary>
        ''' OEM information (OEMIdentifier specific) 
        ''' </summary>
        Friend OEMInformation As UShort
        ''' <summary>
        ''' Reserved words
        ''' </summary>
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Friend ReservedWords2 As UShort()
        ''' <summary>
        ''' File address of new exe header
        ''' </summary>
        Friend ExeHeaderAddress As UShort

        Friend ReadOnly Property isValidDosHeader() As Boolean
            Get
                Return ConvertToString(MagicNumber) = "MZ"
            End Get
        End Property

    End Structure
#End Region

#Region " IMAGE_NT_HEADERS "

#Region " IMAGE_NT_HEADERS32 "
    Friend Structure IMAGE_NT_HEADERS32
        <FieldOffset(0)> _
        Friend Signature As UInteger
        <FieldOffset(4)> _
        Friend FileHeader As IMAGE_FILE_HEADER
        <FieldOffset(24)> _
        Friend OptionalHeader As IMAGE_OPTIONAL_HEADER32

        Friend ReadOnly Property GetSignature() As String
            Get
                Return ConvertToString(Signature) & vbNullChar & vbNullChar & _
                  ConvertToString(If(isX32 = True, Cls_Core20Reader.MagicType.IMAGE_NT_OPTIONAL_HDR32_MAGIC, If(isX64 = True, Cls_Core20Reader.MagicType.IMAGE_NT_OPTIONAL_HDR64_MAGIC, "")))
            End Get
        End Property

        Private ReadOnly Property _GetSignature() As String
            Get
                Return ConvertToString(Signature)
            End Get
        End Property

        Friend ReadOnly Property isValid() As Boolean
            Get
                Return _GetSignature = "PE" & vbNullChar & vbNullChar AndAlso (isX32 Or isX64)
            End Get
        End Property

        Private ReadOnly Property isX32() As Boolean
            Get
                Return OptionalHeader.Magic = Cls_Core20Reader.MagicType.IMAGE_NT_OPTIONAL_HDR32_MAGIC
            End Get
        End Property

        Private ReadOnly Property isX64() As Boolean
            Get

                Return OptionalHeader.Magic = Cls_Core20Reader.MagicType.IMAGE_NT_OPTIONAL_HDR64_MAGIC
            End Get
        End Property

        Friend ReadOnly Property GetDataDirectories As Dictionary(Of Integer, Cls_DataDirectory)
            Get
                Return m_dataDirectories
            End Get
        End Property

        Friend ReadOnly Property isManaged() As Boolean
            Get
                Return (If((m_dataDirectories.Count >= 14 AndAlso m_dataDirectories.Item(14).Size > 0) = True, True, False))
            End Get
        End Property
    End Structure
#End Region

#Region " IMAGE_NT_HEADERS64 "
    Friend Structure IMAGE_NT_HEADERS64
        <FieldOffset(0)> _
        Friend Signature As UInteger
        <FieldOffset(4)> _
        Friend FileHeader As IMAGE_FILE_HEADER
        <FieldOffset(24)> _
        Friend OptionalHeader As IMAGE_OPTIONAL_HEADER64

        Friend ReadOnly Property GetSignature() As String
            Get
                Return ConvertToString(Signature) & vbNullChar & vbNullChar & _
                  ConvertToString(If(isX32 = True, Cls_Core20Reader.MagicType.IMAGE_NT_OPTIONAL_HDR32_MAGIC, If(isX64 = True, Cls_Core20Reader.MagicType.IMAGE_NT_OPTIONAL_HDR64_MAGIC, "")))
            End Get
        End Property

        Private ReadOnly Property _GetSignature() As String
            Get
                Return ConvertToString(Signature)
            End Get
        End Property

        Friend ReadOnly Property isValid() As Boolean
            Get
                Return _GetSignature = "PE" & vbNullChar & vbNullChar AndAlso (isX32 Or isX64)
            End Get
        End Property

        Friend ReadOnly Property isX32() As Boolean
            Get
                Return OptionalHeader.Magic = Cls_Core20Reader.MagicType.IMAGE_NT_OPTIONAL_HDR32_MAGIC
            End Get
        End Property

        Private ReadOnly Property isX64() As Boolean
            Get
                Return OptionalHeader.Magic = Cls_Core20Reader.MagicType.IMAGE_NT_OPTIONAL_HDR64_MAGIC
            End Get
        End Property

        Private ReadOnly Property GetDataDirectories As Dictionary(Of Integer, Cls_DataDirectory)
            Get
                Return m_dataDirectories
            End Get
        End Property

        Friend ReadOnly Property isManaged() As Boolean
            Get
                Return (If((m_dataDirectories.Count >= 14 AndAlso m_dataDirectories.Item(14).Size > 0) = True, True, False))
            End Get
        End Property
    End Structure
#End Region

#Region " FILE_HEADER "
    Friend Structure IMAGE_FILE_HEADER
        Friend Machine As MachineType
        Friend NumberOfSections As UShort
        Friend TimeDateStamp As UInteger
        Friend PointerToSymbolTable As UInteger
        Friend NumberOfSymbols As UInteger
        Friend SizeOfOptionalHeader As UShort
        Friend Characteristics As IFHCharacteristics
    End Structure

    Friend Enum MachineType As UShort
        Native = 0
        I386 = &H14C
        Itanium = &H200
        x64 = &H8664
    End Enum

    Friend Enum IFHCharacteristics As UShort
        RelocationInformationStrippedFromFile = &H1
        Executable = &H2
        LineNumbersStripped = &H4
        SymbolTableStripped = &H8
        AggresiveTrimWorkingSet = &H10
        LargeAddressAware = &H20
        Supports16Bit = &H40
        ReservedBytesWo = &H80
        Supports32Bit = &H100
        DebugInfoStripped = &H200
        RunFromSwapIfInRemovableMedia = &H400
        RunFromSwapIfInNetworkMedia = &H800
        IsSytemFile = &H1000
        IsDLL = &H2000
        IsOnlyForSingleCoreProcessor = &H4000
        BytesOfWordReserved = &H8000
    End Enum

    Friend Enum MagicType As UShort
        IMAGE_NT_OPTIONAL_HDR32_MAGIC = &H10B
        IMAGE_NT_OPTIONAL_HDR64_MAGIC = &H20B
    End Enum
#End Region

#Region " OPTIONAL_HEADER "

#Region " OPTIONAL_HEADER_32 "
    Friend Structure IMAGE_OPTIONAL_HEADER32
        <FieldOffset(0)> _
        Friend Magic As MagicType

        <FieldOffset(2)> _
        Friend MajorLinkerVersion As Byte

        <FieldOffset(3)> _
        Friend MinorLinkerVersion As Byte

        <FieldOffset(4)> _
        Friend SizeOfCode As UInteger

        <FieldOffset(8)> _
        Friend SizeOfInitializedData As UInteger

        <FieldOffset(12)> _
        Friend SizeOfUninitializedData As UInteger

        <FieldOffset(16)> _
        Friend AddressOfEntryPoint As UInteger

        <FieldOffset(20)> _
        Friend BaseOfCode As UInteger

        ' PE32 contains this additional field
        <FieldOffset(24)> _
        Friend BaseOfData As UInteger

        <FieldOffset(28)> _
        Friend ImageBase As UInteger

        <FieldOffset(32)> _
        Friend SectionAlignment As UInteger

        <FieldOffset(36)> _
        Friend FileAlignment As UInteger

        <FieldOffset(40)> _
        Friend MajorOperatingSystemVersion As UShort

        <FieldOffset(42)> _
        Friend MinorOperatingSystemVersion As UShort

        <FieldOffset(44)> _
        Friend MajorImageVersion As UShort

        <FieldOffset(46)> _
        Friend MinorImageVersion As UShort

        <FieldOffset(48)> _
        Friend MajorSubsystemVersion As UShort

        <FieldOffset(50)> _
        Friend MinorSubsystemVersion As UShort

        <FieldOffset(52)> _
        Friend Win32VersionValue As UInteger

        <FieldOffset(56)> _
        Friend SizeOfImage As UInteger

        <FieldOffset(60)> _
        Friend SizeOfHeaders As UInteger

        <FieldOffset(64)> _
        Friend CheckSum As UInteger

        <FieldOffset(68)> _
        Friend Subsystem As SubSystemTypes

        <FieldOffset(70)> _
        Friend DllCharacteristics As DllCharacteristicsType

        <FieldOffset(72)> _
        Friend SizeOfStackReserve As UInteger

        <FieldOffset(76)> _
        Friend SizeOfStackCommit As UInteger

        <FieldOffset(80)> _
        Friend SizeOfHeapReserve As UInteger

        <FieldOffset(84)> _
        Friend SizeOfHeapCommit As UInteger

        <FieldOffset(88)> _
        Friend LoaderFlags As UInteger

        <FieldOffset(92)> _
        Friend NumberOfRvaAndSizes As UInteger

        <FieldOffset(96)> _
        Friend ExportTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(104)> _
        Friend ImportTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(112)> _
        Friend ResourceTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(120)> _
        Friend ExceptionTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(128)> _
        Friend CertificateTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(136)> _
        Friend BaseRelocationTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(144)> _
        Friend Debug As IMAGE_DATA_DIRECTORY

        <FieldOffset(152)> _
        Friend Architecture As IMAGE_DATA_DIRECTORY

        <FieldOffset(160)> _
        Friend GlobalPtr As IMAGE_DATA_DIRECTORY

        <FieldOffset(168)> _
        Friend TLSTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(176)> _
        Friend LoadConfigTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(184)> _
        Friend BoundImport As IMAGE_DATA_DIRECTORY

        <FieldOffset(192)> _
        Friend IAT As IMAGE_DATA_DIRECTORY

        <FieldOffset(200)> _
        Friend DelayImportDescriptor As IMAGE_DATA_DIRECTORY

        <FieldOffset(208)> _
        Friend CLRRuntimeHeader As IMAGE_DATA_DIRECTORY

        <FieldOffset(216)> _
        Friend Reserved As IMAGE_DATA_DIRECTORY
    End Structure
#End Region

#Region " OPTIONAL_HEADER_64 "
    <StructLayout(LayoutKind.Explicit)> _
    Friend Structure IMAGE_OPTIONAL_HEADER64
        <FieldOffset(0)> _
        Friend Magic As MagicType

        <FieldOffset(2)> _
        Friend MajorLinkerVersion As Byte

        <FieldOffset(3)> _
        Friend MinorLinkerVersion As Byte

        <FieldOffset(4)> _
        Friend SizeOfCode As UInteger

        <FieldOffset(8)> _
        Friend SizeOfInitializedData As UInteger

        <FieldOffset(12)> _
        Friend SizeOfUninitializedData As UInteger

        <FieldOffset(16)> _
        Friend AddressOfEntryPoint As UInteger

        <FieldOffset(20)> _
        Friend BaseOfCode As UInteger

        <FieldOffset(24)> _
        Friend ImageBase As ULong

        <FieldOffset(32)> _
        Friend SectionAlignment As UInteger

        <FieldOffset(36)> _
        Friend FileAlignment As UInteger

        <FieldOffset(40)> _
        Friend MajorOperatingSystemVersion As UShort

        <FieldOffset(42)> _
        Friend MinorOperatingSystemVersion As UShort

        <FieldOffset(44)> _
        Friend MajorImageVersion As UShort

        <FieldOffset(46)> _
        Friend MinorImageVersion As UShort

        <FieldOffset(48)> _
        Friend MajorSubsystemVersion As UShort

        <FieldOffset(50)> _
        Friend MinorSubsystemVersion As UShort

        <FieldOffset(52)> _
        Friend Win32VersionValue As UInteger

        <FieldOffset(56)> _
        Friend SizeOfImage As UInteger

        <FieldOffset(60)> _
        Friend SizeOfHeaders As UInteger

        <FieldOffset(64)> _
        Friend CheckSum As UInteger

        <FieldOffset(68)> _
        Friend Subsystem As SubSystemTypes

        <FieldOffset(70)> _
        Friend DllCharacteristics As DllCharacteristicsType

        <FieldOffset(72)> _
        Friend SizeOfStackReserve As ULong

        <FieldOffset(80)> _
        Friend SizeOfStackCommit As ULong

        <FieldOffset(88)> _
        Friend SizeOfHeapReserve As ULong

        <FieldOffset(96)> _
        Friend SizeOfHeapCommit As ULong

        <FieldOffset(104)> _
        Friend LoaderFlags As UInteger

        <FieldOffset(108)> _
        Friend NumberOfRvaAndSizes As UInteger

        <FieldOffset(112)> _
        Friend ExportTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(120)> _
        Friend ImportTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(128)> _
        Friend ResourceTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(136)> _
        Friend ExceptionTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(144)> _
        Friend CertificateTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(152)> _
        Friend BaseRelocationTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(160)> _
        Friend Debug As IMAGE_DATA_DIRECTORY

        <FieldOffset(168)> _
        Friend Architecture As IMAGE_DATA_DIRECTORY

        <FieldOffset(176)> _
        Friend GlobalPtr As IMAGE_DATA_DIRECTORY

        <FieldOffset(184)> _
        Friend TLSTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(192)> _
        Friend LoadConfigTable As IMAGE_DATA_DIRECTORY

        <FieldOffset(200)> _
        Friend BoundImport As IMAGE_DATA_DIRECTORY

        <FieldOffset(208)> _
        Friend IAT As IMAGE_DATA_DIRECTORY

        <FieldOffset(216)> _
        Friend DelayImportDescriptor As IMAGE_DATA_DIRECTORY

        <FieldOffset(224)> _
        Friend CLRRuntimeHeader As IMAGE_DATA_DIRECTORY

        <FieldOffset(232)> _
        Friend Reserved As IMAGE_DATA_DIRECTORY
    End Structure
#End Region

#Region " DATA_DIRECTORIES "
    Friend Structure IMAGE_DATA_DIRECTORY
        Friend VirtualAddress As Integer
        Friend Size As Integer
    End Structure

    Friend Structure IMAGE_COR20_HEADER
        ' Header versioning
        Friend cb As UInteger
        Friend MajorRuntimeVersion As UShort
        Friend MinorRuntimeVersion As UShort
        ' Symbol table and startup information
        Friend MetaData As IMAGE_DATA_DIRECTORY
        Friend Flags As UInteger
        Friend EntryPointToken As UInteger
        ' Binding information
        Friend Resources As IMAGE_DATA_DIRECTORY
        Friend StrongNameSignature As IMAGE_DATA_DIRECTORY
        ' Regular fixup and binding information
        Friend CodeManagerTable As IMAGE_DATA_DIRECTORY
        Friend VTableFixups As IMAGE_DATA_DIRECTORY
        Friend ExportAddressTableJumps As IMAGE_DATA_DIRECTORY
        ' Precompiled image info (internal use only - set to zero)
        Friend ManagedNativeHeader As IMAGE_DATA_DIRECTORY
    End Structure

    Friend Enum RuntimeFlags
        ILOnly = 1
        Required32Bit = 2
        NativeEntryPoint = &H10
        ILLibrary = 4
        StrongNameSigned = 8
        TrackDebugData = &H10000
    End Enum

    Friend Structure METADATA_HEADER
        Public Signature As UInt32
        Public MajorVersion As UInt16
        Public MinorVersion As UInt16
        Public Reserved As UInt32
        Public VersionLength As UInt32
        Public VersionString As UInt32
        Public Flags As UInt16
        Public NumberOfStreams As UInt16

        Friend ReadOnly Property GetTargetFramework() As String
            Get
                Return ConvertToString(VersionString)
            End Get
        End Property
    End Structure
#End Region

    Friend Enum SubSystemTypes As UShort
        IMAGE_SUBSYSTEM_UNKNOWN = 0
        IMAGE_SUBSYSTEM_NATIVE = 1
        IMAGE_SUBSYSTEM_WINDOWS_GUI = 2
        IMAGE_SUBSYSTEM_WINDOWS_CUI = 3
        IMAGE_SUBSYSTEM_POSIX_CUI = 7
        IMAGE_SUBSYSTEM_WINDOWS_CE_GUI = 9
        IMAGE_SUBSYSTEM_EFI_APPLICATION = 10
        IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER = 11
        IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER = 12
        IMAGE_SUBSYSTEM_EFI_ROM = 13
        IMAGE_SUBSYSTEM_XBOX = 14
    End Enum

    Friend Enum DllCharacteristicsType As UShort
        RES_0 = &H1
        RES_1 = &H2
        RES_2 = &H4
        RES_3 = &H8
        IMAGE_DLL_CHARACTERISTICS_DYNAMIC_BASE = &H40
        IMAGE_DLL_CHARACTERISTICS_FORCE_INTEGRITY = &H80
        IMAGE_DLL_CHARACTERISTICS_NX_COMPAT = &H100
        IMAGE_DLLCHARACTERISTICS_NO_ISOLATION = &H200
        IMAGE_DLLCHARACTERISTICS_NO_SEH = &H400
        IMAGE_DLLCHARACTERISTICS_NO_BIND = &H800
        RES_4 = &H1000
        IMAGE_DLLCHARACTERISTICS_WDM_DRIVER = &H2000
        IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE = &H8000
    End Enum
#End Region

#End Region

#Region " IMAGE_SECTION_HEADERS "
    Friend Structure IMAGE_SECTION_HEADER
        <FieldOffset(0)> _
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
        Private Name As Char()

        <FieldOffset(8)> _
        Friend VirtualSize As UInt32

        <FieldOffset(12)> _
        Friend VirtualAddress As UInt32

        <FieldOffset(16)> _
        Friend SizeOfRawData As UInt32

        <FieldOffset(20)> _
        Friend PointerToRawData As UInt32

        <FieldOffset(24)> _
        Friend PointerToRelocations As UInt32

        <FieldOffset(28)> _
        Friend PointerToLinenumbers As UInt32

        <FieldOffset(32)> _
        Friend NumberOfRelocations As UInt16

        <FieldOffset(34)> _
        Friend NumberOfLinenumbers As UInt16

        <FieldOffset(36)> _
        Friend Characteristics As DataSectionFlags

        Friend ReadOnly Property GetSectionName() As String
            Get
                Return New String(Name.TakeWhile(Function(b) Not b.Equals(ControlChars.NullChar)).ToArray())
            End Get
        End Property

        Friend Enum DataSectionFlags As UInteger
            ''' <summary>Reserved for future use.</summary>
            TypeReg = &H0
            ''' <summary>Reserved for future use.</summary>
            TypeDsect = &H1
            ''' <summary>Reserved for future use.</summary>
            TypeNoLoad = &H2
            ''' <summary>Reserved for future use.</summary>
            TypeGroup = &H4
            ''' <summary>
            ''' The section should not be padded to the next boundary. This flag is obsolete and is replaced by IMAGE_SCN_ALIGN_1BYTES. This is valid only for object files.
            ''' </summary>
            TypeNoPadded = &H8
            ''' <summary>Reserved for future use.</summary>
            TypeCopy = &H10
            ''' <summary>The section contains executable code.</summary>
            ContentCode = &H20
            ''' <summary>The section contains initialized data.</summary>
            ContentInitializedData = &H40
            ''' <summary>The section contains uninitialized data.</summary>
            ContentUninitializedData = &H80
            ''' <summary>Reserved for future use.</summary>
            LinkOther = &H100
            ''' <summary>The section contains comments or other information. The .drectve section has this type. This is valid for object files only.</summary>
            LinkInfo = &H200
            ''' <summary>Reserved for future use.</summary>
            TypeOver = &H400
            ''' <summary>The section will not become part of the image. This is valid only for object files.</summary>
            LinkRemove = &H800
            ''' <summary>
            ''' The section contains COMDAT data. For more information, see section 5.5.6, COMDAT Sections (Object Only). This is valid only for object files.
            ''' </summary>
            LinkComDat = &H1000
            ''' <summary>Reset speculative exceptions handling bits in the TLB entries for this section.</summary>
            NoDeferSpecExceptions = &H4000
            ''' <summary>The section contains data referenced through the global pointer (GP).</summary>
            RelativeGP = &H8000
            ''' <summary>Reserved for future use.</summary>
            MemPurgeable = &H20000
            ''' <summary>Reserved for future use.</summary>
            Memory16Bit = &H20000
            ''' <summary>Reserved for future use.</summary>
            MemoryLocked = &H40000
            ''' <summary>Reserved for future use.</summary>
            MemoryPreload = &H80000
            ''' <summary>Align data on a 1-byte boundary. Valid only for object files.</summary>
            Align1Bytes = &H100000
            ''' <summary>Align data on a 2-byte boundary. Valid only for object files.</summary>
            Align2Bytes = &H200000
            ''' <summary>Align data on a 4-byte boundary. Valid only for object files. </summary>
            Align4Bytes = &H300000
            ''' <summary>Align data on an 8-byte boundary. Valid only for object files.</summary>
            Align8Bytes = &H400000
            ''' <summary>Align data on a 16-byte boundary. Valid only for object files.</summary>
            Align16Bytes = &H500000
            ''' <summary>Align data on a 32-byte boundary. Valid only for object files.</summary>
            Align32Bytes = &H600000
            ''' <summary>Align data on a 64-byte boundary. Valid only for object files.</summary>
            Align64Bytes = &H700000
            ''' <summary>Align data on a 128-byte boundary. Valid only for object files.</summary>
            Align128Bytes = &H800000
            ''' <summary>Align data on a 256-byte boundary. Valid only for object files.</summary>
            Align256Bytes = &H900000
            ''' <summary>Align data on a 512-byte boundary. Valid only for object files.</summary>
            Align512Bytes = &HA00000
            ''' <summary>Align data on a 1024-byte boundary. Valid only for object files.</summary>
            Align1024Bytes = &HB00000
            ''' <summary>Align data on a 2048-byte boundary. Valid only for object files.</summary>
            Align2048Bytes = &HC00000
            ''' <summary>Align data on a 4096-byte boundary. Valid only for object files.</summary>
            Align4096Bytes = &HD00000
            ''' <summary>Align data on an 8192-byte boundary. Valid only for object files.</summary>
            Align8192Bytes = &HE00000
            ''' <summary>The section contains extended relocations.</summary>
            LinkExtendedRelocationOverflow = &H1000000
            ''' <summary>The section can be discarded as needed.</summary>
            MemoryDiscardable = &H2000000
            ''' <summary>The section cannot be cached.</summary>
            MemoryNotCached = &H4000000
            ''' <summary>The section is not pageable.</summary>
            MemoryNotPaged = &H8000000
            ''' <summary>The section can be shared in memory.</summary>
            MemoryShared = &H10000000
            ''' <summary>The section can be executed as code.</summary>
            MemoryExecute = &H20000000
            ''' <summary>The section can be read.</summary>
            MemoryRead = &H40000000
            ''' <summary>The section can be written to.</summary>
            MemoryWrite = &H80000000UI
        End Enum
    End Structure
#End Region

#End Region

#Region " ########################### INITIALIZE ########################### "

    Public Sub ReadFile(filePath As String) Implements ICore20Reader.ReadFile
        Try
            m_filePath = filePath
            Using stream As New MemoryStream(System.IO.File.ReadAllBytes(filePath), False)

                Using reader As New BinaryReader(stream)
                    m_DosHeader = FromBinaryReader(Of IMAGE_DOS_HEADER)(reader) ''''''''''''''''''''''''''''''''''''''''''''''''''SizeOf m_DosHeader : 62 

                    If m_DosHeader.isValidDosHeader Then
                        stream.Seek(m_DosHeader.ExeHeaderAddress, SeekOrigin.Begin) ''''''''''''''''''''''''''''''''''''''''''''''StartOf Dos Stub : 128 
                        Dim DosHeaderPositionEnd = stream.Position

                        m_NTHeader32 = FromBinaryReader(Of IMAGE_NT_HEADERS32)(reader) '''''''''''''''''''''''''''''''''''''''''''SizeOf m_NTHeader32 : 248
                        m_FileHeader = m_NTHeader32.FileHeader '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''SizeOf m_FileHeader : 20

                        If isExecutable() Then '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''Verifying executable flag from IMAGE_FILE_HEADER Characteristics 
                            m_PlatformSignature = "x86"
                            Dim dataDirectoriesOffset As UShort = 96 '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''Start DataDirectory Offset for PE32 file (0x10b == PE32 (32Bit))  
                            If Is32BitHeader Then
                                If m_NTHeader32.isValid Then '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''Verifying if the NTHeader signature is correct like PE**
                                    If m_NTHeader32.OptionalHeader.Magic = &H20B Then
                                        m_PlatformSignature = "x64"
                                        dataDirectoriesOffset = 112 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''Start DataDirectory Offset for PE32+ file (0x20b == PE32+ (64Bit))
                                    End If
                                    m_entryPoint = m_NTHeader32.OptionalHeader.AddressOfEntryPoint.ToString ''''''''''''''''''''''Return the AddressOfEntryPoint from the optionalheader32
                                    Dim SizeOfNtHeader32% = Marshal.SizeOf(m_NTHeader32)
                                    Dim SizeOfOptionalHeader% = Marshal.SizeOf(m_NTHeader32.OptionalHeader) ''''''''''''''''''''''SizeOf OptionalHeader : 128 
                                    Dim EndOfOptionalHeader% = m_DosHeader.ExeHeaderAddress + SizeOfNtHeader32 - SizeOfOptionalHeader

                                    stream.Position = EndOfOptionalHeader ''''''''''''''''''''''''''''''''''''''''''''''''''''''''EndOf OptionalHeader : 152

                                    Dim dataDictionaryStart As UShort = CUShort(stream.Position + dataDirectoriesOffset)
                                    stream.Position = dataDictionaryStart ''''''''''''''''''''''''''''''''''''''''''''''''''''''''StartOf DataDirectories : 248

                                    ReadDataDirectories(reader, m_NTHeader32.OptionalHeader.NumberOfRvaAndSizes) '''''''''''''''''Reading DataDirectories. Usually 16 entries

                                    Dim dataDictionaryEnd As UShort = stream.Position ''''''''''''''''''''''''''''''''''''''''''''EndOf DataDirectories : 376

                                    ReadSections(reader) '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''Reading Sections

                                    Dim sectionEnd As UShort = stream.Position '''''''''''''''''''''''''''''''''''''''''''''''''''EndOf reading sections : 536
                                    stream.Position = dataDictionaryEnd

                                    If m_NTHeader32.isManaged Then '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''Verifying if the file is DotNet : DataDictionary 14 exist and its size > 0
                                        ReadClrAndMetadataHeader(reader) '''''''''''''''''''''''''''''''''''''''''''''''''''''''''Reading the CliHeader and the Metadata header
                                        m_TargetFramework = m_metadata.GetTargetFramework ''''''''''''''''''''''''''''''''''''''''Return the TargetFramework from the Metadata header
                                    End If

                                End If
                            Else
                                stream.Position = DosHeaderPositionEnd
                                m_NTHeader64 = FromBinaryReader(Of IMAGE_NT_HEADERS64)(reader)
                                m_FileHeader = m_NTHeader64.FileHeader

                                If m_NTHeader64.isValid Then

                                    If m_NTHeader64.OptionalHeader.Magic = &H20B Then
                                        m_PlatformSignature = "x64"
                                        dataDirectoriesOffset = &H70
                                    End If
                                    m_entryPoint = m_NTHeader64.OptionalHeader.AddressOfEntryPoint.ToString ''''''''''''''''''''''Return the AddressOfEntryPoint from the optionalheader64
                                    Dim SizeOfNtHeader64% = Marshal.SizeOf(m_NTHeader64)
                                    Dim SizeOfOptionalHeader% = Marshal.SizeOf(m_NTHeader64.OptionalHeader)
                                    Dim EndOfOptionalHeader% = m_DosHeader.ExeHeaderAddress + SizeOfNtHeader64 - SizeOfOptionalHeader

                                    stream.Position = EndOfOptionalHeader

                                    Dim dataDictionaryStart As UShort = CShort(stream.Position + dataDirectoriesOffset)
                                    stream.Position = dataDictionaryStart

                                    ReadDataDirectories(reader, m_NTHeader64.OptionalHeader.NumberOfRvaAndSizes)

                                    Dim dataDictionaryEnd As UShort = stream.Position
                                    ReadSections(reader)
                                    Dim sectionEnd As UShort = stream.Position
                                    stream.Position = dataDictionaryEnd

                                    If m_NTHeader64.isManaged Then
                                        ReadClrAndMetadataHeader(reader)
                                        m_TargetFramework = m_metadata.GetTargetFramework
                                    End If
                                End If
                            End If
                        End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            Throw New BadImageFormatException("Error", m_filePath, ex)
        End Try
    End Sub

#End Region

End Class