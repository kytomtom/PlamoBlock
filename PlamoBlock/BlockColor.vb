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

    Public ReadOnly Property Color(ColorName As ColorName) As ColorSetting
        Get
            Return Me.dicColor(ColorName.ToString)
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
