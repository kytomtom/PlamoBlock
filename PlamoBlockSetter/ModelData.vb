Public Class ModelData
    Public Const Version As Single = 1

    Private objLayer As Dictionary(Of Integer, List(Of Block))

    Public Name As String
    Public DisplayName As String
    Public Twitter As String
    Public Copyright As String
    Public PlateWidth As Integer
    Public PlateHeight As Integer
    Public PlateColor As String

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
                .Add(String.Format("""{0}"":""{1}""", "w", IIf(Width < Height, Width, Height)))
                .Add(String.Format("""{0}"":""{1}""", "d", IIf(Width < Height, Height, Width)))
                .Add(String.Format("""{0}"":""{1}""", "r", IIf(Width < Height, Rotation, Math.Abs(Rotation - 1))))
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
                If objLayer(intBuf).Count > 0 Then
                    lintMaxLayer = Math.Max(lintMaxLayer, intBuf)
                End If
            Next

            Return lintMaxLayer
        End Get
    End Property

    Public Sub New()
        objLayer = New Dictionary(Of Integer, List(Of Block))
        Clear()
    End Sub
    Public Sub New(pobjModelDataFull As ModelDataFull)
        Me.New()
        SetModelDataFromFull(pobjModelDataFull)
    End Sub

    Public Sub Clear()
        Name = ""
        DisplayName = ""
        Twitter = ""
        Copyright = ""
        PlateWidth = 14
        PlateHeight = 14
        PlateColor = BlockColor.ColorName.White.ToString

        objLayer.Clear()
    End Sub

    Public Sub SetModelDataFromFull(pobjModelDataFull As ModelDataFull)
        Dim lobjGroup As ModelDataFull.BlockGroup

        Clear()

        With pobjModelDataFull
            Name = .Name
            DisplayName = .DisplayName
            Twitter = .Twitter
            Copyright = .Copyright
            PlateWidth = .PlateWidth
            PlateHeight = .PlateHeight
            PlateColor = .PlateColor
        End With

        For i As Integer = 0 To pobjModelDataFull.PartsNum - 1
            lobjGroup = pobjModelDataFull.Parts(i)

            For j = 0 To lobjGroup.Layer.Count - 1
                For Each lobjBlock As ModelDataFull.Position In lobjGroup.Layer(j)
                    AddBlockFromFull(pobjModelDataFull, lobjGroup.BottomPos + j, lobjBlock)
                Next
            Next
        Next
    End Sub

    Public Sub AddBlockFromFull(pobjModelDataFull As ModelDataFull, pintLayer As Integer, pobjBlock As ModelDataFull.Position)
        Dim lobjBlock As Block

        Dim lintCol As Integer
        Dim lintRow As Integer

        With pobjBlock
            If pobjModelDataFull.Version < 1 Then
                lintCol = CInt(.X - pobjModelDataFull.PlateWidth / 2 - 1)
                lintRow = CInt(.Y - pobjModelDataFull.PlateHeight / 2 - 1)
            Else
                lintCol = pobjBlock.X
                lintRow = pobjBlock.Y
            End If

            lobjBlock = AddBlock(pintLayer, lintRow, lintCol, .W, .D, .R, .C)
        End With
    End Sub

    Public Sub AddLayer(pintLayerPos As Integer)
        If Not objLayer.ContainsKey(pintLayerPos) Then
            objLayer.Add(pintLayerPos, New List(Of Block))
        End If
    End Sub

    Public Function AddBlock(pintLayer As Integer, pintRow As Integer, pintCol As Integer, pintWidth As Integer, pintHeight As Integer, pintRotation As Integer, pstrColor As String) As Block
        Dim lobjBlock As Block

        AddLayer(pintLayer)

        lobjBlock = New Block
        With lobjBlock
            .Row = pintRow
            .Col = pintCol
            .Width = pintWidth
            .Height = pintHeight
            .Rotation = pintRotation
            .Color = pstrColor
        End With

        objLayer(pintLayer).Add(lobjBlock)

        Return lobjBlock
    End Function

    Public Function ToJSON() As String
        Dim lobjResult As List(Of String)

        lobjResult = New List(Of String)

        With lobjResult
            .Add(String.Format("""{0}"":""{1:0.00}""", "Version", Version))
            .Add(CharaToJSON())
        End With

        Return String.Concat("{", vbCrLf, String.Join(vbCrLf & ",", lobjResult.ToArray), vbCrLf, "}")
    End Function

    Public Function CharaToJSON() As String
        Dim lobjResult As List(Of String)

        lobjResult = New List(Of String)

        With lobjResult
            .Add(String.Format("""{0}"":""{1}""", "Name", Name))
            .Add(String.Format("""{0}"":""{1}""", "DisplayName", DisplayName))
            .Add(String.Format("""{0}"":""{1}""", "Twitter", Twitter))
            .Add(String.Format("""{0}"":""{1}""", "Copyright", Copyright))
            .Add(String.Format("""{0}"":[""{1}"",""{2}"",""{3}""]", "Plate", PlateWidth, PlateHeight, PlateColor))
            .Add(BlockToJSON())
        End With

        Return String.Concat("""Chara"":{", vbCrLf, String.Join(vbCrLf & ",", lobjResult.ToArray), vbCrLf, "}")
    End Function

    Public Function BlockToJSON() As String
        Dim lobjResult As List(Of String)

        lobjResult = New List(Of String)

        With lobjResult
            .Add(String.Format("""{0}"":""{1}""", "Name", "全身"))
            .Add(String.Format("""{0}"":""{1}""", "BottomPos", 1))
            .Add(LayerToJSON())
        End With

        Return String.Concat("""Block"":[{", vbCrLf, String.Join(vbCrLf & ",", lobjResult.ToArray), vbCrLf, "}]")
    End Function

    Private Function LayerToJSON() As String
        Dim lobjResult As List(Of String)

        lobjResult = New List(Of String)

        For i As Integer = 1 To MaxLayer
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

    Public Function IsCellBlank(pintLayer As Integer, pintRow As Integer, pintCol As Integer, pintWidth As Integer, pintHeight As Integer) As Boolean
        Dim lobjRectTarget As Rectangle
        Dim lobjRectBlock As Rectangle

        lobjRectTarget = New Rectangle(pintCol, pintRow, pintWidth, pintHeight)

        For Each lobjBlock As Block In Layer(pintLayer)
            With lobjBlock
                lobjRectBlock = New Rectangle(.Col, .Row, .RotateWidth, .RotateHeight)
            End With
            If lobjRectTarget.IntersectsWith(lobjRectBlock) Then
                Return False
            End If
        Next

        Return True
    End Function
    Public Function IsCellBlank(pintLayer As Integer, pintRow As Integer, pintCol As Integer) As Boolean
        Return IsCellBlank(pintLayer, pintRow, pintCol, 1, 1)
    End Function

    Public Function RemoveCellBlock(pintLayer As Integer, pintRow As Integer, pintCol As Integer) As Block
        Dim lobjRectTarget As Rectangle
        Dim lobjRectBlock As Rectangle

        lobjRectTarget = New Rectangle(pintCol, pintRow, 1, 1)

        For Each lobjBlock As Block In Layer(pintLayer)
            With lobjBlock
                lobjRectBlock = New Rectangle(.Col, .Row, .RotateWidth, .RotateHeight)
            End With
            If lobjRectTarget.IntersectsWith(lobjRectBlock) Then
                Layer(pintLayer).Remove(lobjBlock)
                Return lobjBlock
            End If
        Next

        Return Nothing
    End Function
End Class
