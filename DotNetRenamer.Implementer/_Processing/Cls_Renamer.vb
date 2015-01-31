Imports dnlib.DotNet
Imports dnlib.DotNet.Emit
Imports DotNetRenamer.Implementer.Context
Imports DotNetRenamer.Helper.RandomizeHelper
Imports DotNetRenamer.Helper.CecilHelper

Namespace Processing
    ''' <summary>
    ''' INFO : This is the forth step of the renamer library. 
    '''        This is the core of the rename library !
    ''' </summary>
    Friend NotInheritable Class Cls_Renamer

#Region " Methods "
        ''' <summary>
        ''' INFO : Rename the method. Return methodDefinition member.
        ''' </summary>
        ''' <param name="method"></param>
        Friend Shared Function RenameMethod(type As TypeDef, method As MethodDef) As MethodDef
            Dim MethodOriginal$ = method.Name
            Dim MethodPublicObf$ = Cls_Randomizer.GenerateNew()

            If method.IsPinvokeImpl Then
                If method.ImplMap.Name = String.Empty Then method.ImplMap.Name = MethodOriginal
            End If

            method.Name = Cls_Mapping.RenameMethodMember(method, MethodPublicObf)

            Return method
        End Function

        ''' <summary>
        ''' INFO : Rename Parameters from method.
        ''' </summary>
        ''' <param name="method"></param>
        Friend Shared Sub RenameParameters(method As MethodDef)
            If method.HasParamDefs Then
                For Each ParDef As ParamDef In method.ParamDefs
                    If ParDef.CustomAttributes.Count = 0 Then
                        ParDef.Name = Cls_Mapping.RenameParamMember(ParDef, Cls_Randomizer.GenerateNew())
                    End If
                Next
            End If
            If method.HasGenericParameters Then
                For Each GenPar As GenericParam In method.GenericParameters
                    If GenPar.CustomAttributes.Count = 0 Then
                        GenPar.Name = Cls_Mapping.RenameGenericParamMember(GenPar, Cls_Randomizer.GenerateNew())
                    End If
                Next
            End If
        End Sub

        Friend Shared Sub RenameSettings(mDef As MethodDef, originalN$, obfuscatedN$)
            If Not mDef Is Nothing Then
                If Not mDef.DeclaringType.BaseType Is Nothing AndAlso mDef.DeclaringType.BaseType.Name = "ApplicationSettingsBase" Then
                    If mDef.HasBody Then
                        Dim instruction As Instruction
                        If mDef.Body.Instructions.Count <> 0 Then
                            For Each instruction In mDef.Body.Instructions
                                If TypeOf instruction.Operand Is String Then
                                    Dim Name$ = instruction.Operand
                                    If originalN = Name Then
                                        If mDef.Name.StartsWith("set_") Then
                                            mDef.Name = "set_" & obfuscatedN
                                        ElseIf mDef.Name.StartsWith("get_") Then
                                            mDef.Name = "get_" & obfuscatedN
                                        End If
                                        instruction.Operand = obfuscatedN
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' INFO : Rename Variables from method.
        ''' </summary>
        ''' <param name="method"></param>
        Friend Shared Sub RenameVariables(Method As MethodDef)
            If Method.HasBody Then
                For Each vari In Method.Body.Variables
                    vari.Name = Cls_Mapping.RenameVariableMember(vari, Cls_Randomizer.GenerateNew())
                Next
            End If
        End Sub

        ''' <summary>
        ''' INFO : Rename embedded Resources from Resources dir and updates method bodies.
        ''' </summary>
        ''' <param name="TypeDef"></param>
        ''' <param name="NamespaceOriginal"></param>
        ''' <param name="NamespaceObfuscated"></param>
        ''' <param name="TypeOriginal"></param>
        ''' <param name="TypeObfuscated"></param>
        Friend Shared Sub RenameResources(TypeDef As TypeDef, ByRef NamespaceOriginal$, ByRef NamespaceObfuscated$, TypeOriginal$, TypeObfuscated$)
            Dim ModuleDef As ModuleDef = TypeDef.Module
            For Each EmbRes As EmbeddedResource In ModuleDef.Resources
                If EmbRes.Name = NamespaceOriginal & "." & TypeOriginal & ".resources" Then
                    EmbRes.Name = If(NamespaceObfuscated = String.Empty, TypeObfuscated & ".resources", NamespaceObfuscated & "." & TypeObfuscated & ".resources")
                End If
            Next

            If TypeDef.HasMethods Then
                For Each method In TypeDef.Methods
                    If method.HasBody Then
                        For Each inst In method.Body.Instructions
                            If inst.OpCode Is OpCodes.Ldstr Then
                                If inst.Operand.ToString() = (NamespaceOriginal & "." & TypeOriginal) Then
                                    inst.Operand = If(NamespaceObfuscated = String.Empty, TypeObfuscated, NamespaceObfuscated & "." & TypeObfuscated)
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' INFO : Rename embedded Resources from Resources dir and from ResourcesManager method.
        ''' </summary>
        ''' <param name="typeDef"></param>
        Friend Shared Sub RenameResourceManager(typeDef As TypeDef)
            For Each pr In typeDef.Properties
                If Not pr.GetMethod Is Nothing Then
                    If pr.GetMethod.Name = "get_ResourceManager" Then
                        If pr.GetMethod.HasBody Then
                            Dim instruction As Instruction
                            If pr.GetMethod.Body.Instructions.Count <> 0 Then
                                For Each instruction In pr.GetMethod.Body.Instructions
                                    If TypeOf instruction.Operand Is String Then
                                        Dim NewResManagerName$ = instruction.Operand
                                        For Each EmbRes As EmbeddedResource In typeDef.Module.Resources
                                            If EmbRes.Name = instruction.Operand & ".resources" Then
                                                NewResManagerName = Cls_Randomizer.GenerateNew()
                                                EmbRes.Name = NewResManagerName & ".resources"
                                            End If
                                        Next
                                        instruction.Operand = NewResManagerName
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
            Next
        End Sub

        ''' <summary>
        ''' INFO : Rename Property.
        ''' </summary>
        ''' <param name="prop"></param>
        ''' <param name="obfuscatedN"></param>
        Friend Shared Sub RenameProperty(ByRef prop As PropertyDef, obfuscatedN$, Optional ByVal RenameSpecialMethod As Boolean = False)
            prop.Name = Cls_Mapping.RenamePropertyMember(prop, obfuscatedN)

            If RenameSpecialMethod Then
                If Not prop.GetMethod Is Nothing Then
                    Dim meth = Cls_Renamer.RenameMethod(prop.DeclaringType, prop.GetMethod)
                    Cls_Renamer.RenameParameters(meth)
                    Cls_Renamer.RenameVariables(meth)
                End If
                If Not prop.SetMethod Is Nothing Then
                    Dim meth = Cls_Renamer.RenameMethod(prop.DeclaringType, prop.SetMethod)
                    Cls_Renamer.RenameParameters(meth)
                    Cls_Renamer.RenameVariables(meth)
                End If

                For Each m In prop.OtherMethods
                    If Not m Is Nothing Then
                        If Cls_DnlibHelper.IsRenamable(m) Then
                            Dim meth = Cls_Renamer.RenameMethod(prop.DeclaringType, m)
                            Cls_Renamer.RenameParameters(meth)
                            Cls_Renamer.RenameVariables(meth)
                        End If
                    End If
                Next
            End If

        End Sub

        ''' <summary>
        ''' INFO : Rename Field.
        ''' </summary>
        ''' <param name="field"></param>
        ''' <param name="obfuscatedN"></param>
        Friend Shared Sub RenameField(field As FieldDef, obfuscatedN$)
            field.Name = Cls_Mapping.RenameFieldMember(field, obfuscatedN)
        End Sub

        ''' <summary>
        ''' INFO : Rename Event.
        ''' </summary>
        ''' <param name="events"></param>
        ''' <param name="obfuscatedN"></param>
        Friend Shared Sub RenameEvent(ByRef events As EventDef, obfuscatedN$)
            events.Name = Cls_Mapping.RenameEventMember(events, obfuscatedN)
        End Sub

        ''' <summary>
        ''' INFO : Rename CustomAttributes.
        ''' </summary>
        ''' <remarks>
        ''' REMARKS : Only AccessedThroughPropertyAttribute attribute is renamed to prevent de4Dot to retrieve original names.
        ''' </remarks>
        ''' <param name="type"></param>
        ''' <param name="prop"></param>
        ''' <param name="originalN"></param>
        ''' <param name="obfuscatedN"></param> 
        Friend Shared Sub RenameCustomAttributes(type As TypeDef, prop As PropertyDef, originalN$, obfuscatedN$)
            If type.HasFields Then
                For Each field As FieldDef In type.Fields
                    If field.IsPrivate Then
                        If field.HasCustomAttributes Then
                            For Each ca In field.CustomAttributes
                                If ca.AttributeType.Name = "AccessedThroughPropertyAttribute" Then
                                    If ca.HasConstructorArguments Then
                                        For Each arg In ca.ConstructorArguments
                                            If arg.Value = originalN Then
                                                ca.ConstructorArguments(0) = New CAArgument(arg.Type, obfuscatedN)
                                                RenameProperty(prop, obfuscatedN)
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If
                Next
            End If
            RenameCustomAttributesValues(prop)
        End Sub

        Friend Shared Sub RenameCustomAttributesValues(member As Object)
            If member.HasCustomAttributes Then
                For Each ca In member.CustomAttributes
                    If Not ca Is Nothing Then
                        If ca.AttributeType.Name = "CategoryAttribute" OrElse ca.AttributeType.Name = "DescriptionAttribute" Then
                            If ca.HasConstructorArguments Then
                                For Each arg In ca.ConstructorArguments
                                    ca.ConstructorArguments(0) = New CAArgument(arg.Type, Cls_Randomizer.GenerateNew())
                                    Exit For
                                Next
                            End If
                        End If
                    End If
                Next
            End If
        End Sub

        Friend Shared Sub RenameInitializeComponentsValues(TypeDef As TypeDef, NewKeyName$, OriginalKeyName$, ByVal Properties As Boolean)
            Dim methodSearch As MethodDef = Cls_DnlibHelper.FindMethod(TypeDef, "InitializeComponent")
            If Not methodSearch Is Nothing Then
                If methodSearch.HasBody Then
                    If methodSearch.Body.Instructions.Count <> 0 Then
                        For i As Integer = 0 To methodSearch.Body.Instructions.Count - 1
                            Dim Instruction As Instruction = methodSearch.Body.Instructions(i)
                            If TypeOf Instruction.Operand Is String Then
                                If Properties Then
                                    If Not methodSearch.Body.Instructions(i - 1) Is Nothing Then
                                        If methodSearch.Body.Instructions(i - 1).OpCode Is OpCodes.Callvirt AndAlso methodSearch.Body.Instructions(i - 1).Operand.ToString.EndsWith("get_" & OriginalKeyName & "()") Then
                                            If CStr(Instruction.Operand) = OriginalKeyName Then
                                                Instruction.Operand = NewKeyName
                                            End If
                                        End If
                                    End If
                                Else
                                    If Not methodSearch.Body.Instructions(i + 1) Is Nothing Then
                                        If methodSearch.Body.Instructions(i + 1).OpCode Is OpCodes.Callvirt AndAlso methodSearch.Body.Instructions(i + 1).ToString.EndsWith("set_Name(System.String)") Then
                                            If CStr(Instruction.Operand) = OriginalKeyName Then
                                                Instruction.Operand = NewKeyName
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End Sub

#End Region

    End Class
End Namespace
