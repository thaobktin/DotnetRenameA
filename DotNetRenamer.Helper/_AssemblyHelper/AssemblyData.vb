Imports System.Reflection

Namespace AssemblyHelper
    Public Class AssemblyData

        Public Property AssName As String
        Public Property AssVersion As String
        Public Property IsWpf As Boolean
        Public Property Location As String
        Public Property EntryPoint As MethodInfo
        Public Property AssemblyReferences As AssemblyName()
        Public Property Result As Message

        Public Enum Message
            Failed = 0
            Success = 1
        End Enum

        Sub New()
        End Sub
    End Class
End Namespace

