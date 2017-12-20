Public Class BlockImageSide
    Protected objImage As Bitmap

    Protected intLen As Integer
    Protected intCellSize As Integer
    Protected objBaseColor As Color
    Protected objEdgeColor As Color
    Protected sngOpacity As Single

    Public ReadOnly Property Image() As Bitmap
        Get
            Return objImage
        End Get
    End Property

    Public ReadOnly Property Len() As Integer
        Get
            Return intLen
        End Get
    End Property
    Public ReadOnly Property CellSize() As Integer
        Get
            Return intCellSize
        End Get
    End Property
    Public ReadOnly Property BaseColor() As Color
        Get
            Return objBaseColor
        End Get
    End Property
    Public ReadOnly Property EdgeColor() As Color
        Get
            Return objEdgeColor
        End Get
    End Property
    Public ReadOnly Property Opacity() As Single
        Get
            Return sngOpacity
        End Get
    End Property

    Public ReadOnly Property Width() As Integer
        Get
            Return intLen * intCellSize
        End Get
    End Property
    Public ReadOnly Property Height() As Integer
        Get
            Return intCellSize
        End Get
    End Property

    Public Sub New(pintLen As Integer, pintCellSize As Integer, pobjBaseColor As Color, pobjEdgeColor As Color, psngOpacity As Single)
        intLen = pintLen
        intCellSize = pintCellSize
        objBaseColor = pobjBaseColor
        objEdgeColor = pobjEdgeColor
        sngOpacity = psngOpacity

        DrawBlockImage()
    End Sub
    Public Sub New(pintLen As Integer, pintCellSize As Integer, pobjColorSetting As BlockColor.ColorSetting)
        Me.New(pintLen, pintCellSize, pobjColorSetting.Base, pobjColorSetting.Edge, pobjColorSetting.Opacity)
    End Sub
    Public Sub New()
        Me.New(1, 16, New BlockColor.ColorSetting())
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        objImage.Dispose()
    End Sub

    Protected Overridable Sub DrawBlockImage()
        Dim canvas As Bitmap
        Dim g As Graphics
        Dim r As Rectangle
        Dim b As Brush
        Dim p As Pen

        canvas = New Bitmap(Width, Height)

        g = Graphics.FromImage(canvas)

        r = New Rectangle(0, 0, Width - 1, Height - 1)

        b = New SolidBrush(Color.FromArgb(CInt(255 * sngOpacity), objBaseColor))
        g.FillRectangle(b, r)

        p = New Pen(objEdgeColor, 1)
        p.DashStyle = Drawing2D.DashStyle.Solid
        g.DrawRectangle(p, r)

        'リソースを解放する
        b.Dispose()
        p.Dispose()
        g.Dispose()

        objImage = canvas
    End Sub
End Class
