Public Class WorkArea
    Inherits ImageBase

    Private objBG As Bitmap

    Public Sub SetWorkAreaSize(pintMaxWidth As Integer, pintMaxHeight As Integer, pintMaxCols As Integer, pintMaxRows As Integer)
        Me.intMaxWidth = pintMaxWidth
        Me.intMaxHeight = pintMaxHeight
        Me.intMaxCols = pintMaxCols
        Me.intMaxRows = pintMaxRows

        Call Me.SetCellSize()
        Call Me.SetControlSize()
        Call Me.DrawBackGround()

        Me.Image = Me.objBG
    End Sub

    Private Sub SetControlSize()
        Me.Width = (Me.intMaxCols + 1) * Me.intCellSize + 1
        Me.Height = (Me.intMaxRows + 1) * Me.intCellSize + 1
    End Sub

    Private Sub DrawBackGround()
        Dim g As Graphics
        Dim p As Pen
        Dim fnt As Font
        Dim sf As StringFormat
        Dim i As Integer

        Me.objBG = New Bitmap(Me.Width, Me.Height)

        g = Graphics.FromImage(Me.objBG)

        p = New Pen(Color.Gray, 1)
        p.DashStyle = Drawing2D.DashStyle.Dash

        fnt = New Font("MS UI Gothic", CSng(8 * intCellSize / 14))

        sf = New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        For i = 1 To Me.intMaxCols
            g.DrawLine(p, i * intCellSize, 0, i * intCellSize, Me.Height - 1)
            g.DrawString(i.ToString, fnt, Brushes.Blue, CSng((i + 0.5) * intCellSize + 0.5), CSng(intCellSize * 0.5 + 0.5), sf)
        Next
        For i = 1 To Me.intMaxRows
            g.DrawLine(p, 0, i * intCellSize, Me.Width - 1, i * Me.intCellSize)
            g.DrawString(i.ToString, fnt, Brushes.Blue, CSng(intCellSize * 0.5 + 0.5), CSng((i + 0.5) * intCellSize + 0.5), sf)
        Next

        p.Color = Color.Black
        p.DashStyle = Drawing2D.DashStyle.Solid
        g.DrawLine(p, intCellSize, 0, intCellSize, Me.Height - 1)
        g.DrawLine(p, 0, intCellSize, Me.Width - 1, Me.intCellSize)

        p.Color = Color.Blue
        p.DashStyle = Drawing2D.DashStyle.Solid
        g.DrawLine(p, CInt(Math.Truncate(Me.intMaxCols / 2 + 1) * Me.intCellSize), 0, CInt(Math.Truncate(Me.intMaxCols / 2 + 1) * Me.intCellSize), Me.Height - 1)
        g.DrawLine(p, 0, CInt(Math.Truncate(Me.intMaxRows / 2 + 1) * Me.intCellSize), Me.Width - 1, CInt(Math.Truncate(Me.intMaxRows / 2 + 1) * Me.intCellSize))

        'リソースを解放する
        p.Dispose()
        g.Dispose()
    End Sub
End Class