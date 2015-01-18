Imports DotNetRenamer.XertzLoginTheme

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Main
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BgwRenameTask = New System.ComponentModel.BackgroundWorker()
        Me.LogInThemeContainer1 = New DotNetRenamer.XertzLoginTheme.LogInThemeContainer()
        Me.LnkLblBlogSpot = New System.Windows.Forms.LinkLabel()
        Me.LsbMain = New DotNetRenamer.XertzLoginTheme.LogInStatusBar()
        Me.GbxPresets = New DotNetRenamer.XertzLoginTheme.LogInGroupBox()
        Me.LblPresets = New DotNetRenamer.XertzLoginTheme.LogInLabel()
        Me.BtnStart = New DotNetRenamer.XertzLoginTheme.LogInButton()
        Me.TbcPresets = New DotNetRenamer.XertzLoginTheme.LogInTabControl()
        Me.TpCharacters = New System.Windows.Forms.TabPage()
        Me.PnlCharactersPresets = New System.Windows.Forms.Panel()
        Me.RdbGreekCharacters = New DotNetRenamer.XertzLoginTheme.LogInRadioButton()
        Me.RdbAlphabeticCharacters = New DotNetRenamer.XertzLoginTheme.LogInRadioButton()
        Me.RdbJapaneseCharacters = New DotNetRenamer.XertzLoginTheme.LogInRadioButton()
        Me.RdbDotsCharacters = New DotNetRenamer.XertzLoginTheme.LogInRadioButton()
        Me.RdbChineseCharacters = New DotNetRenamer.XertzLoginTheme.LogInRadioButton()
        Me.RdbInvisibleCharacters = New DotNetRenamer.XertzLoginTheme.LogInRadioButton()
        Me.TpNamespaces = New System.Windows.Forms.TabPage()
        Me.PnlNamespacesPresets = New System.Windows.Forms.Panel()
        Me.ChbNamespacesRP = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.PnlNamespacesGroup = New System.Windows.Forms.Panel()
        Me.ChbReplaceNamespaceByEmptyNamespaces = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.ChbRenameMainNamespaceOnlyNamespaces = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.TpTypes = New System.Windows.Forms.TabPage()
        Me.PnlTypesPresets = New System.Windows.Forms.Panel()
        Me.ChbTypesRP = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.TpMethods = New System.Windows.Forms.TabPage()
        Me.PnlMethodsPresets = New System.Windows.Forms.Panel()
        Me.ChbMethodsRP = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.TpProperties = New System.Windows.Forms.TabPage()
        Me.PnlPropertiesPresets = New System.Windows.Forms.Panel()
        Me.ChbPropertiesRP = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.TpEvents = New System.Windows.Forms.TabPage()
        Me.PnlEventsPresets = New System.Windows.Forms.Panel()
        Me.ChbEventsRP = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.TpFields = New System.Windows.Forms.TabPage()
        Me.PnlFieldsPresets = New System.Windows.Forms.Panel()
        Me.ChbFieldsRP = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.TpAttributes = New System.Windows.Forms.TabPage()
        Me.PnlAttributesPresets = New System.Windows.Forms.Panel()
        Me.ChbAttributesRP = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.TpParameters = New System.Windows.Forms.TabPage()
        Me.PnlParametersPresets = New System.Windows.Forms.Panel()
        Me.ChbParametersRP = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.CbxPresets = New DotNetRenamer.XertzLoginTheme.LogInComboBox()
        Me.GbxAsemblyInfos = New DotNetRenamer.XertzLoginTheme.LogInGroupBox()
        Me.LblType = New DotNetRenamer.XertzLoginTheme.LogInLabel()
        Me.TxbType = New System.Windows.Forms.TextBox()
        Me.LblCpuTargetInfo = New DotNetRenamer.XertzLoginTheme.LogInLabel()
        Me.TxbCpuTargetInfo = New System.Windows.Forms.TextBox()
        Me.TxbFrameworkInfo = New System.Windows.Forms.TextBox()
        Me.LblFrameworkInfo = New DotNetRenamer.XertzLoginTheme.LogInLabel()
        Me.TxbVersionInfo = New System.Windows.Forms.TextBox()
        Me.TxbAssemblyInfo = New System.Windows.Forms.TextBox()
        Me.LblVersionInfo = New DotNetRenamer.XertzLoginTheme.LogInLabel()
        Me.LblAssemblyInfo = New DotNetRenamer.XertzLoginTheme.LogInLabel()
        Me.GbxSelectFile = New DotNetRenamer.XertzLoginTheme.LogInGroupBox()
        Me.TxbSelectedOutput = New System.Windows.Forms.TextBox()
        Me.BtnSelectOutput = New DotNetRenamer.XertzLoginTheme.LogInButton()
        Me.TxbSelectedFile = New System.Windows.Forms.TextBox()
        Me.PbxSelectedFile = New System.Windows.Forms.PictureBox()
        Me.BtnSelectFile = New DotNetRenamer.XertzLoginTheme.LogInButton()
        Me.LogInThemeContainer1.SuspendLayout()
        Me.GbxPresets.SuspendLayout()
        Me.TbcPresets.SuspendLayout()
        Me.TpCharacters.SuspendLayout()
        Me.PnlCharactersPresets.SuspendLayout()
        Me.TpNamespaces.SuspendLayout()
        Me.PnlNamespacesPresets.SuspendLayout()
        Me.PnlNamespacesGroup.SuspendLayout()
        Me.TpTypes.SuspendLayout()
        Me.PnlTypesPresets.SuspendLayout()
        Me.TpMethods.SuspendLayout()
        Me.PnlMethodsPresets.SuspendLayout()
        Me.TpProperties.SuspendLayout()
        Me.PnlPropertiesPresets.SuspendLayout()
        Me.TpEvents.SuspendLayout()
        Me.PnlEventsPresets.SuspendLayout()
        Me.TpFields.SuspendLayout()
        Me.PnlFieldsPresets.SuspendLayout()
        Me.TpAttributes.SuspendLayout()
        Me.PnlAttributesPresets.SuspendLayout()
        Me.TpParameters.SuspendLayout()
        Me.PnlParametersPresets.SuspendLayout()
        Me.GbxAsemblyInfos.SuspendLayout()
        Me.GbxSelectFile.SuspendLayout()
        CType(Me.PbxSelectedFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BgwRenameTask
        '
        Me.BgwRenameTask.WorkerReportsProgress = True
        Me.BgwRenameTask.WorkerSupportsCancellation = True
        '
        'LogInThemeContainer1
        '
        Me.LogInThemeContainer1.AllowClose = True
        Me.LogInThemeContainer1.AllowMaximize = False
        Me.LogInThemeContainer1.AllowMinimize = True
        Me.LogInThemeContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.LogInThemeContainer1.BaseColour = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.LogInThemeContainer1.BorderColour = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.LogInThemeContainer1.ContainerColour = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.LogInThemeContainer1.Controls.Add(Me.LnkLblBlogSpot)
        Me.LogInThemeContainer1.Controls.Add(Me.LsbMain)
        Me.LogInThemeContainer1.Controls.Add(Me.GbxPresets)
        Me.LogInThemeContainer1.Controls.Add(Me.GbxAsemblyInfos)
        Me.LogInThemeContainer1.Controls.Add(Me.GbxSelectFile)
        Me.LogInThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogInThemeContainer1.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogInThemeContainer1.FontSize = 12
        Me.LogInThemeContainer1.HoverColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.LogInThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.LogInThemeContainer1.Name = "LogInThemeContainer1"
        Me.LogInThemeContainer1.ShowIcon = True
        Me.LogInThemeContainer1.Size = New System.Drawing.Size(685, 691)
        Me.LogInThemeContainer1.TabIndex = 0
        Me.LogInThemeContainer1.Text = "                                                        DotNet Renamer"
        '
        'LnkLblBlogSpot
        '
        Me.LnkLblBlogSpot.ActiveLinkColor = System.Drawing.Color.White
        Me.LnkLblBlogSpot.AutoSize = True
        Me.LnkLblBlogSpot.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.LnkLblBlogSpot.LinkColor = System.Drawing.Color.White
        Me.LnkLblBlogSpot.Location = New System.Drawing.Point(263, 672)
        Me.LnkLblBlogSpot.Name = "LnkLblBlogSpot"
        Me.LnkLblBlogSpot.Size = New System.Drawing.Size(161, 13)
        Me.LnkLblBlogSpot.TabIndex = 6
        Me.LnkLblBlogSpot.TabStop = True
        Me.LnkLblBlogSpot.Text = "http://3dotdevcoder.blogspot.fr/"
        Me.LnkLblBlogSpot.VisitedLinkColor = System.Drawing.Color.BlueViolet
        '
        'LsbMain
        '
        Me.LsbMain.Alignment = DotNetRenamer.XertzLoginTheme.LogInStatusBar.Alignments.Center
        Me.LsbMain.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.LsbMain.BorderColour = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.LsbMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LsbMain.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LsbMain.ForeColor = System.Drawing.Color.White
        Me.LsbMain.LinesToShow = DotNetRenamer.XertzLoginTheme.LogInStatusBar.LinesCount.Two
        Me.LsbMain.Location = New System.Drawing.Point(0, 668)
        Me.LsbMain.Name = "LsbMain"
        Me.LsbMain.RectangleColor = System.Drawing.Color.BlueViolet
        Me.LsbMain.ShowBorder = True
        Me.LsbMain.ShowLine = False
        Me.LsbMain.Size = New System.Drawing.Size(685, 23)
        Me.LsbMain.TabIndex = 5
        Me.LsbMain.TextColour = System.Drawing.Color.White
        '
        'GbxPresets
        '
        Me.GbxPresets.BorderColour = System.Drawing.SystemColors.ButtonShadow
        Me.GbxPresets.Controls.Add(Me.LblPresets)
        Me.GbxPresets.Controls.Add(Me.BtnStart)
        Me.GbxPresets.Controls.Add(Me.TbcPresets)
        Me.GbxPresets.Controls.Add(Me.CbxPresets)
        Me.GbxPresets.Enabled = False
        Me.GbxPresets.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GbxPresets.HeaderColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.GbxPresets.Location = New System.Drawing.Point(12, 300)
        Me.GbxPresets.MainColour = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.GbxPresets.Name = "GbxPresets"
        Me.GbxPresets.Size = New System.Drawing.Size(661, 356)
        Me.GbxPresets.TabIndex = 4
        Me.GbxPresets.Text = "Presets"
        Me.GbxPresets.TextColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        'LblPresets
        '
        Me.LblPresets.AutoSize = True
        Me.LblPresets.BackColor = System.Drawing.Color.Transparent
        Me.LblPresets.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblPresets.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblPresets.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblPresets.Location = New System.Drawing.Point(421, 49)
        Me.LblPresets.Name = "LblPresets"
        Me.LblPresets.Size = New System.Drawing.Size(214, 15)
        Me.LblPresets.TabIndex = 8
        Me.LblPresets.Text = "You can't change presets in this mode !"
        '
        'BtnStart
        '
        Me.BtnStart.BackColor = System.Drawing.Color.Transparent
        Me.BtnStart.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.BtnStart.BorderColour = System.Drawing.Color.DimGray
        Me.BtnStart.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnStart.HoverColour = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(52, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.BtnStart.Location = New System.Drawing.Point(7, 285)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.PressedColour = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.BtnStart.ProgressColour = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnStart.Size = New System.Drawing.Size(647, 62)
        Me.BtnStart.TabIndex = 7
        Me.BtnStart.Text = "Start"
        '
        'TbcPresets
        '
        Me.TbcPresets.ActiveColour = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.TbcPresets.BackTabColour = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TbcPresets.BaseColour = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TbcPresets.BorderColour = System.Drawing.SystemColors.ButtonShadow
        Me.TbcPresets.Controls.Add(Me.TpCharacters)
        Me.TbcPresets.Controls.Add(Me.TpNamespaces)
        Me.TbcPresets.Controls.Add(Me.TpTypes)
        Me.TbcPresets.Controls.Add(Me.TpMethods)
        Me.TbcPresets.Controls.Add(Me.TpProperties)
        Me.TbcPresets.Controls.Add(Me.TpEvents)
        Me.TbcPresets.Controls.Add(Me.TpFields)
        Me.TbcPresets.Controls.Add(Me.TpAttributes)
        Me.TbcPresets.Controls.Add(Me.TpParameters)
        Me.TbcPresets.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.TbcPresets.HorizontalLineColour = System.Drawing.Color.BlueViolet
        Me.TbcPresets.ItemSize = New System.Drawing.Size(240, 32)
        Me.TbcPresets.Location = New System.Drawing.Point(3, 87)
        Me.TbcPresets.Name = "TbcPresets"
        Me.TbcPresets.SelectedIndex = 0
        Me.TbcPresets.Size = New System.Drawing.Size(655, 189)
        Me.TbcPresets.TabIndex = 6
        Me.TbcPresets.TextColour = System.Drawing.Color.White
        Me.TbcPresets.UpLineColour = System.Drawing.Color.BlueViolet
        '
        'TpCharacters
        '
        Me.TpCharacters.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TpCharacters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TpCharacters.Controls.Add(Me.PnlCharactersPresets)
        Me.TpCharacters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TpCharacters.Location = New System.Drawing.Point(4, 36)
        Me.TpCharacters.Name = "TpCharacters"
        Me.TpCharacters.Padding = New System.Windows.Forms.Padding(3)
        Me.TpCharacters.Size = New System.Drawing.Size(647, 149)
        Me.TpCharacters.TabIndex = 7
        Me.TpCharacters.Text = "Characters"
        '
        'PnlCharactersPresets
        '
        Me.PnlCharactersPresets.Controls.Add(Me.RdbGreekCharacters)
        Me.PnlCharactersPresets.Controls.Add(Me.RdbAlphabeticCharacters)
        Me.PnlCharactersPresets.Controls.Add(Me.RdbJapaneseCharacters)
        Me.PnlCharactersPresets.Controls.Add(Me.RdbDotsCharacters)
        Me.PnlCharactersPresets.Controls.Add(Me.RdbChineseCharacters)
        Me.PnlCharactersPresets.Controls.Add(Me.RdbInvisibleCharacters)
        Me.PnlCharactersPresets.Enabled = False
        Me.PnlCharactersPresets.Location = New System.Drawing.Point(6, 6)
        Me.PnlCharactersPresets.Name = "PnlCharactersPresets"
        Me.PnlCharactersPresets.Size = New System.Drawing.Size(633, 135)
        Me.PnlCharactersPresets.TabIndex = 5
        '
        'RdbGreekCharacters
        '
        Me.RdbGreekCharacters.BaseColour = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.RdbGreekCharacters.BorderColour = System.Drawing.Color.DimGray
        Me.RdbGreekCharacters.Checked = False
        Me.RdbGreekCharacters.CheckedColour = System.Drawing.Color.BlueViolet
        Me.RdbGreekCharacters.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.RdbGreekCharacters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdbGreekCharacters.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RdbGreekCharacters.HighlightColour = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.RdbGreekCharacters.Location = New System.Drawing.Point(373, 80)
        Me.RdbGreekCharacters.Name = "RdbGreekCharacters"
        Me.RdbGreekCharacters.Size = New System.Drawing.Size(98, 18)
        Me.RdbGreekCharacters.TabIndex = 5
        Me.RdbGreekCharacters.Tag = "5"
        Me.RdbGreekCharacters.Text = "Greek"
        '
        'RdbAlphabeticCharacters
        '
        Me.RdbAlphabeticCharacters.BaseColour = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.RdbAlphabeticCharacters.BorderColour = System.Drawing.Color.DimGray
        Me.RdbAlphabeticCharacters.Checked = False
        Me.RdbAlphabeticCharacters.CheckedColour = System.Drawing.Color.BlueViolet
        Me.RdbAlphabeticCharacters.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.RdbAlphabeticCharacters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdbAlphabeticCharacters.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RdbAlphabeticCharacters.HighlightColour = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.RdbAlphabeticCharacters.Location = New System.Drawing.Point(177, 32)
        Me.RdbAlphabeticCharacters.Name = "RdbAlphabeticCharacters"
        Me.RdbAlphabeticCharacters.Size = New System.Drawing.Size(98, 18)
        Me.RdbAlphabeticCharacters.TabIndex = 0
        Me.RdbAlphabeticCharacters.Tag = "0"
        Me.RdbAlphabeticCharacters.Text = "Alphabetic"
        '
        'RdbJapaneseCharacters
        '
        Me.RdbJapaneseCharacters.BaseColour = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.RdbJapaneseCharacters.BorderColour = System.Drawing.Color.DimGray
        Me.RdbJapaneseCharacters.Checked = False
        Me.RdbJapaneseCharacters.CheckedColour = System.Drawing.Color.BlueViolet
        Me.RdbJapaneseCharacters.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.RdbJapaneseCharacters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdbJapaneseCharacters.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RdbJapaneseCharacters.HighlightColour = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.RdbJapaneseCharacters.Location = New System.Drawing.Point(373, 56)
        Me.RdbJapaneseCharacters.Name = "RdbJapaneseCharacters"
        Me.RdbJapaneseCharacters.Size = New System.Drawing.Size(98, 18)
        Me.RdbJapaneseCharacters.TabIndex = 4
        Me.RdbJapaneseCharacters.Tag = "4"
        Me.RdbJapaneseCharacters.Text = "Japanese"
        '
        'RdbDotsCharacters
        '
        Me.RdbDotsCharacters.BaseColour = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.RdbDotsCharacters.BorderColour = System.Drawing.Color.DimGray
        Me.RdbDotsCharacters.Checked = False
        Me.RdbDotsCharacters.CheckedColour = System.Drawing.Color.BlueViolet
        Me.RdbDotsCharacters.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.RdbDotsCharacters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdbDotsCharacters.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RdbDotsCharacters.HighlightColour = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.RdbDotsCharacters.Location = New System.Drawing.Point(177, 56)
        Me.RdbDotsCharacters.Name = "RdbDotsCharacters"
        Me.RdbDotsCharacters.Size = New System.Drawing.Size(99, 18)
        Me.RdbDotsCharacters.TabIndex = 1
        Me.RdbDotsCharacters.Tag = "1"
        Me.RdbDotsCharacters.Text = "Dots"
        '
        'RdbChineseCharacters
        '
        Me.RdbChineseCharacters.BaseColour = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.RdbChineseCharacters.BorderColour = System.Drawing.Color.DimGray
        Me.RdbChineseCharacters.Checked = False
        Me.RdbChineseCharacters.CheckedColour = System.Drawing.Color.BlueViolet
        Me.RdbChineseCharacters.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.RdbChineseCharacters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdbChineseCharacters.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RdbChineseCharacters.HighlightColour = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.RdbChineseCharacters.Location = New System.Drawing.Point(373, 32)
        Me.RdbChineseCharacters.Name = "RdbChineseCharacters"
        Me.RdbChineseCharacters.Size = New System.Drawing.Size(98, 18)
        Me.RdbChineseCharacters.TabIndex = 3
        Me.RdbChineseCharacters.Tag = "3"
        Me.RdbChineseCharacters.Text = "Chinese"
        '
        'RdbInvisibleCharacters
        '
        Me.RdbInvisibleCharacters.BaseColour = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.RdbInvisibleCharacters.BorderColour = System.Drawing.Color.DimGray
        Me.RdbInvisibleCharacters.Checked = False
        Me.RdbInvisibleCharacters.CheckedColour = System.Drawing.Color.BlueViolet
        Me.RdbInvisibleCharacters.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.RdbInvisibleCharacters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdbInvisibleCharacters.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RdbInvisibleCharacters.HighlightColour = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.RdbInvisibleCharacters.Location = New System.Drawing.Point(177, 80)
        Me.RdbInvisibleCharacters.Name = "RdbInvisibleCharacters"
        Me.RdbInvisibleCharacters.Size = New System.Drawing.Size(98, 18)
        Me.RdbInvisibleCharacters.TabIndex = 2
        Me.RdbInvisibleCharacters.Tag = "2"
        Me.RdbInvisibleCharacters.Text = "Invisible"
        '
        'TpNamespaces
        '
        Me.TpNamespaces.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TpNamespaces.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TpNamespaces.Controls.Add(Me.PnlNamespacesPresets)
        Me.TpNamespaces.Location = New System.Drawing.Point(4, 36)
        Me.TpNamespaces.Name = "TpNamespaces"
        Me.TpNamespaces.Padding = New System.Windows.Forms.Padding(3)
        Me.TpNamespaces.Size = New System.Drawing.Size(647, 149)
        Me.TpNamespaces.TabIndex = 0
        Me.TpNamespaces.Text = "Namespaces"
        '
        'PnlNamespacesPresets
        '
        Me.PnlNamespacesPresets.Controls.Add(Me.ChbNamespacesRP)
        Me.PnlNamespacesPresets.Controls.Add(Me.PnlNamespacesGroup)
        Me.PnlNamespacesPresets.Location = New System.Drawing.Point(6, 6)
        Me.PnlNamespacesPresets.Name = "PnlNamespacesPresets"
        Me.PnlNamespacesPresets.Size = New System.Drawing.Size(633, 135)
        Me.PnlNamespacesPresets.TabIndex = 2
        '
        'ChbNamespacesRP
        '
        Me.ChbNamespacesRP.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbNamespacesRP.BorderColour = System.Drawing.Color.DimGray
        Me.ChbNamespacesRP.Checked = True
        Me.ChbNamespacesRP.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbNamespacesRP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbNamespacesRP.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbNamespacesRP.Location = New System.Drawing.Point(0, 0)
        Me.ChbNamespacesRP.Name = "ChbNamespacesRP"
        Me.ChbNamespacesRP.Size = New System.Drawing.Size(83, 22)
        Me.ChbNamespacesRP.TabIndex = 4
        Me.ChbNamespacesRP.Tag = "Namespaces"
        Me.ChbNamespacesRP.Text = "Rename"
        '
        'PnlNamespacesGroup
        '
        Me.PnlNamespacesGroup.Controls.Add(Me.ChbReplaceNamespaceByEmptyNamespaces)
        Me.PnlNamespacesGroup.Controls.Add(Me.ChbRenameMainNamespaceOnlyNamespaces)
        Me.PnlNamespacesGroup.Location = New System.Drawing.Point(0, 29)
        Me.PnlNamespacesGroup.Name = "PnlNamespacesGroup"
        Me.PnlNamespacesGroup.Size = New System.Drawing.Size(633, 60)
        Me.PnlNamespacesGroup.TabIndex = 2
        '
        'ChbReplaceNamespaceByEmptyNamespaces
        '
        Me.ChbReplaceNamespaceByEmptyNamespaces.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbReplaceNamespaceByEmptyNamespaces.BorderColour = System.Drawing.Color.DimGray
        Me.ChbReplaceNamespaceByEmptyNamespaces.Checked = True
        Me.ChbReplaceNamespaceByEmptyNamespaces.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbReplaceNamespaceByEmptyNamespaces.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbReplaceNamespaceByEmptyNamespaces.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbReplaceNamespaceByEmptyNamespaces.Location = New System.Drawing.Point(336, 18)
        Me.ChbReplaceNamespaceByEmptyNamespaces.Name = "ChbReplaceNamespaceByEmptyNamespaces"
        Me.ChbReplaceNamespaceByEmptyNamespaces.Size = New System.Drawing.Size(258, 22)
        Me.ChbReplaceNamespaceByEmptyNamespaces.TabIndex = 3
        Me.ChbReplaceNamespaceByEmptyNamespaces.Tag = "1"
        Me.ChbReplaceNamespaceByEmptyNamespaces.Text = "Replace namespace(s) by empty value"
        '
        'ChbRenameMainNamespaceOnlyNamespaces
        '
        Me.ChbRenameMainNamespaceOnlyNamespaces.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbRenameMainNamespaceOnlyNamespaces.BorderColour = System.Drawing.Color.DimGray
        Me.ChbRenameMainNamespaceOnlyNamespaces.Checked = False
        Me.ChbRenameMainNamespaceOnlyNamespaces.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbRenameMainNamespaceOnlyNamespaces.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbRenameMainNamespaceOnlyNamespaces.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbRenameMainNamespaceOnlyNamespaces.Location = New System.Drawing.Point(58, 18)
        Me.ChbRenameMainNamespaceOnlyNamespaces.Name = "ChbRenameMainNamespaceOnlyNamespaces"
        Me.ChbRenameMainNamespaceOnlyNamespaces.Size = New System.Drawing.Size(246, 22)
        Me.ChbRenameMainNamespaceOnlyNamespaces.TabIndex = 2
        Me.ChbRenameMainNamespaceOnlyNamespaces.Tag = "0"
        Me.ChbRenameMainNamespaceOnlyNamespaces.Text = "Rename the main namespace only "
        '
        'TpTypes
        '
        Me.TpTypes.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TpTypes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TpTypes.Controls.Add(Me.PnlTypesPresets)
        Me.TpTypes.Location = New System.Drawing.Point(4, 36)
        Me.TpTypes.Name = "TpTypes"
        Me.TpTypes.Padding = New System.Windows.Forms.Padding(3)
        Me.TpTypes.Size = New System.Drawing.Size(647, 149)
        Me.TpTypes.TabIndex = 1
        Me.TpTypes.Text = "Types"
        '
        'PnlTypesPresets
        '
        Me.PnlTypesPresets.Controls.Add(Me.ChbTypesRP)
        Me.PnlTypesPresets.Location = New System.Drawing.Point(6, 6)
        Me.PnlTypesPresets.Name = "PnlTypesPresets"
        Me.PnlTypesPresets.Size = New System.Drawing.Size(633, 135)
        Me.PnlTypesPresets.TabIndex = 3
        '
        'ChbTypesRP
        '
        Me.ChbTypesRP.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbTypesRP.BorderColour = System.Drawing.Color.DimGray
        Me.ChbTypesRP.Checked = True
        Me.ChbTypesRP.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbTypesRP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbTypesRP.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbTypesRP.Location = New System.Drawing.Point(0, 0)
        Me.ChbTypesRP.Name = "ChbTypesRP"
        Me.ChbTypesRP.Size = New System.Drawing.Size(83, 22)
        Me.ChbTypesRP.TabIndex = 5
        Me.ChbTypesRP.Tag = "Types"
        Me.ChbTypesRP.Text = "Rename"
        '
        'TpMethods
        '
        Me.TpMethods.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TpMethods.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TpMethods.Controls.Add(Me.PnlMethodsPresets)
        Me.TpMethods.Location = New System.Drawing.Point(4, 36)
        Me.TpMethods.Name = "TpMethods"
        Me.TpMethods.Padding = New System.Windows.Forms.Padding(3)
        Me.TpMethods.Size = New System.Drawing.Size(647, 149)
        Me.TpMethods.TabIndex = 2
        Me.TpMethods.Text = "Methods"
        '
        'PnlMethodsPresets
        '
        Me.PnlMethodsPresets.Controls.Add(Me.ChbMethodsRP)
        Me.PnlMethodsPresets.Location = New System.Drawing.Point(6, 6)
        Me.PnlMethodsPresets.Name = "PnlMethodsPresets"
        Me.PnlMethodsPresets.Size = New System.Drawing.Size(633, 135)
        Me.PnlMethodsPresets.TabIndex = 4
        '
        'ChbMethodsRP
        '
        Me.ChbMethodsRP.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbMethodsRP.BorderColour = System.Drawing.Color.DimGray
        Me.ChbMethodsRP.Checked = True
        Me.ChbMethodsRP.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbMethodsRP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbMethodsRP.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbMethodsRP.Location = New System.Drawing.Point(0, 0)
        Me.ChbMethodsRP.Name = "ChbMethodsRP"
        Me.ChbMethodsRP.Size = New System.Drawing.Size(83, 22)
        Me.ChbMethodsRP.TabIndex = 6
        Me.ChbMethodsRP.Tag = "Methods"
        Me.ChbMethodsRP.Text = "Rename"
        '
        'TpProperties
        '
        Me.TpProperties.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TpProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TpProperties.Controls.Add(Me.PnlPropertiesPresets)
        Me.TpProperties.Location = New System.Drawing.Point(4, 36)
        Me.TpProperties.Name = "TpProperties"
        Me.TpProperties.Padding = New System.Windows.Forms.Padding(3)
        Me.TpProperties.Size = New System.Drawing.Size(647, 149)
        Me.TpProperties.TabIndex = 3
        Me.TpProperties.Text = "Properties"
        '
        'PnlPropertiesPresets
        '
        Me.PnlPropertiesPresets.Controls.Add(Me.ChbPropertiesRP)
        Me.PnlPropertiesPresets.Location = New System.Drawing.Point(6, 6)
        Me.PnlPropertiesPresets.Name = "PnlPropertiesPresets"
        Me.PnlPropertiesPresets.Size = New System.Drawing.Size(633, 135)
        Me.PnlPropertiesPresets.TabIndex = 4
        '
        'ChbPropertiesRP
        '
        Me.ChbPropertiesRP.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbPropertiesRP.BorderColour = System.Drawing.Color.DimGray
        Me.ChbPropertiesRP.Checked = True
        Me.ChbPropertiesRP.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbPropertiesRP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbPropertiesRP.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbPropertiesRP.Location = New System.Drawing.Point(0, 0)
        Me.ChbPropertiesRP.Name = "ChbPropertiesRP"
        Me.ChbPropertiesRP.Size = New System.Drawing.Size(83, 22)
        Me.ChbPropertiesRP.TabIndex = 7
        Me.ChbPropertiesRP.Tag = "Properties"
        Me.ChbPropertiesRP.Text = "Rename"
        '
        'TpEvents
        '
        Me.TpEvents.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TpEvents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TpEvents.Controls.Add(Me.PnlEventsPresets)
        Me.TpEvents.Location = New System.Drawing.Point(4, 36)
        Me.TpEvents.Name = "TpEvents"
        Me.TpEvents.Padding = New System.Windows.Forms.Padding(3)
        Me.TpEvents.Size = New System.Drawing.Size(647, 149)
        Me.TpEvents.TabIndex = 4
        Me.TpEvents.Text = "Events"
        '
        'PnlEventsPresets
        '
        Me.PnlEventsPresets.Controls.Add(Me.ChbEventsRP)
        Me.PnlEventsPresets.Location = New System.Drawing.Point(6, 6)
        Me.PnlEventsPresets.Name = "PnlEventsPresets"
        Me.PnlEventsPresets.Size = New System.Drawing.Size(633, 135)
        Me.PnlEventsPresets.TabIndex = 4
        '
        'ChbEventsRP
        '
        Me.ChbEventsRP.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbEventsRP.BorderColour = System.Drawing.Color.DimGray
        Me.ChbEventsRP.Checked = True
        Me.ChbEventsRP.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbEventsRP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbEventsRP.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbEventsRP.Location = New System.Drawing.Point(0, 0)
        Me.ChbEventsRP.Name = "ChbEventsRP"
        Me.ChbEventsRP.Size = New System.Drawing.Size(83, 22)
        Me.ChbEventsRP.TabIndex = 8
        Me.ChbEventsRP.Tag = "Events"
        Me.ChbEventsRP.Text = "Rename"
        '
        'TpFields
        '
        Me.TpFields.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TpFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TpFields.Controls.Add(Me.PnlFieldsPresets)
        Me.TpFields.Location = New System.Drawing.Point(4, 36)
        Me.TpFields.Name = "TpFields"
        Me.TpFields.Padding = New System.Windows.Forms.Padding(3)
        Me.TpFields.Size = New System.Drawing.Size(647, 149)
        Me.TpFields.TabIndex = 5
        Me.TpFields.Text = "Fields"
        '
        'PnlFieldsPresets
        '
        Me.PnlFieldsPresets.Controls.Add(Me.ChbFieldsRP)
        Me.PnlFieldsPresets.Location = New System.Drawing.Point(6, 6)
        Me.PnlFieldsPresets.Name = "PnlFieldsPresets"
        Me.PnlFieldsPresets.Size = New System.Drawing.Size(633, 135)
        Me.PnlFieldsPresets.TabIndex = 4
        '
        'ChbFieldsRP
        '
        Me.ChbFieldsRP.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbFieldsRP.BorderColour = System.Drawing.Color.DimGray
        Me.ChbFieldsRP.Checked = True
        Me.ChbFieldsRP.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbFieldsRP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbFieldsRP.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbFieldsRP.Location = New System.Drawing.Point(0, 0)
        Me.ChbFieldsRP.Name = "ChbFieldsRP"
        Me.ChbFieldsRP.Size = New System.Drawing.Size(83, 22)
        Me.ChbFieldsRP.TabIndex = 9
        Me.ChbFieldsRP.Tag = "Fields"
        Me.ChbFieldsRP.Text = "Rename"
        '
        'TpAttributes
        '
        Me.TpAttributes.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TpAttributes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TpAttributes.Controls.Add(Me.PnlAttributesPresets)
        Me.TpAttributes.Location = New System.Drawing.Point(4, 36)
        Me.TpAttributes.Name = "TpAttributes"
        Me.TpAttributes.Padding = New System.Windows.Forms.Padding(3)
        Me.TpAttributes.Size = New System.Drawing.Size(647, 149)
        Me.TpAttributes.TabIndex = 6
        Me.TpAttributes.Text = "Attributes"
        '
        'PnlAttributesPresets
        '
        Me.PnlAttributesPresets.Controls.Add(Me.ChbAttributesRP)
        Me.PnlAttributesPresets.Location = New System.Drawing.Point(6, 6)
        Me.PnlAttributesPresets.Name = "PnlAttributesPresets"
        Me.PnlAttributesPresets.Size = New System.Drawing.Size(633, 135)
        Me.PnlAttributesPresets.TabIndex = 5
        '
        'ChbAttributesRP
        '
        Me.ChbAttributesRP.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbAttributesRP.BorderColour = System.Drawing.Color.DimGray
        Me.ChbAttributesRP.Checked = True
        Me.ChbAttributesRP.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbAttributesRP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbAttributesRP.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbAttributesRP.Location = New System.Drawing.Point(0, 0)
        Me.ChbAttributesRP.Name = "ChbAttributesRP"
        Me.ChbAttributesRP.Size = New System.Drawing.Size(83, 22)
        Me.ChbAttributesRP.TabIndex = 10
        Me.ChbAttributesRP.Tag = "Attributes"
        Me.ChbAttributesRP.Text = "Rename"
        '
        'TpParameters
        '
        Me.TpParameters.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.TpParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TpParameters.Controls.Add(Me.PnlParametersPresets)
        Me.TpParameters.Location = New System.Drawing.Point(4, 36)
        Me.TpParameters.Name = "TpParameters"
        Me.TpParameters.Padding = New System.Windows.Forms.Padding(3)
        Me.TpParameters.Size = New System.Drawing.Size(647, 149)
        Me.TpParameters.TabIndex = 8
        Me.TpParameters.Text = "Parameters"
        '
        'PnlParametersPresets
        '
        Me.PnlParametersPresets.Controls.Add(Me.ChbParametersRP)
        Me.PnlParametersPresets.Location = New System.Drawing.Point(6, 6)
        Me.PnlParametersPresets.Name = "PnlParametersPresets"
        Me.PnlParametersPresets.Size = New System.Drawing.Size(633, 135)
        Me.PnlParametersPresets.TabIndex = 6
        '
        'ChbParametersRP
        '
        Me.ChbParametersRP.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbParametersRP.BorderColour = System.Drawing.Color.DimGray
        Me.ChbParametersRP.Checked = True
        Me.ChbParametersRP.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbParametersRP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbParametersRP.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbParametersRP.Location = New System.Drawing.Point(0, 0)
        Me.ChbParametersRP.Name = "ChbParametersRP"
        Me.ChbParametersRP.Size = New System.Drawing.Size(83, 22)
        Me.ChbParametersRP.TabIndex = 11
        Me.ChbParametersRP.Tag = "Parameters"
        Me.ChbParametersRP.Text = "Rename"
        '
        'CbxPresets
        '
        Me.CbxPresets.ArrowColour = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.CbxPresets.BackColor = System.Drawing.Color.Transparent
        Me.CbxPresets.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.CbxPresets.BorderColour = System.Drawing.Color.DimGray
        Me.CbxPresets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CbxPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbxPresets.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.CbxPresets.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CbxPresets.Items.AddRange(New Object() {"Full", "Medium", "Customize"})
        Me.CbxPresets.LineColour = System.Drawing.Color.BlueViolet
        Me.CbxPresets.Location = New System.Drawing.Point(278, 44)
        Me.CbxPresets.Name = "CbxPresets"
        Me.CbxPresets.Size = New System.Drawing.Size(115, 26)
        Me.CbxPresets.SqaureColour = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.CbxPresets.SqaureHoverColour = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(52, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.CbxPresets.TabIndex = 0
        '
        'GbxAsemblyInfos
        '
        Me.GbxAsemblyInfos.BorderColour = System.Drawing.SystemColors.ButtonShadow
        Me.GbxAsemblyInfos.Controls.Add(Me.LblType)
        Me.GbxAsemblyInfos.Controls.Add(Me.TxbType)
        Me.GbxAsemblyInfos.Controls.Add(Me.LblCpuTargetInfo)
        Me.GbxAsemblyInfos.Controls.Add(Me.TxbCpuTargetInfo)
        Me.GbxAsemblyInfos.Controls.Add(Me.TxbFrameworkInfo)
        Me.GbxAsemblyInfos.Controls.Add(Me.LblFrameworkInfo)
        Me.GbxAsemblyInfos.Controls.Add(Me.TxbVersionInfo)
        Me.GbxAsemblyInfos.Controls.Add(Me.TxbAssemblyInfo)
        Me.GbxAsemblyInfos.Controls.Add(Me.LblVersionInfo)
        Me.GbxAsemblyInfos.Controls.Add(Me.LblAssemblyInfo)
        Me.GbxAsemblyInfos.Enabled = False
        Me.GbxAsemblyInfos.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GbxAsemblyInfos.HeaderColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.GbxAsemblyInfos.Location = New System.Drawing.Point(12, 175)
        Me.GbxAsemblyInfos.MainColour = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.GbxAsemblyInfos.Name = "GbxAsemblyInfos"
        Me.GbxAsemblyInfos.Size = New System.Drawing.Size(661, 114)
        Me.GbxAsemblyInfos.TabIndex = 3
        Me.GbxAsemblyInfos.Text = "Assembly informations"
        Me.GbxAsemblyInfos.TextColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        'LblType
        '
        Me.LblType.AutoSize = True
        Me.LblType.BackColor = System.Drawing.Color.Transparent
        Me.LblType.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblType.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblType.Location = New System.Drawing.Point(495, 77)
        Me.LblType.Name = "LblType"
        Me.LblType.Size = New System.Drawing.Size(39, 15)
        Me.LblType.TabIndex = 13
        Me.LblType.Text = "Type :"
        '
        'TxbType
        '
        Me.TxbType.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.TxbType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbType.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbType.ForeColor = System.Drawing.Color.White
        Me.TxbType.Location = New System.Drawing.Point(540, 73)
        Me.TxbType.Name = "TxbType"
        Me.TxbType.ReadOnly = True
        Me.TxbType.Size = New System.Drawing.Size(70, 25)
        Me.TxbType.TabIndex = 14
        Me.TxbType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblCpuTargetInfo
        '
        Me.LblCpuTargetInfo.AutoSize = True
        Me.LblCpuTargetInfo.BackColor = System.Drawing.Color.Transparent
        Me.LblCpuTargetInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblCpuTargetInfo.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblCpuTargetInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblCpuTargetInfo.Location = New System.Drawing.Point(333, 77)
        Me.LblCpuTargetInfo.Name = "LblCpuTargetInfo"
        Me.LblCpuTargetInfo.Size = New System.Drawing.Size(70, 15)
        Me.LblCpuTargetInfo.TabIndex = 6
        Me.LblCpuTargetInfo.Text = "CPU target :"
        '
        'TxbCpuTargetInfo
        '
        Me.TxbCpuTargetInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.TxbCpuTargetInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbCpuTargetInfo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbCpuTargetInfo.ForeColor = System.Drawing.Color.White
        Me.TxbCpuTargetInfo.Location = New System.Drawing.Point(409, 73)
        Me.TxbCpuTargetInfo.Name = "TxbCpuTargetInfo"
        Me.TxbCpuTargetInfo.ReadOnly = True
        Me.TxbCpuTargetInfo.Size = New System.Drawing.Size(70, 25)
        Me.TxbCpuTargetInfo.TabIndex = 12
        Me.TxbCpuTargetInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxbFrameworkInfo
        '
        Me.TxbFrameworkInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.TxbFrameworkInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbFrameworkInfo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbFrameworkInfo.ForeColor = System.Drawing.Color.White
        Me.TxbFrameworkInfo.Location = New System.Drawing.Point(257, 73)
        Me.TxbFrameworkInfo.Name = "TxbFrameworkInfo"
        Me.TxbFrameworkInfo.ReadOnly = True
        Me.TxbFrameworkInfo.Size = New System.Drawing.Size(70, 25)
        Me.TxbFrameworkInfo.TabIndex = 11
        Me.TxbFrameworkInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblFrameworkInfo
        '
        Me.LblFrameworkInfo.AutoSize = True
        Me.LblFrameworkInfo.BackColor = System.Drawing.Color.Transparent
        Me.LblFrameworkInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblFrameworkInfo.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblFrameworkInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblFrameworkInfo.Location = New System.Drawing.Point(179, 77)
        Me.LblFrameworkInfo.Name = "LblFrameworkInfo"
        Me.LblFrameworkInfo.Size = New System.Drawing.Size(72, 15)
        Me.LblFrameworkInfo.TabIndex = 8
        Me.LblFrameworkInfo.Text = "Framework :"
        '
        'TxbVersionInfo
        '
        Me.TxbVersionInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.TxbVersionInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbVersionInfo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbVersionInfo.ForeColor = System.Drawing.Color.White
        Me.TxbVersionInfo.Location = New System.Drawing.Point(103, 73)
        Me.TxbVersionInfo.Name = "TxbVersionInfo"
        Me.TxbVersionInfo.ReadOnly = True
        Me.TxbVersionInfo.Size = New System.Drawing.Size(70, 25)
        Me.TxbVersionInfo.TabIndex = 10
        Me.TxbVersionInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxbAssemblyInfo
        '
        Me.TxbAssemblyInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.TxbAssemblyInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbAssemblyInfo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbAssemblyInfo.ForeColor = System.Drawing.Color.White
        Me.TxbAssemblyInfo.Location = New System.Drawing.Point(103, 42)
        Me.TxbAssemblyInfo.Name = "TxbAssemblyInfo"
        Me.TxbAssemblyInfo.ReadOnly = True
        Me.TxbAssemblyInfo.Size = New System.Drawing.Size(507, 25)
        Me.TxbAssemblyInfo.TabIndex = 9
        Me.TxbAssemblyInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblVersionInfo
        '
        Me.LblVersionInfo.AutoSize = True
        Me.LblVersionInfo.BackColor = System.Drawing.Color.Transparent
        Me.LblVersionInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblVersionInfo.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblVersionInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblVersionInfo.Location = New System.Drawing.Point(45, 77)
        Me.LblVersionInfo.Name = "LblVersionInfo"
        Me.LblVersionInfo.Size = New System.Drawing.Size(52, 15)
        Me.LblVersionInfo.TabIndex = 4
        Me.LblVersionInfo.Text = "Version :"
        '
        'LblAssemblyInfo
        '
        Me.LblAssemblyInfo.AutoSize = True
        Me.LblAssemblyInfo.BackColor = System.Drawing.Color.Transparent
        Me.LblAssemblyInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblAssemblyInfo.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblAssemblyInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblAssemblyInfo.Location = New System.Drawing.Point(33, 46)
        Me.LblAssemblyInfo.Name = "LblAssemblyInfo"
        Me.LblAssemblyInfo.Size = New System.Drawing.Size(64, 15)
        Me.LblAssemblyInfo.TabIndex = 2
        Me.LblAssemblyInfo.Text = "Assembly :"
        '
        'GbxSelectFile
        '
        Me.GbxSelectFile.BorderColour = System.Drawing.SystemColors.ButtonShadow
        Me.GbxSelectFile.Controls.Add(Me.TxbSelectedOutput)
        Me.GbxSelectFile.Controls.Add(Me.BtnSelectOutput)
        Me.GbxSelectFile.Controls.Add(Me.TxbSelectedFile)
        Me.GbxSelectFile.Controls.Add(Me.PbxSelectedFile)
        Me.GbxSelectFile.Controls.Add(Me.BtnSelectFile)
        Me.GbxSelectFile.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GbxSelectFile.HeaderColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.GbxSelectFile.Location = New System.Drawing.Point(12, 46)
        Me.GbxSelectFile.MainColour = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.GbxSelectFile.Name = "GbxSelectFile"
        Me.GbxSelectFile.Size = New System.Drawing.Size(661, 118)
        Me.GbxSelectFile.TabIndex = 1
        Me.GbxSelectFile.Text = "Select .Net file (C#, VbNet)"
        Me.GbxSelectFile.TextColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        'TxbSelectedOutput
        '
        Me.TxbSelectedOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.TxbSelectedOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbSelectedOutput.Enabled = False
        Me.TxbSelectedOutput.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbSelectedOutput.ForeColor = System.Drawing.Color.White
        Me.TxbSelectedOutput.Location = New System.Drawing.Point(103, 75)
        Me.TxbSelectedOutput.Name = "TxbSelectedOutput"
        Me.TxbSelectedOutput.ReadOnly = True
        Me.TxbSelectedOutput.Size = New System.Drawing.Size(507, 25)
        Me.TxbSelectedOutput.TabIndex = 13
        Me.TxbSelectedOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BtnSelectOutput
        '
        Me.BtnSelectOutput.BackColor = System.Drawing.Color.Transparent
        Me.BtnSelectOutput.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.BtnSelectOutput.BorderColour = System.Drawing.Color.DimGray
        Me.BtnSelectOutput.Enabled = False
        Me.BtnSelectOutput.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnSelectOutput.HoverColour = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(52, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.BtnSelectOutput.Location = New System.Drawing.Point(22, 75)
        Me.BtnSelectOutput.Name = "BtnSelectOutput"
        Me.BtnSelectOutput.PressedColour = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.BtnSelectOutput.ProgressColour = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnSelectOutput.Size = New System.Drawing.Size(75, 25)
        Me.BtnSelectOutput.TabIndex = 11
        Me.BtnSelectOutput.Text = "Output"
        '
        'TxbSelectedFile
        '
        Me.TxbSelectedFile.AllowDrop = True
        Me.TxbSelectedFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.TxbSelectedFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxbSelectedFile.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxbSelectedFile.ForeColor = System.Drawing.Color.White
        Me.TxbSelectedFile.Location = New System.Drawing.Point(103, 44)
        Me.TxbSelectedFile.Name = "TxbSelectedFile"
        Me.TxbSelectedFile.ReadOnly = True
        Me.TxbSelectedFile.Size = New System.Drawing.Size(507, 25)
        Me.TxbSelectedFile.TabIndex = 10
        Me.TxbSelectedFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PbxSelectedFile
        '
        Me.PbxSelectedFile.BackColor = System.Drawing.Color.Transparent
        Me.PbxSelectedFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PbxSelectedFile.Location = New System.Drawing.Point(616, 40)
        Me.PbxSelectedFile.Name = "PbxSelectedFile"
        Me.PbxSelectedFile.Size = New System.Drawing.Size(32, 32)
        Me.PbxSelectedFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PbxSelectedFile.TabIndex = 2
        Me.PbxSelectedFile.TabStop = False
        '
        'BtnSelectFile
        '
        Me.BtnSelectFile.BackColor = System.Drawing.Color.Transparent
        Me.BtnSelectFile.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.BtnSelectFile.BorderColour = System.Drawing.Color.DimGray
        Me.BtnSelectFile.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnSelectFile.HoverColour = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(52, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.BtnSelectFile.Location = New System.Drawing.Point(22, 44)
        Me.BtnSelectFile.Name = "BtnSelectFile"
        Me.BtnSelectFile.PressedColour = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.BtnSelectFile.ProgressColour = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnSelectFile.Size = New System.Drawing.Size(75, 25)
        Me.BtnSelectFile.TabIndex = 0
        Me.BtnSelectFile.Text = "Browse"
        '
        'Frm_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 691)
        Me.Controls.Add(Me.LogInThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = Global.DotNetRenamer.My.Resources.Resources.DNR
        Me.MaximizeBox = False
        Me.Name = "Frm_Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "                          DotNet Renamer"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.LogInThemeContainer1.ResumeLayout(False)
        Me.LogInThemeContainer1.PerformLayout()
        Me.GbxPresets.ResumeLayout(False)
        Me.GbxPresets.PerformLayout()
        Me.TbcPresets.ResumeLayout(False)
        Me.TpCharacters.ResumeLayout(False)
        Me.PnlCharactersPresets.ResumeLayout(False)
        Me.TpNamespaces.ResumeLayout(False)
        Me.PnlNamespacesPresets.ResumeLayout(False)
        Me.PnlNamespacesGroup.ResumeLayout(False)
        Me.TpTypes.ResumeLayout(False)
        Me.PnlTypesPresets.ResumeLayout(False)
        Me.TpMethods.ResumeLayout(False)
        Me.PnlMethodsPresets.ResumeLayout(False)
        Me.TpProperties.ResumeLayout(False)
        Me.PnlPropertiesPresets.ResumeLayout(False)
        Me.TpEvents.ResumeLayout(False)
        Me.PnlEventsPresets.ResumeLayout(False)
        Me.TpFields.ResumeLayout(False)
        Me.PnlFieldsPresets.ResumeLayout(False)
        Me.TpAttributes.ResumeLayout(False)
        Me.PnlAttributesPresets.ResumeLayout(False)
        Me.TpParameters.ResumeLayout(False)
        Me.PnlParametersPresets.ResumeLayout(False)
        Me.GbxAsemblyInfos.ResumeLayout(False)
        Me.GbxAsemblyInfos.PerformLayout()
        Me.GbxSelectFile.ResumeLayout(False)
        Me.GbxSelectFile.PerformLayout()
        CType(Me.PbxSelectedFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GbxSelectFile As LogInGroupBox
    Friend WithEvents BtnSelectFile As LogInButton
    Friend WithEvents PbxSelectedFile As System.Windows.Forms.PictureBox
    Friend WithEvents GbxAsemblyInfos As LogInGroupBox
    Friend WithEvents LblCpuTargetInfo As LogInLabel
    Friend WithEvents LblVersionInfo As LogInLabel
    Friend WithEvents LblAssemblyInfo As LogInLabel
    Friend WithEvents LblFrameworkInfo As LogInLabel
    Friend WithEvents TxbAssemblyInfo As System.Windows.Forms.TextBox
    Friend WithEvents TxbCpuTargetInfo As System.Windows.Forms.TextBox
    Friend WithEvents TxbFrameworkInfo As System.Windows.Forms.TextBox
    Friend WithEvents TxbVersionInfo As System.Windows.Forms.TextBox
    Friend WithEvents TxbSelectedFile As System.Windows.Forms.TextBox
    Friend WithEvents GbxPresets As LogInGroupBox
    Friend WithEvents CbxPresets As LogInComboBox
    Friend WithEvents TbcPresets As LogInTabControl
    Friend WithEvents TpNamespaces As System.Windows.Forms.TabPage
    Friend WithEvents TpTypes As System.Windows.Forms.TabPage
    Friend WithEvents TpMethods As System.Windows.Forms.TabPage
    Friend WithEvents TpProperties As System.Windows.Forms.TabPage
    Friend WithEvents TpEvents As System.Windows.Forms.TabPage
    Friend WithEvents TpFields As System.Windows.Forms.TabPage
    Friend WithEvents TpAttributes As System.Windows.Forms.TabPage
    Friend WithEvents TpCharacters As System.Windows.Forms.TabPage
    Friend WithEvents TpParameters As System.Windows.Forms.TabPage
    Friend WithEvents BtnStart As LogInButton
    Friend WithEvents LblType As LogInLabel
    Friend WithEvents TxbType As System.Windows.Forms.TextBox
    Friend WithEvents PnlNamespacesPresets As System.Windows.Forms.Panel
    Friend WithEvents PnlNamespacesGroup As System.Windows.Forms.Panel
    Friend WithEvents ChbReplaceNamespaceByEmptyNamespaces As LogInCheckBox
    Friend WithEvents ChbRenameMainNamespaceOnlyNamespaces As LogInCheckBox
    Friend WithEvents PnlTypesPresets As System.Windows.Forms.Panel
    Friend WithEvents PnlMethodsPresets As System.Windows.Forms.Panel
    Friend WithEvents PnlPropertiesPresets As System.Windows.Forms.Panel
    Friend WithEvents PnlEventsPresets As System.Windows.Forms.Panel
    Friend WithEvents PnlFieldsPresets As System.Windows.Forms.Panel
    Friend WithEvents PnlAttributesPresets As System.Windows.Forms.Panel
    Friend WithEvents PnlParametersPresets As System.Windows.Forms.Panel
    Friend WithEvents PnlCharactersPresets As System.Windows.Forms.Panel
    Friend WithEvents RdbAlphabeticCharacters As LogInRadioButton
    Friend WithEvents RdbJapaneseCharacters As LogInRadioButton
    Friend WithEvents RdbDotsCharacters As LogInRadioButton
    Friend WithEvents RdbChineseCharacters As LogInRadioButton
    Friend WithEvents RdbInvisibleCharacters As LogInRadioButton
    Friend WithEvents BgwRenameTask As System.ComponentModel.BackgroundWorker
    Friend WithEvents LsbMain As DotNetRenamer.XertzLoginTheme.LogInStatusBar
    Friend WithEvents LnkLblBlogSpot As System.Windows.Forms.LinkLabel
    Friend WithEvents LblPresets As DotNetRenamer.XertzLoginTheme.LogInLabel
    Friend WithEvents ChbNamespacesRP As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents ChbTypesRP As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents ChbMethodsRP As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents ChbPropertiesRP As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents ChbEventsRP As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents ChbFieldsRP As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents ChbAttributesRP As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents ChbParametersRP As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents RdbGreekCharacters As DotNetRenamer.XertzLoginTheme.LogInRadioButton
    Friend WithEvents TxbSelectedOutput As System.Windows.Forms.TextBox
    Friend WithEvents BtnSelectOutput As DotNetRenamer.XertzLoginTheme.LogInButton
    Friend WithEvents LogInThemeContainer1 As DotNetRenamer.XertzLoginTheme.LogInThemeContainer
End Class
