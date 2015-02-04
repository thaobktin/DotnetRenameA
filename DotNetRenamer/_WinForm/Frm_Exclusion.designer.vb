
Imports DotNetRenamer.XertzLoginTheme

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Exclusion
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Exclusion))
        Me.BgwRenameTask = New System.ComponentModel.BackgroundWorker()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.LogInThemeContainer1 = New DotNetRenamer.XertzLoginTheme.LogInThemeContainer()
        Me.GbxExclusion = New DotNetRenamer.XertzLoginTheme.LogInGroupBox()
        Me.LblExcluded = New DotNetRenamer.XertzLoginTheme.LogInLabel()
        Me.LblAllEntities = New DotNetRenamer.XertzLoginTheme.LogInLabel()
        Me.ChbAllEntities = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.LblExclusion = New DotNetRenamer.XertzLoginTheme.LogInLabel()
        Me.ChbExclusion = New DotNetRenamer.XertzLoginTheme.LogInCheckBox()
        Me.TvExclusion = New DotNetRenamer.XertzLoginTheme.TreeViewEx()
        Me.BgwExclusion = New System.ComponentModel.BackgroundWorker()
        Me.LogInThemeContainer1.SuspendLayout()
        Me.GbxExclusion.SuspendLayout()
        Me.SuspendLayout()
        '
        'BgwRenameTask
        '
        Me.BgwRenameTask.WorkerReportsProgress = True
        Me.BgwRenameTask.WorkerSupportsCancellation = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Assembly.png")
        Me.ImageList1.Images.SetKeyName(1, "Library.png")
        Me.ImageList1.Images.SetKeyName(2, "NameSpace.png")
        Me.ImageList1.Images.SetKeyName(3, "Class.png")
        Me.ImageList1.Images.SetKeyName(4, "Constructor.png")
        Me.ImageList1.Images.SetKeyName(5, "Delegate.png")
        Me.ImageList1.Images.SetKeyName(6, "Enum.png")
        Me.ImageList1.Images.SetKeyName(7, "EnumValue.png")
        Me.ImageList1.Images.SetKeyName(8, "Event.png")
        Me.ImageList1.Images.SetKeyName(9, "Field.png")
        Me.ImageList1.Images.SetKeyName(10, "Interface.png")
        Me.ImageList1.Images.SetKeyName(11, "Method.png")
        Me.ImageList1.Images.SetKeyName(12, "PInvokeMethod.png")
        Me.ImageList1.Images.SetKeyName(13, "Property.png")
        Me.ImageList1.Images.SetKeyName(14, "StaticClass.png")
        '
        'LogInThemeContainer1
        '
        Me.LogInThemeContainer1.AllowClose = True
        Me.LogInThemeContainer1.AllowMaximize = False
        Me.LogInThemeContainer1.AllowMinimize = False
        Me.LogInThemeContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.LogInThemeContainer1.BaseColour = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.LogInThemeContainer1.BorderColour = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.LogInThemeContainer1.ContainerColour = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.LogInThemeContainer1.Controls.Add(Me.GbxExclusion)
        Me.LogInThemeContainer1.Controls.Add(Me.TvExclusion)
        Me.LogInThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogInThemeContainer1.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogInThemeContainer1.FontSize = 12
        Me.LogInThemeContainer1.HoverColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.LogInThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.LogInThemeContainer1.Name = "LogInThemeContainer1"
        Me.LogInThemeContainer1.ShowIcon = False
        Me.LogInThemeContainer1.Size = New System.Drawing.Size(685, 691)
        Me.LogInThemeContainer1.TabIndex = 0
        Me.LogInThemeContainer1.Text = "                                                              Exclusion rules"
        '
        'GbxExclusion
        '
        Me.GbxExclusion.BorderColour = System.Drawing.SystemColors.ButtonShadow
        Me.GbxExclusion.Controls.Add(Me.LblExcluded)
        Me.GbxExclusion.Controls.Add(Me.LblAllEntities)
        Me.GbxExclusion.Controls.Add(Me.ChbAllEntities)
        Me.GbxExclusion.Controls.Add(Me.LblExclusion)
        Me.GbxExclusion.Controls.Add(Me.ChbExclusion)
        Me.GbxExclusion.Enabled = False
        Me.GbxExclusion.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GbxExclusion.HeaderColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.GbxExclusion.Location = New System.Drawing.Point(12, 44)
        Me.GbxExclusion.MainColour = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.GbxExclusion.Name = "GbxExclusion"
        Me.GbxExclusion.Size = New System.Drawing.Size(661, 70)
        Me.GbxExclusion.TabIndex = 2
        Me.GbxExclusion.Text = "Rule"
        Me.GbxExclusion.TextColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        'LblExcluded
        '
        Me.LblExcluded.AutoSize = True
        Me.LblExcluded.BackColor = System.Drawing.Color.Transparent
        Me.LblExcluded.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblExcluded.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblExcluded.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblExcluded.Location = New System.Drawing.Point(521, 41)
        Me.LblExcluded.Name = "LblExcluded"
        Me.LblExcluded.Size = New System.Drawing.Size(101, 15)
        Me.LblExcluded.TabIndex = 20
        Me.LblExcluded.Text = "0 items excluded  "
        '
        'LblAllEntities
        '
        Me.LblAllEntities.AutoSize = True
        Me.LblAllEntities.BackColor = System.Drawing.Color.Transparent
        Me.LblAllEntities.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblAllEntities.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblAllEntities.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblAllEntities.Location = New System.Drawing.Point(182, 41)
        Me.LblAllEntities.Name = "LblAllEntities"
        Me.LblAllEntities.Size = New System.Drawing.Size(62, 15)
        Me.LblAllEntities.TabIndex = 19
        Me.LblAllEntities.Text = "All entities"
        '
        'ChbAllEntities
        '
        Me.ChbAllEntities.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbAllEntities.BorderColour = System.Drawing.Color.DimGray
        Me.ChbAllEntities.Checked = False
        Me.ChbAllEntities.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbAllEntities.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbAllEntities.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbAllEntities.Location = New System.Drawing.Point(155, 38)
        Me.ChbAllEntities.Name = "ChbAllEntities"
        Me.ChbAllEntities.Size = New System.Drawing.Size(21, 22)
        Me.ChbAllEntities.TabIndex = 18
        Me.ChbAllEntities.Tag = ""
        '
        'LblExclusion
        '
        Me.LblExclusion.AutoSize = True
        Me.LblExclusion.BackColor = System.Drawing.Color.Transparent
        Me.LblExclusion.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LblExclusion.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblExclusion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblExclusion.Location = New System.Drawing.Point(48, 41)
        Me.LblExclusion.Name = "LblExclusion"
        Me.LblExclusion.Size = New System.Drawing.Size(47, 15)
        Me.LblExclusion.TabIndex = 17
        Me.LblExclusion.Text = "Exclude"
        '
        'ChbExclusion
        '
        Me.ChbExclusion.BaseColour = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ChbExclusion.BorderColour = System.Drawing.Color.DimGray
        Me.ChbExclusion.Checked = False
        Me.ChbExclusion.CheckedColour = System.Drawing.Color.BlueViolet
        Me.ChbExclusion.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChbExclusion.FontColour = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ChbExclusion.Location = New System.Drawing.Point(21, 38)
        Me.ChbExclusion.Name = "ChbExclusion"
        Me.ChbExclusion.Size = New System.Drawing.Size(21, 22)
        Me.ChbExclusion.TabIndex = 16
        Me.ChbExclusion.Tag = ""
        '
        'TvExclusion
        '
        Me.TvExclusion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TvExclusion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TvExclusion.ImageIndex = 0
        Me.TvExclusion.ImageList = Me.ImageList1
        Me.TvExclusion.Location = New System.Drawing.Point(12, 120)
        Me.TvExclusion.Name = "TvExclusion"
        Me.TvExclusion.SelectedImageIndex = 0
        Me.TvExclusion.Size = New System.Drawing.Size(661, 559)
        Me.TvExclusion.TabIndex = 0
        '
        'BgwExclusion
        '
        Me.BgwExclusion.WorkerReportsProgress = True
        '
        'Frm_Exclusion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 691)
        Me.ControlBox = False
        Me.Controls.Add(Me.LogInThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "Frm_Exclusion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.LogInThemeContainer1.ResumeLayout(False)
        Me.GbxExclusion.ResumeLayout(False)
        Me.GbxExclusion.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LogInThemeContainer1 As LogInThemeContainer
    Friend WithEvents BgwRenameTask As System.ComponentModel.BackgroundWorker
    Friend WithEvents TvExclusion As TreeViewEx
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents GbxExclusion As DotNetRenamer.XertzLoginTheme.LogInGroupBox
    Friend WithEvents ChbExclusion As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents LblExclusion As DotNetRenamer.XertzLoginTheme.LogInLabel
    Friend WithEvents LblAllEntities As DotNetRenamer.XertzLoginTheme.LogInLabel
    Friend WithEvents ChbAllEntities As DotNetRenamer.XertzLoginTheme.LogInCheckBox
    Friend WithEvents LblExcluded As DotNetRenamer.XertzLoginTheme.LogInLabel
    Friend WithEvents BgwExclusion As System.ComponentModel.BackgroundWorker
End Class
