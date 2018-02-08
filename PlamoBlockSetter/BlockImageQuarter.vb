Public Class BlockImageQuarter
    Protected objImage As Bitmap

    Protected intRows As Integer
    Protected intCols As Integer
    Protected intCellSize As Integer
    Protected objBaseColor As Color
    Protected objEdgeColor As Color
    Protected sngOpacity As Single
    Protected intRotation As Integer
    Protected bolIsEdgeView As Boolean

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
    Public ReadOnly Property IsEdgeView() As Boolean
        Get
            Return bolIsEdgeView
        End Get
    End Property

    Public ReadOnly Property RotateRows() As Integer
        Get
            Return CInt(IIf(intRotation = 0, intRows, intCols))
        End Get
    End Property
    Public ReadOnly Property RotateCols() As Integer
        Get
            Return CInt(IIf(intRotation = 0, intCols, intRows))
        End Get
    End Property
    Public ReadOnly Property Width() As Integer
        Get
            Return (1 + (RotateCols - 1) * 0.5 + (RotateRows - 1) * 0.5) * intCellSize
        End Get
    End Property
    Public ReadOnly Property Height() As Integer
        Get
            Return (0.5 + (RotateRows - 1) * 0.25 + (RotateCols - 1) * 0.25) * intCellSize
        End Get
    End Property

    Public Sub New(pintRows As Integer, pintCols As Integer, pintCellSize As Integer, pobjBaseColor As Color, pobjEdgeColor As Color, psngOpacity As Single, pintRotation As Integer, pbolIsEdgeView As Boolean)
        intRows = pintRows
        intCols = pintCols
        intCellSize = pintCellSize
        objBaseColor = pobjBaseColor
        objEdgeColor = pobjEdgeColor
        sngOpacity = psngOpacity
        intRotation = pintRotation
        bolIsEdgeView = pbolIsEdgeView

        DrawBlockImage()
    End Sub
    Public Sub New(pintRows As Integer, pintCols As Integer, pintCellSize As Integer, pobjColorSetting As BlockColor.ColorSetting, pintRotation As Integer, pbolIsEdgeView As Boolean)
        Me.New(pintRows, pintCols, pintCellSize, pobjColorSetting.Base, pobjColorSetting.Edge, pobjColorSetting.Opacity, pintRotation, pbolIsEdgeView)
    End Sub
    Public Sub New(objBlock As ModelData.Block, pintCellSize As Integer, pbolIsEdgeView As Boolean)
        Me.New(objBlock.Height, objBlock.Width, pintCellSize, objBlock.ColorSetting, objBlock.Rotation, pbolIsEdgeView)
    End Sub
    Public Sub New(pobjSelectBlock As SelectBlock, pintCellSize As Integer, pbolIsEdgeView As Boolean)
        Me.New(pobjSelectBlock.Height, pobjSelectBlock.Width, pintCellSize, pobjSelectBlock.ColorSetting, pobjSelectBlock.Rotation, pbolIsEdgeView)
    End Sub
    Public Sub New()
        Me.New(1, 1, 16, New BlockColor.ColorSetting(), 0, True)
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

        Dim points As Point()
        Dim myPath As New Drawing2D.GraphicsPath

        canvas = New Bitmap(Width, Height)

        points = {
                    New Point(0, intCellSize * 0.25 * RotateCols) _
                    , New Point(0, Height - intCellSize * 0.25 * RotateRows) _
                    , New Point(intCellSize * 0.5 * RotateRows, Height) _
                    , New Point(Width, Height - intCellSize * 0.25 * RotateCols) _
                    , New Point(Width, intCellSize * 0.25 * RotateCols) _
                    , New Point(Width - intCellSize * 0.5 * RotateRows, 0)
                }
        With myPath
            '輪郭
            .StartFigure()
            .AddPolygon(points)
            .AddLine(New Point(0, intCellSize * 0.25 * RotateCols), New Point(intCellSize * 0.5 * RotateRows, Height - intCellSize * 0.5))
            .AddLine(New Point(intCellSize * 0.5 * RotateRows, Height), New Point(intCellSize * 0.5 * RotateRows, Height - intCellSize * 0.5))
            .AddLine(New Point(intCellSize * 0.25 * RotateRows, Height - intCellSize * 0.25 * RotateCols), New Point(intCellSize * 0.5 * RotateRows, Height - intCellSize * 0.5))
        End With
        Console.WriteLine(points.ToString)
        g = Graphics.FromImage(canvas)

        'r = New Rectangle(0, 0, Width - 1, Height - 1)

        'b = New SolidBrush(Color.FromArgb(CInt(255 * sngOpacity), objBaseColor))
        'g.FillRectangle(b, r)

        p = New Pen(objEdgeColor, 1)
        p.DashStyle = Drawing2D.DashStyle.Solid
        'g.DrawRectangle(p, r)
        g.DrawPath(p, myPath)

        'For row As Integer = 1 To CInt(IIf(intRotation = 0, intRows, intCols))
        '    For col As Integer = 1 To CInt(IIf(intRotation = 0, intCols, intRows))
        '        g.DrawRectangle(p, CSng((col - 0.5) * intCellSize - intCellSize / 6), CSng((row - 0.5) * intCellSize - intCellSize / 6), CSng(Me.intCellSize / 3), CSng(Me.intCellSize / 3))
        '    Next
        'Next

        'リソースを解放する
        'b.Dispose()
        p.Dispose()
        g.Dispose()

        objImage = canvas
    End Sub
End Class
