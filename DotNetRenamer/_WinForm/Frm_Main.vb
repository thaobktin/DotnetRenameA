Imports DotNetRenamer.Implementer.Analyzer
Imports DotNetRenamer.Implementer.Context
Imports DotNetRenamer.XertzLoginTheme
Imports DotNetRenamer.Implementer.Task
Imports DotNetRenamer.Helper.RandomizeHelper
Imports System.IO
Imports System.Text
Imports DotNetRenamer.WinConsole
Imports DotNetRenamer.Settings
Imports DotNetRenamer.Implementer.Exclusion

Public Class Frm_Main

#Region " Fields "
    Private WithEvents _param As Cls_Parameters
    Private WithEvents _Task As Cls_Task
    Private WithEvents _exclude As Frm_Exclusion
    Private _controlList As List(Of Control)
    Private _taskIsRunning As Boolean
    Private _LanguageType%
    Private _result As Integer
#End Region

#Region " Delegates "
    Private Delegate Sub OnRenamedItemDelegate(ByVal e As Cls_RenamedItem)
#End Region

#Region " Constructor "
    Public Sub New()
        InitializeComponent()
        InitializeCustomize()
    End Sub

    Private Sub InitializeCustomize()
        Cls_Settings.SetDefault()
        CbxPresets.SelectedIndex = Cls_Settings.GetCustomValue("ModeTypeInteger")
    End Sub
#End Region

