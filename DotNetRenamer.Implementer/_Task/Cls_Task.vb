Imports DotNetRenamer.Implementer.Context

Namespace Task
    Public Class Cls_Task
        Inherits Cls_Context

#Region " Fields "
        Private _Result As Cls_TaskResult
        Private _NRenamed, _TRenamed, _MRenamed, _PRenamed, _GRenamed, _ERenamed, _FRenamed, _PaRenamed, _VRenamed As Integer
#End Region

#Region " Enumerations "
        Public Enum TaskType
            windows = 0
            cmd = 1
        End Enum
#End Region

#Region " Property "
        ReadOnly Property Result As Cls_TaskResult
            Get
                Return _Result
            End Get
        End Property
#End Region

#Region " Constructor "
        Sub New(Parameters As Cls_Parameters, RenamerState As Cls_RenamerState, Optional ByVal TaskT As TaskType = TaskType.windows)
            MyBase.New(Parameters, RenamerState, TaskT)
            _Result = New Cls_TaskResult From {{"Namespace", 0}, _
                                               {"Type", 0}, _
                                               {"Method", 0}, _
                                               {"Parameter", 0}, _
                                               {"Generic Parameter", 0}, _
                                               {"Variable", 0}, _
                                               {"Property", 0}, _
                                               {"Event", 0}, _
                                               {"Field", 0}}
        End Sub
#End Region

#Region " Methods "
        Public Sub StartTask()
            ReadAssembly()
            If parameters.RenamingAccept.ResourcesContent Then RenameResourceContent()
            RenameAssembly()
            WriteAssembly()
            CleanUp()
            ClearIncrement()
        End Sub

        Overridable Sub OnrenamedItem(sender As Object, e As Cls_RenamedItemEventArgs) Handles Me.RenamedItem
            Select Case e.item.ItemType
                Case "Namespace"
                    _NRenamed += 1
                Case "Type"
                    _TRenamed += 1
                Case "Method"
                    _MRenamed += 1
                Case "Parameter"
                    _PaRenamed += 1
                Case "Generic Parameter"
                    _GRenamed += 1
                Case "Variable"
                    _VRenamed += 1
                Case "Property"
                    _PRenamed += 1
                Case "Event"
                    _ERenamed += 1
                Case "Field"
                    _FRenamed += 1
            End Select
            AddTo(e.item.ItemType)
            '_Log.WriteLine(e.item.ItemType & " : From --> " & e.item.ItemName & " : To --> " & e.item.obfuscatedItemName)
        End Sub

        Private Sub AddTo(ItemName)
            If _Result.ContainsKey(ItemName) Then
                _Result.Item(ItemName) += 1
            Else
                _Result.Add(ItemName, 1)
            End If
        End Sub

        Private Sub ClearIncrement()
            _NRenamed = 0
            _TRenamed = 0
            _MRenamed = 0
            _PaRenamed = 0
            _GRenamed = 0
            _VRenamed = 0
            _PRenamed = 0
            _ERenamed = 0
            _FRenamed = 0
        End Sub
#End Region

    End Class
End Namespace
