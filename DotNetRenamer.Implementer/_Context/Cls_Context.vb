Imports DotNetRenamer.Implementer.Processing
Imports DotNetRenamer.Helper.RandomizeHelper
Imports System.IO
Imports DotNetRenamer.Helper.UtilsHelper
Imports dnlib.DotNet
Imports DotNetRenamer.Implementer.Resources
Imports DotNetRenamer.Implementer.Task.Cls_Task

Namespace Context

    ''' <summary>
    ''' INFO : This is the second step of the renamer library. 
    '''        You must pass one argument (parameter) when instantiating this class and calling the RenameAssembly routine.
    ''' </summary>
    Public Class Cls_Context

#Region " Fields "
        Private _AssDef As AssemblyDef
        Private _processing As Cls_Processing
        Private _fi As FileInfo
        Private _TaskType As TaskType
        Public ReadOnly parameters As Cls_Parameters
#End Region

#Region " Events "
        Public Shared Event RenamedItem As RenamedItem
#End Region

#Region " Initialize "
        ''' <summary>
        ''' INFO : Initializes a new instance of the Context.Cls_Context class which allows to add parameters such as members and types state before the task of renaming starts.
        ''' </summary>
        ''' <param name="Parameters"></param>
        Public Sub New(ByVal params As Cls_Parameters, RenamerState As Cls_RenamerState, TaskT As TaskType)
            parameters = params
            parameters.RenamingAccept = RenamerState
            parameters.currentFile = parameters.inputFile
            _processing = New Cls_Processing(parameters.RenamingAccept)
            _TaskType = TaskT
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
            _AssDef = AssemblyDef.Load(parameters.inputFile)
        End Sub

        ''' <summary>
        ''' INFO : Verifying if loaded assembly is not nothing.
        ''' </summary>
        Public Function IsAsemblyLoaded()
            Return Not (_AssDef Is Nothing)
        End Function

        Public Sub RenameResourceContent()
            _processing.ProcessResourceContent(_AssDef)
        End Sub

        ''' <summary>
        ''' INFO : Loop through the modules and types of the loaded assembly and start renaming.
        ''' </summary>
        Public Sub RenameAssembly()
            Dim assemblyMainName$ = _AssDef.ManifestModule.EntryPoint.DeclaringType.Namespace
            For Each modul As ModuleDef In _AssDef.Modules
                If modul.HasTypes Then
                    For Each type As TypeDef In modul.GetTypes()
                        If _TaskType = TaskType.windows Then
                            If parameters.ExcludeList.isTypeExclude(type) = False Then
                                RenameSelectedNamespace(type, assemblyMainName)
                            End If
                        ElseIf _TaskType = TaskType.cmd Then
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
            If parameters.RenamingAccept.RenameMainNamespaceSetting = CBool(Cls_RenamerState.RenameMainNamespace.Only) Then
                If type.Namespace.StartsWith(assemblyMainName) Then
                    _processing.ProcessType(type)
                End If
            Else
                _processing.ProcessType(type)
            End If

            If type.HasProperties Then _processing.ProcessProperties(type)
            If type.HasMethods Then _processing.ProcessMethods(type)
            If type.HasFields Then _processing.ProcessFields(type)
            If type.HasEvents Then _processing.ProcessEvents(type)
        End Sub

        ''' <summary>
        ''' INFO : Records changes to the loaded assembly. It uses Mono Cecil library.
        ''' </summary>
        Public Sub WriteAssembly()
            _AssDef.Write(parameters.outputFile)
        End Sub

        ''' <summary>
        ''' INFO : Clear the randomize names from the dictionary.
        ''' </summary>
        Public Sub CleanUp()
            Cls_Randomizer.CleanUp()
            Cls_Mapping.CleanUp()
            Cls_Content.Cleanup()
            If _TaskType = TaskType.windows Then parameters.ExcludeList.CleanUp()
        End Sub
#End Region

    End Class
End Namespace
