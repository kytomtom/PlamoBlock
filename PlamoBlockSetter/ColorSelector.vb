Public Class ColorSelector
    Inherits FlowLayoutPanel

    'プロパティの既定値
    Private Const _Default_ButtonWidth As Integer = 48
    Private Const _Default_ButtonHeight As Integer = 32
    Private Const _Default_ButtonFontSize As Single = 6
    Private Const _Default_MaxRows As Integer = 3
    Private Const _Default_MaxCols As Integer = 8

    Private objBlockColor As BlockColor
    Private strColorName As String

    Private intButtonWidth As Integer
    Private intButtonHeight As Integer
    Private sngButtonFontSize As Single
    Private intMaxRows As Integer
    Private intMaxCols As Integer

    Public Event ChangeColor(ByVal sender As Object, ByVal e As EventArgs)

    Public ReadOnly Property SelectColorName() As String
        Get
            Return strColorName
        End Get
    End Property
    Public ReadOnly Property SelectColorSetting() As BlockColor.ColorSetting
        Get
            Return objBlockColor.Color(strColorName)
        End Get
    End Property

    <System.ComponentModel.Category("_追加設定")>
    <System.ComponentModel.DefaultValue(_Default_ButtonWidth)>
    Public Property ButtonWidth() As Integer
        Get
            Return intButtonWidth
        End Get
        Set(ByVal value As Integer)
            intButtonWidth = value
            SetControlSize()
        End Set
    End Property
    <System.ComponentModel.Category("_追加設定")>
    <System.ComponentModel.DefaultValue(_Default_ButtonHeight)>
    Public Property ButtonHeight() As Integer
        Get
            Return intButtonHeight
        End Get
        Set(ByVal value As Integer)
            intButtonHeight = value
            SetControlSize()
        End Set
    End Property
    <System.ComponentModel.Category("_追加設定")>
    <System.ComponentModel.DefaultValue(_Default_ButtonFontSize)>
    Public Property ButtonFontSize() As Single
        Get
            Return sngButtonFontSize
        End Get
        Set(ByVal value As Single)
            sngButtonFontSize = value
            SetControlSize()
        End Set
    End Property
    <System.ComponentModel.Category("_追加設定")>
    <System.ComponentModel.DefaultValue(_Default_MaxRows)>
    Public Property MaxRows() As Integer
        Get
            Return intMaxRows
        End Get
        Set(ByVal value As Integer)
            intMaxRows = value
            SetControlSize()
        End Set
    End Property
    <System.ComponentModel.Category("_追加設定")>
    <System.ComponentModel.DefaultValue(_Default_MaxCols)>
    Public Property MaxCols() As Integer
        Get
            Return intMaxCols
        End Get
        Set(ByVal value As Integer)
            intMaxCols = value
            SetControlSize()
        End Set
    End Property

    Public Sub New()
        intButtonWidth = _Default_ButtonWidth
        intButtonHeight = _Default_ButtonHeight
        sngButtonFontSize = _Default_ButtonFontSize
        intMaxRows = _Default_MaxRows
        intMaxCols = _Default_MaxCols
        objBlockColor = New BlockColor
        strColorName = BlockColor.ColorName.White.ToString
    End Sub

    Private Sub ColorSelector_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetControlSize()
    End Sub

    Public Function SetBlockColor(pobjBlockColor As BlockColor) As Boolean
        objBlockColor = pobjBlockColor

        strColorName = ""

        Controls.Clear()

        For Each lstrColorName As String In objBlockColor.Color.Keys
            AddColor(lstrColorName)
        Next

        SelectColor(Me.objBlockColor.Color.Keys(0))

        Return True
    End Function

    Private Function AddColor(pstrColotName As String) As Boolean
        Dim lobjNewColor As Button

        lobjNewColor = New Button
        With lobjNewColor
            .Name = MakeSubControlName(pstrColotName)
            .Width = intButtonWidth
            .Height = intButtonHeight
            .Margin = New Padding(0)
            .Text = objBlockColor.Color(pstrColotName).Kana
            .ForeColor = objBlockColor.Color(pstrColotName).Edge
            .BackColor = objBlockColor.Color(pstrColotName).Base
            .Font = New Font(.Font.FontFamily, 8)
            .Tag = pstrColotName
        End With
        Controls.Add(lobjNewColor)

        AddHandler lobjNewColor.MouseDown, AddressOf ColorButton_MouseDown

        Return True
    End Function
    Private Function MakeSubControlName(pstrColotName As String) As String
        Return String.Format("ColorButton_{0}", pstrColotName)
    End Function

    Private Sub ColorButton_MouseDown(sender As Object, e As MouseEventArgs)
        SelectColor(DirectCast(sender, Control).Tag.ToString)
    End Sub

    Public Function SelectColor(pstrColorName As String) As BlockColor.ColorSetting
        strColorName = pstrColorName

        RaiseEvent ChangeColor(Me, New EventArgs)

        Return SelectColorSetting
    End Function
    Public Function SelectColor(pintColorName As BlockColor.ColorName) As BlockColor.ColorSetting
        Return SelectColor(pintColorName.ToString)
    End Function

    Private Sub SetControlSize()
        Width = intButtonWidth * intMaxCols
        Height = intButtonHeight * intMaxRows
    End Sub
End Class
