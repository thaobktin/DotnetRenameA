Imports System.Reflection
Imports DotNetRenamer.Core20Reader
Imports DotNetRenamer.Helper.AssemblyHelper
Imports DotNetRenamer.Implementer.Exclusion
Imports System.IO
Imports System.Windows.Forms
Imports DotNetRenamer.Implementer.Context.Cls_Parameters

Namespace Analyzer

    ''' <summary>
    ''' INFO : This is the first step of the renamer library. 
    '''        You must pass two arguments (inputFile and outputFile properties) when instantiating this class.
    '''        You can either check if the selected file if executable and DotNet by calling the isValidFile routine.
    ''' </summary>
    Public Class Cls_Analyzer

#Region " Variables "
        Private _pe As ICore20Reader
        Private _assemblyName As String = String.Empty
        Private _assemblyVersion As String = String.Empty
        Private _isWpfProgram As Boolean
#End Region

#Region " Events "
        Public Event FileValidated(sender As Object, e As Cls_ValidatedFile)
#End Region

#Region " Properties "
        Public Property inputFile As String
        Public Property outputFile As String
        Public Property currentFile As String
#End Region

#Region " Initialize "
        ''' <summary>
        ''' INFO : Initilize a new instance of the class Analyzer.Cls_Analyzer which used to check if the selected inputfile is a valid PE and executable file. 
        ''' </summary>
        ''' <param name="inputFilePath"></param>
        ''' <param name="outputFilePath"></param>
        Public Sub New(inputFilePath$, outPutFilePath$)
            _inputFile = inputFilePath
            _outputFile = outPutFilePath
            _pe = New Cls_Core20Reader()
            _pe.ReadFile(_inputFile)
        End Sub
#End Region

#Region " Methods "
        ''' <summary>
        ''' INFO : Check if inputfile is valid DotNet and executable and not Wpf !
        ''' </summary>
        Public Function isValidFile() As Boolean
            If _pe.isExecutable Then
                If _pe.isManagedFile Then
                    Dim infos As AssemblyData = AssemblyHelper.LoadMinimal(_inputFile)
                    If infos.Result = AssemblyData.Message.Success Then
                        If Not infos.EntryPoint Is Nothing Then
                            _assemblyName = infos.AssName
                            _isWpfProgram = infos.IsWpf
                            _assemblyVersion = infos.AssVersion
                            If _isWpfProgram = False Then
                                RaiseEvent FileValidated(Me, New Cls_ValidatedFile(True))
                                Return True
                            End If
                        End If
                    End If
                End If
            End If
            RaiseEvent FileValidated(Me, New Cls_ValidatedFile(False))
            Return False
        End Function

        Public Function getAssemblyName() As String
            Return _assemblyName
        End Function

        Public Function getAssemblyVersion() As String
            Return _assemblyVersion
        End Function

        Public Function getModuleKind() As String
            Return _pe.GetSystemType
        End Function

        Public Function getRuntime() As String
            Return _pe.GetTargetRuntime
        End Function

        Public Function getProcessArchitecture() As String
            Return _pe.GetTargetPlatform
        End Function

        Public Function isWpfProgram() As Boolean
            Return _isWpfProgram
        End Function

        Public Function getTreeViewHandler() As Cls_ExclusionTreeview
            Return New Cls_ExclusionTreeview(_inputFile)
        End Function
#End Region

    End Class
End Namespace
