Public Class LayerSelector
    'プロパティの既定値
    Private Const _Default_Rows As Integer = 8
    Private Const _Default_Cols As Integer = 8
    Private Const _Default_Layers As Integer = 8

    Private objViewPictureBox(3) As PictureBox
    Private intCols As Integer
    Private intRows As Integer
    Private intLayers As Integer
    Private intCellSize As Integer

    Public Enum TargetBox
        Front = 0
        Back = 1
        Left = 2
        Right = 3
    End Enum

    <System.ComponentModel.Category("_追加設定")>
    <System.ComponentModel.DefaultValue(_Default_Rows)>
    Public Property Rows() As Integer
        Get
            Return intRows
        End Get
        Set(ByVal value As Integer)
            intRows = value
            SetViewPictureBoxSize()
            Redraw()
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
            SetViewPictureBoxSize()
            Redraw()
        End Set
    End Property
    <System.ComponentModel.Category("_追加設定")>
    <System.ComponentModel.DefaultValue(_Default_Layers)>
    Public Property Layers() As Integer
        Set(value As Integer)
            SelectLayer.Maximum = value
            SetViewPictureBoxSize()
            Redraw()
        End Set
        Get
            Return CInt(SelectLayer.Maximum - SelectLayer.Minimum + 1)
        End Get
    End Property
    <System.ComponentModel.Category("_追加設定")>
    Public ReadOnly Property CellSize() As Integer
        Get
            Return intCellSize
        End Get
    End Property

    Public Event ChangeLayer(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ChangeGroup(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        intRows = _Default_Rows
        intCols = _Default_Cols
        SelectLayer.Minimum = 1
        SelectLayer.Maximum = _Default_Layers

        For i As Integer = objViewPictureBox.GetLowerBound(0) To objViewPictureBox.GetUpperBound(0)
            objViewPictureBox(i) = New PictureBox
            With objViewPictureBox(i)
                .Margin = New Padding(0)
                .Size = New Size(100, 100)
                .BackColor = Color.White
            End With
        Next

        PanelFront.Controls.Add(objViewPictureBox(TargetBox.Front))
        PanelBack.Controls.Add(objViewPictureBox(TargetBox.Back))
        PanelLeft.Controls.Add(objViewPictureBox(TargetBox.Left))
        PanelRight.Controls.Add(objViewPictureBox(TargetBox.Right))

        SetViewPictureBoxSize()
    End Sub

    Private Sub SetViewPictureBoxSize()
        FixViewPictureBoxSize(PanelFront, TargetBox.Front)
        FixViewPictureBoxSize(PanelBack, TargetBox.Back)
        FixViewPictureBoxSize(PanelLeft, TargetBox.Left)
        FixViewPictureBoxSize(PanelRight, TargetBox.Right)
    End Sub
    Private Sub FixViewPictureBoxSize(pobjPanel As Panel, pintTargetBox As TargetBox)
        Dim lintMaxWidth As Integer
        Dim lintMaxHeight As Integer

        Dim lintCellWidth As Integer
        Dim lintCellHeight As Integer

        Dim lintMaxSize As Integer

        lintMaxSize = Math.Max(Layers, Math.Max(Cols, Rows))

        lintCellWidth = 0
        For i = 0 To BaseLayout.GetColumnSpan(pobjPanel) - 1
            With BaseLayout.ColumnStyles(BaseLayout.GetColumn(pobjPanel) + i)
                lintCellWidth += CInt(IIf(.SizeType = SizeType.Percent, .Width * BaseLayout.Width / 100, .Width))
            End With
        Next
        lintMaxWidth = CInt(Math.Truncate(lintCellWidth / lintMaxSize) * lintMaxSize)

        lintCellHeight = 0
        For i = 0 To BaseLayout.GetRowSpan(pobjPanel) - 1
            With BaseLayout.RowStyles(BaseLayout.GetRow(pobjPanel) + i)
                lintCellHeight += CInt(IIf(.SizeType = SizeType.Percent, .Height * (BaseLayout.Height - BaseLayout.RowStyles(0).Height) / 100, .Height))
            End With
        Next
        lintMaxHeight = CInt(Math.Truncate(lintCellHeight / lintMaxSize) * lintMaxSize)

        If objViewPictureBox(pintTargetBox) IsNot Nothing Then
            With objViewPictureBox(pintTargetBox)
                .Width = Math.Min(lintMaxWidth, lintMaxHeight)
                .Height = .Width
                .Location = New Point(CInt((lintCellWidth - .Width) / 2), CInt((lintCellHeight - .Height) / 2))
                intCellSize = CInt(.Width / lintMaxSize)
            End With
        End If
    End Sub

    Private Sub LayerSelector_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetViewPictureBoxSize()
        Redraw()
    End Sub

    Private Sub SelectLayer_ValueChanged(sender As Object, e As EventArgs) Handles SelectLayer.ValueChanged
        Redraw()
        RaiseEvent ChangeLayer(Me, New EventArgs)
    End Sub

    Public Sub Redraw()
        For i As Integer = objViewPictureBox.GetLowerBound(0) To objViewPictureBox.GetUpperBound(0)
            DrawBox(i)
        Next
    End Sub

    Private Sub DrawBox(pintTargetBox As Integer)
        Dim objCanvas As Bitmap
        Dim objGraph As Graphics

        If objViewPictureBox(pintTargetBox) Is Nothing Then
            Exit Sub
        End If

        With objViewPictureBox(pintTargetBox)
            objCanvas = New Bitmap(.Width, .Height)

            objGraph = Graphics.FromImage(objCanvas)

            '枠線の描画
            DrawBoxBackGround(objGraph, CType(pintTargetBox, TargetBox))

            '各層の描画
            If Common.ModelData IsNot Nothing Then
                For Each lstrGroup As String In Common.ModelData.Group.Keys
                    For i As Integer = CInt(SelectLayer.Minimum) To CInt(SelectLayer.Maximum)
                        DrawLayer(objGraph, CType(pintTargetBox, TargetBox), lstrGroup, i)
                    Next
                Next
            End If

            DrawLayerMarker(objGraph, CType(pintTargetBox, TargetBox))

            objGraph.Dispose()

            .Image = objCanvas
        End With
    End Sub
    Private Sub DrawBoxBackGround(pobjGraph As Graphics, pintTargetBox As TargetBox)
        Dim lobjPen As Pen

        lobjPen = New Pen(Color.Black, 1)
        lobjPen.DashStyle = Drawing2D.DashStyle.Solid

        pobjGraph.DrawRectangle(lobjPen, 0, 0, objViewPictureBox(pintTargetBox).Width - 1, objViewPictureBox(pintTargetBox).Height - 1)

        lobjPen.Dispose()
    End Sub
    Private Sub DrawLayer(pobjGraph As Graphics, pintTargetBox As TargetBox, pstrGroup As String, pintTargetLayer As Integer)
        If Common.ModelData Is Nothing Then
            Exit Sub
        End If

        For Each objBlock As ModelDataG.Block In SortBlock(pintTargetBox, pstrGroup, pintTargetLayer)
            DrawBlock(pobjGraph, pintTargetBox, pstrGroup, pintTargetLayer, objBlock)
        Next
    End Sub
    Private Sub DrawBlock(pobjGraph As Graphics, pintTargetBox As TargetBox, pstrGroup As String, pintTargetLayer As Integer, pobjBlock As ModelDataG.Block)
        Dim lobjBlockImage As Bitmap
        Dim lobjPos As Point
        Dim lobjCM As System.Drawing.Imaging.ColorMatrix
        Dim lobjImgAtr As System.Drawing.Imaging.ImageAttributes
        Dim lintLen As Integer

        Dim lobjBrush As Brush

        lintLen = BlockLen(pintTargetBox, pobjBlock)

        lobjBlockImage = DirectCast(New BlockImageSide(lintLen, intCellSize, pobjBlock.ColorSetting, IsEdgeView.Checked).Image.Clone, Bitmap)

        lobjPos = CellPoint(pintTargetBox, pintTargetLayer, pobjBlock, 0)

        If IsLatterHalfArea(pintTargetBox, pobjBlock) = False Then
            pobjGraph.DrawImage(lobjBlockImage, lobjPos)
        Else
            '奥のブロックは半透明で表示
            lobjCM = New System.Drawing.Imaging.ColorMatrix()
            With lobjCM
                .Matrix00 = 1
                .Matrix11 = 1
                .Matrix22 = 1
                .Matrix33 = 0.75F
                .Matrix44 = 1
            End With

            lobjImgAtr = New System.Drawing.Imaging.ImageAttributes()
            lobjImgAtr.SetColorMatrix(lobjCM)

            pobjGraph.DrawImage(lobjBlockImage, New Rectangle(lobjPos, lobjBlockImage.Size), 0, 0, lobjBlockImage.Width, lobjBlockImage.Height, GraphicsUnit.Pixel, lobjImgAtr)
        End If

        If pstrGroup <> SelectGroup.SelectedItem.ToString Then
            lobjBrush = New SolidBrush(Color.FromArgb(200, 0, 0, 0))
            pobjGraph.FillRectangle(lobjBrush, New Rectangle(lobjPos, lobjBlockImage.Size))
            lobjBrush.Dispose()
        End If
    End Sub
    Private Sub DrawLayerMarker(pobjGraph As Graphics, pintTargetBox As TargetBox)
        Dim lobjPen As Pen

        Dim lintY As Integer

        lobjPen = New Pen(Color.Red, 1)
        lobjPen.DashStyle = Drawing2D.DashStyle.Solid

        lintY = CInt(objViewPictureBox(pintTargetBox).Height - intCellSize * SelectLayer.Value)

        pobjGraph.DrawRectangle(lobjPen, 0, lintY, objViewPictureBox(pintTargetBox).Width - 1, intCellSize)

        lobjPen.Dispose()
    End Sub

    Public Function SortBlock(pintTargetBox As TargetBox, pstrGroup As String, pintTargetLayer As Integer) As List(Of ModelDataG.Block)
        Dim lobjResult As List(Of ModelDataG.Block)

        Select Case pintTargetBox
            Case TargetBox.Front, TargetBox.Back
                lobjResult = Common.ModelData.Group(pstrGroup).Layer(pintTargetLayer).OrderBy(Function(n) (n.Row + CInt(IIf(n.Rotation = 0, n.Height, n.Width)) - 1)).ToList()

            Case TargetBox.Left, TargetBox.Right
                lobjResult = Common.ModelData.Group(pstrGroup).Layer(pintTargetLayer).OrderBy(Function(n) (n.Col + CInt(IIf(n.Rotation = 0, n.Width, n.Height)) - 1)).ToList()

            Case Else
                lobjResult = Common.ModelData.Group(pstrGroup).Layer(pintTargetLayer)
        End Select

        Select Case pintTargetBox
            Case TargetBox.Back, TargetBox.Right
                lobjResult.Reverse()
        End Select

        Return lobjResult
    End Function
    Private Function BlockLen(pintTargetBox As TargetBox, pobjBlock As ModelDataG.Block) As Integer
        With pobjBlock
            Select Case pintTargetBox
                Case TargetBox.Front, TargetBox.Back
                    Return CInt(IIf(.Rotation = 0, .Width, .Height))

                Case TargetBox.Left, TargetBox.Right
                    Return CInt(IIf(.Rotation = 0, .Height, .Width))
            End Select
        End With

        Return 0
    End Function
    Private Function CellPoint(pintTargetBox As TargetBox, pintTargetLayer As Integer, pobjBlock As ModelDataG.Block, pintShift As Integer) As Point
        Dim lintX As Integer
        Dim lintY As Integer
        Dim lintPos As Integer

        With pobjBlock
            Select Case pintTargetBox
                Case TargetBox.Front
                    lintPos = .Col

                Case TargetBox.Back
                    lintPos = Math.Abs(.Col + 1) * CInt(IIf(.Col < 0, 1, -1)) - (.RotateWidth - 1)

                Case TargetBox.Left
                    lintPos = Math.Abs(.Row + 1) * CInt(IIf(.Row < 0, 1, -1)) - (.RotateHeight - 1)

                Case TargetBox.Right
                    lintPos = .Row
            End Select
        End With
        lintPos += CInt(Layers / 2)

        lintX = intCellSize * lintPos + pintShift

        lintY = objViewPictureBox(pintTargetBox).Height - intCellSize * pintTargetLayer + pintShift

        Return New Point(lintX, lintY)
    End Function
    Private Function IsLatterHalfArea(pintTargetBox As TargetBox, pobjBlock As ModelDataG.Block) As Boolean
        With pobjBlock
            Select Case pintTargetBox
                Case TargetBox.Front
                    If (.Row + CInt(IIf(.Rotation = 0, .Height, .Width)) - 1) < 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Case TargetBox.Back
                    If .Row < 0 Then
                        Return False
                    Else
                        Return True
                    End If

                Case TargetBox.Left
                    If (.Col + CInt(IIf(.Rotation = 0, .Width, .Height)) - 1) < 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Case TargetBox.Right
                    If .Col < 0 Then
                        Return False
                    Else
                        Return True
                    End If
            End Select
        End With

        Return False
    End Function

    Private Sub IsEdgeView_CheckedChanged(sender As Object, e As EventArgs) Handles IsEdgeView.CheckedChanged
        Redraw()
    End Sub

    Public Function LayerShift(pintShiftValue As Integer) As Integer
        With SelectLayer
            .Value = Math.Min(.Maximum, Math.Max(.Minimum, .Value + pintShiftValue))
        End With
        Return SelectLayer.Value
    End Function

    Private Sub SelectGroup_SelectedValueChanged(sender As Object, e As EventArgs) Handles SelectGroup.SelectedValueChanged
        Redraw()
        RaiseEvent ChangeGroup(Me, New EventArgs)
    End Sub

    Private Sub BtnAddNewGroup_Click(sender As Object, e As EventArgs) Handles BtnAddNewGroup.Click
        Dim lstrBuf As String

        lstrBuf = InputBox("新しい部品グループ名")

        If String.IsNullOrWhiteSpace(lstrBuf) Then
            Return
        End If

        If SelectGroup.Items.Contains(lstrBuf) Then
            MsgBox("そのグループはすでに存在します", MsgBoxStyle.Critical)
            Return
        End If

        Common.ModelData.AddGroup(lstrBuf, 1)
        SelectGroup.SelectedIndex = SelectGroup.Items.Add(lstrBuf)
    End Sub
End Class
