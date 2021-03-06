﻿Imports dnlib.DotNet

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
        Public Shared Function IsRenamable(method As MethodDef) As Boolean
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
            Dim def As PropertyDef
            For Each def In type.Properties
                list.Add(def.GetMethod)
                list.Add(def.SetMethod)
                If def.HasOtherMethods Then
                    Dim def2 As MethodDef
                    For Each def2 In def.OtherMethods
                        list.Add(def2)
                    Next
                End If
            Next
            Dim def3 As EventDef
            For Each def3 In type.Events
                list.Add(def3.AddMethod)
                list.Add(def3.RemoveMethod)
                list.Add(def3.InvokeMethod)
                If def3.HasOtherMethods Then
                    Dim def4 As MethodDef
                    For Each def4 In def3.OtherMethods
                        list.Add(def4)
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