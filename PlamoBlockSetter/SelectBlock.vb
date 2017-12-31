Public Class SelectBlock
    Public Width As Integer
    Public Height As Integer
    Public ColorSetting As BlockColor.ColorSetting
    Public Rotation As Integer

    Public ReadOnly Property RotateWidth() As Integer
        Get
            Return CInt(IIf(Rotation = 0, Width, Height))
        End Get
    End Property

    Public ReadOnly Property RotateHeight() As Integer
        Get
            Return CInt(IIf(Rotation = 0, Height, Width))
        End Get
    End Property

    Public Sub New(pintRows As Integer, pintCols As Integer, pobjColorSetting As BlockColor.ColorSetting, pintRotation As Integer)
        Height = pintRows
        Width = pintCols
        ColorSetting = pobjColorSetting
        Rotation = pintRotation
    End Sub
    Public Sub New()
        Me.New(1, 1, New BlockColor.ColorSetting, 0)
    End Sub
End Class