#Region " Select Assembly "

    Private Sub TxbSelectedFile_DragEnter(sender As Object, e As DragEventArgs) Handles TxbSelectedFile.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub TxbSelectedFile_DragDrop(sender As Object, e As DragEventArgs) Handles TxbSelectedFile.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim MyFiles() As String
            MyFiles = e.Data.GetData(DataFormats.FileDrop)
            TxbSelectedFile.Text = MyFiles(0)
            Me.ShowSelectedFileInfos(TxbSelectedFile.Text)
        End If
    End Sub

    Private Sub BtnSelectFile_Click(sender As Object, e As EventArgs) Handles BtnSelectFile.Click
        Using ofd As New OpenFileDialog
            With ofd
                .Title = "Select a DotNet program (VbNet, C#)"
                .Filter = "Exe|*.exe;*.exe"
                .CheckFileExists = True
                .Multiselect = False
                If .ShowDialog() = DialogResult.OK Then
                    Me.ShowSelectedFileInfos(.FileName)
                End If
            End With
        End Using
    End Sub

    Private Sub BtnSelectOutput_Click(sender As Object, e As EventArgs) Handles BtnSelectOutput.Click
        Using sfd As New SaveFileDialog
            With sfd
                .Title = "Select the Output Protected Name"
                .Filter = "Exe|*.exe;*.exe"
                .CheckFileExists = False
                .OverwritePrompt = False
                If .ShowDialog() = DialogResult.OK Then
                    If Not _param.inputFile.ToLower = .FileName.ToLower Then
                        TxbSelectedOutput.Text = .FileName
                        _param = New Cls_Parameters(_param.inputFile)
                    Else
                        MessageBox.Show("Input and Output file can't be the same !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
            End With
        End Using
    End Sub

    Private Sub TxbSelectedOutput_Validated(sender As Object, e As EventArgs) Handles TxbSelectedOutput.Validated
        If Not TxbSelectedFile.Text = String.Empty Then
            ShowSelectedFileInfos(TxbSelectedFile.Text)
        End If
    End Sub

    Private Sub ShowSelectedFileInfos(FilePath$)
        Try
            _param = New Cls_Parameters(FilePath)
            If _param.isValidFile Then
                If TxbSelectedOutput.Text = String.Empty Then
                    TxbSelectedOutput.Text = Path.GetDirectoryName(FilePath$) & "\" & Path.GetFileNameWithoutExtension(FilePath$) & "Protected.exe"
                End If

                _param.outputFile = TxbSelectedOutput.Text
                TxbAssemblyInfo.Text = _param.getAssemblyName
                TxbVersionInfo.Text = _param.getAssemblyVersion
                TxbType.Text = _param.getModuleKind
                TxbFrameworkInfo.Text = _param.getRuntime
                TxbCpuTargetInfo.Text = _param.getProcessArchitecture
                TxbSelectedFile.Text = FilePath
                PbxSelectedFile.Image = Icon.ExtractAssociatedIcon(FilePath).ToBitmap

                _exclude = New Frm_Exclusion(FilePath)
                _exclude.InitializeExcludeList()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub OnFileValidationTeminated(ByVal sender As Object, ByVal e As Cls_ValidatedFile) Handles _param.FileValidated
        If e.isValid Then
            BtnSelectOutput.Enabled = True
            TxbSelectedOutput.Enabled = True
            GbxAsemblyInfos.Enabled = True
            GbxPresets.Enabled = True
            BtnStart.Enabled = True
        Else
            BtnSelectOutput.Enabled = False
            TxbSelectedOutput.Enabled = False
            GbxAsemblyInfos.Enabled = False
            GbxPresets.Enabled = False
            EmptyTextBox()
        End If
    End Sub

    Private Sub EmptyTextBox()
        TxbAssemblyInfo.Text = String.Empty
        TxbVersionInfo.Text = String.Empty
        TxbType.Text = String.Empty
        TxbFrameworkInfo.Text = String.Empty
        TxbCpuTargetInfo.Text = String.Empty
        TxbSelectedFile.Text = String.Empty
        TxbSelectedOutput.Text = String.Empty
        PbxSelectedFile.Image = Nothing
    End Sub
#End Region

#Region " Select Presets "
    Private Sub ChbNamespacesRP_CheckedChanged(ByVal sender As Object, e As System.EventArgs) Handles ChbNamespacesRP.CheckedChanged
        BugFixEnabledChanged()
    End Sub

    Private Sub PnlNamespacesPresets_EnabledChanged(sender As Object, e As System.EventArgs) Handles PnlNamespacesPresets.EnabledChanged
        BugFixEnabledChanged()
    End Sub

    Private Sub BugFixEnabledChanged()
        If ChbNamespacesRP.Checked Then
            PnlNamespacesGroup.Enabled = True
        Else
            PnlNamespacesGroup.Enabled = False
        End If
    End Sub

    Private _rdbClick As Boolean

    Private Sub RdbAlphabetic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RdbJapaneseCharacters.Click, RdbInvisibleCharacters.Click, RdbDotsCharacters.Click, RdbChineseCharacters.Click, RdbAlphabeticCharacters.Click, RdbGreekCharacters.Click
        Dim rdb = TryCast(sender, LogInRadioButton)
        Cls_Settings.SetCustomValue("LanguageTypeInteger", rdb.TabIndex)
    End Sub

    Private Sub RdbAlphabetic_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RdbJapaneseCharacters.CheckedChanged, RdbInvisibleCharacters.CheckedChanged, RdbDotsCharacters.CheckedChanged, RdbChineseCharacters.CheckedChanged, RdbAlphabeticCharacters.CheckedChanged, RdbGreekCharacters.CheckedChanged
        Dim rdb = TryCast(sender, LogInRadioButton)
        If rdb.Checked Then _LanguageType = rdb.TabIndex
    End Sub

    Private Sub CbxPresets_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CbxPresets.SelectedIndexChanged
        If Not _controlList Is Nothing Then _controlList.Clear()
        _controlList = New List(Of Control) From { _
                                            {RdbAlphabeticCharacters}, {RdbDotsCharacters}, {RdbInvisibleCharacters}, _
                                            {RdbChineseCharacters}, {RdbJapaneseCharacters}, {RdbGreekCharacters}, {ChbRenameMainNamespaceOnlyNamespaces}, _
                                            {ChbReplaceNamespaceByEmptyNamespaces}, {ChbNamespacesRP}, {ChbTypesRP}, {ChbMethodsRP}, _
                                            {ChbPropertiesRP}, {ChbEventsRP}, {ChbFieldsRP}, {ChbAttributesRP}, {ChbParametersRP}}

        Dim State As Cls_RenamerState = Cls_Settings.GetConfig(CbxPresets.SelectedIndex)

        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("Characters") AndAlso x.Tag = State.RenamingType).First, LogInRadioButton).Checked = True
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("Namespaces") AndAlso x.Tag = 0).First, LogInCheckBox).Checked = CBool(State.RenameMainNamespaceSetting)
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("Namespaces") AndAlso x.Tag = 1).First, LogInCheckBox).Checked = CBool(State.ReplaceNamespacesSetting)
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("RP") AndAlso x.Tag.ToString = "Namespaces").First, LogInCheckBox).Checked = CBool(State.Namespaces)
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("RP") AndAlso x.Tag.ToString = "Types").First, LogInCheckBox).Checked = CBool(State.Types)
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("RP") AndAlso x.Tag.ToString = "Methods").First, LogInCheckBox).Checked = CBool(State.Methods)
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("RP") AndAlso x.Tag.ToString = "Properties").First, LogInCheckBox).Checked = CBool(State.Properties)
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("RP") AndAlso x.Tag.ToString = "Attributes").First, LogInCheckBox).Checked = CBool(State.CustomAttributes)
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("RP") AndAlso x.Tag.ToString = "Fields").First, LogInCheckBox).Checked = CBool(State.Fields)
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("RP") AndAlso x.Tag.ToString = "Events").First, LogInCheckBox).Checked = CBool(State.Events)
        TryCast(_controlList.Where(Function(x) x.Name.EndsWith("RP") AndAlso x.Tag.ToString = "Parameters").First, LogInCheckBox).Checked = CBool(State.Parameters)

        If CbxPresets.SelectedIndex = 0 OrElse CbxPresets.SelectedIndex = 1 Then
            EnabledPresets(False)
        Else
            EnabledPresets(True)
        End If

        Cls_Settings.SetCustomValue("ModeTypeInteger", CbxPresets.SelectedIndex)
    End Sub

    Private Sub EnabledPresets(ByVal state As Boolean)
        PnlCharactersPresets.Enabled = state
        PnlNamespacesPresets.Enabled = state
        PnlTypesPresets.Enabled = state
        PnlMethodsPresets.Enabled = state
        PnlPropertiesPresets.Enabled = state
        PnlEventsPresets.Enabled = state
        PnlFieldsPresets.Enabled = state
        PnlAttributesPresets.Enabled = state
        PnlParametersPresets.Enabled = state
    End Sub

    Private Sub ChbTypesRP_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChbNamespacesRP.CheckedChanged, _
                                                                ChbTypesRP.CheckedChanged, _
                                                                ChbMethodsRP.CheckedChanged, _
                                                                ChbPropertiesRP.CheckedChanged, _
                                                                ChbEventsRP.CheckedChanged, _
                                                                ChbFieldsRP.CheckedChanged, _
                                                                ChbAttributesRP.CheckedChanged, _
                                                                ChbParametersRP.CheckedChanged
        If CbxPresets.SelectedIndex = 2 Then
            Dim chb As LogInCheckBox = TryCast(sender, LogInCheckBox)
            Cls_Settings.SetCustomValue(chb.Name, CInt(chb.Checked))
        End If
    End Sub

    Private Sub ChbRenameMainNamespaceOnlyNamespaces_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChbRenameMainNamespaceOnlyNamespaces.CheckedChanged
        If CbxPresets.SelectedIndex = 2 Then Cls_Settings.SetCustomValue(ChbRenameMainNamespaceOnlyNamespaces.Name, CInt(ChbRenameMainNamespaceOnlyNamespaces.Checked))
    End Sub

    Private Sub ChbReplaceNamespaceByEmptyNamespaces_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChbReplaceNamespaceByEmptyNamespaces.CheckedChanged
        If CbxPresets.SelectedIndex = 2 Then Cls_Settings.SetCustomValue(ChbReplaceNamespaceByEmptyNamespaces.Name, CInt(ChbReplaceNamespaceByEmptyNamespaces.Checked))
    End Sub
