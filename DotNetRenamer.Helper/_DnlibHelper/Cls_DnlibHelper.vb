Imports dnlib.DotNet
Imports dnlib.DotNet.Emit

Namespace CecilHelper
    Public NotInheritable Class Cls_DnlibHelper

#Region " Methods "
        ''' <summary>
        ''' INFO : Verifying if typeDefinition is renamable
        ''' </summary>
        ''' <param name="type"></param>
        Public Shared Function IsRenamable(type As TypeDef) As Boolean
            If Not type.BaseType Is Nothing Then
                If type.BaseType Is GetType(Array) Then
                    Return False
                End If
            End If
            Return Not type.FullName = "<Module>" AndAlso Not type.IsImport
        End Function

        ''' <summary>
        ''' INFO : Verifying if methodDefinition is renamable
        ''' </summary>
        ''' <param name="method"></param>
        Public Shared Function IsRenamable(method As MethodDef, Optional ByVal Force As Boolean = False) As Boolean
            If Force Then
                If method.HasBody Then
                    If method.Body.Instructions.Count <> 0 Then
                        For i As Integer = 0 To method.Body.Instructions.Count - 1
                            Dim Instruct As Instruction = method.Body.Instructions(i)
                            If Instruct.OpCode Is OpCodes.Stfld OrElse Instruct.OpCode Is OpCodes.Ldfld Then
                                Dim mr As IMemberRef = TryCast(Instruct.Operand, IMemberRef)
                                If Not mr Is Nothing Then
                                    If mr.IsFieldDef Then
                                        Dim fd As FieldDef = TryCast(mr, FieldDef)
                                        If fd.HasCustomAttributes Then
                                            For Each ca In fd.CustomAttributes
                                                If ca.AttributeType.Name = "AccessedThroughPropertyAttribute" Then
                                                    If ca.HasConstructorArguments Then
                                                        For Each arg In ca.ConstructorArguments
                                                            If arg.Value = method.Name.Replace("set_", "").Replace("get_", "") Then
                                                                Return True
                                                                Exit For
                                                            End If
                                                        Next
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        If method.Body.Instructions(0).OpCode Is OpCodes.Call AndAlso method.Body.Instructions(0).Operand.ToString.EndsWith("get_ResourceManager()") Then
                            If method.Body.Instructions(1).OpCode Is OpCodes.Ldstr AndAlso (Not method.Body.Instructions(1).Operand Is Nothing) Then
                                If CStr(method.Body.Instructions(1).Operand) = method.Name.Replace("set_", "").Replace("get_", "") Then
                                    Return True
                                End If
                            End If
                        End If

                        If method.Name.StartsWith("add_") OrElse method.Name.StartsWith("remove_") Then
                            Return True
                        End If
                    End If
                End If
            End If
            Return method IsNot Nothing AndAlso Not (method.IsRuntimeSpecialName OrElse method.IsRuntime OrElse method.IsSpecialName OrElse method.IsConstructor OrElse method.HasOverrides OrElse method.IsVirtual OrElse method.IsAbstract OrElse method.Name.EndsWith("GetEnumerator"))
        End Function

        ''' <summary>
        ''' INFO : Verifying if eventDefinition is renamable
        ''' </summary>
        ''' <param name="Events"></param>
        Public Shared Function IsRenamable(ByVal Events As EventDef) As Boolean
            Return If(Not Events.IsSpecialName OrElse Not Events.IsRuntimeSpecialName OrElse Not Events.EventType.IsTypeDef, True, False)
        End Function

        ''' <summary>
        ''' INFO : Verifying if propertyDefinition is renamable
        ''' </summary>
        ''' <param name="prop"></param>
        Public Shared Function IsRenamable(prop As PropertyDef) As Boolean
            Return Not ((prop.IsSpecialName OrElse prop.IsRuntimeSpecialName))
        End Function

        ''' <summary>
        ''' INFO : Verifying if fieldDefinition is renamable
        ''' </summary>
        ''' <param name="field"></param>
        Public Shared Function IsRenamable(field As FieldDef) As Boolean
            If (Not field.IsRuntimeSpecialName AndAlso Not field.DeclaringType.HasGenericParameters) And Not field.IsPinvokeImpl AndAlso Not field.IsSpecialName Then
                Return True
            End If
            Return False
        End Function

        Public Shared Function GetAccessorMethods(ByVal type As TypeDef) As List(Of MethodDef)
            Dim list As New List(Of MethodDef)
            For Each pDef In type.Properties
                list.Add(pDef.GetMethod)
                list.Add(pDef.SetMethod)
                If pDef.HasOtherMethods Then
                    For Each pDefO In pDef.OtherMethods
                        list.Add(pDefO)
                    Next
                End If
            Next
            For Each eDef In type.Events
                list.Add(eDef.AddMethod)
                list.Add(eDef.RemoveMethod)
                list.Add(eDef.InvokeMethod)
                If eDef.HasOtherMethods Then
                    For Each eDefO In eDef.OtherMethods
                        list.Add(eDefO)
                    Next
                End If
            Next
            Return list
        End Function

        Public Shared Function FindType(moduleDef As ModuleDef, Name As String) As TypeDef
            For Each typeDef As TypeDef In moduleDef.Types
                Dim returnType As TypeDef = Nothing

                If typeDef.Name = Name Then
                    Return typeDef
                End If

                returnType = FindNestedType(typeDef, Name)

                If returnType IsNot Nothing Then
                    Return returnType
                End If
            Next

            Return Nothing
        End Function

        Private Shared Function FindNestedType(parentType As TypeDef, fullname As String) As TypeDef
            For Each type In parentType.NestedTypes
                If type.FullName = fullname Then
                    Return type
                End If

                If type.HasNestedTypes Then
                    Return FindNestedType(type, fullname)
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function FindType(parentDef As TypeDef, name As String) As TypeDef
            Return parentDef.NestedTypes.First(Function(t) t.Name = name)
        End Function

        Public Shared Function FindMethod(assDef As AssemblyDef, name As String) As MethodDef
            For Each t In assDef.ManifestModule.GetTypes
                If t.HasMethods Then
                    For Each methodDef As MethodDef In t.Methods
                        If methodDef.Name = name Then
                            Return methodDef
                        End If
                    Next
                End If
            Next
            Return Nothing
        End Function

        Public Shared Function FindMethod(typeDef As TypeDef, name As String) As MethodDef
            For Each methodDef As MethodDef In typeDef.Methods
                If methodDef.Name = name Then
                    Return methodDef
                End If
            Next
            Return Nothing
        End Function

        Public Shared Function FindField(parentType As TypeDef, name As String) As FieldDef
            Return parentType.Fields.First(Function(f) f.Name = name)
        End Function
#End Region

    End Class
End Namespace