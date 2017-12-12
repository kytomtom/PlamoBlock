Imports Newtonsoft.Json

Public Class BlockColor
    Private dicColor As Dictionary(Of String, ColorSetting)

    Public Enum ColorName
        White
        Cream
        SilverGray
        Gray
        Black
        DarkBrown
        Brown
        Flesh
        Yellow
        Orange
        Pink
        Red
        LightPurple
        LightBlue
        Blue
        Green
        LightGreen
        Clear
        ClearBlue
        Beige
        ClearRed
        KhakiGreen
        NavyBlue
        PastelPink
    End Enum

    Public ReadOnly Property Color() As Dictionary(Of String, ColorSetting)
        Get
            Return Me.dicColor
        End Get
    End Property
    Public ReadOnly Property Color(pstrColorName As String) As ColorSetting
        Get
            Return Me.dicColor(pstrColorName)
        End Get
    End Property
    Public ReadOnly Property Color(pintColorName As ColorName) As ColorSetting
        Get
            Return Me.Color(pintColorName.ToString)
        End Get
    End Property

    Public Class ColorSetting
        Public Kana As String
        Public Base As Color
        Public Edge As Color
        Public Opacity As Single
    End Class

    Public Sub New()
        Me.dicColor = JsonConvert.DeserializeObject(Of Dictionary(Of String, ColorSetting))(Common.GetResourceText("JSON_BlockColor.json"))
    End Sub
End Class
