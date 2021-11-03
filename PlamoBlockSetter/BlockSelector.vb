Public Class BlockSelector
    Inherits FlowLayoutPanel

    Private Const _Default_CellSize As Integer = 16

    Private intBlockSize As Integer()

    Private objColorSetting As BlockColor.ColorSetting

    Private intCellSize As Integer

    Private lstBlockSize As List(Of Integer())

    Public Event ChangeBlockSize(ByVal sender As Object, ByVal e As EventArgs)

    <System.ComponentModel.Category("_追加設定")>
    <System.ComponentModel.DefaultValue(_Default_CellSize)>
    Public Property CellSize() As Integer
        Get
            Return intCellSize
        End Get
        Set(ByVal value As Integer)
            intCellSize = value
            SetBlockObject(objColorSetting)
        End Set
    End Property

    Public ReadOnly Property SelectBlockSize() As Integer()
        Get
            Return intBlockSize
        End Get
    End Property
    Public ReadOnly Property SelectBlockSizeRows() As Integer
        Get
            Return intBlockSize(0)
        End Get
    End Property
    Public ReadOnly Property SelectBlockSizeCols() As Integer
        Get
            Return intBlockSize(1)
        End Get
    End Property

    Public Property ColorSetting() As BlockColor.ColorSetting
        Get
            Return objColorSetting
        End Get
        Set(value As BlockColor.ColorSetting)
            objColorSetting = value
            SetBlockObject(objColorSetting)
        End Set
    End Property

    Public Sub New()
        intCellSize = _Default_CellSize
        intBlockSize = {1, 1}
        SetBlockSizeList()
        SetBlockObject(New BlockColor.ColorSetting)
    End Sub

    Private Sub SetBlockSizeList()
        lstBlockSize = New List(Of Integer())
        With lstBlockSize
            .Add({1, 1})
            .Add({1, 2})
            .Add({1, 3})
            .Add({1, 4})
            .Add({1, 6})
            .Add({1, 8})
            .Add({2, 2})
            .Add({2, 3})
            .Add({2, 4})
            .Add({2, 6})
            .Add({2, 8})
        End With
    End Sub

    Public Function SetBlockObject(pobjColorSetting As BlockColor.ColorSetting) As Boolean
        objColorSetting = pobjColorSetting

        Controls.Clear()

        For Each lintBlockSize As Integer() In lstBlockSize
            AddBlock(lintBlockSize(0), lintBlockSize(1))
        Next

        'SelectBlock(DirectCast(Controls(0).Tag, Integer()))
        SelectBlock(intBlockSize)

        Return True
    End Function
    Private Function AddBlock(pintRows As Integer, pintCols As Integer) As Boolean
        Dim lobjNewBlock As Button
        Dim lobjBlockImage As BlockImage

        lobjNewBlock = New Button
        lobjBlockImage = New BlockImage(pintRows, pintCols, intCellSize, objColorSetting, 0)
        With lobjNewBlock
            .Name = MakeSubControlName(pintRows, pintCols)
            .Width = lobjBlockImage.Width + 6
            .Height = lobjBlockImage.Height + 6
            .Margin = New Padding(0, 0, 2, 2)
            .Image = DirectCast(lobjBlockImage.Image.Clone, Image)
            .Tag = DirectCast({pintRows, pintCols}, Integer())
        End With
        Controls.Add(lobjNewBlock)

        AddHandler lobjNewBlock.MouseDown, AddressOf BlockButton_MouseDown

        lobjBlockImage = Nothing

        Return True
    End Function
    Private Function MakeSubControlName(pintRows As Integer, pintCols As Integer) As String
        Return String.Format("BlockButton_{0}x{1}", pintRows, pintCols)
    End Function

    Private Sub BlockButton_MouseDown(sender As Object, e As MouseEventArgs)
        SelectBlock(DirectCast(DirectCast(sender, Control).Tag, Integer()))
    End Sub

    Public Function SelectBlock(pintBlockSize As Integer()) As Integer()
        intBlockSize = pintBlockSize

        RaiseEvent ChangeBlockSize(Me, New EventArgs)

        Return SelectBlockSize
    End Function
End Class
