Imports dnlib.DotNet

Namespace Exclusion
    Public Class Cls_ExclusionState
        Enum mType
            Namespaces = 0
            Types = 1
            Methods = 2
            Properties = 3
            Events = 4
            Fields = 5
        End Enum

        Public Property exclude As Boolean
        Public Property member As Object
        Public Property memberType As mType
        Public Property allEntities As Boolean

        Public Sub New(exclude As Boolean, member As Object, memberType As mType, allEntities As Boolean)
            _exclude = exclude
            _member = member
            _memberType = memberType
            _allEntities = allEntities
        End Sub

    End Class
End Namespace
