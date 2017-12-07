Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class WorkArea
    Inherits PictureBox

    Private intMaxWidth As Integer
    Private intMaxHeight As Integer
    Private intCols As Integer
    Private intRows As Integer
    Private intCellSize As Integer

    Private objBG As Bitmap



    Public Sub SetWorkAreaSize(pintMaxWidth As Integer, pintMaxHeight As Integer, pintCols As Integer, pintRows As Integer)
        Me.intMaxWidth = pintMaxWidth
        Me.intMaxHeight = pintMaxHeight
        Me.intCols = pintCols
        Me.intRows = pintRows

        Call Me.SetControlSize()
        Call Me.MakeBackGround()

        Me.Image = Me.objBG
    End Sub

    Private Sub SetControlSize()
        Me.intCellSize = CInt(Math.Truncate(Math.Min((Me.intMaxWidth - 1) / (Me.intCols + 1), (Me.intMaxHeight - 1) / (Me.intRows + 1))))

        Me.Width = (Me.intCols + 1) * Me.intCellSize + 1
        Me.Height = (Me.intRows + 1) * Me.intCellSize + 1
    End Sub

    Private Sub MakeBackGround()
        Dim g As Graphics
        Dim p As Pen
        Dim i As Integer



        Me.objBG = New Bitmap(Me.Width, Me.Height)

        g = Graphics.FromImage(Me.objBG)

        p = New Pen(Color.Gray, 1)
        p.DashStyle = Drawing2D.DashStyle.Dash

        For i = 1 To Me.intCols
            g.DrawLine(p, i * intCellSize, 0, i * intCellSize, Me.Height - 1)
        Next
        For i = 1 To Me.intRows
            g.DrawLine(p, 0, i * intCellSize, Me.Width - 1, i * Me.intCellSize)
        Next

        p.Color = Color.Black
        p.DashStyle = Drawing2D.DashStyle.Solid
        g.DrawLine(p, intCellSize, 0, intCellSize, Me.Height - 1)
        g.DrawLine(p, 0, intCellSize, Me.Width - 1, Me.intCellSize)

        p.Color = Color.Blue
        p.DashStyle = Drawing2D.DashStyle.Solid
        g.DrawLine(p, CInt(Math.Truncate(Me.intCols / 2 + 1) * Me.intCellSize), 0, CInt(Math.Truncate(Me.intCols / 2 + 1) * Me.intCellSize), Me.Height - 1)
        g.DrawLine(p, 0, CInt(Math.Truncate(Me.intRows / 2 + 1) * Me.intCellSize), Me.Width - 1, CInt(Math.Truncate(Me.intRows / 2 + 1) * Me.intCellSize))

        'リソースを解放する
        p.Dispose()
        g.Dispose()
    End Sub
End Class