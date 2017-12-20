Public Class LayerSelector
    Private objViewPictureBox(3) As PictureBox
    Private intCellSize As Integer

    Public ReadOnly Property CellSize() As Integer
        Get
            Return intCellSize
        End Get
    End Property

    Public Event ChangeLayer(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        SelectLayer.Minimum = 0
        SelectLayer.Maximum = 23

        For i As Integer = 0 To 3
            objViewPictureBox(i) = New PictureBox
            With objViewPictureBox(i)
                .Margin = New Padding(0)
                .Size = New Size(100, 100)
                .BackColor = Color.White
            End With
        Next

        PanelFront.Controls.Add(objViewPictureBox(0))
        PanelBack.Controls.Add(objViewPictureBox(1))
        PanelLeft.Controls.Add(objViewPictureBox(2))
        PanelRight.Controls.Add(objViewPictureBox(3))

        SetViewPictureBoxSize()
    End Sub

    Private Sub SetViewPictureBoxSize()
        FixViewPictureBoxSize(PanelFront, objViewPictureBox(0))
        FixViewPictureBoxSize(PanelBack, objViewPictureBox(1))
        FixViewPictureBoxSize(PanelLeft, objViewPictureBox(2))
        FixViewPictureBoxSize(PanelRight, objViewPictureBox(3))
    End Sub
    Private Sub FixViewPictureBoxSize(pobjPanel As Panel, pobjView As PictureBox)
        Dim lintMaxWidth As Integer
        Dim lintMaxHeight As Integer

        Dim lintLayers As Integer

        lintLayers = SelectLayer.Maximum - SelectLayer.Minimum + 1

        With BaseLayout.ColumnStyles(0)
            lintMaxWidth = Math.Truncate(IIf(.SizeType = SizeType.Percent, .Width * Me.Width / 100, .Width) / lintLayers) * lintLayers
        End With
        With BaseLayout.RowStyles(1)
            lintMaxHeight = Math.Truncate(IIf(.SizeType = SizeType.Percent, .Height * Me.Height / 100, .Height) * 2 / lintLayers) * lintLayers
        End With

        If pobjView IsNot Nothing Then
            With pobjView
                .Width = Math.Min(lintMaxWidth, lintMaxHeight)
                .Height = .Width
                .Location = New Point(CInt((lintMaxWidth - .Width) / 2), CInt((lintMaxHeight - .Height) / 2))
                intCellSize = .Width / lintLayers
            End With
        End If
    End Sub

    Private Sub LayerSelector_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetViewPictureBoxSize()
    End Sub

    Private Sub SelectLayer_ValueChanged(sender As Object, e As EventArgs) Handles SelectLayer.ValueChanged
        RaiseEvent ChangeLayer(Me, New EventArgs)
    End Sub
End Class
