Namespace Context
    Public Delegate Sub RenamedItem(sender As Object, e As Cls_RenamedItemEventArgs)

    Public Class Cls_RenamedItemEventArgs
        Inherits EventArgs

        Private m_item As Cls_RenamedItem

        Public Sub New(item As Cls_RenamedItem)
            m_item = item
        End Sub

        Public ReadOnly Property item As Cls_RenamedItem
            Get
                Return m_item
            End Get
        End Property

    End Class
End Namespace
