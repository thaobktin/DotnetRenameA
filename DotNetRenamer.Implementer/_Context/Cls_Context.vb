Imports DotNetRenamer.Implementer.Processing
Imports DotNetRenamer.Helper.RandomizeHelper
Imports System.IO
Imports DotNetRenamer.Helper.UtilsHelper
Imports dnlib.DotNet
Imports DotNetRenamer.Implementer.Exclusion
Imports DotNetRenamer.Implementer.Resources

Namespace Context

    ''' <summary>
    ''' INFO : This is the second step of the renamer library. 
    '''        You must pass one argument (parameter) when instantiating this class and calling the RenameAssembly routine.
    ''' </summary>
    Public Class Cls_Context

#Region " Fields "
        Public AssDef As AssemblyDef
        Private _parameters As Cls_Parameters
        Private _processing As Cls_Processing
        Private _fi As FileInfo
#End Region

#Region " Events "
        Public Shared Event RenamedItem As RenamedItem
#End Region

#Region " Initialize "
        ''' <summary>
        ''' INFO : Initializes a new instance of the Context.Cls_Context class which allows to add parameters such as members and types state before the task of renaming starts.
        ''' </summary>
        ''' <param name="Parameters"></param>
        Public Sub New(ByVal parameters As Cls_Parameters, RenamerState As Cls_RenamerState)
            _parameters = parameters
            _parameters.RenamingAccept = RenamerState
            _parameters.currentFile = _parameters.inputFile
            _processing = New Cls_Processing(_parameters.RenamingAccept)
        End Sub

#End Region

#Region " Methods "
        ''' <summary>
        ''' INFO : Raise event when a type or a member renamed.
        ''' </summary>
        ''' <param name="it"></param>
        Public Shared Sub RaiseRenamedItemEvent(it As Cls_RenamedItem)
            Dim itemEvent As New Cls_RenamedItemEventArgs(it)
            RaiseEvent RenamedItem(Nothing, itemEvent)
            itemEvent = Nothing
        End Sub

        ''' <summary>
        ''' INFO : this routine reads the assembly. It uses Mono Cecil library.
        ''' </summary>
        Public Sub ReadAssembly()
            AssDef = AssemblyDef.Load(_parameters.inputFile)
        End Sub

        ''' <summary>
        ''' INFO : Verifying if loaded assembly is not nothing.
        ''' </summary>
        Public Function IsAsemblyLoaded()
            Return Not (AssDef Is Nothing)
        End Function

        Public Sub RenameResourceContent()
            _processing.ProcessResourceContent(AssDef)
        End Sub

        ''' <summary>
        ''' INFO : Loop through the modules and types of the loaded assembly and start renaming.
        ''' </summary>
        Public Sub RenameAssembly()
            Dim assemblyMainName$ = AssDef.ManifestModule.EntryPoint.DeclaringType.Namespace
            For Each modul As ModuleDef In AssDef.Modules
                If modul.HasTypes Then
                    For Each type As TypeDef In modul.GetTypes()
                        If _parameters.ExcludeList.isTypeExclude(type) = False Then
                            RenameSelectedNamespace(type, assemblyMainName)
                        End If
                    Next
                End If
            Next
        End Sub

        ''' <summary>
        ''' INFO : Rename the main Namespace or all namespaces according to Cls_Parameters.RenameMainNamespaceSetting setting.
        ''' </summary>
        ''' <param name="type"></param>
        ''' <param name="assemblyMainName"></param>
        ''' <param name="processing"></param>
        Private Sub RenameSelectedNamespace(type As TypeDef, assemblyMainName$)
            If _parameters.RenamingAccept.RenameMainNamespaceSetting = CBool(Cls_RenamerState.RenameMainNamespace.Only) Then
                If type.Namespace.StartsWith(assemblyMainName) Then
                    _processing.ProcessType(type)
                End If
            Else
                _processing.ProcessType(type)
            End If

            If type.HasProperties Then _processing.ProcessProperties(type)
            If type.HasMethods Then _processing.ProcessMethods(type)
            If type.HasFields Then _processing.ProcessFields(type)
            If type.HasEvents Then  _processing.ProcessEvents(type)
        End Sub

        ''' <summary>
        ''' INFO : Records changes to the loaded assembly. It uses Mono Cecil library.
        ''' </summary>
        Public Sub WriteAssembly()
            AssDef.Write(_parameters.outputFile)
        End Sub

        ''' <summary>
        ''' INFO : Clear the randomize names from the dictionary.
        ''' </summary>
        Public Sub CleanUp()
            Cls_Randomizer.CleanUp()
            Cls_Mapping.CleanUp()
            Cls_Content.Cleanup()
            _parameters.ExcludeList.CleanUp()
            'Me._parameters.RenamingAccept.CleanUp()
        End Sub
#End Region

    End Class
End Namespace
