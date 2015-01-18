Namespace Context

    Public Class Cls_RenamedItem

#Region " Variables "
        Private ReadOnly _itemType As Cls_RenamedItemType.RenamedItemType
        Private ReadOnly _itemName$
        Private ReadOnly _obfuscatedItemName$
#End Region

#Region " Initialize "
        Friend Sub New(ItemType As Cls_RenamedItemType.RenamedItemType, ItemName$, obfuscatedItemName$)
            Me._itemType = ItemType
            Me._itemName = ItemName
            Me._obfuscatedItemName = obfuscatedItemName
        End Sub
#End Region

#Region " Properties "
        Public ReadOnly Property ItemType As String
            Get
                Return ReturnTypeToString(Me._itemType)
            End Get
        End Property

        Private Function ReturnTypeToString(ItemType%) As String
            Select Case ItemType
                Case 0
                    Return "Namespace"
                    Exit Select
                Case 1
                    Return "Type"
                    Exit Select
                Case 2
                    Return "Method"
                    Exit Select
                Case 3
                    Return "Parameter"
                    Exit Select
                Case 4
                    Return "Generic Parameter"
                    Exit Select
                Case 5
                    Return "Variable"
                    Exit Select
                Case 6
                    Return "Property"
                    Exit Select
                Case 7
                    Return "Event"
                    Exit Select
                Case 8
                    Return "Field"
                    Exit Select
            End Select
            Return Nothing
        End Function

        Public ReadOnly Property ItemName As String
            Get
                Return Me._itemName
            End Get
        End Property

        Public ReadOnly Property obfuscatedItemName As String
            Get
                Return Me._obfuscatedItemName
            End Get
        End Property
#End Region

    End Class
End Namespace
