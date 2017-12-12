Public Class BlockImage
    Inherits ImageBase

    Private objColorSetting As BlockColor.ColorSetting
    Private intCols As Integer
    Private intRows As Integer

    Public ReadOnly Property ColorSetting() As BlockColor.ColorSetting
        Get
            Return Me.objColorSetting
        End Get
    End Property
    Public ReadOnly Property Cols() As Integer
        Get
            Return Me.intCols
        End Get
    End Property
    Public ReadOnly Property Rows() As Integer
        Get
            Return Me.intRows
        End Get
    End Property

    Public Sub SetBlockSize(pobjColorSetting As BlockColor.ColorSetting, pintRows As Integer, pintCols As Integer, pintMaxWidth As Integer, pintMaxHeight As Integer, pintMaxCols As Integer, pintMaxRows As Integer)
        Me.objColorSetting = pobjColorSetting
        Me.intCols = pintCols
        Me.intRows = pintRows
        Me.intMaxWidth = pintMaxWidth
        Me.intMaxHeight = pintMaxHeight
        Me.intMaxCols = pintMaxCols
        Me.intMaxRows = pintMaxRows

        Call Me.SetCellSize()
        Call Me.SetControlSize()
        Call Me.DrawBlockImage()
    End Sub
    Public Sub SetBlockSize(pobjColorSetting As BlockColor.ColorSetting, pintRows As Integer, pintCols As Integer, pintCellSize As Integer)
        Me.objColorSetting = pobjColorSetting
        Me.intCols = pintCols
        Me.intRows = pintRows
        Me.intCellSize = pintCellSize

        Call Me.SetControlSize()
        Call Me.DrawBlockImage()
    End Sub

    Private Sub SetControlSize()
        Me.Width = Me.intCols * Me.intCellSize
        Me.Height = Me.intRows * Me.intCellSize
    End Sub

    Private Sub DrawBlockImage()
        Dim canvas As Bitmap
        Dim g As Graphics
        Dim r As Rectangle
        Dim b As Brush
        Dim p As Pen

        canvas = New Bitmap(Me.Width, Me.Height)

        g = Graphics.FromImage(canvas)

        r = New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)

        b = New SolidBrush(Color.FromArgb(255 * Me.objColorSetting.Opacity, Me.objColorSetting.Base))
        g.FillRectangle(b, r)

        p = New Pen(Me.objColorSetting.Edge, 1)
        p.DashStyle = Drawing2D.DashStyle.Solid
        g.DrawRectangle(p, r)

        For row = 1 To Me.intRows
            For col = 1 To Me.intCols
                g.DrawRectangle(p, CSng((col - 0.5) * Me.intCellSize - Me.intCellSize / 6), CSng((row - 0.5) * Me.intCellSize - Me.intCellSize / 6), CSng(Me.intCellSize / 3), CSng(Me.intCellSize / 3))
            Next
        Next

        'リソースを解放する
        b.Dispose()
        p.Dispose()
        g.Dispose()

        Me.Image = canvas
    End Sub
End Class
