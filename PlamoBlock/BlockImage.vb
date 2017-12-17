Public Class BlockImage
    Protected objImage As Bitmap

    Protected intRows As Integer
    Protected intCols As Integer
    Protected intCellSize As Integer
    Protected objBaseColor As Color
    Protected objEdgeColor As Color
    Protected sngOpacity As Single
    Protected intRotation As Integer

    Public ReadOnly Property Image() As Bitmap
        Get
            Return objImage
        End Get
    End Property

    Public ReadOnly Property Rows() As Integer
        Get
            Return intRows
        End Get
    End Property
    Public ReadOnly Property Cols() As Integer
        Get
            Return intCols
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
    Public ReadOnly Property Rotation() As Integer
        Get
            Return intRotation
        End Get
    End Property

    Public ReadOnly Property Width() As Integer
        Get
            Return CInt(IIf(intRotation = 0, intCols, intRows)) * intCellSize
        End Get
    End Property
    Public ReadOnly Property Height() As Integer
        Get
            Return CInt(IIf(intRotation = 0, intRows, intCols)) * intCellSize
        End Get
    End Property

    Public Sub New(pintRows As Integer, pintCols As Integer, pintCellSize As Integer, pobjBaseColor As Color, pobjEdgeColor As Color, psngOpacity As Single, pintRotation As Integer)
        intRows = pintRows
        intCols = pintCols
        intCellSize = pintCellSize
        objBaseColor = pobjBaseColor
        objEdgeColor = pobjEdgeColor
        sngOpacity = psngOpacity
        intRotation = pintRotation

        DrawBlockImage()
    End Sub
    Public Sub New(pintRows As Integer, pintCols As Integer, pintCellSize As Integer, pobjColorSetting As BlockColor.ColorSetting, pintRotation As Integer)
        Me.New(pintRows, pintCols, pintCellSize, pobjColorSetting.Base, pobjColorSetting.Edge, pobjColorSetting.Opacity, pintRotation)
    End Sub
    Public Sub New(pobjSelectBlock As SelectBlock, pintCellSize As Integer)
        Me.New(pobjSelectBlock.Rows, pobjSelectBlock.Cols, pintCellSize, pobjSelectBlock.ColorSetting, pobjSelectBlock.Rotation)
    End Sub
    Public Sub New()
        Me.New(1, 1, 16, New BlockColor.ColorSetting(), 0)
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

        For row As Integer = 1 To CInt(IIf(intRotation = 0, intRows, intCols))
            For col As Integer = 1 To CInt(IIf(intRotation = 0, intCols, intRows))
                g.DrawRectangle(p, CSng((col - 0.5) * intCellSize - intCellSize / 6), CSng((row - 0.5) * intCellSize - intCellSize / 6), CSng(Me.intCellSize / 3), CSng(Me.intCellSize / 3))
            Next
        Next

        'リソースを解放する
        b.Dispose()
        p.Dispose()
        g.Dispose()

        objImage = canvas
    End Sub
End Class
