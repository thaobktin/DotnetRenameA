Imports DotNetRenamer.WinConsole
Imports System.IO
Imports DotNetRenamer.Implementer.Context
Imports DotNetRenamer.Settings
Imports DotNetRenamer.Implementer.Task

Namespace WinConsole
    Friend Class Cmd_Main

#Region "Win32"
        Declare Function AttachConsole Lib "kernel32" (ByVal dwProcessId As Int32) As Boolean
        Declare Function AllocConsole Lib "kernel32" () As Boolean
        Declare Function FreeConsole Lib "kernel32.dll" () As Boolean
#End Region

#Region "Delegates"
        Private Delegate Sub OnRenamedItemDelegate(ByVal e As Cls_RenamedItem)
#End Region

#Region "Fields"
        Friend WithEvents _Task As Cls_Task

        Private _fileInfos As FileInfo = Nothing
        Private _mode As String = Cls_Settings.modeName(0)
        Private _args As String()
#End Region

#Region "Constructor"
        Public Sub New(Args As String())
            _args = Args
            _mode = GetPreset()
            _fileInfos = New FileInfo(Args(0))
        End Sub
#End Region
     
#Region "Methods"
        Private Function GetPreset() As String
            If (Enumerable.Count(Of String)(Me._args) = 1) Then
                Return Cls_Settings.modeName(0)
            End If
            If (Enumerable.Count(Of String)(Me._args) = 2) Then
                If (((Me._args(1) Is Nothing) OrElse (Me._args(1).ToLower <> Cls_Settings.modeName(1))) OrElse (Me._args(1).ToLower <> Cls_Settings.modeName(2))) Then
                    Return Cls_Settings.modeName(0)
                End If
                If (Me._args(1).ToLower = Cls_Settings.modeName(0)) Then
                    Return Cls_Settings.modeName(0)
                End If
                If (Me._args(1).ToLower = Cls_Settings.modeName(1)) Then
                    Return Cls_Settings.modeName(1)
                End If
                If (Me._args(1).ToLower = Cls_Settings.modeName(2)) Then
                    Return Cls_Settings.modeName(2)
                End If
            End If
            Return Cls_Settings.modeName(0)
        End Function

        Public Sub RunApplication()
            If _fileInfos.Exists AndAlso _fileInfos.Length <> 0 Then
                Dim _param = New Cls_Parameters(_fileInfos.FullName, Path.GetDirectoryName(_fileInfos.FullName) & "\" & Path.GetFileNameWithoutExtension(_fileInfos.FullName) & "Protected.exe")
                If _param.isValidFile Then
                    If Not AttachConsole(-1) Then
                        AllocConsole()
                        StartCmdTask(_param)
                        FreeConsole()
                    Else
                        StartCmdTask(_param)
                    End If
                End If
            End If
        End Sub

        Private Sub StartCmdTask(param As Cls_Parameters)
            Cls_Settings.SetDefault()
            Try
                _Task = New Cls_Task(param, Cls_Settings.GetConfig(Cls_Settings.GetIntMode(_mode)))
                Console.WriteLine(vbNewLine & "######################## Start Task with : " & _mode & " Mode" & vbNewLine)
                _Task.StartTask()
                Console.WriteLine(vbNewLine & "######################## End Task !" & vbNewLine)
                Console.Write("Renamed :" & vbNewLine & vbNewLine & _
                                                  "Namespace(s) : " & CType(_Task.Result, Cls_TaskResult).Item("Namespace").ToString & vbNewLine & _
                                                  "Type(s) : " & CType(_Task.Result, Cls_TaskResult).Item("Type").ToString & vbNewLine & _
                                                  "Method(s) : " & CType(_Task.Result, Cls_TaskResult).Item("Method").ToString & vbNewLine & _
                                                  "Parameter(s) : " & CType(_Task.Result, Cls_TaskResult).Item("Parameter").ToString & vbNewLine & _
                                                  "Generic Parameter(s) : " & CType(_Task.Result, Cls_TaskResult).Item("Generic Parameter").ToString & vbNewLine & _
                                                  "Variable(s) : " & CType(_Task.Result, Cls_TaskResult).Item("Variable").ToString & vbNewLine & _
                                                  "Property(s) : " & CType(_Task.Result, Cls_TaskResult).Item("Property").ToString & vbNewLine & _
                                                  "Event(s) : " & CType(_Task.Result, Cls_TaskResult).Item("Event").ToString & vbNewLine & _
                                                  "Field(s) : " & CType(_Task.Result, Cls_TaskResult).Item("Field").ToString)
                Console.ReadLine()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End Sub

        Private Sub OnRenamedItem(ByVal sender As Object, ByVal e As Cls_RenamedItemEventArgs) Handles _Task.RenamedItem
            Console.WriteLine(e.item.ItemType & " : From --> " & e.item.ItemName & " : To --> " & e.item.obfuscatedItemName)
        End Sub

#End Region
    
    End Class
End Namespace
