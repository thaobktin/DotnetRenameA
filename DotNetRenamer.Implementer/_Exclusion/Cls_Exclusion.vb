Imports dnlib.DotNet
Imports System.Windows.Forms
Imports DotNetRenamer.Helper.CecilHelper
Imports System.Drawing

Namespace Exclusion
    Public Class Cls_Exclusion

#Region " Fields "
        Private Shared _AssDef As AssemblyDef = Nothing
#End Region

#Region " Methods "
        Public Shared Function LoadTreeNode(FilePath$) As TreeNode
            _AssDef = AssemblyDef.Load(FilePath)

            Dim assNode As New TreeNode(_AssDef.FullName)
            assNode.ExpandAll()
            SetImageKey(assNode, "assembly.png")

            Dim namespaces As New Dictionary(Of String, TreeNode)

            For Each Mdef In _AssDef.Modules
                Dim libNode As New TreeNode(Mdef.Name)
                libNode.ExpandAll()
                SetImageKey(libNode, "library.png")
                For Each tDef In Mdef.Types
                    Dim tNode As TreeNode = Nothing
                    If Not namespaces.ContainsKey(tDef.Namespace) Then
                        tNode = New TreeNode(tDef.Namespace) With { _
                            .Tag = New Cls_ExclusionState(False, tDef, Cls_ExclusionState.mType.Namespaces, False)}
                        SetImageKey(tNode, "namespace.png")
                        namespaces.Add(tDef.Namespace, tNode)
                        libNode.Nodes.Add(tNode)
                    Else
                        tNode = namespaces.Item(tDef.Namespace)
                    End If
                    If (tNode.Text = tDef.Namespace) Then
                        Dim destNode As New TreeNode(tDef.Name)
                        For Each ntDef In tDef.NestedTypes
                            Dim ntNode As New TreeNode(ntDef.Name)
                            CreateMembers(ntDef, ntNode)
                            destNode.Nodes.Add(ntNode)
                        Next
                        CreateMembers(tDef, destNode)
                        tNode.Nodes.Add(destNode)
                    Else
                        Continue For
                    End If
                Next
                assNode.Nodes.Add(libNode)
            Next
            namespaces.Clear()
            Return assNode
        End Function

        Private Shared Sub CreateMembers(ByRef OriginalType As TypeDef, ByRef DestNode As TreeNode)

            DestNode.Tag = New Cls_ExclusionState(False, OriginalType, Cls_ExclusionState.mType.Types, False)
            SetImageKey(DestNode, GetTypeImage(OriginalType))

            For Each mDef As MethodDef In OriginalType.Methods
                If Not Cls_DnlibHelper.GetAccessorMethods(OriginalType).Contains(mDef) Then
                    CreateMethodNode(mDef, DestNode)
                End If
            Next

            For Each fieldDef In OriginalType.Fields
                Dim fieldNode = New TreeNode(fieldDef.Name.ToString & " : " & fieldDef.FieldType.TypeName) With { _
                    .Tag = New Cls_ExclusionState(False, fieldDef, Cls_ExclusionState.mType.Fields, False)}
                SetImageKey(fieldNode, "field.png")

                DestNode.Nodes.Add(fieldNode)
            Next

            For Each propDef In OriginalType.Properties
                Dim propNode = New TreeNode(propDef.Name.ToString & " : " & propDef.PropertySig.RetType.TypeName) With { _
                    .Tag = New Cls_ExclusionState(False, propDef, Cls_ExclusionState.mType.Properties, False)}
                SetImageKey(propNode, "property.png")

                If Not propDef.GetMethod Is Nothing Then CreateMethodNode(propDef.GetMethod, propNode)
                If Not propDef.SetMethod Is Nothing Then CreateMethodNode(propDef.SetMethod, propNode)

                For Each def In propDef.OtherMethods
                    CreateMethodNode(def, propNode)
                Next

                DestNode.Nodes.Add(propNode)
            Next

            For Each EventDef In OriginalType.Events
                Dim eventNode = New TreeNode(EventDef.Name) With { _
                    .Tag = New Cls_ExclusionState(False, EventDef, Cls_ExclusionState.mType.Events, False)}
                SetImageKey(eventNode, "event.png")

                If Not EventDef.AddMethod Is Nothing Then CreateMethodNode(EventDef.AddMethod, eventNode)
                If Not EventDef.RemoveMethod Is Nothing Then CreateMethodNode(EventDef.RemoveMethod, eventNode)

                For Each def In EventDef.OtherMethods
                    CreateMethodNode(def, eventNode)
                Next

                DestNode.Nodes.Add(eventNode)
            Next
        End Sub

        Private Shared Sub CreateMethodNode(mDef As MethodDef, DestNode As TreeNode)
            Dim methodNode As New TreeNode(mDef.Name) With { _
                .Tag = New Cls_ExclusionState(False, mDef, Cls_ExclusionState.mType.Methods, False)}
            SetImageKey(methodNode, GetMethodImage(mDef))

            Dim tmpStr As String = Nothing

            For Each paramDef In mDef.Parameters
                If Not paramDef.Type.TypeName = mDef.DeclaringType.Name Then
                    tmpStr &= String.Concat(paramDef.Type.TypeName, ",")
                End If
            Next

            methodNode.Text &= String.Concat("(", (If(tmpStr IsNot Nothing, tmpStr.TrimEnd(New Char() {","c, " "c}), Nothing)), ")")
            DestNode.Nodes.Add(methodNode)
        End Sub

        Private Shared Function GetMethodImage(mdef As MethodDef) As String
            Dim str = "Method.png"
            If mdef.IsConstructor Then
                str = "Constructor.png"
            ElseIf mdef.IsPinvokeImpl Then
                str = "PInvokeMethod.png"
            End If
            Return str
        End Function

        Private Shared Function GetTypeImage(mdef As TypeDef) As String
            Dim str = "class.png"
            If mdef.IsInterface Then
                str = "interface.png"
            ElseIf mdef.IsEnum Then
                str = "enum.png"
            ElseIf mdef.IsValueType Then
                str = "enumvalue.png"
            ElseIf (mdef.BaseType IsNot Nothing) AndAlso (mdef.BaseType.Name.ToLower.Contains("delegate")) Then
                str = "delegate.png"
            ElseIf mdef.IsSealed Then
                str = "staticclass.png"
            End If
            Return str
        End Function

        Private Shared Sub SetImageKey(node As TreeNode, imageKey$)
            node.ImageKey = imageKey
            node.SelectedImageKey = imageKey
        End Sub

        Public Shared Function isRenamable(Obj As Object) As Boolean
            If Obj Is Nothing Then Return False
            Dim b As Boolean = False
            If TypeOf TryCast(Obj.member, MethodDef) Is MethodDef Then
                b = Cls_DnlibHelper.IsRenamable(TryCast(Obj.member, MethodDef), True)
            ElseIf TypeOf TryCast(Obj.member, TypeDef) Is TypeDef Then
                b = Cls_DnlibHelper.IsRenamable(TryCast(Obj.member, TypeDef))
            ElseIf TypeOf TryCast(Obj.member, EventDef) Is EventDef Then
                b = Cls_DnlibHelper.IsRenamable(TryCast(Obj.member, EventDef))
            ElseIf TypeOf TryCast(Obj.member, PropertyDef) Is PropertyDef Then
                b = Cls_DnlibHelper.IsRenamable(TryCast(Obj.member, PropertyDef))
            ElseIf TypeOf TryCast(Obj.member, FieldDef) Is FieldDef Then
                b = Cls_DnlibHelper.IsRenamable(TryCast(Obj.member, FieldDef))
            End If
            Return b
        End Function

        Public Shared Function isTypedef(n As Object) As Boolean
            If n Is Nothing Then Return False
            Return TypeOf TryCast(n.member, TypeDef) Is TypeDef
        End Function

        Public Shared Function getEntitiesVal(n As Object) As Boolean
            If n Is Nothing Then Return False
            Return CBool(n.AllEntities)
        End Function

        Public Shared Function isExclude(n As Object) As Boolean
            If n Is Nothing Then Return False
            Return n.exclude
        End Function
#End Region

    End Class
End Namespace
