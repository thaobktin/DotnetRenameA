Option Strict On

Imports System.Drawing.Text
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

''     DO NOT REMOVE CREDITS! IF YOU USE PLEASE CREDIT!     ''

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''''''           Added by Sh@rp aka 3DotDev  :      ''''''
'''''' - RadioButton CheckedState Event manager     ''''''
'''''' - Enabled/Disabled control States            ''''''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Namespace XertzLoginTheme

    ''' <summary>
    ''' LogIn GDI+ Theme
    ''' Creator: Xertz (HF)
    ''' Version: 1.3
    ''' Control Count: 28
    ''' Date Created: 18/12/2013
    ''' Date Changed: 23/04/2014
    ''' UID: 1602992
    ''' For any bugs / errors, PM me.
    ''' </summary>
    ''' <remarks></remarks>

    Module DrawHelpers

#Region "Functions"

        Public Function RoundRectangle(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
            Dim P As GraphicsPath = New GraphicsPath()
            Dim ArcRectangleWidth As Integer = Curve * 2
            P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
            P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
            P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
            P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
            P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
            Return P
        End Function

        Public Function RoundRect(x!, y!, w!, h!, Optional r! = 0.3, Optional TL As Boolean = True, Optional TR As Boolean = True, Optional BR As Boolean = True, Optional BL As Boolean = True) As GraphicsPath
            Dim d! = Math.Min(w, h) * r, xw = x + w, yh = y + h
            RoundRect = New GraphicsPath

            With RoundRect
                If TL Then .AddArc(x, y, d, d, 180, 90) Else .AddLine(x, y, x, y)
                If TR Then .AddArc(xw - d, y, d, d, 270, 90) Else .AddLine(xw, y, xw, y)
                If BR Then .AddArc(xw - d, yh - d, d, d, 0, 90) Else .AddLine(xw, yh, xw, yh)
                If BL Then .AddArc(x, yh - d, d, d, 90, 90) Else .AddLine(x, yh, x, yh)

                .CloseFigure()
            End With
        End Function

        Enum MouseState As Byte
            None = 0
            Over = 1
            Down = 2
            Block = 3
        End Enum

#End Region

    End Module

    Public Class LogInThemeContainer
        Inherits ContainerControl

#Region "Declarations"
        Private _AllowClose As Boolean = True
        Private _AllowMinimize As Boolean = True
        Private _AllowMaximize As Boolean = True
        Private _FontSize As Integer = 12
        Private ReadOnly _Font As Font = New Font("Segoe UI", _FontSize)
        Private _ShowIcon As Boolean = True
        Private State As MouseState = MouseState.None
        Private MouseXLoc As Integer
        Private MouseYLoc As Integer
        Private CaptureMovement As Boolean = False
        Private Const MoveHeight As Integer = 35
        Private MouseP As Point = New Point(0, 0)
        Private _FontColour As Color = Color.FromArgb(255, 255, 255)
        Private _BaseColour As Color = Color.FromArgb(35, 35, 35)
        Private _ContainerColour As Color = Color.FromArgb(54, 54, 54)
        Private _BorderColour As Color = Color.FromArgb(60, 60, 60)
        Private _HoverColour As Color = Color.FromArgb(42, 42, 42)
#End Region

#Region "Properties & Events"

        <Category("Control")>
        Public Property FontSize As Integer
            Get
                Return _FontSize
            End Get
            Set(value As Integer)
                _FontSize = value
            End Set
        End Property

        <Category("Control")>
        Public Property AllowMinimize As Boolean
            Get
                Return _AllowMinimize
            End Get
            Set(value As Boolean)
                _AllowMinimize = value
            End Set
        End Property

        <Category("Control")>
        Public Property AllowMaximize As Boolean
            Get
                Return _AllowMaximize
            End Get
            Set(value As Boolean)
                _AllowMaximize = value
            End Set
        End Property

        <Category("Control")>
        Public Property ShowIcon As Boolean
            Get
                Return _ShowIcon
            End Get
            Set(value As Boolean)
                _ShowIcon = value
            End Set
        End Property

        <Category("Control")>
        Public Property AllowClose As Boolean
            Get
                Return _AllowClose
            End Get
            Set(value As Boolean)
                _AllowClose = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property HoverColour As Color
            Get
                Return _HoverColour
            End Get
            Set(value As Color)
                _HoverColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BaseColour As Color
            Get
                Return _BaseColour
            End Get
            Set(value As Color)
                _BaseColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property ContainerColour As Color
            Get
                Return _ContainerColour
            End Get
            Set(value As Color)
                _ContainerColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property FontColour As Color
            Get
                Return _FontColour
            End Get
            Set(value As Color)
                _FontColour = value
            End Set
        End Property

        Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseUp(e)
            CaptureMovement = False
            State = MouseState.Over
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over : Invalidate()
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None : Invalidate()
        End Sub

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            MouseXLoc = e.Location.X
            MouseYLoc = e.Location.Y
            Invalidate()
            If CaptureMovement Then
                Parent.Location = MousePosition - CType(MouseP, Size)
            End If
            If e.X < Width - 90 AndAlso e.Y > 35 Then Cursor = Cursors.Arrow Else Cursor = Cursors.Hand
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            If MouseXLoc > Width - 39 AndAlso MouseXLoc < Width - 16 AndAlso MouseYLoc < 22 Then
                If _AllowClose Then
                    Environment.Exit(0)
                End If
            ElseIf MouseXLoc > Width - 64 AndAlso MouseXLoc < Width - 41 AndAlso MouseYLoc < 22 Then
                If _AllowMaximize Then
                    Select Case FindForm.WindowState
                        Case FormWindowState.Maximized
                            FindForm.WindowState = FormWindowState.Normal
                        Case FormWindowState.Normal
                            FindForm.WindowState = FormWindowState.Maximized
                    End Select
                End If
            ElseIf MouseXLoc > Width - 89 AndAlso MouseXLoc < Width - 66 AndAlso MouseYLoc < 22 Then
                If _AllowMinimize Then
                    Select Case FindForm.WindowState
                        Case FormWindowState.Normal
                            FindForm.WindowState = FormWindowState.Minimized
                        Case FormWindowState.Maximized
                            FindForm.WindowState = FormWindowState.Minimized
                    End Select
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Left And New Rectangle(0, 0, Width - 90, MoveHeight).Contains(e.Location) Then
                CaptureMovement = True
                MouseP = e.Location
            ElseIf e.Button = Windows.Forms.MouseButtons.Left And New Rectangle(Width - 90, 22, 75, 13).Contains(e.Location) Then
                CaptureMovement = True
                MouseP = e.Location
            ElseIf e.Button = Windows.Forms.MouseButtons.Left And New Rectangle(Width - 15, 0, 15, MoveHeight).Contains(e.Location) Then
                CaptureMovement = True
                MouseP = e.Location
            Else
                Focus()
            End If
            State = MouseState.Down
            Invalidate()
        End Sub

#End Region


#Region "Draw Control"

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                    ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
            Me.DoubleBuffered = True
            Me.BackColor = _BaseColour
            Me.Dock = DockStyle.Fill
        End Sub

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            ParentForm.FormBorderStyle = FormBorderStyle.None
            ParentForm.AllowTransparency = False
            ParentForm.TransparencyKey = Color.Fuchsia
            ParentForm.FindForm.StartPosition = FormStartPosition.CenterScreen
            Dock = DockStyle.Fill
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)

            Dim G = e.Graphics
            With G
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .FillRectangle(New SolidBrush(_BaseColour), New Rectangle(0, 0, Width, Height))
                .FillRectangle(New SolidBrush(_ContainerColour), New Rectangle(2, 35, Width - 4, Height - 37))
                .DrawRectangle(New Pen(_BorderColour), New Rectangle(0, 0, Width, Height))
                Dim ControlBoxPoints() As Point = {New Point(Width - 90, 0), New Point(Width - 90, 22), New Point(Width - 15, 22), New Point(Width - 15, 0)}
                .DrawLines(New Pen(_BorderColour), ControlBoxPoints)
                .DrawLine(New Pen(_BorderColour), Width - 65, 0, Width - 65, 22)
                Select Case State
                    Case MouseState.Over
                        If MouseXLoc > Width - 39 AndAlso MouseXLoc < Width - 16 AndAlso MouseYLoc < 22 Then
                            .FillRectangle(New SolidBrush(_HoverColour), New Rectangle(Width - 39, 0, 23, 22))
                        ElseIf MouseXLoc > Width - 64 AndAlso MouseXLoc < Width - 41 AndAlso MouseYLoc < 22 Then
                            .FillRectangle(New SolidBrush(_HoverColour), New Rectangle(Width - 64, 0, 23, 22))
                        ElseIf MouseXLoc > Width - 89 AndAlso MouseXLoc < Width - 66 AndAlso MouseYLoc < 22 Then
                            .FillRectangle(New SolidBrush(_HoverColour), New Rectangle(Width - 89, 0, 23, 22))
                        End If
                End Select
                .DrawLine(New Pen(_BorderColour), Width - 40, 0, Width - 40, 22)
                ''Close Button
                .DrawLine(New Pen(_FontColour), Width - 33, 6, Width - 22, 16)
                .DrawLine(New Pen(_FontColour), Width - 33, 16, Width - 22, 6)
                ''Minimize Button
                .DrawLine(New Pen(_FontColour), Width - 83, 16, Width - 72, 16)
                ''Maximize Button
                .DrawLine(New Pen(_FontColour), Width - 58, 16, Width - 47, 16)
                .DrawLine(New Pen(_FontColour), Width - 58, 16, Width - 58, 6)
                .DrawLine(New Pen(_FontColour), Width - 47, 16, Width - 47, 6)
                .DrawLine(New Pen(_FontColour), Width - 58, 6, Width - 47, 6)
                .DrawLine(New Pen(_FontColour), Width - 58, 7, Width - 47, 7)
                If _ShowIcon Then
                    .DrawIcon(FindForm.Icon, New Rectangle(6, 6, 22, 22))
                    .DrawString(Text, _Font, New SolidBrush(_FontColour), New RectangleF(31, 0, Width - 110, 35), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
                Else
                    .DrawString(Text, _Font, New SolidBrush(_FontColour), New RectangleF(3, 0, Width - 110, 35), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
                End If
                .InterpolationMode = CType(7, InterpolationMode)
            End With
        End Sub

#End Region

    End Class

    <DefaultEvent("CheckedChanged"), InitializationEvent("CheckedState")> _
    Public Class LogInCheckBox
        Inherits Control

#Region "Declarations"
        Private _Checked As Boolean
        Private State As MouseState = MouseState.None
        Private _CheckedColour As Color = Color.FromArgb(173, 173, 174)
        Private _BorderColour As Color = Color.DimGray
        Private _BackColour As Color = Color.FromArgb(47, 47, 47)
        Private _TextColour As Color = Color.FromArgb(255, 255, 255)
#End Region

#Region "Colour & Other Properties"

        <Category("Colours")>
        Public Property BaseColour As Color
            Get
                Return _BackColour
            End Get
            Set(value As Color)
                _BackColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property CheckedColour As Color
            Get
                Return _CheckedColour
            End Get
            Set(value As Color)
                _CheckedColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property FontColour As Color
            Get
                Return _TextColour
            End Get
            Set(value As Color)
                _TextColour = value
            End Set
        End Property

        Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
            MyBase.OnTextChanged(e)
            Invalidate()
        End Sub

        Property Checked() As Boolean
            Get
                Return _Checked
            End Get
            Set(ByVal value As Boolean)
                _Checked = value
                Invalidate()
            End Set
        End Property

        Event CheckedChanged(ByVal sender As Object)
        Protected Overrides Sub OnClick(ByVal e As EventArgs)
            _Checked = Not _Checked
            RaiseEvent CheckedChanged(Me)
            MyBase.OnClick(e)
        End Sub


        'Public Delegate Sub CheckedChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)

        '<Category("Configuration"), Browsable(True), Description("CheckedChanged")> _
        'Public Event CheckedChanged As CheckedChangedHandler

        '<Category("Configuration"), Browsable(True), Description("CheckedState")> _
        'Public Event CheckedState(ByVal sender As Object)

        'Protected Overridable Sub OnCheckedChanged(ByVal e As System.EventArgs)
        '    RaiseEvent CheckedChanged(Me, e)
        'End Sub

        'Property Checked() As Boolean
        '    Get
        '        Return _Checked
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _Checked = value
        '        OnCheckedChanged(New CheckedArgs(_Checked))
        '        Invalidate()
        '    End Set
        'End Property

        'Private _CheckState As CheckState
        'Public Property CheckState As CheckState
        '    Get
        '        Return _CheckState
        '    End Get
        '    Set(ByVal V As CheckState)
        '        _CheckState = V
        '        RaiseEvent CheckedState(Me)
        '        Invalidate()
        '    End Set
        'End Property

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Height = 22
        End Sub
#End Region

#Region "Mouse States"

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            State = MouseState.Down : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            State = MouseState.Over : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None : Invalidate()
        End Sub

#End Region

#Region "Draw Control"
        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                       ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)
            DoubleBuffered = True
            Cursor = Cursors.Hand
            Size = New Size(100, 22)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g = e.Graphics
            Dim Base As New Rectangle(0, 0, 19, 19)
            With g
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .Clear(Color.FromArgb(54, 54, 54))
                .FillRectangle(New SolidBrush(_BackColour), Base)
                .DrawRectangle(New Pen(_BorderColour), New Rectangle(1, 1, 19, 19))
                Select Case State
                    Case MouseState.Over
                        .FillRectangle(New SolidBrush(Color.FromArgb(50, 49, 51)), Base)
                        .DrawRectangle(New Pen(_BorderColour), New Rectangle(1, 1, 19, 19))
                End Select
                If Checked Then
                    Dim P() As Point = {New Point(4, 11), New Point(6, 8), New Point(9, 12), New Point(15, 3), New Point(17, 6), New Point(9, 16)}
                    _CheckedColour = If(Me.Enabled = False, Color.Gray, Color.BlueViolet)
                    .FillPolygon(New SolidBrush(_CheckedColour), P)
                End If
                _TextColour = If(Me.Parent.Enabled = False, Color.Gray, Color.FromArgb(255, 255, 255))
                .DrawString(Text, Font, New SolidBrush(_TextColour), New Rectangle(24, 1, Width, Height - 2), New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
                .InterpolationMode = CType(7, InterpolationMode)
            End With
        End Sub
#End Region

    End Class

    Public Class LogInNormalTextBox
        Inherits Control

#Region "Declarations"
        Private State As MouseState = MouseState.None
        Private WithEvents TB As Windows.Forms.TextBox
        Private _BaseColour As Color = Color.FromArgb(42, 42, 42)
        Private _TextColour As Color = Color.FromArgb(255, 255, 255)
        Private _BorderColour As Color = Color.FromArgb(35, 35, 35)
        Private _Style As Styles = Styles.NotRounded
        Private _TextAlign As HorizontalAlignment = HorizontalAlignment.Left
        Private _MaxLength As Integer = 32767
        Private _ReadOnly As Boolean
        Private _UseSystemPasswordChar As Boolean
        Private _Multiline As Boolean
#End Region

#Region "TextBox Properties"

        Enum Styles
            Rounded
            NotRounded
        End Enum

        <Category("Options")>
        Property TextAlign() As HorizontalAlignment
            Get
                Return _TextAlign
            End Get
            Set(ByVal value As HorizontalAlignment)
                _TextAlign = value
                If TB IsNot Nothing Then
                    TB.TextAlign = value
                End If
            End Set
        End Property

        <Category("Options")>
        Property MaxLength() As Integer
            Get
                Return _MaxLength
            End Get
            Set(ByVal value As Integer)
                _MaxLength = value
                If TB IsNot Nothing Then
                    TB.MaxLength = value
                End If
            End Set
        End Property

        <Category("Options")>
        Property [ReadOnly]() As Boolean
            Get
                Return _ReadOnly
            End Get
            Set(ByVal value As Boolean)
                _ReadOnly = value
                If TB IsNot Nothing Then
                    TB.ReadOnly = value
                End If
            End Set
        End Property

        <Category("Options")>
        Property UseSystemPasswordChar() As Boolean
            Get
                Return _UseSystemPasswordChar
            End Get
            Set(ByVal value As Boolean)
                _UseSystemPasswordChar = value
                If TB IsNot Nothing Then
                    TB.UseSystemPasswordChar = value
                End If
            End Set
        End Property

        <Category("Options")>
        Property Multiline() As Boolean
            Get
                Return _Multiline
            End Get
            Set(ByVal value As Boolean)
                _Multiline = value
                If TB IsNot Nothing Then
                    TB.Multiline = value

                    If value Then
                        TB.Height = Height - 11
                    Else
                        Height = TB.Height + 11
                    End If

                End If
            End Set
        End Property

        <Category("Options")>
        Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                MyBase.Text = value
                If TB IsNot Nothing Then
                    TB.Text = value
                End If
            End Set
        End Property

        <Category("Options")>
        Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(ByVal value As Font)
                MyBase.Font = value
                If TB IsNot Nothing Then
                    TB.Font = value
                    TB.Location = New Point(3, 5)
                    TB.Width = Width - 6

                    If Not _Multiline Then
                        Height = TB.Height + 11
                    End If
                End If
            End Set
        End Property

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            If Not Controls.Contains(TB) Then
                Controls.Add(TB)
            End If
        End Sub

        Private Sub OnBaseTextChanged(ByVal s As Object, ByVal e As EventArgs)
            Text = TB.Text
        End Sub

        Private Sub OnBaseKeyDown(ByVal s As Object, ByVal e As KeyEventArgs)
            If e.Control AndAlso e.KeyCode = Keys.A Then
                TB.SelectAll()
                e.SuppressKeyPress = True
            End If
            If e.Control AndAlso e.KeyCode = Keys.C Then
                TB.Copy()
                e.SuppressKeyPress = True
            End If
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            TB.Location = New Point(5, 5)
            TB.Width = Width - 10

            If _Multiline Then
                TB.Height = Height - 11
            Else
                Height = TB.Height + 11
            End If

            MyBase.OnResize(e)
        End Sub

        Public Property Style As Styles
            Get
                Return _Style
            End Get
            Set(value As Styles)
                _Style = value
            End Set
        End Property

        Public Sub SelectAll()
            TB.Focus()
            TB.SelectAll()
        End Sub


#End Region

#Region "Colour Properties"

        <Category("Colours")>
        Public Property BackgroundColour As Color
            Get
                Return _BaseColour
            End Get
            Set(value As Color)
                _BaseColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property TextColour As Color
            Get
                Return _TextColour
            End Get
            Set(value As Color)
                _TextColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

#End Region

#Region "Mouse States"

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            State = MouseState.Down : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            State = MouseState.Over : TB.Focus() : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None : Invalidate()
        End Sub

#End Region

#Region "Draw Control"
        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                     ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                     ControlStyles.SupportsTransparentBackColor, True)
            DoubleBuffered = True
            BackColor = Color.Transparent
            TB = New Windows.Forms.TextBox
            TB.Height = 190
            TB.Font = New Font("Segoe UI", 9)
            TB.Text = Text
            TB.BackColor = Color.FromArgb(42, 42, 42)
            TB.ForeColor = Color.FromArgb(255, 255, 255)
            TB.MaxLength = _MaxLength
            TB.Multiline = False
            TB.ReadOnly = _ReadOnly
            TB.UseSystemPasswordChar = _UseSystemPasswordChar
            TB.BorderStyle = BorderStyle.None
            TB.Location = New Point(5, 5)
            TB.Width = Width - 35
            AddHandler TB.TextChanged, AddressOf OnBaseTextChanged
            AddHandler TB.KeyDown, AddressOf OnBaseKeyDown
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g = e.Graphics
            Dim GP As GraphicsPath
            Dim Base As New Rectangle(0, 0, Width, Height)
            With g
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .Clear(BackColor)
                TB.BackColor = Color.FromArgb(42, 42, 42)
                TB.ForeColor = Color.FromArgb(255, 255, 255)
                Select Case _Style
                    Case Styles.Rounded
                        GP = RoundRectangle(Base, 6)
                        .FillPath(New SolidBrush(Color.FromArgb(42, 42, 42)), GP)
                        .DrawPath(New Pen(New SolidBrush(Color.FromArgb(35, 35, 35)), 2), GP)
                        GP.Dispose()
                    Case Styles.NotRounded
                        .FillRectangle(New SolidBrush(Color.FromArgb(42, 42, 42)), New Rectangle(0, 0, Width - 1, Height - 1))
                        .DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(35, 35, 35)), 2), New Rectangle(0, 0, Width, Height))
                End Select
                .InterpolationMode = CType(7, InterpolationMode)
            End With
        End Sub

#End Region

    End Class

#Region "Sh@rp : RadioButton EventArgs Class Added "
    Public Class CheckedArgs
        Inherits EventArgs
        Private _Checked As Boolean
        Public Sub New(ByVal theEventChecked As Boolean)
            _Checked = theEventChecked
        End Sub
        Public Property Checked As Boolean
            Get
                Return Me._Checked
            End Get
            Set(ByVal Value As Boolean)
                _Checked = Value
            End Set
        End Property
    End Class
#End Region

    <DefaultEvent("CheckedChanged"), InitializationEvent("CheckedState")> _
    Public Class LogInRadioButton
        Inherits Control

#Region "Declarations"
        Private _Checked As Boolean
        Private State As MouseState = MouseState.None
        Private _HoverColour As Color = Color.FromArgb(50, 49, 51)
        Private _CheckedColour As Color = Color.FromArgb(173, 173, 174)
        Private _BorderColour As Color = Color.DimGray
        Private _BackColour As Color = Color.FromArgb(54, 54, 54)
        Private _TextColour As Color = Color.FromArgb(255, 255, 255)
#End Region

#Region "Colour & Other Properties"

        <Category("Colours")>
        Public Property HighlightColour As Color
            Get
                Return _HoverColour
            End Get
            Set(value As Color)
                _HoverColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BaseColour As Color
            Get
                Return _BackColour
            End Get
            Set(value As Color)
                _BackColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property CheckedColour As Color
            Get
                Return _CheckedColour
            End Get
            Set(value As Color)
                _CheckedColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property FontColour As Color
            Get
                Return _TextColour
            End Get
            Set(value As Color)
                _TextColour = value
            End Set
        End Property

#Region "Sh@rp : RadioButton CheckedState Added "
        Public Delegate Sub CheckedChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)

        <Category("Configuration"), Browsable(True), Description("CheckedChanged")> _
        Public Event CheckedChanged As CheckedChangedHandler

        <Category("Configuration"), Browsable(True), Description("CheckedState")> _
        Public Event CheckedState(ByVal sender As Object)

        Protected Overridable Sub OnCheckedChanged(ByVal e As System.EventArgs)
            RaiseEvent CheckedChanged(Me, e)
        End Sub

        Private _CheckState As CheckState
        Public Property CheckState As CheckState
            Get
                Return _CheckState
            End Get
            Set(ByVal V As CheckState)
                _CheckState = V
                RaiseEvent CheckedState(Me)
                Invalidate()
            End Set
        End Property
#End Region

        Property Checked() As Boolean
            Get
                Return _Checked
            End Get
            Set(value As Boolean)
                _Checked = value
                InvalidateControls()
                RaiseEvent CheckedChanged(Me, New EventArgs)
                Invalidate()
            End Set
        End Property

        Protected Overrides Sub OnClick(e As EventArgs)
            If Not _Checked Then Checked = True
            MyBase.OnClick(e)
        End Sub

        Private Sub InvalidateControls()
            If Not IsHandleCreated OrElse Not _Checked Then Return
            For Each C As Control In Parent.Controls
                If C IsNot Me AndAlso TypeOf C Is LogInRadioButton Then
                    DirectCast(C, LogInRadioButton).Checked = False
                    Invalidate()
                End If
            Next
        End Sub
        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            InvalidateControls()
        End Sub
        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Height = 18
        End Sub
#End Region

#Region "Mouse States"

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            State = MouseState.Down : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            State = MouseState.Over : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None : Invalidate()
        End Sub

#End Region

#Region "Draw Control"
        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                       ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)
            DoubleBuffered = True
            Cursor = Cursors.Hand
            Size = New Size(100, 18)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G = e.Graphics
            Dim Base As New Rectangle(1, 1, Height - 2, Height - 2)
            Dim Circle As New Rectangle(6, 6, Height - 12, Height - 12)
            With G
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .Clear(_BackColour)
                .FillEllipse(New SolidBrush(Color.FromArgb(47, 47, 47)), Base)
                .DrawEllipse(New Pen(_BorderColour, 1), Base)
                If Checked Then
                    Select Case State
                        Case MouseState.Over
                            .FillEllipse(New SolidBrush(_HoverColour), New Rectangle(2, 2, Height - 4, Height - 4))
                    End Select
                    _CheckedColour = If(Me.Enabled = False, Color.Gray, Color.BlueViolet)
                    .FillEllipse(New SolidBrush(_CheckedColour), Circle)
                Else
                    Select Case State
                        Case MouseState.Over
                            .FillEllipse(New SolidBrush(_HoverColour), New Rectangle(2, 2, Height - 4, Height - 4))
                    End Select
                End If
                _TextColour = If(Me.Parent.Enabled = False, Color.Gray, Color.FromArgb(255, 255, 255))
                .DrawString(Text, Font, New SolidBrush(_TextColour), New Rectangle(24, 0, Width, Height), New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})
                .InterpolationMode = CType(7, InterpolationMode)
            End With
        End Sub
#End Region

    End Class

    Public Class LogInLabel
        Inherits Label

#Region "Declaration"
        Private _FontColour As Color = Color.FromArgb(255, 255, 255)
#End Region

#Region "Property & Event"

        <Category("Colours")>
        Public Property FontColour As Color
            Get
                Return _FontColour
            End Get
            Set(value As Color)
                _FontColour = value
            End Set
        End Property

        Protected Overrides Sub OnTextChanged(e As EventArgs)
            MyBase.OnTextChanged(e) : Invalidate()
        End Sub

#End Region

#Region "Draw Control"

        Sub New()
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            Font = New Font("Segoe UI", 9)
            ForeColor = _FontColour
            BackColor = Color.Transparent
            Text = Text
        End Sub

#End Region

    End Class

    Public Class LogInButton
        Inherits Control

#Region "Declarations"
        Private ReadOnly _Font As New Font("Segoe UI", 9)
        Private _ProgressColour As Color = Color.FromArgb(0, 191, 255)
        Private _BorderColour As Color = Color.FromArgb(25, 25, 25)
        Private _FontColour As Color = Color.FromArgb(255, 255, 255)
        Private _MainColour As Color = Color.FromArgb(42, 42, 42)
        Private _HoverColour As Color = Color.FromArgb(52, 52, 52)
        Private _PressedColour As Color = Color.FromArgb(47, 47, 47)
        Private State As New MouseState
#End Region

#Region "Mouse States"

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            State = MouseState.Down : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            State = MouseState.Over : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over : Invalidate()
        End Sub
        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None : Invalidate()
        End Sub

#End Region

#Region "Properties"

        <Category("Colours")>
        Public Property ProgressColour As Color
            Get
                Return _ProgressColour
            End Get
            Set(value As Color)
                _ProgressColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property FontColour As Color
            Get
                Return _FontColour
            End Get
            Set(value As Color)
                _FontColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BaseColour As Color
            Get
                Return _MainColour
            End Get
            Set(value As Color)
                _MainColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property HoverColour As Color
            Get
                Return _HoverColour
            End Get
            Set(value As Color)
                _HoverColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property PressedColour As Color
            Get
                Return _PressedColour
            End Get
            Set(value As Color)
                _PressedColour = value
            End Set
        End Property

#End Region

#Region "Draw Control"
        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                  ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                  ControlStyles.SupportsTransparentBackColor, True)
            DoubleBuffered = True
            Size = New Size(75, 30)
            BackColor = Color.Transparent
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G = e.Graphics
            With G
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                '.SmoothingMode = SmoothingMode.HighQuality
                '.PixelOffsetMode = PixelOffsetMode.HighQuality

                .Clear(BackColor)
                Dim bru As Brush = Brushes.White
                bru = If(Me.Enabled = False, Brushes.Gray, Brushes.White)
                Select Case State
                    Case MouseState.None
                        .FillRectangle(New SolidBrush(_MainColour), New Rectangle(0, 0, Width, Height))
                        .DrawRectangle(New Pen(_BorderColour, 2), New Rectangle(0, 0, Width, Height))
                        .DrawString(Text, _Font, bru, New Point(CInt(Width / 2), CInt(Height / 2)), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                    Case MouseState.Over
                        .FillRectangle(New SolidBrush(_HoverColour), New Rectangle(0, 0, Width, Height))
                        .DrawRectangle(New Pen(_BorderColour, 1), New Rectangle(1, 1, Width - 2, Height - 2))
                        .DrawString(Text, _Font, bru, New Point(CInt(Width / 2), CInt(Height / 2)), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                    Case MouseState.Down
                        .FillRectangle(New SolidBrush(_PressedColour), New Rectangle(0, 0, Width, Height))
                        .DrawRectangle(New Pen(_BorderColour, 1), New Rectangle(1, 1, Width - 2, Height - 2))
                        .DrawString(Text, _Font, bru, New Point(CInt(Width / 2), CInt(Height / 2)), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                End Select

                .InterpolationMode = CType(7, InterpolationMode)
            End With

        End Sub

#End Region

    End Class

    Public Class LogInGroupBox
        Inherits ContainerControl

#Region "Declarations"
        Private _MainColour As Color = Color.FromArgb(47, 47, 47)
        Private _HeaderColour As Color = Color.FromArgb(42, 42, 42)
        Private _TextColour As Color = Color.FromArgb(255, 255, 255)
        'Private _BorderColour As Color = Color.FromArgb(35, 35, 35)
        Private _BorderColour As Color = Color.DimGray
#End Region

#Region "Properties"

        <Category("Colours")>
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property TextColour As Color
            Get
                Return _TextColour
            End Get
            Set(value As Color)
                _TextColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property HeaderColour As Color
            Get
                Return _HeaderColour
            End Get
            Set(value As Color)
                _HeaderColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property MainColour As Color
            Get
                Return _MainColour
            End Get
            Set(value As Color)
                _MainColour = value
            End Set
        End Property

#End Region

#Region "Draw Control"
        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                   ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                   ControlStyles.SupportsTransparentBackColor, True)
            DoubleBuffered = True
            Size = New Size(160, 110)
            Font = New Font("Segoe UI", 9, FontStyle.Bold)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g = e.Graphics
            With g
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .Clear(Color.FromArgb(54, 54, 54))
                .FillRectangle(New SolidBrush(_MainColour), New Rectangle(0, 28, Width, Height))
                _TextColour = If(Me.Enabled = False, Color.Gray, Color.FromArgb(255, 255, 255))
                .FillRectangle(New SolidBrush(_HeaderColour), New Rectangle(New Point(0, 0), New Size(Width + 10, CInt(.MeasureString(Text, Font).Height + 10))))
                .DrawString(Text, Font, New SolidBrush(_TextColour), New Point(5, 5))

                Dim P() As Point = {New Point(0, 0), New Point(CInt(Me.Width), 0), New Point(CInt(Me.Width), Me.Height), _
                                 New Point(Width, Me.Height), New Point(Width, Height), New Point(0, Height), New Point(0, 0)}
                .DrawLines(New Pen(_BorderColour), P)
                .InterpolationMode = CType(7, InterpolationMode)
            End With
        End Sub
#End Region

    End Class

    Public Class LogInColourTable
        Inherits ProfessionalColorTable

#Region "Declarations"

        Private _BackColour As Color = Color.FromArgb(42, 42, 42)
        Private _BorderColour As Color = Color.FromArgb(35, 35, 35)
        Private _SelectedColour As Color = Color.FromArgb(47, 47, 47)

#End Region

#Region "Properties"

        <Category("Colours")>
        Public Property SelectedColour As Color
            Get
                Return _SelectedColour
            End Get
            Set(value As Color)
                _SelectedColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BackColour As Color
            Get
                Return _BackColour
            End Get
            Set(value As Color)
                _BackColour = value
            End Set
        End Property

        Public Overrides ReadOnly Property ButtonSelectedBorder() As Color
            Get
                Return _BackColour
            End Get
        End Property

        Public Overrides ReadOnly Property CheckBackground() As Color
            Get
                Return _BackColour
            End Get
        End Property

        Public Overrides ReadOnly Property CheckPressedBackground() As Color
            Get
                Return _BackColour
            End Get
        End Property

        Public Overrides ReadOnly Property CheckSelectedBackground() As Color
            Get
                Return _BackColour
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientBegin() As Color
            Get
                Return _BackColour
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientEnd() As Color
            Get
                Return _BackColour
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientMiddle() As Color
            Get
                Return _BackColour
            End Get
        End Property

        Public Overrides ReadOnly Property MenuBorder() As Color
            Get
                Return _BorderColour
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemBorder() As Color
            Get
                Return _BackColour
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelected() As Color
            Get
                Return _SelectedColour
            End Get
        End Property

        Public Overrides ReadOnly Property SeparatorDark() As Color
            Get
                Return _BorderColour
            End Get
        End Property

        Public Overrides ReadOnly Property ToolStripDropDownBackground() As Color
            Get
                Return _BackColour
            End Get
        End Property

#End Region

    End Class

    Public Class LogInComboBox
        Inherits ComboBox

#Region "Declarations"
        'Private _StartIndex As Integer = 0
        Private _BorderColour As Color = Color.FromArgb(35, 35, 35)
        Private _BaseColour As Color = Color.FromArgb(42, 42, 42)
        Private _FontColour As Color = Color.FromArgb(255, 255, 255)
        Private _LineColour As Color = Color.FromArgb(23, 119, 151)
        Private _SqaureColour As Color = Color.FromArgb(47, 47, 47)
        Private _ArrowColour As Color = Color.FromArgb(30, 30, 30)
        Private _SqaureHoverColour As Color = Color.FromArgb(52, 52, 52)
        Private State As MouseState = MouseState.None
#End Region

#Region "Properties & Events"

        <Category("Colours")>
        Public Property LineColour As Color
            Get
                Return _LineColour
            End Get
            Set(value As Color)
                _LineColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property SqaureColour As Color
            Get
                Return _SqaureColour
            End Get
            Set(value As Color)
                _SqaureColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property ArrowColour As Color
            Get
                Return _ArrowColour
            End Get
            Set(value As Color)
                _ArrowColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property SqaureHoverColour As Color
            Get
                Return _SqaureHoverColour
            End Get
            Set(value As Color)
                _SqaureHoverColour = value
            End Set
        End Property

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            State = MouseState.Over : Invalidate()
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            State = MouseState.None : Invalidate()
        End Sub

        <Category("Colours")>
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BaseColour As Color
            Get
                Return _BaseColour
            End Get
            Set(value As Color)
                _BaseColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property FontColour As Color
            Get
                Return _FontColour
            End Get
            Set(value As Color)
                _FontColour = value
            End Set
        End Property

        'Public Property StartIndex As Integer
        '    Get
        '        Return _StartIndex
        '    End Get
        '    Set(ByVal value As Integer)
        '        _StartIndex = value
        '        MyBase.SelectedIndex = value
        '        Invalidate()
        '    End Set
        'End Property

        Protected Overrides Sub OnSelectedItemChanged(e As System.EventArgs)
            MyBase.OnSelectedItemChanged(e)
        End Sub

        Protected Overrides Sub OnTextChanged(e As System.EventArgs)
            MyBase.OnTextChanged(e)
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            Invalidate()
            OnMouseClick(e)
        End Sub

        Protected Overrides Sub OnMouseUp(e As System.Windows.Forms.MouseEventArgs)
            Invalidate()
            MyBase.OnMouseUp(e)
        End Sub

#End Region

#Region "Draw Control"

        Sub ReplaceItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
            e.DrawBackground()
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
            Dim Rect As New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 1, e.Bounds.Height + 1)
            With e.Graphics
                If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                    .FillRectangle(New SolidBrush(_SqaureColour), Rect)
                    .DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), Font, New SolidBrush(_FontColour), 1, e.Bounds.Top + 2)
                Else
                    .FillRectangle(New SolidBrush(_BaseColour), Rect)
                    .DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), Font, New SolidBrush(_FontColour), 1, e.Bounds.Top + 2)
                End If
            End With
            e.DrawFocusRectangle()
            Invalidate()
        End Sub

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                   ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                   ControlStyles.SupportsTransparentBackColor, True)
            DoubleBuffered = True
            BackColor = Color.Transparent
            DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
            DropDownStyle = ComboBoxStyle.DropDownList
            Width = 163
            Font = New Font("Segoe UI", 9)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim g = e.Graphics
            With g
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .Clear(BackColor)

                Dim Square As New Rectangle(Width - 25, 0, Width, Height)
                .FillRectangle(New SolidBrush(_BaseColour), New Rectangle(0, 0, Width - 25, Height))
                Select Case State
                    Case MouseState.None
                        .FillRectangle(New SolidBrush(_SqaureColour), Square)
                    Case MouseState.Over
                        .FillRectangle(New SolidBrush(_SqaureHoverColour), Square)
                End Select
                .DrawLine(New Pen(_LineColour, 2), New Point(Width - 26, 1), New Point(Width - 26, Height - 1))
                If Me.Parent.Enabled = False Then
                    _FontColour = Color.Gray
                Else
                    _FontColour = Color.FromArgb(255, 255, 255)
                End If
                If SelectedIndex <> -1 Then
                    .DrawString(Text, Font, New SolidBrush(_FontColour), New Rectangle(3, 0, Width - 20, Height), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
                Else
                    If Not Items Is Nothing And Items.Count > 0 Then
                        SelectedIndex = 0
                        .DrawString(Items(0).ToString, Font, New SolidBrush(_FontColour), New Rectangle(3, 0, Width - 20, Height), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
                    End If
                End If
                .DrawRectangle(New Pen(_BorderColour, 2), New Rectangle(0, 0, Width, Height))
                Dim P() As Point = {New Point(Width - 17, 11), New Point(Width - 13, 5), New Point(Width - 9, 11)}
                .FillPolygon(New SolidBrush(_BorderColour), P)
                .DrawPolygon(New Pen(_ArrowColour), P)
                Dim P1() As Point = {New Point(Width - 17, 15), New Point(Width - 13, 21), New Point(Width - 9, 15)}
                .FillPolygon(New SolidBrush(_BorderColour), P1)
                .DrawPolygon(New Pen(_ArrowColour), P1)
                .InterpolationMode = CType(7, InterpolationMode)
            End With

        End Sub

#End Region

    End Class

    Public Class LogInTabControl
        Inherits TabControl

#Region "Declarations"

        Private _TextColour As Color = Color.FromArgb(255, 255, 255)
        Private _BackTabColour As Color = Color.FromArgb(54, 54, 54)
        Private _BaseColour As Color = Color.FromArgb(35, 35, 35)
        Private _ActiveColour As Color = Color.FromArgb(47, 47, 47)
        Private _BorderColour As Color = Color.FromArgb(30, 30, 30)
        Private _UpLineColour As Color = Color.FromArgb(0, 160, 199)
        Private _HorizLineColour As Color = Color.FromArgb(23, 119, 151)
        Private CenterSF As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}

#End Region

#Region "Properties"

        <Category("Colours")> _
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property UpLineColour As Color
            Get
                Return _UpLineColour
            End Get
            Set(value As Color)
                _UpLineColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property HorizontalLineColour As Color
            Get
                Return _HorizLineColour
            End Get
            Set(value As Color)
                _HorizLineColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property TextColour As Color
            Get
                Return _TextColour
            End Get
            Set(value As Color)
                _TextColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property BackTabColour As Color
            Get
                Return _BackTabColour
            End Get
            Set(value As Color)
                _BackTabColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property BaseColour As Color
            Get
                Return _BaseColour
            End Get
            Set(value As Color)
                _BaseColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property ActiveColour As Color
            Get
                Return _ActiveColour
            End Get
            Set(value As Color)
                _ActiveColour = value
            End Set
        End Property

        Protected Overrides Sub CreateHandle()
            MyBase.CreateHandle()
            Alignment = TabAlignment.Top
        End Sub

#End Region

#Region "Draw Control"

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                     ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
            DoubleBuffered = True
            Font = New Font("Segoe UI", 9)
            SizeMode = TabSizeMode.Normal
            ItemSize = New Size(240, 32)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g = e.Graphics
            With g
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .Clear(_BaseColour)
                Try : SelectedTab.BackColor = _BackTabColour : Catch : End Try
                Try : SelectedTab.BorderStyle = BorderStyle.FixedSingle : Catch : End Try
                .DrawRectangle(New Pen(_BorderColour, 2), New Rectangle(0, 0, Width, Height))
                For i = 0 To TabCount - 1
                    Dim Base As New Rectangle(New Point(GetTabRect(i).Location.X, GetTabRect(i).Location.Y), New Size(GetTabRect(i).Width, GetTabRect(i).Height))
                    Dim BaseSize As New Rectangle(Base.Location, New Size(Base.Width, Base.Height))
                    If i = SelectedIndex Then
                        _TextColour = If(Me.Enabled = False, Color.Gray, Color.White)
                        .FillRectangle(New SolidBrush(_BaseColour), BaseSize)
                        .FillRectangle(New SolidBrush(_ActiveColour), New Rectangle(Base.X + 1, Base.Y - 3, Base.Width, Base.Height + 5))
                        .DrawString(TabPages(i).Text, Font, New SolidBrush(_TextColour), New Rectangle(Base.X + 7, Base.Y, Base.Width - 3, Base.Height), CenterSF)
                        .DrawLine(New Pen(_HorizLineColour, 2), New Point(Base.X + 3, CInt(Base.Height / 2 + 2)), New Point(Base.X + 9, CInt(Base.Height / 2 + 2)))
                        .DrawLine(New Pen(_UpLineColour, 2), New Point(Base.X + 3, Base.Y - 3), New Point(Base.X + 3, Base.Height + 5))
                    Else
                        _TextColour = If(Me.Enabled = False, Color.Gray, Color.White)
                        .DrawString(TabPages(i).Text, Font, New SolidBrush(_TextColour), BaseSize, CenterSF)
                    End If
                Next
                Dim P() As Point = {New Point(0, 0), New Point(CInt(Me.Width), 0), New Point(CInt(Me.Width), Me.Height), _
                               New Point(Width, Me.Height), New Point(Width, Height), New Point(0, Height), New Point(0, 0)}
                .DrawLines(New Pen(_BorderColour), P)
                .InterpolationMode = InterpolationMode.HighQualityBicubic
            End With
        End Sub

#End Region

    End Class

    <DefaultEvent("Scroll")>
    Public Class LogInVerticalScrollBar
        Inherits Control

#Region "Declarations"

        Private ThumbMovement As Integer
        Private TSA As Rectangle
        Private BSA As Rectangle
        Private Shaft As Rectangle
        Private Thumb As Rectangle
        Private ShowThumb As Boolean
        Private ThumbPressed As Boolean
        Private _ThumbSize As Integer = 24
        Public _Minimum As Integer = 0
        Public _Maximum As Integer = 100
        Public _Value As Integer = 0
        Public _SmallChange As Integer = 1
        Private _ButtonSize As Integer = 16
        Public _LargeChange As Integer = 10
        Private _ThumbBorder As Color = Color.FromArgb(35, 35, 35)
        Private _LineColour As Color = Color.FromArgb(23, 119, 151)
        Private _ArrowColour As Color = Color.FromArgb(37, 37, 37)
        Private _BaseColour As Color = Color.FromArgb(47, 47, 47)
        Private _ThumbColour As Color = Color.FromArgb(55, 55, 55)
        Private _ThumbSecondBorder As Color = Color.FromArgb(65, 65, 65)
        Private _FirstBorder As Color = Color.FromArgb(55, 55, 55)
        Private _SecondBorder As Color = Color.FromArgb(35, 35, 35)

#End Region

#Region "Properties & Events"

        <Category("Colours")> _
        Public Property ThumbBorder As Color
            Get
                Return _ThumbBorder
            End Get
            Set(value As Color)
                _ThumbBorder = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property LineColour As Color
            Get
                Return _LineColour
            End Get
            Set(value As Color)
                _LineColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property ArrowColour As Color
            Get
                Return _ArrowColour
            End Get
            Set(value As Color)
                _ArrowColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property BaseColour As Color
            Get
                Return _BaseColour
            End Get
            Set(value As Color)
                _BaseColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property ThumbColour As Color
            Get
                Return _ThumbColour
            End Get
            Set(value As Color)
                _ThumbColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property ThumbSecondBorder As Color
            Get
                Return _ThumbSecondBorder
            End Get
            Set(value As Color)
                _ThumbSecondBorder = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property FirstBorder As Color
            Get
                Return _FirstBorder
            End Get
            Set(value As Color)
                _FirstBorder = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property SecondBorder As Color
            Get
                Return _SecondBorder
            End Get
            Set(value As Color)
                _SecondBorder = value
            End Set
        End Property

        Event Scroll(ByVal sender As Object)

        Property Minimum() As Integer
            Get
                Return _Minimum
            End Get
            Set(ByVal value As Integer)
                _Minimum = value
                If value > _Value Then _Value = value
                If value > _Maximum Then _Maximum = value
                InvalidateLayout()
            End Set
        End Property

        Property Maximum() As Integer
            Get
                Return _Maximum
            End Get
            Set(ByVal value As Integer)
                If value < _Value Then _Value = value
                If value < _Minimum Then _Minimum = value
            End Set
        End Property

        Property Value() As Integer
            Get
                Return _Value
            End Get
            Set(ByVal value As Integer)
                Select Case value
                    Case Is = _Value
                        Exit Property
                    Case Is < _Minimum
                        _Value = _Minimum
                    Case Is > _Maximum
                        _Value = _Maximum
                    Case Else
                        _Value = value
                End Select
                InvalidatePosition()
                RaiseEvent Scroll(Me)
            End Set
        End Property

        Public Property SmallChange() As Integer
            Get
                Return _SmallChange
            End Get
            Set(ByVal value As Integer)
                Select Case value
                    Case Is < 1
                    Case Is >
                        CInt(_SmallChange = value)
                End Select
            End Set
        End Property

        Public Property LargeChange() As Integer
            Get
                Return _LargeChange
            End Get
            Set(ByVal value As Integer)
                Select Case value
                    Case Is < 1
                    Case Else
                        _LargeChange = value
                End Select
            End Set
        End Property

        Public Property ButtonSize As Integer
            Get
                Return _ButtonSize
            End Get
            Set(value As Integer)
                Select Case value
                    Case Is < 16
                        _ButtonSize = 16
                    Case Else
                        _ButtonSize = value
                End Select
            End Set
        End Property

        Protected Overrides Sub OnSizeChanged(e As EventArgs)
            InvalidateLayout()
        End Sub

        Private Sub InvalidateLayout()
            TSA = New Rectangle(0, 1, Width, 0)
            Shaft = New Rectangle(0, TSA.Bottom - 1, Width, Height - 3)
            ShowThumb = CBool(((_Maximum - _Minimum)))
            If ShowThumb Then
                Thumb = New Rectangle(1, 0, Width - 3, _ThumbSize)
            End If
            RaiseEvent Scroll(Me)
            InvalidatePosition()
        End Sub

        Private Sub InvalidatePosition()
            Thumb.Y = CInt(((_Value - _Minimum) / (_Maximum - _Minimum)) * (Shaft.Height - _ThumbSize) + 1)
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            If e.Button = Windows.Forms.MouseButtons.Left AndAlso ShowThumb Then
                If TSA.Contains(e.Location) Then
                    ThumbMovement = _Value - _SmallChange
                ElseIf BSA.Contains(e.Location) Then
                    ThumbMovement = _Value + _SmallChange
                Else
                    If Thumb.Contains(e.Location) Then
                        ThumbPressed = True
                        Return
                    Else
                        If e.Y < Thumb.Y Then
                            ThumbMovement = _Value - _LargeChange
                        Else
                            ThumbMovement = _Value + _LargeChange
                        End If
                    End If
                End If
                Value = Math.Min(Math.Max(ThumbMovement, _Minimum), _Maximum)
                InvalidatePosition()
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            If ThumbPressed AndAlso ShowThumb Then
                Dim ThumbPosition As Integer = e.Y - TSA.Height - (_ThumbSize \ 2)
                Dim ThumbBounds As Integer = Shaft.Height - _ThumbSize
                ThumbMovement = CInt((ThumbPosition / ThumbBounds) * (_Maximum - _Minimum)) + _Minimum
                Value = Math.Min(Math.Max(ThumbMovement, _Minimum), _Maximum)
                InvalidatePosition()
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            ThumbPressed = False
        End Sub

#End Region

#Region "Draw Control"

        Sub New()
            SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or _
                                ControlStyles.UserPaint Or ControlStyles.Selectable Or _
                                ControlStyles.SupportsTransparentBackColor, True)
            DoubleBuffered = True
            Size = New Size(24, 50)
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Dim g = e.Graphics
            With g
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .Clear(_BaseColour)
                Dim P() As Point = {New Point(CInt(Width / 2), 5), New Point(CInt(Width / 4), 13), New Point(CInt(Width / 2 - 2), 13), New Point(CInt(Width / 2 - 2), Height - 13), _
                                    New Point(CInt(Width / 4), Height - 13), New Point(CInt(Width / 2), Height - 5), New Point(CInt(Width - Width / 4 - 1), Height - 13), New Point(CInt(Width / 2 + 2), Height - 13), _
                                    New Point(CInt(Width / 2 + 2), 13), New Point(CInt(Width - Width / 4 - 1), 13)}
                .FillPolygon(New SolidBrush(_ArrowColour), P)
                .FillRectangle(New SolidBrush(_ThumbColour), Thumb)
                .DrawRectangle(New Pen(_ThumbBorder), Thumb)
                .DrawRectangle(New Pen(_ThumbSecondBorder), Thumb.X + 1, Thumb.Y + 1, Thumb.Width - 2, Thumb.Height - 2)
                .DrawLine(New Pen(_LineColour, 2), New Point(CInt(Thumb.Width / 2 + 1), Thumb.Y + 4), New Point(CInt(Thumb.Width / 2 + 1), Thumb.Bottom - 4))
                .DrawRectangle(New Pen(_FirstBorder), 0, 0, Width - 1, Height - 1)
                .DrawRectangle(New Pen(_SecondBorder), 1, 1, Width - 3, Height - 3)
                .InterpolationMode = InterpolationMode.HighQualityBicubic
            End With

        End Sub

#End Region

    End Class

    <DefaultEvent("Scroll")> _
    Public Class LogInHorizontalScrollBar
        Inherits Control

#Region "Declarations"

        Private ThumbMovement As Integer
        Private LSA As Rectangle
        Private RSA As Rectangle
        Private Shaft As Rectangle
        Private Thumb As Rectangle
        Private ShowThumb As Boolean
        Private ThumbPressed As Boolean
        Private _ThumbSize As Integer = 24
        Private _Minimum As Integer = 0
        Private _Maximum As Integer = 100
        Private _Value As Integer = 0
        Private _SmallChange As Integer = 1
        Private _ButtonSize As Integer = 16
        Private _LargeChange As Integer = 10
        Private _ThumbBorder As Color = Color.FromArgb(35, 35, 35)
        Private _LineColour As Color = Color.FromArgb(23, 119, 151)
        Private _ArrowColour As Color = Color.FromArgb(37, 37, 37)
        Private _BaseColour As Color = Color.FromArgb(47, 47, 47)
        Private _ThumbColour As Color = Color.FromArgb(55, 55, 55)
        Private _ThumbSecondBorder As Color = Color.FromArgb(65, 65, 65)
        Private _FirstBorder As Color = Color.FromArgb(55, 55, 55)
        Private _SecondBorder As Color = Color.FromArgb(35, 35, 35)
        Private ThumbDown As Boolean = False
#End Region

#Region "Properties & Events"

        <Category("Colours")> _
        Public Property ThumbBorder As Color
            Get
                Return _ThumbBorder
            End Get
            Set(value As Color)
                _ThumbBorder = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property LineColour As Color
            Get
                Return _LineColour
            End Get
            Set(value As Color)
                _LineColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property ArrowColour As Color
            Get
                Return _ArrowColour
            End Get
            Set(value As Color)
                _ArrowColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property BaseColour As Color
            Get
                Return _BaseColour
            End Get
            Set(value As Color)
                _BaseColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property ThumbColour As Color
            Get
                Return _ThumbColour
            End Get
            Set(value As Color)
                _ThumbColour = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property ThumbSecondBorder As Color
            Get
                Return _ThumbSecondBorder
            End Get
            Set(value As Color)
                _ThumbSecondBorder = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property FirstBorder As Color
            Get
                Return _FirstBorder
            End Get
            Set(value As Color)
                _FirstBorder = value
            End Set
        End Property

        <Category("Colours")> _
        Public Property SecondBorder As Color
            Get
                Return _SecondBorder
            End Get
            Set(value As Color)
                _SecondBorder = value
            End Set
        End Property

        Event Scroll(ByVal sender As Object)

        Property Minimum() As Integer
            Get
                Return _Minimum
            End Get
            Set(ByVal value As Integer)
                _Minimum = value
                If value > _Value Then _Value = value
                If value > _Maximum Then _Maximum = value
                InvalidateLayout()
            End Set
        End Property

        Property Maximum() As Integer
            Get
                Return _Maximum
            End Get
            Set(ByVal value As Integer)
                If value < _Value Then _Value = value
                If value < _Minimum Then _Minimum = value
            End Set
        End Property

        Property Value() As Integer
            Get
                Return _Value
            End Get
            Set(ByVal value As Integer)
                Select Case value
                    Case Is = _Value
                        Exit Property
                    Case Is < _Minimum
                        _Value = _Minimum
                    Case Is > _Maximum
                        _Value = _Maximum
                    Case Else
                        _Value = value
                End Select
                InvalidatePosition()
                RaiseEvent Scroll(Me)
            End Set
        End Property

        Public Property SmallChange() As Integer
            Get
                Return _SmallChange
            End Get
            Set(ByVal value As Integer)
                Select Case value
                    Case Is < 1
                    Case Is >
                        CInt(_SmallChange = value)
                End Select
            End Set
        End Property

        Public Property LargeChange() As Integer
            Get
                Return _LargeChange
            End Get
            Set(ByVal value As Integer)
                Select Case value
                    Case Is < 1
                    Case Else
                        _LargeChange = value
                End Select
            End Set
        End Property

        Public Property ButtonSize As Integer
            Get
                Return _ButtonSize
            End Get
            Set(value As Integer)
                Select Case value
                    Case Is < 16
                        _ButtonSize = 16
                    Case Else
                        _ButtonSize = value
                End Select
            End Set
        End Property

        Protected Overrides Sub OnSizeChanged(e As EventArgs)
            InvalidateLayout()
        End Sub

        Private Sub InvalidateLayout()
            LSA = New Rectangle(0, 1, 0, Height)
            Shaft = New Rectangle(LSA.Right + 1, 0, Width - 3, Height)
            ShowThumb = CBool(((_Maximum - _Minimum)))
            Thumb = New Rectangle(0, 1, _ThumbSize, Height - 3)
            RaiseEvent Scroll(Me)
            InvalidatePosition()
        End Sub

        Private Sub InvalidatePosition()
            Thumb.X = CInt(((_Value - _Minimum) / (_Maximum - _Minimum)) * (Shaft.Width - _ThumbSize) + 1)
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            If e.Button = Windows.Forms.MouseButtons.Left AndAlso ShowThumb Then
                If LSA.Contains(e.Location) Then
                    ThumbMovement = _Value - _SmallChange
                ElseIf RSA.Contains(e.Location) Then
                    ThumbMovement = _Value + _SmallChange
                Else
                    If Thumb.Contains(e.Location) Then
                        ThumbDown = True
                        Return
                    Else
                        If e.X < Thumb.X Then
                            ThumbMovement = _Value - _LargeChange
                        Else
                            ThumbMovement = _Value + _LargeChange
                        End If
                    End If
                End If
                Value = Math.Min(Math.Max(ThumbMovement, _Minimum), _Maximum)
                InvalidatePosition()
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
            If ThumbDown AndAlso ShowThumb Then
                Dim ThumbPosition As Integer = e.X - LSA.Width - (_ThumbSize \ 2)
                Dim ThumbBounds As Integer = Shaft.Width - _ThumbSize

                ThumbMovement = CInt((ThumbPosition / ThumbBounds) * (_Maximum - _Minimum)) + _Minimum

                Value = Math.Min(Math.Max(ThumbMovement, _Minimum), _Maximum)
                InvalidatePosition()
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            ThumbDown = False
        End Sub

#End Region

#Region "Draw Control"

        Sub New()
            SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or _
                               ControlStyles.UserPaint Or ControlStyles.Selectable Or _
                               ControlStyles.SupportsTransparentBackColor, True)
            DoubleBuffered = True
            Height = 18
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Dim g = e.Graphics
            With g
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .Clear(Color.FromArgb(47, 47, 47))
                Dim P() As Point = {New Point(5, CInt(Height / 2)), New Point(13, CInt(Height / 4)), New Point(13, CInt(Height / 2 - 2)), New Point(Width - 13, CInt(Height / 2 - 2)), _
                        New Point(Width - 13, CInt(Height / 4)), New Point(Width - 5, CInt(Height / 2)), New Point(Width - 13, CInt(Height - Height / 4 - 1)), New Point(Width - 13, CInt(Height / 2 + 2)), _
                                   New Point(13, CInt(Height / 2 + 2)), New Point(13, CInt(Height - Height / 4 - 1))}
                .FillPolygon(New SolidBrush(_ArrowColour), P)
                .FillRectangle(New SolidBrush(_ThumbColour), Thumb)
                .DrawRectangle(New Pen(_ThumbBorder), Thumb)
                .DrawRectangle(New Pen(_ThumbSecondBorder), Thumb.X + 1, Thumb.Y + 1, Thumb.Width - 2, Thumb.Height - 2)
                .DrawLine(New Pen((_LineColour), 2), New Point(Thumb.X + 4, (CInt(Thumb.Height / 2 + 1))), New Point(Thumb.Right - 4, (CInt(Thumb.Height / 2 + 1))))
                .DrawRectangle(New Pen(_FirstBorder), 0, 0, Width - 1, Height - 1)
                .DrawRectangle(New Pen(_SecondBorder), 1, 1, Width - 3, Height - 3)
                .InterpolationMode = InterpolationMode.HighQualityBicubic
            End With
        End Sub

#End Region

    End Class

    Public Class LogInStatusBar
        Inherits Control

#Region "Variables"
        Private _BaseColour As Color = Color.FromArgb(42, 42, 42)
        Private _BorderColour As Color = Color.FromArgb(35, 35, 35)
        Private _TextColour As Color = Color.White
        Private _RectColour As Color = Color.FromArgb(21, 117, 149)
        Private _ShowLine As Boolean = True
        Private _LinesToShow As LinesCount = LinesCount.One
        Private _Alignment As Alignments = Alignments.Left
        Private _ShowBorder As Boolean = True
#End Region

#Region "Properties"

        <Category("Colours")>
        Public Property BaseColour As Color
            Get
                Return _BaseColour
            End Get
            Set(value As Color)
                _BaseColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property BorderColour As Color
            Get
                Return _BorderColour
            End Get
            Set(value As Color)
                _BorderColour = value
            End Set
        End Property

        <Category("Colours")>
        Public Property TextColour As Color
            Get
                Return _TextColour
            End Get
            Set(value As Color)
                _TextColour = value
            End Set
        End Property

        Enum LinesCount As Integer
            One = 1
            Two = 2
        End Enum

        Enum Alignments
            Left
            Center
            Right
        End Enum

        <Category("Control")>
        Public Property Alignment As Alignments
            Get
                Return _Alignment
            End Get
            Set(value As Alignments)
                _Alignment = value
            End Set
        End Property

        <Category("Control")>
        Public Property LinesToShow As LinesCount
            Get
                Return _LinesToShow
            End Get
            Set(value As LinesCount)
                _LinesToShow = value
            End Set
        End Property

        Public Property ShowBorder As Boolean
            Get
                Return _ShowBorder
            End Get
            Set(value As Boolean)
                _ShowBorder = value
            End Set
        End Property

        Protected Overrides Sub CreateHandle()
            MyBase.CreateHandle()
            Dock = DockStyle.Bottom
        End Sub

        Protected Overrides Sub OnTextChanged(e As EventArgs)
            MyBase.OnTextChanged(e) : Invalidate()
        End Sub

        <Category("Colours")> _
        Public Property RectangleColor As Color
            Get
                Return _RectColour
            End Get
            Set(value As Color)
                _RectColour = value
            End Set
        End Property

        Public Property ShowLine As Boolean
            Get
                Return _ShowLine
            End Get
            Set(value As Boolean)
                _ShowLine = value
            End Set
        End Property

#End Region

#Region "Draw Control"

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                     ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
            DoubleBuffered = True
            Font = New Font("Segoe UI", 9)
            ForeColor = Color.White
            Size = New Size(Width, 20)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G = e.Graphics
            Dim Base As New Rectangle(0, 0, Width, Height)
            With G
                .SmoothingMode = SmoothingMode.HighQuality
                .PixelOffsetMode = PixelOffsetMode.HighQuality
                .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                .Clear(BaseColour)
                .FillRectangle(New SolidBrush(BaseColour), Base)
                If _ShowLine = True Then
                    Select Case _LinesToShow
                        Case LinesCount.One
                            If _Alignment = Alignments.Left Then
                                .DrawString(Text, Font, New SolidBrush(_TextColour), New Rectangle(22, 2, Width, Height), New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})
                            ElseIf _Alignment = Alignments.Center Then
                                .DrawString(Text, Font, New SolidBrush(_TextColour), New Rectangle(0, 0, Width, Height), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                            Else
                                .DrawString(Text, Font, New SolidBrush(_TextColour), New Rectangle(0, 0, Width - 5, Height), New StringFormat With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center})
                            End If
                            .FillRectangle(New SolidBrush(_RectColour), New Rectangle(5, 9, 14, 3))
                        Case LinesCount.Two
                            If _Alignment = Alignments.Left Then
                                .DrawString(Text, Font, New SolidBrush(_TextColour), New Rectangle(22, 2, Width, Height), New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})
                            ElseIf _Alignment = Alignments.Center Then
                                .DrawString(Text, Font, New SolidBrush(_TextColour), New Rectangle(0, 0, Width, Height), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                            Else
                                .DrawString(Text, Font, New SolidBrush(_TextColour), New Rectangle(0, 0, Width - 22, Height), New StringFormat With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center})
                            End If
                            .FillRectangle(New SolidBrush(_RectColour), New Rectangle(5, 9, 14, 3))
                            .FillRectangle(New SolidBrush(_RectColour), New Rectangle(Width - 20, 9, 14, 3))
                    End Select
                Else
                    .DrawString(Text, Font, Brushes.White, New Rectangle(5, 2, Width, Height), New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})
                End If
                If _ShowBorder Then
                    .DrawLine(New Pen(_BorderColour, 2), New Point(0, 0), New Point(Width, 0))
                Else
                End If
                .InterpolationMode = InterpolationMode.HighQualityBicubic
            End With
        End Sub

#End Region

    End Class

    Public Class LogInListbox
        Inherits ListBox

        Private _BorderColour As Color = Color.DimGray

        Sub New()
            SetStyle(ControlStyles.DoubleBuffer, True)
            Font = New Font("Segoe UI", 9)
            BorderStyle = Windows.Forms.BorderStyle.None
            DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
            ItemHeight = 20
            ForeColor = Color.White
            BackColor = Color.FromArgb(42, 42, 42)
            IntegralHeight = False
        End Sub

        Private _selecteditemcolor As Color = Color.Gray

        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            'MyBase.WndProc(m)
            'If m.Msg = 15 Then CustomPaint()

            Select Case m.Msg
                Case 15
                    MyBase.WndProc(m)
                    CustomPaint()
                    Exit Select
                Case Else
                    MyBase.WndProc(m)
                    Exit Select
            End Select
        End Sub

        Private Sub CustomPaint()
            CreateGraphics.DrawRectangle(New Pen(_BorderColour), New Rectangle(0, 0, Width - 1, Height - 1))
        End Sub

        Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)
            Try
                If e.Index < 0 Then Exit Sub
                e.DrawBackground()
                Dim rect As New Rectangle(New Point(e.Bounds.Left, e.Bounds.Top + 2), New Size(Bounds.Width, 16))
                Dim bsh As Brush = Brushes.White
                _selecteditemcolor = Color.Gray
                If Not Enabled Then
                    bsh = Brushes.Gray
                    _selecteditemcolor = Color.FromArgb(120, Color.Gray)
                End If
                If InStr(e.State.ToString, "Selected,") > 0 Then
                    e.Graphics.FillRectangle(Brushes.Black, e.Bounds)
                    Dim x2 As Rectangle = New Rectangle(e.Bounds.Location, New Size(e.Bounds.Width - 1, e.Bounds.Height))
                    Dim x3 As Rectangle = New Rectangle(x2.Location, New Size(x2.Width, CInt((x2.Height / 2))))
                    Dim G1 As New LinearGradientBrush(New Point(x2.X, x2.Y), New Point(x2.X, x2.Y + x2.Height), _selecteditemcolor, Color.FromArgb(50, 50, 50))
                    Dim H As New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(15, Color.Black), Color.Transparent)
                    e.Graphics.FillRectangle(G1, x2) : G1.Dispose()
                    e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(25, Color.White)), x3)
                    e.Graphics.FillRectangle(H, x2)
                    e.Graphics.DrawString(" " & Items(e.Index).ToString(), Font, bsh, e.Bounds.X, e.Bounds.Y + 1)
                Else
                    e.Graphics.DrawString(" " & Items(e.Index).ToString(), Font, bsh, e.Bounds.X, e.Bounds.Y + 1)
                End If

                MyBase.OnDrawItem(e)

            Catch ex As Exception : End Try
        End Sub

    End Class

End Namespace