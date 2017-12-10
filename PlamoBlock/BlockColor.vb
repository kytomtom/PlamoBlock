Public Class BlockColor
    Public ReadOnly Color As Dictionary(Of ColorName, ColorSetting)

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

    Public Class ColorSetting
        Public ReadOnly Name As String
        Public ReadOnly Kana As String
        Public ReadOnly BaseColor As Color
        Public ReadOnly EdgeColor As Color
        Public ReadOnly Opacity As Single

        Public Sub New(pstrName As String, pstrKana As String, pobjBaseColor As Color, pobjEdgeColor As Color, psngOpacity As Single)
            Me.Name = pstrName
            Me.Kana = pstrKana
            Me.BaseColor = pobjBaseColor
            Me.EdgeColor = pobjEdgeColor
            Me.Opacity = psngOpacity
        End Sub
    End Class

    Public Sub New()
        Me.Color = New Dictionary(Of ColorName, ColorSetting)

        With Me.Color
            .Add(ColorName.White, New ColorSetting("White", "ホワイト", ColorTranslator.FromHtml("#FFFFFF"), ColorTranslator.FromHtml("#4C4C4C"), 1))
            .Add(ColorName.Cream, New ColorSetting("Cream", "クリーム", ColorTranslator.FromHtml("#FFEF85"), ColorTranslator.FromHtml("#4C4727"), 1))
            .Add(ColorName.SilverGray, New ColorSetting("SilverGray", "シルバーグレー", ColorTranslator.FromHtml("#AAAAAA"), ColorTranslator.FromHtml("#333333"), 1))
            .Add(ColorName.Gray, New ColorSetting("Gray", "グレー", ColorTranslator.FromHtml("#555555"), ColorTranslator.FromHtml("#191919"), 1))
            .Add(ColorName.Black, New ColorSetting("Black", "ブラック", ColorTranslator.FromHtml("#000000"), ColorTranslator.FromHtml("#888888"), 1))
            .Add(ColorName.DarkBrown, New ColorSetting("DarkBrown", "ダークブラウン", ColorTranslator.FromHtml("#362519"), ColorTranslator.FromHtml("#100B07"), 1))
            .Add(ColorName.Brown, New ColorSetting("Brown", "ブラウン", ColorTranslator.FromHtml("#6D4C33"), ColorTranslator.FromHtml("#20160F"), 1))
            .Add(ColorName.Flesh, New ColorSetting("Flesh", "ハダイロ", ColorTranslator.FromHtml("#FFE4B5"), ColorTranslator.FromHtml("#4C4436"), 1))
            .Add(ColorName.Yellow, New ColorSetting("Yellow", "イエロー", ColorTranslator.FromHtml("#FFFF00"), ColorTranslator.FromHtml("#4C4C00"), 1))
            .Add(ColorName.Orange, New ColorSetting("Orange", "オレンジ", ColorTranslator.FromHtml("#FF8C00"), ColorTranslator.FromHtml("#4C2A00"), 1))
            .Add(ColorName.Pink, New ColorSetting("Pink", "ピンク", ColorTranslator.FromHtml("#FF69B4"), ColorTranslator.FromHtml("#4C1F36"), 1))
            .Add(ColorName.Red, New ColorSetting("Red", "レッド", ColorTranslator.FromHtml("#FF0000"), ColorTranslator.FromHtml("#4C0000"), 1))
            .Add(ColorName.LightPurple, New ColorSetting("LightPurple", "ライトパープル", ColorTranslator.FromHtml("#660033"), ColorTranslator.FromHtml("#1E000F"), 1))
            .Add(ColorName.LightBlue, New ColorSetting("LightBlue", "ライトブルー", ColorTranslator.FromHtml("#0085C9"), ColorTranslator.FromHtml("#00273C"), 1))
            .Add(ColorName.Blue, New ColorSetting("Blue", "ブルー", ColorTranslator.FromHtml("#0000FF"), ColorTranslator.FromHtml("#00004C"), 1))
            .Add(ColorName.Green, New ColorSetting("Green", "グリーン", ColorTranslator.FromHtml("#006633"), ColorTranslator.FromHtml("#001E0F"), 1))
            .Add(ColorName.LightGreen, New ColorSetting("LightGreen", "ライトグリーン", ColorTranslator.FromHtml("#32CD32"), ColorTranslator.FromHtml("#0F3D0F"), 1))
            .Add(ColorName.Clear, New ColorSetting("Clear", "クリア", ColorTranslator.FromHtml("#FFFFFF"), ColorTranslator.FromHtml("#4C4C4C"), 0.25))
            .Add(ColorName.ClearBlue, New ColorSetting("ClearBlue", "クリアブルー", ColorTranslator.FromHtml("#006FAB"), ColorTranslator.FromHtml("#002133"), 0.75))
            .Add(ColorName.Beige, New ColorSetting("Beige", "ベージュ", ColorTranslator.FromHtml("#F5AF4E"), ColorTranslator.FromHtml("#493417"), 1))
            .Add(ColorName.ClearRed, New ColorSetting("ClearRed", "クリアレッド", ColorTranslator.FromHtml("#FF0000"), ColorTranslator.FromHtml("#4C0000"), 0.75))
            .Add(ColorName.KhakiGreen, New ColorSetting("KhakiGreen", "カーキグリーン", ColorTranslator.FromHtml("#545F1D"), ColorTranslator.FromHtml("#191C08"), 1))
            .Add(ColorName.NavyBlue, New ColorSetting("NavyBlue", "ネイビーブルー", ColorTranslator.FromHtml("#010F29"), ColorTranslator.FromHtml("#00040C"), 1))
            .Add(ColorName.PastelPink, New ColorSetting("PastelPink", "パステルピンク", ColorTranslator.FromHtml("#FFB6C1"), ColorTranslator.FromHtml("#4C3639"), 1))
        End With
    End Sub

    Public Function NameToColor(pstrColorName As String) As ColorSetting
        Return Me.Color(DirectCast([Enum].Parse(GetType(ColorName), pstrColorName), ColorName))
    End Function
End Class
