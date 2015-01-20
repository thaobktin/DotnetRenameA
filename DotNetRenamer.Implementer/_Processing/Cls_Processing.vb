Imports DotNetRenamer.Implementer.Context
Imports DotNetRenamer.Helper.CecilHelper
Imports DotNetRenamer.Helper.RandomizeHelper
Imports DotNetRenamer.Implementer.Resources
Imports dnlib.DotNet


Namespace Processing

    ''' <summary>
    ''' INFO : This is the third step of the renamer library. 
    '''        This will process to rename types and members from the selected assembly with settings of your choice.
    ''' </summary>
    Public Class Cls_Processing

#Region " Variables "
        Private _RenamingAccept As Cls_RenamerState
#End Region

#Region " Initialize "
        ''' <summary>
        ''' INFO : Initializes a new instance of the Processing.Cls_Processing class from which is started the task of renaming.
        ''' </summary>
        ''' <param name="RenamingAccept"></param>
        Public Sub New(RenamingAccept As Cls_RenamerState)
            _RenamingAccept = RenamingAccept
            If _RenamingAccept.RenameRuleSetting = Cls_RenamerState.RenameRule.Full Then
                _RenamingAccept.Namespaces = True
                _RenamingAccept.Types = True
                _RenamingAccept.Methods = True
                _RenamingAccept.Properties = True
                _RenamingAccept.Fields = True
                _RenamingAccept.CustomAttributes = True
                _RenamingAccept.Events = True
                _RenamingAccept.Variables = True
                _RenamingAccept.Parameters = True
                _RenamingAccept.ReplaceNamespacesSetting = True
                Cls_RandomizerType.RenameSetting = Cls_RandomizerType.RenameEnum.Alphabetic
            ElseIf RenamingAccept.RenameRuleSetting = Cls_RenamerState.RenameRule.Medium Then
                _RenamingAccept.Namespaces = True
                _RenamingAccept.Types = True
                _RenamingAccept.Methods = True
                _RenamingAccept.Properties = True
                _RenamingAccept.Fields = True
                _RenamingAccept.CustomAttributes = True
                _RenamingAccept.Events = False
                _RenamingAccept.Variables = False
                _RenamingAccept.Parameters = False
                _RenamingAccept.ReplaceNamespacesSetting = False
                Cls_RandomizerType.RenameSetting = Cls_RandomizerType.RenameEnum.Japanese
            Else
                Cls_RandomizerType.RenameSetting = RenamingAccept.RenamingType
            End If
        End Sub
#End Region

