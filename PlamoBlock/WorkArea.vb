Public Class WorkArea
    Inherits PictureBox

    'プロパティの既定値
    Private Const _Default_Rows As Integer = 8
    Private Const _Default_Cols As Integer = 8
    Private Const _Default_CellSize As Integer = 16

    Private intCols As Integer
    Private intRows As Integer
    Private intCellSize As Integer

    Private objBackImage As Bitmap

    <System.ComponentModel.Category("エリアサイズ設定")>
    <System.ComponentModel.DefaultValue(_Default_Rows)>
    Public Property Rows() As Integer
        Get
            Return intRows
        End Get
        Set(ByVal value As Integer)
            intRows = value
            SetWorkAreaSize(intRows, intCols, intCellSize)
        End Set
    End Property
    <System.ComponentModel.Category("エリアサイズ設定")>
    <System.ComponentModel.DefaultValue(_Default_Cols)>
    Public Property Cols() As Integer
        Get
            Return intCols
        End Get
        Set(ByVal value As Integer)
            intCols = value
            SetWorkAreaSize(intRows, intCols, intCellSize)
        End Set
    End Property
    <System.ComponentModel.Category("エリアサイズ設定")>
    <System.ComponentModel.DefaultValue(_Default_CellSize)>
    Public Property CellSize() As Integer
        Get
            Return intCellSize
        End Get
        Set(ByVal value As Integer)
            intCellSize = value
            SetWorkAreaSize(intRows, intCols, intCellSize)
        End Set
    End Property

    Public Sub New()
        BackColor = Color.White

        intRows = _Default_Rows
        intCols = _Default_Cols
        intCellSize = _Default_CellSize
        SetWorkAreaSize(intRows, intCols, intCellSize)
    End Sub

    Private Sub WorkArea_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetControlSize()
    End Sub

    Public Sub SetWorkAreaSize(pintRows As Integer, pintCols As Integer, pintCellSize As Integer)
        intRows = pintRows
        intCols = pintCols
        intCellSize = pintCellSize

        SetControlSize()
        DrawBackGround()

        Image = objBackImage
    End Sub

    Private Sub SetControlSize()
        Width = (Me.intCols + 1) * intCellSize + 1
        Height = (Me.intRows + 1) * intCellSize + 1
    End Sub

    Private Sub DrawBackGround()
        Dim g As Graphics
        Dim p As Pen
        Dim fnt As Font
        Dim sf As StringFormat
        Dim i As Integer

        objBackImage = New Bitmap(Me.Width, Height)

        g = Graphics.FromImage(Me.objBackImage)

        p = New Pen(Color.Gray, 1)
        p.DashStyle = Drawing2D.DashStyle.Dash

        fnt = New Font("MS UI Gothic", CSng(8 * intCellSize / 14))

        sf = New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        For i = 1 To intCols
            g.DrawLine(p, i * intCellSize, 0, i * intCellSize, Height - 1)
            g.DrawString(i.ToString, fnt, Brushes.Blue, CSng((i + 0.5) * intCellSize + 0.5), CSng(intCellSize * 0.5 + 0.5), sf)
        Next
        For i = 1 To intRows
            g.DrawLine(p, 0, i * intCellSize, Width - 1, i * intCellSize)
            g.DrawString(i.ToString, fnt, Brushes.Blue, CSng(intCellSize * 0.5 + 0.5), CSng((i + 0.5) * intCellSize + 0.5), sf)
        Next

        p.Color = Color.Black
        p.DashStyle = Drawing2D.DashStyle.Solid
        g.DrawLine(p, intCellSize, 0, intCellSize, Height - 1)
        g.DrawLine(p, 0, intCellSize, Width - 1, intCellSize)

        p.Color = Color.Blue
        p.DashStyle = Drawing2D.DashStyle.Solid
        g.DrawLine(p, CInt(Math.Truncate(Me.intCols / 2 + 1) * intCellSize), 0, CInt(Math.Truncate(Me.intCols / 2 + 1) * intCellSize), Height - 1)
        g.DrawLine(p, 0, CInt(Math.Truncate(Me.intRows / 2 + 1) * intCellSize), Width - 1, CInt(Math.Truncate(Me.intRows / 2 + 1) * intCellSize))

        'リソースを解放する
        p.Dispose()
        g.Dispose()
    End Sub
End Class