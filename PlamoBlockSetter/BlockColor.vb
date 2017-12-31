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
            Return dicColor
        End Get
    End Property
    Public ReadOnly Property Color(pstrColorName As String) As ColorSetting
        Get
            Return dicColor(pstrColorName)
        End Get
    End Property
    Public ReadOnly Property Color(pintColorName As ColorName) As ColorSetting
        Get
            Return Color(pintColorName.ToString)
        End Get
    End Property

    Public Class ColorSetting
        Public Name As String
        Public Kana As String
        Public Base As Color
        Public Edge As Color
        Public Opacity As Single

        Public Sub New()
            Name = ""
            Kana = ""
            Base = System.Drawing.Color.White
            Edge = System.Drawing.Color.Black
            Opacity = 1
        End Sub
    End Class

    Public Sub New()
        dicColor = JsonConvert.DeserializeObject(Of Dictionary(Of String, ColorSetting))(Common.GetResourceText("JSON_BlockColor.json"))

        For Each lstrBuf As String In dicColor.Keys
            dicColor(lstrBuf).Name = lstrBuf
        Next
    End Sub
End Class