#Region " Methods "

        ''' <summary>
        ''' INFO : This is the EntryPoint of the renamer method ! EmbeddedResources renaming.
        ''' </summary>
        ''' <param name="AssDef"></param>
        Public Sub ProcessResourceContent(AssDef As AssemblyDef)
            Cls_Content.Rename(AssDef)
        End Sub

        ''' <summary>
        ''' INFO : This is the EntryPoint of the renamer method ! Namespaces, Types and Resources renaming.
        ''' </summary>
        ''' <param name="type"></param>
        Public Sub ProcessType(type As TypeDef)

            Dim NamespaceOriginal$ = type.Namespace
            Dim NamespaceObfuscated$ = type.Namespace

            If Not type.Name = "<Module>" Then
                If _RenamingAccept.Namespaces Then
                    NamespaceObfuscated = If(CBool(_RenamingAccept.ReplaceNamespacesSetting) = True, String.Empty, Cls_Randomizer.GenerateNew())
                    type.Namespace = Cls_Mapping.RenameTypeDef(type, NamespaceObfuscated, True)
                End If
            End If

            If Cls_CecilHelper.IsRenamable(type) Then
                Dim TypeOriginal$ = type.Name
                If _RenamingAccept.CustomAttributes Then
                    Cls_Renamer.RenameCustomAttributesValues(type)
                End If
                If _RenamingAccept.Types Then
                    type.Name = Cls_Mapping.RenameTypeDef(type, Cls_Randomizer.GenerateNew())
                    Cls_Renamer.RenameResources(type, NamespaceOriginal, NamespaceObfuscated, TypeOriginal, type.Name)
                End If

                If _RenamingAccept.Namespaces Then
                    type.Namespace = Cls_Mapping.RenameTypeDef(type, NamespaceObfuscated, True)
                    Cls_Renamer.RenameResources(type, NamespaceOriginal, NamespaceObfuscated, TypeOriginal, TypeOriginal)
                End If

                If type.HasProperties Then
                    Cls_Renamer.RenameResourceManager(type)
                End If

                If _RenamingAccept.Types OrElse _RenamingAccept.Namespaces Then
                    Cls_Renamer.RenameInitializeComponentsValues(type, type.Name, TypeOriginal, False)
                End If
            End If
        End Sub

        ''' <summary>
        ''' INFO : Methods, Parameters and Variables renamer routine.
        ''' </summary>
        ''' <param name="type"></param>
        Public Sub ProcessMethods(type As TypeDef)
            For Each method As MethodDef In type.Methods
                If Cls_CecilHelper.IsRenamable(method) Then
                    Dim meth As MethodDef = method
                    If _RenamingAccept.Methods Then
                        meth = Cls_Renamer.RenameMethod(type, meth)
                    End If
                    If _RenamingAccept.Parameters Then
                        Cls_Renamer.RenameParameters(meth)
                    End If
                    If _RenamingAccept.Variables Then
                        Cls_Renamer.RenameVariables(meth)
                    End If
                Else
                    If _RenamingAccept.Parameters Then
                        Cls_Renamer.RenameParameters(method)
                    End If
                    If _RenamingAccept.Variables Then
                        Cls_Renamer.RenameVariables(method)
                    End If
                End If
            Next
        End Sub


        ''' <summary>
        ''' INFO : Properties, CustomAttributes (Only "AccessedThroughPropertyAttribute" attribute) renamer routine. 
        ''' </summary>
        ''' <param name="type"></param>
        Public Sub ProcessProperties(type As TypeDef)
            If _RenamingAccept.Properties OrElse _RenamingAccept.CustomAttributes Then
                For Each propDef As PropertyDef In type.Properties
                    If Cls_CecilHelper.IsRenamable(propDef) Then

                        Dim obfuscatedN As String = Cls_Randomizer.GenerateNew()
                        Dim originalN As String = propDef.Name

                        propDef.Name = Cls_Mapping.RenamePropertyMember(propDef, obfuscatedN)
                        Cls_Renamer.RenameInitializeComponentsValues(propDef.DeclaringType, obfuscatedN, originalN, True)

                        Cls_Renamer.RenameSettings(propDef.GetMethod, originalN, obfuscatedN)
                        Cls_Renamer.RenameSettings(propDef.SetMethod, originalN, obfuscatedN)

                        If _RenamingAccept.CustomAttributes Then
                            Cls_Renamer.RenameCustomAttributes(type, propDef, originalN, obfuscatedN)
                        End If
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' INFO : Fields renamer routine. 
        ''' </summary>
        ''' <param name="type"></param>
        Public Sub ProcessFields(type As TypeDef)
            If _RenamingAccept.Fields Then
                For Each field As FieldDef In type.Fields
                    If Cls_CecilHelper.IsRenamable(field) Then
                        Cls_Renamer.RenameField(field, Cls_Randomizer.GenerateNew())
                    End If
                Next
            End If

        End Sub

        ''' <summary>
        ''' INFO : Events renamer routine. 
        ''' </summary>
        ''' <param name="type"></param>
        Public Sub ProcessEvents(type As TypeDef)
            If _RenamingAccept.Events Then
                For Each events As EventDef In type.Events
                    If Cls_CecilHelper.IsRenamable(events) Then
                        If _RenamingAccept.CustomAttributes Then
                            Cls_Renamer.RenameCustomAttributesValues(events)
                        End If
                        If _RenamingAccept.Events Then
                            Cls_Renamer.RenameEvent(events, Cls_Randomizer.GenerateNew())
                        End If
                    End If
                Next
            End If
        End Sub

#End Region

    End Class
End Namespace