#End Region

#Region " Exclusion rules "
    Private Sub BtnExclude_Click(sender As Object, e As EventArgs) Handles BtnExclude.Click
        If Not _exclude Is Nothing Then
            _exclude.ShowDialog()
        End If
    End Sub

    Private Sub Frm_Exclusion_OnShowingExclusionInfos(e As Cls_ExcludeList) Handles _exclude.OnShowingExclusionInfos
        If e.itemsCount <> 0 Then
            BtnExclude.BorderColour = Color.DarkViolet
            BtnExclude.Text = "Exclusion rules (" & e.itemsCount & ")"
        Else
            BtnExclude.BorderColour = Color.DimGray
            BtnExclude.Text = "Exclusion rules (0)"
        End If
        BtnExclude.Invalidate()
        _param.ExcludeList = e
    End Sub
#End Region

#Region " Rename Task "
    Private Sub BtnStart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnStart.Click
        If BgwRenameTask.IsBusy = False Then
            LsbMain.ShowLine = True
            BtnStart.Enabled = False
            _taskIsRunning = True
            EnabledControls(False)
            BgwRenameTask.RunWorkerAsync(CbxPresets.SelectedIndex)
        End If
    End Sub

    Private Sub EnabledControls(ByVal state As Boolean)
        GbxAsemblyInfos.Enabled = state
        GbxSelectFile.Enabled = state
        GbxAsemblyInfos.Enabled = state
        GbxPresets.Enabled = state
    End Sub

    Private Sub BgwRenameTask_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BgwRenameTask.DoWork

        Try
            Dim _paramArgs As New Cls_RenamerState() With { _
                                                    .Namespaces = ChbNamespacesRP.Checked, _
                                                    .Types = ChbTypesRP.Checked, _
                                                    .Methods = ChbMethodsRP.Checked, _
                                                    .Properties = ChbPropertiesRP.Checked, _
                                                    .CustomAttributes = ChbAttributesRP.Checked, _
                                                    .Events = ChbEventsRP.Checked, _
                                                    .Fields = ChbFieldsRP.Checked, _
                                                    .Parameters = ChbParametersRP.Checked, _
                                                    .Variables = ChbParametersRP.Checked, _
                                                    .RenameMainNamespaceSetting = ChbRenameMainNamespaceOnlyNamespaces.Checked, _
                                                    .ReplaceNamespacesSetting = ChbReplaceNamespaceByEmptyNamespaces.Checked, _
                                                    .RenamingType = CType(_LanguageType, Cls_RandomizerType.RenameEnum), _
                                                    .RenameRuleSetting = CType(CInt(e.Argument), Cls_RenamerState.RenameRule)}

            _Task = New Cls_Task(_param, _paramArgs)
            _Task.StartTask()

            e.Result = New Object() {"Success", _Task.Result, _paramArgs}

        Catch ex As Exception
            e.Result = New Object() {"Error", ex.ToString}
        End Try

        '############################################################################################################################# 
    End Sub

    Private Sub OnRenamedItem(ByVal sender As Object, ByVal e As Cls_RenamedItemEventArgs) Handles _Task.RenamedItem
        Me.Invoke(New OnRenamedItemDelegate(AddressOf Me.MessageShowing), New Object() {e.item})
    End Sub

    Private Sub MessageShowing(ByVal e As Cls_RenamedItem)
        'MsgBox(e.ItemType & " : From --> " & e.ItemName & " : To --> " & e.obfuscatedItemName)
    End Sub

    Private Sub BgwRenameTask_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BgwRenameTask.RunWorkerCompleted
        Try
            Select Case e.Result(0)
                Case "Error"
                    MessageBox.Show(e.Result(1).ToString, e.Result(0), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Select
                Case "Success"
                    MessageBox.Show("Renamed :" & vbNewLine & vbNewLine & _
                                              "Namespace(s) : " & CType(e.Result(1), Cls_TaskResult).Item("Namespace").ToString & vbNewLine & _
                                              "Type(s) : " & CType(e.Result(1), Cls_TaskResult).Item("Type").ToString & vbNewLine & _
                                              "Method(s) : " & CType(e.Result(1), Cls_TaskResult).Item("Method").ToString & vbNewLine & _
                                              "Parameter(s) : " & CType(e.Result(1), Cls_TaskResult).Item("Parameter").ToString & vbNewLine & _
                                              "Generic Parameter(s) : " & CType(e.Result(1), Cls_TaskResult).Item("Generic Parameter").ToString & vbNewLine & _
                                              "Variable(s) : " & CType(e.Result(1), Cls_TaskResult).Item("Variable").ToString & vbNewLine & _
                                              "Property(s) : " & CType(e.Result(1), Cls_TaskResult).Item("Property").ToString & vbNewLine & _
                                              "Event(s) : " & CType(e.Result(1), Cls_TaskResult).Item("Event").ToString & vbNewLine & _
                                              "Field(s) : " & CType(e.Result(1), Cls_TaskResult).Item("Field").ToString _
                    , "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Select
            End Select

            _taskIsRunning = False
            EmptyTextBox()
            GbxSelectFile.Enabled = True
            GbxAsemblyInfos.Enabled = True
            LsbMain.ShowLine = False
            _exclude.FinalizeExcludeList()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

#End Region

#Region " Other "
    Private Sub LnkLblBlogSpot_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles LnkLblBlogSpot.LinkClicked
        Process.Start(LnkLblBlogSpot.Text)
    End Sub
#End Region

 
End Class