Public Class SelectBlock
    Public Rows As Integer
    Public Cols As Integer
    Public ColorSetting As BlockColor.ColorSetting
    Public Rotation As Integer

    Public Sub New(pintRows As Integer, pintCols As Integer, pobjColorSetting As BlockColor.ColorSetting, pintRotation As Integer)
        Rows = pintRows
        Cols = pintCols
        ColorSetting = pobjColorSetting
        Rotation = pintRotation
    End Sub
    Public Sub New()
        Me.New(1, 1, New BlockColor.ColorSetting, 0)
    End Sub
End Class
