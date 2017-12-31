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

    Private intSelectLayer As Integer

    Public Event ChangeModel(ByVal sender As Object, ByVal e As EventArgs)
    Public Event RemoveBlock(ByVal sender As Object, ByVal e As EventArgs)

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

    Public Property SelectLayer() As Integer
        Get
            Return intSelectLayer
        End Get
        Set(value As Integer)
            intSelectLayer = value
            Redraw()
        End Set
    End Property

    Public Sub New()
        BackColor = Color.White

        intRows = _Default_Rows
        intCols = _Default_Cols
        intCellSize = _Default_CellSize

        intSelectLayer = 1

        objSelectBlock = New SelectBlock

        SetWorkAreaSize(intRows, intCols, intCellSize)
    End Sub

    Private Sub WorkArea_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetControlSize()
    End Sub

    Public Sub SetSelectBlock(pintRows As Integer, pintCols As Integer, pobjColorSetting As BlockColor.ColorSetting, pintRotation As Integer)
        objSelectBlock = New SelectBlock(pintRows, pintCols, pobjColorSetting, pintRotation)

        'objSelectBlockImage = DirectCast(New BlockImageOutline(objSelectBlock, intCellSize).Image.Clone, Image)
        objSelectBlockImage = DirectCast(New BlockImage(objSelectBlock, intCellSize).Image.Clone, Bitmap)

        Redraw()
    End Sub
    Public Sub SetSelectBlock(pintRows As Integer, pintCols As Integer, pobjColorSetting As BlockColor.ColorSetting)
        Dim lintRotation As Integer

        If objSelectBlock Is Nothing Then
            lintRotation = 1
        Else
            lintRotation = objSelectBlock.Rotation
        End If

        SetSelectBlock(pintRows, pintCols, pobjColorSetting, lintRotation)
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
            lintBuf = CellToPoint(0, i, 0).X
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
            lintBuf = CellToPoint(i, 0, 0).Y
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
        lintBuf = CellToPoint(0, 0, 0).X
        g.DrawLine(p, lintBuf, 0, lintBuf, Height - 1)
        lintBuf = CellToPoint(0, 0, 0).Y
        g.DrawLine(p, 0, lintBuf, Width - 1, lintBuf)

        'リソースを解放する
        p.Dispose()
        g.Dispose()
    End Sub

    Private Sub WorkArea_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Redraw()
    End Sub

    Private Sub WorkArea_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        RotateBlock()
    End Sub

    Public Function IsMouseInArea() As Boolean
        Dim mouseScreenPos As Point = Control.MousePosition
        Dim mouseClientPos As Point = Me.PointToClient(mouseScreenPos)

        Return Me.ClientRectangle.Contains(mouseClientPos)
    End Function

    Public Sub Redraw()
        Dim objCanvas As New Bitmap(Width, Height)
        Dim objGraph As Graphics

        objGraph = Graphics.FromImage(objCanvas)
        objGraph.DrawImage(objBackImage, 0, 0)

        DrawLayer(objGraph, True)
        DrawLayer(objGraph, False)

        'マウスカーソルが領域内にある場合、ブロック配置用のカーソルを表示
        If IsMouseInArea() Then
            DrawCursor(objGraph)
        End If

        objGraph.Dispose()

        Image = objCanvas
    End Sub

    Private Sub DrawLayer(pobjGraph As Graphics, pbolUnderLayer As Boolean)
        Dim intTargetLayer As Integer

        If Common.ModelData Is Nothing Then
            Exit Sub
        End If

        'If intSelectLayer > Common.ModelData.MaxLayer Then
        '    Exit Sub
        'End If

        If pbolUnderLayer AndAlso intSelectLayer <= 1 Then
            Exit Sub
        End If

        intTargetLayer = intSelectLayer + CInt(IIf(pbolUnderLayer, -1, 0))

        For Each objBlock As ModelData.Block In Common.ModelData.Layer(intTargetLayer)
            DrawBlock(pobjGraph, objBlock, pbolUnderLayer)
        Next
    End Sub
    Private Sub DrawBlock(pobjGraph As Graphics, pobjBlock As ModelData.Block, pbolUnderLayer As Boolean)
        Dim lobjBlockImage As Bitmap
        Dim lobjPos As Point
        Dim lobjCM As System.Drawing.Imaging.ColorMatrix
        Dim lobjImgAtr As System.Drawing.Imaging.ImageAttributes

        lobjBlockImage = DirectCast(New BlockImage(pobjBlock, intCellSize).Image.Clone, Bitmap)

        lobjPos = CellToPoint(pobjBlock.Row, pobjBlock.Col, 0)

        If pbolUnderLayer = False Then
            pobjGraph.DrawImage(lobjBlockImage, lobjPos)
        Else
            lobjCM = New System.Drawing.Imaging.ColorMatrix()
            With lobjCM
                .Matrix00 = 1
                .Matrix11 = 1
                .Matrix22 = 1
                .Matrix33 = 0.3F
                .Matrix44 = 1
            End With

            lobjImgAtr = New System.Drawing.Imaging.ImageAttributes()
            lobjImgAtr.SetColorMatrix(lobjCM)

            pobjGraph.DrawImage(lobjBlockImage, New Rectangle(lobjPos, lobjBlockImage.Size), 0, 0, lobjBlockImage.Width, lobjBlockImage.Height, GraphicsUnit.Pixel, lobjImgAtr)
        End If
    End Sub

    Private Sub DrawCursor(pobjGraph As Graphics)
        Dim lobjPos As Point
        Dim lobjCM As System.Drawing.Imaging.ColorMatrix
        Dim lobjImgAtr As System.Drawing.Imaging.ImageAttributes

        If objSelectBlockImage Is Nothing Then
            Exit Sub
        End If

        lobjCM = New System.Drawing.Imaging.ColorMatrix()
        With lobjCM
            .Matrix00 = 1
            .Matrix11 = 1
            .Matrix22 = 1
            .Matrix33 = 0.8F
            .Matrix44 = 1
        End With

        lobjImgAtr = New System.Drawing.Imaging.ImageAttributes()
        lobjImgAtr.SetColorMatrix(lobjCM)

        lobjPos = CursorToBlockPoint(3)

        pobjGraph.DrawImage(objSelectBlockImage, New Rectangle(lobjPos, objSelectBlockImage.Size), 0, 0, objSelectBlockImage.Width, objSelectBlockImage.Height, GraphicsUnit.Pixel, lobjImgAtr)
    End Sub

    Private Function CellToPoint(pintRow As Integer, pintCol As Integer, pintShift As Integer) As Point
        Dim lintX As Integer
        Dim lintY As Integer

        lintX = CInt(Math.Truncate(Me.intCols / 2 + 1) + pintCol) * intCellSize + pintShift
        lintY = CInt(Math.Truncate(Me.intRows / 2 + 1) + pintRow) * intCellSize + pintShift

        Return New Point(lintX, lintY)
    End Function
    Private Function CursorToBlockPoint(pintShift As Integer) As Point
        Dim lposMouse As Point
        Dim lposResult As Point

        If objSelectBlockImage Is Nothing Then
            Return New Point(0, 0)
        End If

        lposMouse = PointToClient(Control.MousePosition)

        With lposResult
            .X = CInt(lposMouse.X - objSelectBlockImage.Width / 2)
            .X = CInt(Math.Round(lposResult.X / intCellSize) * intCellSize)
            .X = Math.Min(Math.Max(lposResult.X, intCellSize), Cols * intCellSize) + pintShift

            .Y = CInt(lposMouse.Y - objSelectBlockImage.Height / 2)
            .Y = CInt(Math.Round(lposResult.Y / intCellSize) * intCellSize)
            .Y = Math.Min(Math.Max(lposResult.Y, intCellSize), Rows * intCellSize) + pintShift
        End With

        Return lposResult
    End Function
    Private Function PointToCell(pobjPoint As Point) As Integer()
        Dim lintRow As Integer
        Dim lintCol As Integer

        lintRow = CInt(Math.Truncate(pobjPoint.Y / intCellSize) + MinRow - 1)
        lintCol = CInt(Math.Truncate(pobjPoint.X / intCellSize) + MinCol - 1)

        Return {lintRow, lintCol}
    End Function

    Private Sub RotateBlock()
        If objSelectBlock Is Nothing Then
            Exit Sub
        End If

        With objSelectBlock
            SetSelectBlock(.Height, .Width, .ColorSetting, CInt(Math.Abs(.Rotation - 1)))
        End With
    End Sub

    Private Sub PutBlock(pintRow As Integer, pintCol As Integer)
        Dim lobjBlock As ModelData.Block

        '既にブロックが配置されている場所には配置できない
        If Common.ModelData.IsCellBlank(intSelectLayer, pintRow, pintCol, objSelectBlock.RotateWidth, objSelectBlock.RotateHeight) = False Then
            Exit Sub
        End If

        With objSelectBlock
            lobjBlock = Common.ModelData.AddBlock(intSelectLayer, pintRow, pintCol, .Width, .Height, .Rotation, .ColorSetting.Name)
        End With

        'Common.ModelData.Layer(intSelectLayer).Add(lobjBlock)
    End Sub
    Private Sub PutBlock(pintPoint As Integer())
        PutBlock(pintPoint(0), pintPoint(1))
    End Sub
    Private Sub PutBlock(pobjPoint As Point)
        PutBlock(pobjPoint.Y, pobjPoint.X)
    End Sub

    Private Sub PullBlock(pintRow As Integer, pintCol As Integer)
        Dim lobjBlock As ModelData.Block

        If Common.ModelData.IsCellBlank(intSelectLayer, pintRow, pintCol) Then
            Exit Sub
        End If

        lobjBlock = Common.ModelData.RemoveCellBlock(intSelectLayer, pintRow, pintCol)

        With objSelectBlock
            .Width = lobjBlock.Width
            .Height = lobjBlock.Height
            .ColorSetting = lobjBlock.ColorSetting
            .Rotation = lobjBlock.Rotation
        End With
        objSelectBlockImage = DirectCast(New BlockImage(objSelectBlock, intCellSize).Image.Clone, Bitmap)
    End Sub
    Private Sub PullBlock(pintPoint As Integer())
        PullBlock(pintPoint(0), pintPoint(1))
    End Sub
    Private Sub PullBlock(pobjPoint As Point)
        PullBlock(pobjPoint.Y, pobjPoint.X)
    End Sub

    Private Sub WorkArea_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Select Case e.Button
            Case MouseButtons.Left
                PutBlock(PointToCell(CursorToBlockPoint(0)))

                Redraw()

                RaiseEvent ChangeModel(Me, New EventArgs)

            Case MouseButtons.Right
                PullBlock(PointToCell(PointToClient(Control.MousePosition)))

                Redraw()

                RaiseEvent RemoveBlock(Me, New EventArgs)
        End Select
    End Sub
End Class