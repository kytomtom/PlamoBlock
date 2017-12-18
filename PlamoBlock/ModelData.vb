Public Class ModelData
    Private objLayer As Dictionary(Of Integer, List(Of Position))

    Public Class Position
        Public X As Integer
        Public Y As Integer
        Public W As Integer
        Public D As Integer
        Public R As Integer
        Public C As String

        Public Sub New()
            Clear()
        End Sub

        Public Sub Clear()
            X = 0
            Y = 0
            W = 0
            D = 0
            R = 0
            C = ""
        End Sub
    End Class

    Public Property Layer(Index As Integer) As List(Of Position)
        Get
            AddLayer(Index)
            Return objLayer(Index)
        End Get
        Set(value As List(Of Position))
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
        objLayer = New Dictionary(Of Integer, List(Of Position))
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
                    AddBlockFromFull(lobjGroup.BottomPos + j - 1, lobjBlock)
                Next
            Next
        Next
    End Sub

    Public Sub AddBlockFromFull(pintLayerPos As Integer, pobjBlock As ModelDataFull.Position)
        Dim lobjBlock As Position

        AddLayer(pintLayerPos)

        lobjBlock = New Position
        With lobjBlock
            .X = pobjBlock.X
            .Y = pobjBlock.Y
            .W = pobjBlock.W
            .D = pobjBlock.D
            .R = pobjBlock.R
            .C = pobjBlock.C
        End With

        objLayer(pintLayerPos).Add(lobjBlock)
    End Sub

    Public Sub AddLayer(pintLayerPos As Integer)
        If Not objLayer.ContainsKey(pintLayerPos) Then
            objLayer.Add(pintLayerPos, New List(Of Position))
        End If
    End Sub
End Class
