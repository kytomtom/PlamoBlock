Public Class ModelData
    Private objLayer As Dictionary(Of Integer, List(Of Block))

    Public Class Block
        Public Row As Integer
        Public Col As Integer
        Public Width As Integer
        Public Height As Integer
        Public Rotation As Integer
        Public Color As String

        Public ReadOnly Property ColorSetting() As BlockColor.ColorSetting
            Get
                Return Common.BlockColor.Color(Color)
            End Get
        End Property

        Public Sub New()
            Clear()
        End Sub

        Public Sub Clear()
            Col = 0
            Row = 0
            Width = 0
            Height = 0
            Rotation = 0
            Color = ""
        End Sub
    End Class

    Public Property Layer(Index As Integer) As List(Of Block)
        Get
            AddLayer(Index)
            Return objLayer(Index)
        End Get
        Set(value As List(Of Block))
            AddLayer(Index)
            objLayer(Index) = value
        End Set
    End Property

    Public ReadOnly Property MaxHeight() As Integer
        Get
            Dim lintMaxHeight As Integer

            lintMaxHeight = 0

            For Each intBuf As Integer In objLayer.Keys
                lintMaxHeight = Math.Max(lintMaxHeight, intBuf)
            Next

            Return lintMaxHeight
        End Get
    End Property

    Public Sub New()
        objLayer = New Dictionary(Of Integer, List(Of Block))
    End Sub
    Public Sub New(pobjModelDataFull As ModelDataFull)
        Me.New()
        SetModelDataFromFull(pobjModelDataFull)
    End Sub

    Public Sub SetModelDataFromFull(pobjModelDataFull As ModelDataFull)
        Dim lobjGroup As ModelDataFull.BlockGroup

        For i As Integer = 0 To pobjModelDataFull.PartsNum - 1
            lobjGroup = pobjModelDataFull.Parts(i)

            For j = 0 To lobjGroup.Layer.Count - 1
                For Each lobjBlock As ModelDataFull.Position In lobjGroup.Layer(j)
                    AddBlockFromFull(pobjModelDataFull, lobjGroup.BottomPos + j - 1, lobjBlock)
                Next
            Next
        Next
    End Sub

    Public Sub AddBlockFromFull(pobjModelDataFull As ModelDataFull, pintLayerPos As Integer, pobjBlock As ModelDataFull.Position)
        Dim lobjBlock As Block

        AddLayer(pintLayerPos)

        lobjBlock = New Block
        With lobjBlock
            If pobjModelDataFull.Version < 1 Then
                .Col = pobjBlock.X - pobjModelDataFull.PlateWidth / 2 - 1
                .Row = pobjBlock.Y - pobjModelDataFull.PlateHeight / 2 - 1
            Else
                .Col = pobjBlock.X
                .Row = pobjBlock.Y
            End If
            .Width = pobjBlock.W
            .Height = pobjBlock.D
            .Rotation = pobjBlock.R
            .Color = pobjBlock.C
        End With

        objLayer(pintLayerPos).Add(lobjBlock)
    End Sub

    Public Sub AddLayer(pintLayerPos As Integer)
        If Not objLayer.ContainsKey(pintLayerPos) Then
            objLayer.Add(pintLayerPos, New List(Of Block))
        End If
    End Sub
End Class
