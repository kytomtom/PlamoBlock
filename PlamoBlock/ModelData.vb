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

        Public Function ToJSON() As String
            Dim lobjResult As List(Of String)

            lobjResult = New List(Of String)

            With lobjResult
                .Add(String.Format("""{0}"":""{1}""", "x", Col))
                .Add(String.Format("""{0}"":""{1}""", "y", Row))
                .Add(String.Format("""{0}"":""{1}""", "w", Width))
                .Add(String.Format("""{0}"":""{1}""", "d", Height))
                .Add(String.Format("""{0}"":""{1}""", "r", Rotation))
                .Add(String.Format("""{0}"":""{1}""", "c", Color))
            End With

            Return String.Concat("{", String.Join(",", lobjResult.ToArray), "}")
        End Function
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

    Public ReadOnly Property MaxLayer() As Integer
        Get
            Dim lintMaxLayer As Integer

            lintMaxLayer = 0

            For Each intBuf As Integer In objLayer.Keys
                lintMaxLayer = Math.Max(lintMaxLayer, intBuf)
            Next

            Return lintMaxLayer
        End Get
    End Property

    Public Sub New()
        objLayer = New Dictionary(Of Integer, List(Of Block))
    End Sub
    Public Sub New(pobjModelDataFull As ModelDataFull)
        Me.New()
        SetModelDataFromFull(pobjModelDataFull)
    End Sub

    Public Sub Clear()
        objLayer.Clear()
    End Sub

    Public Sub SetModelDataFromFull(pobjModelDataFull As ModelDataFull)
        Dim lobjGroup As ModelDataFull.BlockGroup

        Clear()

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
                .Col = CInt(pobjBlock.X - pobjModelDataFull.PlateWidth / 2 - 1)
                .Row = CInt(pobjBlock.Y - pobjModelDataFull.PlateHeight / 2 - 1)
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

    Public Function ToJSON() As String
        Dim lobjResult As List(Of String)

        lobjResult = New List(Of String)

        With lobjResult
            .Add(String.Format("""{0}"":""{1}""", "Name", "全身"))
            .Add(String.Format("""{0}"":""{1}""", "BottomPos", 0))
            .Add(LayerToJSON())
        End With

        Return String.Concat("{", vbCrLf, String.Join(vbCrLf & ",", lobjResult.ToArray), vbCrLf, "}")
    End Function

    Private Function LayerToJSON() As String
        Dim lobjResult As List(Of String)

        lobjResult = New List(Of String)

        For i As Integer = 0 To MaxLayer
            lobjResult.Add(LayerToJSON(i))
        Next

        Return String.Concat("""Layer"":[", vbCrLf, String.Join(vbCrLf & ",", lobjResult.ToArray), vbCrLf, "]")
    End Function
    Private Function LayerToJSON(pintLayer As Integer) As String
        Dim lobjResult As List(Of String)

        lobjResult = New List(Of String)

        For Each lobjBlock As Block In Layer(pintLayer)
            lobjResult.Add(lobjBlock.ToJSON)
        Next

        Return String.Concat("[", vbCrLf, String.Join(vbCrLf & ",", lobjResult.ToArray), vbCrLf, "]")
    End Function
End Class
