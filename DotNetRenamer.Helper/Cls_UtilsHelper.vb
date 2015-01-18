Imports System.IO

Namespace UtilsHelper
    Public Class Cls_UtilsHelper
        Private Shared tempFolder$ = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Temp"

        Public Shared Function GetTempFolder() As String
            If Not Directory.Exists(tempFolder) Then
                Directory.CreateDirectory(tempFolder)
            End If
            Return tempFolder
        End Function

    End Class
End Namespace

