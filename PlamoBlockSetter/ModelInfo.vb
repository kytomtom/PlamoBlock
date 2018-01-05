Imports System.Windows.Forms

Public Class ModelInfo

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        With Common.ModelData
            .Name = InfoName.Text
            .DisplayName = InfoDisplayName.Text
            .Twitter = InfoTwitter.Text
            .Copyright = InfoCopyright.Text
            .PlateWidth = InfoPlateWidth.Value
            .PlateHeight = InfoPlateHeight.Value
            .PlateColor = InfoPlateColor.SelectedItem
        End With

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ModelInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitColorList()

        With Common.ModelData
            InfoName.Text = .Name
            InfoDisplayName.Text = .DisplayName
            InfoTwitter.Text = .Twitter
            InfoCopyright.Text = .Copyright
            InfoPlateWidth.Value = .PlateWidth
            InfoPlateHeight.Value = .PlateHeight
            InfoPlateColor.SelectedItem = .PlateColor
        End With
    End Sub

    Private Sub InitColorList()
        InfoPlateColor.DropDownStyle = ComboBoxStyle.DropDownList
        InfoPlateColor.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler InfoPlateColor.DrawItem, AddressOf InfoPlateColor_DrawItem

        For Each lstrColorName As String In Common.BlockColor.Color.Keys
            InfoPlateColor.Items.Add(lstrColorName)
        Next
    End Sub

    Private Sub InfoPlateColor_DrawItem(sender As Object, e As DrawItemEventArgs) Handles InfoPlateColor.DrawItem
        Dim lobjColor As BlockColor.ColorSetting
        Dim lobjBackBrush As Brush
        Dim lobjTextBrush As Brush
        Dim lobjRect As RectangleF

        If e.Index = -1 Then
            Exit Sub
        End If

        lobjColor = Common.BlockColor.Color(InfoPlateColor.Items(e.Index).ToString)
        lobjBackBrush = New SolidBrush(Color.FromArgb(CInt(255 * lobjColor.Opacity), lobjColor.Base))
        lobjTextBrush = New SolidBrush(Color.FromArgb(255, lobjColor.Edge))

        With lobjRect
            .X = e.Bounds.X
            .Y = e.Bounds.Y
            .Width = e.Bounds.Width
            .Height = e.Bounds.Height
        End With

        e.DrawBackground()
        e.Graphics.FillRectangle(lobjBackBrush, lobjRect)
        e.Graphics.DrawString(lobjColor.Kana, e.Font, lobjTextBrush, lobjRect)
        e.DrawFocusRectangle()

        lobjBackBrush.Dispose()
        lobjTextBrush.Dispose()
    End Sub
End Class
