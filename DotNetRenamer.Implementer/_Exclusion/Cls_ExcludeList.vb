Imports dnlib.DotNet
Imports dnlib.DotNet.Emit

Namespace Exclusion
    Public Class Cls_ExcludeList

#Region " Fields "
        Private _ObfTypes As New List(Of TypeDef)
        Private _ObfMethods As New List(Of MethodDef)
        Private _ObfProperties As New List(Of PropertyDef)
        Private _ObfEvents As New List(Of EventDef)
        Private _ObfFields As New List(Of FieldDef)
#End Region

#Region " Methods "

        Public Sub AddTo(m As Object)
            If m.memberType = Cls_ExclusionState.mType.Types Then
                If Not _ObfTypes.Contains(m.member) Then _ObfTypes.Add(m.member)
            ElseIf m.memberType = Cls_ExclusionState.mType.Methods Then
                If Not _ObfMethods.Contains(m.member) Then _ObfMethods.Add(m.member)
            ElseIf m.memberType = Cls_ExclusionState.mType.Properties Then
                If Not _ObfProperties.Contains(m.member) Then _ObfProperties.Add(m.member)
            ElseIf m.memberType = Cls_ExclusionState.mType.Events Then
                If Not _ObfEvents.Contains(m.member) Then _ObfEvents.Add(m.member)
            ElseIf m.memberType = Cls_ExclusionState.mType.Fields Then
                If Not _ObfFields.Contains(m.member) Then _ObfFields.Add(m.member)
            End If
        End Sub

        Public Sub RemoveFrom(m As Object)
            If m.memberType = Cls_ExclusionState.mType.Types Then
                If _ObfTypes.Contains(m.member) Then _ObfTypes.Remove(m.member)
            ElseIf m.memberType = Cls_ExclusionState.mType.Methods Then
                If _ObfMethods.Contains(m.member) Then _ObfMethods.Remove(m.member)
            ElseIf m.memberType = Cls_ExclusionState.mType.Properties Then
                If _ObfProperties.Contains(m.member) Then _ObfProperties.Remove(m.member)
            ElseIf m.memberType = Cls_ExclusionState.mType.Events Then
                If _ObfEvents.Contains(m.member) Then _ObfEvents.Remove(m.member)
            ElseIf m.memberType = Cls_ExclusionState.mType.Fields Then
                If _ObfFields.Contains(m.member) Then _ObfFields.Remove(m.member)
            End If
        End Sub

        Public Sub CleanUp()
            _ObfTypes.Clear()
            _ObfMethods.Clear()
            _ObfProperties.Clear()
            _ObfEvents.Clear()
            _ObfFields.Clear()
        End Sub

        Public Function isTypeExclude(m As TypeDef) As Boolean
            Return _ObfTypes.Any(Function(x) x.FullName = m.FullName)
        End Function

        Public Function itemsCount() As Integer
            Return _ObfTypes.Count + _ObfMethods.Count + _ObfProperties.Count + _ObfEvents.Count + _ObfFields.Count
        End Function

#End Region

    End Class
End Namespace
