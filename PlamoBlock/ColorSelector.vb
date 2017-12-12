Public Class ColorSelector
    Inherits FlowLayoutPanel

    Private objBlockColor As BlockColor
    Private strColorName As String

    Public Event ChangeColor(ByVal sender As Object, ByVal e As EventArgs)

    Public ReadOnly Property SelectColorName() As String
        Get
            Return Me.strColorName
        End Get
    End Property
    Public ReadOnly Property SelectColorSetting() As BlockColor.ColorSetting
        Get
            Return Me.objBlockColor.Color(Me.strColorName)
        End Get
    End Property

    Public Function SetBlockColor(pobjBlockColor As BlockColor) As Boolean
        Me.objBlockColor = pobjBlockColor

        Me.strColorName = ""

        Me.Controls.Clear()

        For Each lstrColorName As String In Me.objBlockColor.Color.Keys
            Me.AddColor(lstrColorName)
        Next

        Me.SelectColor(Me.objBlockColor.Color.Keys(0))

        Return True
    End Function

    Private Function AddColor(pstrColotName As String)
        Dim lobjNewColor As Button

        lobjNewColor = New Button
        With lobjNewColor
            .Name = MakeColorButtonName(pstrColotName)
            .Width = 96
            .Height = 32
            .Margin = New Padding(0)
            .Text = Me.objBlockColor.Color(pstrColotName).Kana
            .ForeColor = Me.objBlockColor.Color(pstrColotName).Edge
            .BackColor = Me.objBlockColor.Color(pstrColotName).Base
            .Tag = pstrColotName
        End With
        Me.Controls.Add(lobjNewColor)

        AddHandler lobjNewColor.Click, AddressOf ColorButton_Click

        Return True
    End Function
    Private Function MakeColorButtonName(pstrColotName As String) As String
        Return String.Format("ColorButton_{0}", pstrColotName)
    End Function

    Private Sub ColorButton_Click(sender As Object, e As EventArgs)
        Call Me.SelectColor(DirectCast(sender, Control).Tag)
    End Sub

    Public Function SelectColor(pstrColorName As String) As BlockColor.ColorSetting
        Me.strColorName = pstrColorName

        RaiseEvent ChangeColor(Me, New EventArgs)

        Return Me.SelectColorSetting
    End Function
    Public Function SelectColor(pintColorName As BlockColor.ColorName) As BlockColor.ColorSetting
        Return Me.SelectColor(pintColorName.ToString)
    End Function
End Class
