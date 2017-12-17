Public Class WorkArea
    Inherits PictureBox

    'プロパティの既定値
    Private Const _Default_Rows As Integer = 8
    Private Const _Default_Cols As Integer = 8
    Private Const _Default_CellSize As Integer = 16

    Private intCols As Integer
    Private intRows As Integer
    Private intCellSize As Integer

    Private objSelectBlock As SelectBlock

    Private objBackImage As Bitmap
    Private objSelectBlockImage As Bitmap

    <System.ComponentModel.Category("_追加設定")>
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
    <System.ComponentModel.Category("_追加設定")>
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
    <System.ComponentModel.Category("_追加設定")>
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

    Public ReadOnly Property MinCol() As Integer
        Get
            Return CInt(Math.Truncate(Me.intCols / 2) - intCols)
        End Get
    End Property
    Public ReadOnly Property MaxCol() As Integer
        Get
            Return CInt(intCols - Math.Truncate(Me.intCols / 2) - 1)
        End Get
    End Property
    Public ReadOnly Property MinRow() As Integer
        Get
            Return CInt(Math.Truncate(Me.intRows / 2) - intRows)
        End Get
    End Property
    Public ReadOnly Property MaxRow() As Integer
        Get
            Return CInt(intRows - Math.Truncate(Me.intRows / 2) - 1)
        End Get
    End Property

    Public ReadOnly Property SelectBlock() As SelectBlock
        Get
            Return objSelectBlock
        End Get
    End Property

    Public Sub New()
        BackColor = Color.White

        intRows = _Default_Rows
        intCols = _Default_Cols
        intCellSize = _Default_CellSize

        objSelectBlock = New SelectBlock

        SetWorkAreaSize(intRows, intCols, intCellSize)
    End Sub

    Private Sub WorkArea_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetControlSize()
    End Sub

    Public Sub SetSelectBlock(pintRows As Integer, pintCols As Integer, pobjColorSetting As BlockColor.ColorSetting, pintRotation As Integer)
        objSelectBlock = New SelectBlock(pintRows, pintCols, pobjColorSetting, pintRotation)


        objSelectBlockImage = DirectCast(New BlockImageOutline(objSelectBlock, intCellSize).Image.Clone, Image)

        Redraw()
    End Sub

    Public Sub SetWorkAreaSize(pintRows As Integer, pintCols As Integer, pintCellSize As Integer)
        intRows = pintRows
        intCols = pintCols
        intCellSize = pintCellSize

        SetControlSize()
        DrawBackGround()

        Redraw()
    End Sub

    Private Sub SetControlSize()
        Width = (Me.intCols + 1) * intCellSize + 1
        Height = (Me.intRows + 1) * intCellSize + 1
    End Sub

    Public Function CellPoint(pintRow As Integer, pintCol As Integer) As Point
        Dim lintX As Integer
        Dim lintY As Integer

        lintX = CInt(Math.Truncate(Me.intCols / 2 + 1) + pintCol) * intCellSize
        lintY = CInt(Math.Truncate(Me.intRows / 2 + 1) + pintRow) * intCellSize

        Return New Point(lintX, lintY)
    End Function

    Private Sub DrawBackGround()
        Dim g As Graphics
        Dim p As Pen
        Dim lobjBrushesBuf As Brush
        Dim fnt As Font
        Dim sf As StringFormat
        Dim i As Integer

        Dim lintBuf As Integer

        objBackImage = New Bitmap(Me.Width, Height)

        g = Graphics.FromImage(Me.objBackImage)

        'エリアの枠線表示
        p = New Pen(Color.Black, 1)
        p.DashStyle = Drawing2D.DashStyle.Solid

        g.DrawRectangle(p, 0, 0, Me.Width - 1, Height - 1)

        'セルの区切り線表示
        p = New Pen(Color.Gray, 1)
        p.DashStyle = Drawing2D.DashStyle.Dash

        '座標文字設定
        fnt = New Font("MS UI Gothic", CSng(8 * intCellSize / 14))
        sf = New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'セルの縦線表示
        For i = MinCol To MaxCol
            lintBuf = CellPoint(0, i).X
            g.DrawLine(p, lintBuf, 0, lintBuf, Height - 1)
            If i < 0 Then
                lobjBrushesBuf = Brushes.Red
            Else
                lobjBrushesBuf = Brushes.Blue
            End If
            g.DrawString(Math.Abs(i).ToString, fnt, lobjBrushesBuf, CSng(lintBuf + intCellSize * 0.5), CSng(intCellSize * 0.5 + 0.5), sf)
        Next
        'セルの横線表示
        For i = MinRow To MaxRow
            lintBuf = CellPoint(i, 0).Y
            g.DrawLine(p, 0, lintBuf, Width - 1, lintBuf)
            If i < 0 Then
                lobjBrushesBuf = Brushes.Red
            Else
                lobjBrushesBuf = Brushes.Blue
            End If
            g.DrawString(Math.Abs(i).ToString, fnt, lobjBrushesBuf, CSng(intCellSize * 0.5 + 0.5), CSng(lintBuf + intCellSize * 0.5 + 1), sf)
        Next

        '区切り線表示
        p.Color = Color.Purple
        p.DashStyle = Drawing2D.DashStyle.Solid
        '座標数値表示
        g.DrawLine(p, intCellSize, 0, intCellSize, Height - 1)
        g.DrawLine(p, 0, intCellSize, Width - 1, intCellSize)
        '座標中央
        lintBuf = CellPoint(0, 0).X
        g.DrawLine(p, lintBuf, 0, lintBuf, Height - 1)
        lintBuf = CellPoint(0, 0).Y
        g.DrawLine(p, 0, lintBuf, Width - 1, lintBuf)

        'リソースを解放する
        p.Dispose()
        g.Dispose()
    End Sub

    Private Sub WorkArea_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Console.WriteLine(e.X.ToString & vbTab & e.Y.ToString)
        Console.WriteLine(IsMouseInArea())
        Redraw()
    End Sub

    Public Function IsMouseInArea() As Boolean
        Dim rect As Rectangle = Me.ClientRectangle
        Return GetContainState(Me, rect)
    End Function
    Private Function GetContainState(ByVal ctrl As Control, ByVal rect As Rectangle) As Boolean
        ' マウス座標（スクリーン座標系）の取得
        Dim mouseScreenPos As Point = Control.MousePosition
        ' マウス座標をクライアント座標系へ変換
        Dim mouseClientPos As Point = ctrl.PointToClient(mouseScreenPos)
        ' マウス座標（クライアント座標系）が領域内かどうか
        Return rect.Contains(mouseClientPos)
    End Function

    Private Sub Redraw()
        Dim canvas As New Bitmap(Width, Height)
        Dim g As Graphics

        Dim posMouse As Point

        g = Graphics.FromImage(canvas)
        g.DrawImage(objBackImage, 0, 0)

        'マウスカーソルが領域内にある場合、ブロック配置用のカーソルを表示
        If IsMouseInArea() Then
            posMouse = SetSelectBlockPoint()
            Console.WriteLine("POS:" & vbTab & posMouse.X.ToString & vbTab & posMouse.Y.ToString)
            g.DrawImage(objSelectBlockImage, posMouse.X, posMouse.Y)
        End If

        Image = canvas
    End Sub

    Private Function SetSelectBlockPoint() As Point
        Dim posMouse As Point
        Dim posResult As Point

        posMouse = PointToClient(Control.MousePosition)

        posResult.X = CInt(posMouse.X - objSelectBlockImage.Width / 2)
        posResult.Y = CInt(posMouse.Y - objSelectBlockImage.Height / 2)

        posResult.X = CInt(Math.Round(posResult.X / intCellSize) * intCellSize)
        posResult.Y = CInt(Math.Round(posResult.Y / intCellSize) * intCellSize)

        posResult.X = Math.Min(Math.Max(posResult.X, intCellSize), Cols * intCellSize) + 2
        posResult.Y = Math.Min(Math.Max(posResult.Y, intCellSize), Rows * intCellSize) + 2

        Return posResult
    End Function

End Class