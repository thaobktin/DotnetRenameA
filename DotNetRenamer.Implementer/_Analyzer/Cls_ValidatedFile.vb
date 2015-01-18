Namespace Analyzer
    Public Class Cls_ValidatedFile
        Inherits EventArgs

        Public Property isValid As Boolean

        Public Sub New(isValid As Boolean)
            MyBase.New()
            Me.isValid = isValid
        End Sub
    End Class
End Namespace