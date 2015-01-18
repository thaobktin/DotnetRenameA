Imports DotNetRenamer.WinConsole
Imports DotNetRenamer.Implementer.Context
Imports DotNetRenamer.Helper.RandomizeHelper

Namespace Settings

    Public Class Cls_Settings

        Private Shared iniPath$
        Public Shared modeName As List(Of String)
        Shared Sub New()
            iniPath = Application.StartupPath & "\DotNetRenamer.ini"
            modeName = New List(Of String)((New String() {"Full", "Medium", "Customize"}))
        End Sub

        Public Shared Function GetCustomValue(Key$) As Integer
            Dim m = Cls_IniFile.INIRead(Cls_Settings.iniPath, modeName(2), Key)
            If m = "" Then
                Cls_IniFile.INIWrite(iniPath, modeName(2), Key, Cls_IniFile.INIRead(Cls_Settings.iniPath, modeName(0), Key))
            End If
            Return CInt(Cls_IniFile.INIRead(Cls_Settings.iniPath, modeName(2), Key))
        End Function

        Public Shared Sub SetCustomValue(Key$, val%)
            Cls_IniFile.INIWrite(iniPath, modeName(2), Key, CStr(val))
        End Sub

        Public Shared Sub SetDefault()
            Dim fullMode = New Dictionary(Of String, String) From {{"ChbNamespacesRP", "1"}, _
                                       {"ChbTypesRP", "1"}, _
                                       {"ChbMethodsRP", "1"}, _
                                       {"ChbPropertiesRP", "1"}, _
                                       {"ChbEventsRP", "1"}, _
                                       {"ChbFieldsRP", "1"}, _
                                       {"ChbAttributesRP", "1"}, _
                                       {"ChbParametersRP", "1"}, _
                                       {"ChbVariablesRP", "1"}, _
                                       {"ChbRenameMainNamespaceOnlyNamespaces", "0"}, _
                                       {"ChbReplaceNamespaceByEmptyNamespaces", "1"}, _
                                       {"LanguageTypeInteger", "0"}, _
                                       {"ModeTypeInteger", "0"}}

            For Each kvp In fullMode
                Cls_IniFile.INIWrite(iniPath, modeName(0), kvp.Key, kvp.Value)
            Next

            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbNamespacesRP", "1")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbTypesRP", "1")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbMethodsRP", "1")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbPropertiesRP", "1")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbEventsRP", "0")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbFieldsRP", "0")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbAttributesRP", "1")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbParametersRP", "0")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbVariablesRP", "0")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbRenameMainNamespaceOnlyNamespaces", "0")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ChbReplaceNamespaceByEmptyNamespaces", "1")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "LanguageTypeInteger", "5")
            Cls_IniFile.INIWrite(iniPath, modeName(1), "ModeTypeInteger", "1")

            If Cls_IniFile.INISectionExist(iniPath, modeName(2)) = False Then
                For Each kvp In fullMode
                    Cls_IniFile.INIWrite(iniPath, modeName(2), kvp.Key, kvp.Value)
                Next
            End If

        End Sub

        Public Shared Function GetConfig(iMode%) As Cls_RenamerState
            Dim _paramArgs As New Cls_RenamerState() With { _
                                                    .Namespaces = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbNamespacesRP")), _
                                                    .Types = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbTypesRP")), _
                                                    .Methods = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbMethodsRP")), _
                                                    .Properties = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbPropertiesRP")), _
                                                    .Events = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbEventsRP")), _
                                                    .Fields = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbFieldsRP")), _
                                                    .CustomAttributes = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbAttributesRP")), _
                                                    .Parameters = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbParametersRP")), _
                                                    .Variables = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbVariablesRP")), _
                                                    .RenameMainNamespaceSetting = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbRenameMainNamespaceOnlyNamespaces")), _
                                                    .ReplaceNamespacesSetting = CBool(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ChbReplaceNamespaceByEmptyNamespaces")), _
                                                    .RenamingType = CType(CInt(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "LanguageTypeInteger")), Cls_RandomizerType.RenameEnum), _
                                                    .RenameRuleSetting = CType(CInt(Cls_IniFile.INIRead(iniPath, GetStrMode(iMode), "ModeTypeInteger")), Cls_RenamerState.RenameRule)}
            Return _paramArgs
        End Function

        Private Shared Function GetStrMode(iMode%) As String
            Return modeName.Item(iMode)
        End Function

        Public Shared Function GetIntMode(sMode$) As Integer
            Return modeName.IndexOf(sMode)
        End Function

    End Class
End Namespace
