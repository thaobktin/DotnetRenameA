Imports System.Threading
Imports DotNetRenamer.WinConsole
Imports System.IO

Friend Class Program

#Region "Sub Main"
    <STAThread()>
    Public Shared Sub Main(ByVal Args As String())
        Dim instanceCountOne As Boolean = False
        Using mtex As Mutex = New Mutex(True, Application.ProductName, instanceCountOne)
            If instanceCountOne Then
                If Args.Length > 0 Then
                    Dim cmd As New Cmd_Main(Args)
                    cmd.RunApplication()
                Else
                    Application.EnableVisualStyles()
                    Application.SetCompatibleTextRenderingDefault(False)
                    Application.Run(New Frm_Main)
                End If
                mtex.ReleaseMutex()
            End If
        End Using
    End Sub
#End Region

End Class

