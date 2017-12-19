Public Class LayerSelector
    Private objViewPictureBox(3) As PictureBox

    Public Event ChangeLayer(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

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
        Dim lobjMaxWidth As Integer
        Dim lobjMaxHeight As Integer

        With BaseLayout.ColumnStyles(0)
            lobjMaxWidth = IIf(.SizeType = SizeType.Percent, .Width * Me.Width / 100, .Width)
        End With
        With BaseLayout.RowStyles(1)
            lobjMaxHeight = IIf(.SizeType = SizeType.Percent, .Height * Me.Height / 100, .Height) * 2
        End With

        If pobjView IsNot Nothing Then
            With pobjView
                .Width = Math.Min(lobjMaxWidth, lobjMaxHeight)
                .Height = .Width
                .Location = New Point(CInt((lobjMaxWidth - .Width) / 2), CInt((lobjMaxHeight - .Height) / 2))
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
