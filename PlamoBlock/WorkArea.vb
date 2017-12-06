Imports System.Windows.Forms
Imports System.Drawing

Public Class WorkArea
    Inherits PictureBox

    Private intCols As Integer
    Private intRows As Integer

    Private objBG As Bitmap


    Public Sub SetWorkAreaSize(pintCols As Integer, pintRows As Integer)
        Me.intCols = pintCols
        Me.intRows = pintRows

        Call MakeBackGround()

        Me.Image = Me.objBG
    End Sub

    Public Sub MakeBackGround()
        Dim g As Graphics
        Dim p As Pen
        Dim i As Integer


        '描画先とするImageオブジェクトを作成する
        Me.objBG = New Bitmap(Me.Width, Me.Height)

        'ImageオブジェクトのGraphicsオブジェクトを作成する
        g = Graphics.FromImage(Me.objBG)

        p = New Pen(Color.Black, 1)
        p.DashStyle = Drawing2D.DashStyle.Dash

        For i = 1 To Me.intCols - 1
            g.DrawLine(p, CInt(i * (Me.Width - 2) / Me.intCols - 1), 0, CInt(i * (Me.Width - 2) / Me.intCols - 1), Me.Height)
        Next
        For i = 1 To Me.intRows - 1
            g.DrawLine(p, 0, CInt(i * (Me.Height - 2) / Me.intRows - 1), Me.Width, CInt(i * (Me.Height - 2) / Me.intRows - 1))
        Next

        p.Color = Color.Blue
        p.DashStyle = Drawing2D.DashStyle.Solid
        g.DrawLine(p, CInt(Me.intCols / 2 * (Me.Width - 2) / Me.intCols - 1), 0, CInt(Me.intCols / 2 * (Me.Width - 2) / Me.intCols - 1), Me.Height)
        g.DrawLine(p, 0, CInt(Me.intRows / 2 * (Me.Height - 2) / Me.intRows - 1), Me.Width, CInt(Me.intRows / 2 * (Me.Height - 2) / Me.intRows - 1))

        'リソースを解放する
        p.Dispose()
        g.Dispose()
    End Sub
End Class