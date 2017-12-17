Public Class BlockImageOutline
    Inherits BlockImage

    Public Sub New(pintRows As Integer, pintCols As Integer, pintCellSize As Integer, pobjEdgeColor As Color, pintRotation As Integer)
        MyBase.New(pintRows, pintCols, pintCellSize, Nothing, pobjEdgeColor, 0, 0)

        DrawBlockImage()
    End Sub
    Public Sub New(pobjSelectBlock As SelectBlock, pintCellSize As Integer)
        Me.New(pobjSelectBlock.Rows, pobjSelectBlock.Cols, pintCellSize, pobjSelectBlock.ColorSetting.Edge, pobjSelectBlock.Rotation)
    End Sub
    Public Sub New()
        Me.New(1, 1, 16, Color.Black, 0)
    End Sub

    Protected Overrides Sub DrawBlockImage()
        Dim canvas As Bitmap
        Dim g As Graphics
        Dim r As Rectangle
        Dim b As Brush
        Dim p As Pen

        canvas = New Bitmap(Width, Height)

        g = Graphics.FromImage(canvas)

        r = New Rectangle(0, 0, Width - 1, Height - 1)

        b = New SolidBrush(Color.FromArgb(100, Color.Black))
        g.FillRectangle(b, r)

        p = New Pen(objEdgeColor, 1)
        p.DashStyle = Drawing2D.DashStyle.Dash
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
