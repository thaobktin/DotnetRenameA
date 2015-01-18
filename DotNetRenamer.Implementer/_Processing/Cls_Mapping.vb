Imports DotNetRenamer.Implementer.Context
Imports dnlib.DotNet
Imports dnlib.DotNet.Emit

Namespace Processing
    ''' <summary>
    ''' INFO : This is the fith step of the renamer library. 
    '''        This will check the existence of the name and if true will return the associated renamed value.
    '''        This class is able to clean dictionaries when the renaming process is complete.
    ''' </summary>
    Public Class Cls_Mapping

#Region " Variables "
        Private Shared _ObfNamespaces As New Dictionary(Of String, String)
        Private Shared _ObfTypes As New Dictionary(Of TypeDef, String)
        Private Shared _ObfMethods As New Dictionary(Of MethodDef, String)
        Private Shared _ObfParameters As New Dictionary(Of ParamDef, String)
        Private Shared _ObfGenericParameters As New Dictionary(Of GenericParam, String)
        Private Shared _ObfVariables As New Dictionary(Of Local, String)
        Private Shared _ObfProperties As New Dictionary(Of PropertyDef, String)
        Private Shared _ObfEvents As New Dictionary(Of EventDef, String)
        Private Shared _ObfFields As New Dictionary(Of FieldDef, String)
#End Region

#Region " Methods "
        ''' <summary>
        ''' INFO : Store Key/Value pair (TypeDefinition/ObfuscatedName and third arg set to True if this is a namespace) to dictionary only if key not exists. Return NamespaceObfuscated value.
        ''' </summary>
        ''' <param name="Type"></param>
        ''' <param name="NamespaceObfuscated"></param>
        Friend Shared Function RenameTypeDef(Type As TypeDef, ByRef NamespaceObfuscated$, Optional ByVal isNamespace As Boolean = False) As String
            If isNamespace Then
                If Not _ObfNamespaces.ContainsKey(Type.Namespace) Then
                    _ObfNamespaces.Add(Type.Namespace, NamespaceObfuscated)
                    Cls_Context.RaiseRenamedItemEvent(New Cls_RenamedItem(Cls_RenamedItemType.RenamedItemType.Namespaces, Type.Namespace, NamespaceObfuscated))
                Else
                    NamespaceObfuscated = _ObfNamespaces.Item(Type.Namespace)
                End If
            Else
                If Not _ObfTypes.ContainsKey(Type) Then
                    _ObfTypes.Add(Type, NamespaceObfuscated)
                    Cls_Context.RaiseRenamedItemEvent(New Cls_RenamedItem(Cls_RenamedItemType.RenamedItemType.Types, Type.Name, NamespaceObfuscated))
                Else
                    NamespaceObfuscated = _ObfTypes.Item(Type)
                End If
            End If
            Return NamespaceObfuscated
        End Function

        ''' <summary>
        ''' INFO : Store Key/Value pair (MethodDefinition/ObfuscatedName) to dictionary only if key not exists. Return MemberObfuscated value.
        ''' </summary>
        ''' <param name="Member"></param>
        ''' <param name="MemberObfuscated"></param>
        Friend Shared Function RenameMethodMember(Member As MethodDef, ByRef MemberObfuscated$) As String
            If Not _ObfMethods.ContainsKey(Member) Then
                _ObfMethods.Add(Member, MemberObfuscated)
                Cls_Context.RaiseRenamedItemEvent(New Cls_RenamedItem(Cls_RenamedItemType.RenamedItemType.Methods, Member.Name, MemberObfuscated))
            Else
                MemberObfuscated = _ObfMethods.Item(Member)
            End If
            Return MemberObfuscated
        End Function

        ''' <summary>
        ''' INFO : Store Key/Value pair (Parameter/ObfuscatedName) to dictionary only if key not exists. Return MemberObfuscated value.
        ''' </summary>
        ''' <param name="Member"></param>
        ''' <param name="MemberObfuscated"></param>
        Friend Shared Function RenameParamMember(Member As ParamDef, ByRef MemberObfuscated$) As String
            If Not _ObfParameters.ContainsKey(Member) Then
                _ObfParameters.Add(Member, MemberObfuscated)
                Cls_Context.RaiseRenamedItemEvent(New Cls_RenamedItem(Cls_RenamedItemType.RenamedItemType.Parameters, Member.Name, MemberObfuscated))
            Else
                MemberObfuscated = _ObfParameters.Item(Member)
            End If
            Return MemberObfuscated
        End Function

        ''' <summary>
        ''' INFO : Store Key/Value pair (GenericParameter/ObfuscatedName) to dictionary only if key not exists. Return MemberObfuscated value.
        ''' </summary>
        ''' <param name="Member"></param>
        ''' <param name="MemberObfuscated"></param>
        Friend Shared Function RenameGenericParamMember(Member As GenericParam, ByRef MemberObfuscated$) As String
            If Not _ObfGenericParameters.ContainsKey(Member) Then
                _ObfGenericParameters.Add(Member, MemberObfuscated)
                Cls_Context.RaiseRenamedItemEvent(New Cls_RenamedItem(Cls_RenamedItemType.RenamedItemType.GenericParameters, Member.Name, MemberObfuscated))
            Else
                MemberObfuscated = _ObfGenericParameters.Item(Member)
            End If
            Return MemberObfuscated
        End Function

        ''' <summary>
        ''' INFO : Store Key/Value pair (VariableDefinition/ObfuscatedName) to dictionary only if key not exists. Return MemberObfuscated value.
        ''' </summary>
        ''' <param name="Member"></param>
        ''' <param name="MemberObfuscated"></param>
        Friend Shared Function RenameVariableMember(Member As Local, ByRef MemberObfuscated$) As String
            If Not _ObfVariables.ContainsKey(Member) Then
                _ObfVariables.Add(Member, MemberObfuscated)
                Cls_Context.RaiseRenamedItemEvent(New Cls_RenamedItem(Cls_RenamedItemType.RenamedItemType.Variables, Member.ToString, MemberObfuscated))
            Else
                MemberObfuscated = _ObfVariables.Item(Member)
            End If
            Return MemberObfuscated
        End Function

        ''' <summary>
        ''' INFO : Store Key/Value pair (PropertydDefinition/ObfuscatedName) to dictionary only if key not exists. Return MemberObfuscated value.
        ''' </summary>
        ''' <param name="Member"></param>
        ''' <param name="MemberObfuscated"></param>
        Friend Shared Function RenamePropertyMember(Member As PropertyDef, ByRef MemberObfuscated$) As String
            If Not _ObfProperties.ContainsKey(Member) Then
                _ObfProperties.Add(Member, MemberObfuscated)
                Cls_Context.RaiseRenamedItemEvent(New Cls_RenamedItem(Cls_RenamedItemType.RenamedItemType.Properties, Member.Name, MemberObfuscated))
            Else
                MemberObfuscated = _ObfProperties.Item(Member)
            End If
            Return MemberObfuscated
        End Function

        ''' <summary>
        ''' INFO : Store Key/Value pair (EventDefinition/ObfuscatedName) to dictionary only if key not exists. Return MemberObfuscated value.
        ''' </summary>
        ''' <param name="Member"></param>
        ''' <param name="MemberObfuscated"></param>
        Friend Shared Function RenameEventMember(Member As EventDef, ByRef MemberObfuscated$) As String
            If Not _ObfEvents.ContainsKey(Member) Then
                _ObfEvents.Add(Member, MemberObfuscated)
                Cls_Context.RaiseRenamedItemEvent(New Cls_RenamedItem(Cls_RenamedItemType.RenamedItemType.Events, Member.Name, MemberObfuscated))
            Else
                MemberObfuscated = _ObfEvents.Item(Member)
            End If
            Return MemberObfuscated
        End Function

        ''' <summary>
        ''' INFO : Store Key/Value pair (FieldDefinition/ObfuscatedName) to dictionary only if key not exists. Return MemberObfuscated value.
        ''' </summary>
        ''' <param name="Member"></param>
        ''' <param name="MemberObfuscated"></param>
        Friend Shared Function RenameFieldMember(Member As FieldDef, ByRef MemberObfuscated$) As String
            If Not _ObfFields.ContainsKey(Member) Then
                _ObfFields.Add(Member, MemberObfuscated)
                Cls_Context.RaiseRenamedItemEvent(New Cls_RenamedItem(Cls_RenamedItemType.RenamedItemType.Fields, Member.Name, MemberObfuscated))
            Else
                MemberObfuscated = _ObfFields.Item(Member)
            End If
            Return MemberObfuscated
        End Function

        ''' <summary>
        ''' INFO : CleanUp Namespaces dictionary and MethodReferences List.
        ''' </summary>
        Friend Shared Sub CleanUp()
            If Not _ObfNamespaces.Count <> 0 Then _ObfNamespaces.Clear()
            If Not _ObfTypes.Count <> 0 Then _ObfTypes.Clear()
            If Not _ObfMethods.Count <> 0 Then _ObfMethods.Clear()
            If Not _ObfGenericParameters.Count <> 0 Then _ObfGenericParameters.Clear()
            If Not _ObfParameters.Count <> 0 Then _ObfParameters.Clear()
            'If Not _ObfVariables.Count <> 0 Then _ObfVariables.Clear()
            If Not _ObfProperties.Count <> 0 Then _ObfProperties.Clear()
            If Not _ObfEvents.Count <> 0 Then _ObfEvents.Clear()
            If Not _ObfFields.Count <> 0 Then _ObfFields.Clear()
        End Sub
#End Region

    End Class
End Namespace


