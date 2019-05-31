Public Class ModelDataG
    Public Const Version As Single = 1

    Public Name As String
    Public DisplayName As String
    Public Twitter As String
    Public Copyright As String
    Public PlateWidth As Integer
    Public PlateHeight As Integer
    Public PlateColor As String

    Public Group As Dictionary(Of String, BlockGroup)

    Public Class BlockGroup
        Public Name As String
        Public BottomPos As Integer

        Friend objLayer As Dictionary(Of Integer, List(Of Block))

        Public Sub New()
            objLayer = New Dictionary(Of Integer, List(Of Block))
            Clear()
        End Sub

        Public Sub New(pobjBlockGroup As BlockGroup)
            With pobjBlockGroup
                Name = .Name
                BottomPos = .BottomPos

                objLayer = New Dictionary(Of Integer, List(Of Block))
                For Each lintKey As Integer In .objLayer.Keys
                    objLayer.Add(lintKey, New List(Of Block))

                    For Each lobjBlock As Block In .objLayer(lintKey)
                        objLayer(lintKey).Add(New Block(lobjBlock))
                    Next
                Next
            End With
        End Sub

        Public Property Layer() As Dictionary(Of Integer, List(Of Block))
            Get
                Return objLayer
            End Get
            Set(value As Dictionary(Of Integer, List(Of Block)))
                objLayer = value
            End Set
        End Property

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

                For Each intBuf As Integer In Layer.Keys
                    If Layer(intBuf).Count > 0 Then
                        lintMaxLayer = Math.Max(lintMaxLayer, intBuf)
                    End If
                Next

                Return lintMaxLayer
            End Get
        End Property

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
        Public Function AddBlock(pintLayer As Integer, pobjBlock As Block) As Block
            AddLayer(pintLayer)

            objLayer(pintLayer).Add(pobjBlock)

            Return pobjBlock
        End Function

        Public Function LayerToJSON() As String
            Dim lobjResult As List(Of String)

            lobjResult = New List(Of String)

            For i As Integer = 1 To MaxLayer
                lobjResult.Add(LayerToJSON(i))
            Next

            Return String.Concat("""Layer"":[", vbCrLf, String.Join(vbCrLf & ",", lobjResult.ToArray), vbCrLf, "]")
        End Function
        Public Function LayerToJSON(pintLayer As Integer) As String
            Dim lobjResult As List(Of String)

            lobjResult = New List(Of String)

            For Each lobjBlock As Block In Layer(pintLayer)
                lobjResult.Add(lobjBlock.ToJSON)
            Next

            Return String.Concat("[", vbCrLf, String.Join(vbCrLf & ",", lobjResult.ToArray), vbCrLf, "]")
        End Function

        Public Sub Clear()
            Name = ""
            BottomPos = 0
            objLayer = New Dictionary(Of Integer, List(Of Block))
        End Sub

        Public Sub ClearLayer(pintLayer As Integer)
            If objLayer.ContainsKey(pintLayer) Then
                objLayer(pintLayer).Clear()
            End If
        End Sub

        Public Function IsCellBlank(pintLayer As Integer, pintRow As Integer, pintCol As Integer, pintWidth As Integer, pintHeight As Integer) As Boolean
            Dim lobjRectTarget As Rectangle
            Dim lobjRectBlock As Rectangle

            lobjRectTarget = New Rectangle(pintCol, pintRow, pintWidth, pintHeight)

            For Each lobjBlock As Block In objLayer(pintLayer)
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

            For Each lobjBlock As Block In objLayer(pintLayer)
                With lobjBlock
                    lobjRectBlock = New Rectangle(.Col, .Row, .RotateWidth, .RotateHeight)
                End With
                If lobjRectTarget.IntersectsWith(lobjRectBlock) Then
                    objLayer(pintLayer).Remove(lobjBlock)
                    Return lobjBlock
                End If
            Next

            Return Nothing
        End Function

        Public Sub CopyLayerBuf(pobjLayerBuf As Dictionary(Of Integer, List(Of Block)))
            objLayer.Clear()
            For Each i As Integer In pobjLayerBuf.Keys
                objLayer.Add(i, pobjLayerBuf(i))
            Next
        End Sub
        Public Sub ShiftLayerUp(pintTopLayer As Integer)
            Dim lobjLayerBuf As Dictionary(Of Integer, List(Of Block))
            Dim lintNew As Integer

            lobjLayerBuf = New Dictionary(Of Integer, List(Of Block))

            For Each lintOld As Integer In objLayer.Keys
                If lintOld = pintTopLayer Then
                    lintNew = 1
                Else
                    lintNew = lintOld + 1
                End If

                lobjLayerBuf.Add(lintNew, objLayer(lintOld))
            Next

            CopyLayerBuf(lobjLayerBuf)
            lobjLayerBuf = Nothing
        End Sub
        Public Sub ShiftLayerDown(pintTopLayer As Integer)
            Dim lobjLayerBuf As Dictionary(Of Integer, List(Of Block))
            Dim lintNew As Integer

            lobjLayerBuf = New Dictionary(Of Integer, List(Of Block))

            For Each lintOld As Integer In objLayer.Keys
                If lintOld = 1 Then
                    lintNew = pintTopLayer
                Else
                    lintNew = lintOld - 1
                End If

                lobjLayerBuf.Add(lintNew, objLayer(lintOld))
            Next

            objLayer.Clear()

            For Each i As Integer In lobjLayerBuf.Keys
                objLayer.Add(i, lobjLayerBuf(i))
            Next

            CopyLayerBuf(lobjLayerBuf)
            lobjLayerBuf = Nothing
        End Sub
        Public Sub ShiftColPl(pintMax As Integer)
            Dim lbolCanShift As Boolean

            lbolCanShift = True
            For Each pobjLayer As List(Of Block) In objLayer.Values
                For Each pobjBlock As Block In pobjLayer
                    If pobjBlock.Col + pobjBlock.RotateWidth - 1 + 1 > pintMax Then
                        lbolCanShift = False
                        Exit For
                    End If
                Next

                If lbolCanShift = False Then
                    Exit For
                End If
            Next

            If lbolCanShift = False Then
                Exit Sub
            End If

            For Each pobjLayer As List(Of Block) In objLayer.Values
                For Each pobjBlock As Block In pobjLayer
                    pobjBlock.Col += 1
                Next
            Next
        End Sub
        Public Sub ShiftColMi(pintMin As Integer)
            Dim lbolCanShift As Boolean

            lbolCanShift = True
            For Each pobjLayer As List(Of Block) In objLayer.Values
                For Each pobjBlock As Block In pobjLayer
                    If pobjBlock.Col - 1 < pintMin Then
                        lbolCanShift = False
                        Exit For
                    End If
                Next

                If lbolCanShift = False Then
                    Exit For
                End If
            Next

            If lbolCanShift = False Then
                Exit Sub
            End If

            For Each pobjLayer As List(Of Block) In objLayer.Values
                For Each pobjBlock As Block In pobjLayer
                    pobjBlock.Col -= 1
                Next
            Next
        End Sub
        Public Sub ShiftRowPl(pintMax As Integer)
            Dim lbolCanShift As Boolean

            lbolCanShift = True
            For Each pobjLayer As List(Of Block) In objLayer.Values
                For Each pobjBlock As Block In pobjLayer
                    If pobjBlock.Row + pobjBlock.RotateHeight - 1 + 1 > pintMax Then
                        lbolCanShift = False
                        Exit For
                    End If
                Next

                If lbolCanShift = False Then
                    Exit For
                End If
            Next

            If lbolCanShift = False Then
                Exit Sub
            End If

            For Each pobjLayer As List(Of Block) In objLayer.Values
                For Each pobjBlock As Block In pobjLayer
                    pobjBlock.Row += 1
                Next
            Next
        End Sub
        Public Sub ShiftRowMi(pintMin As Integer)
            Dim lbolCanShift As Boolean

            lbolCanShift = True
            For Each pobjLayer As List(Of Block) In objLayer.Values
                For Each pobjBlock As Block In pobjLayer
                    If pobjBlock.Row - 1 < pintMin Then
                        lbolCanShift = False
                        Exit For
                    End If
                Next

                If lbolCanShift = False Then
                    Exit For
                End If
            Next

            If lbolCanShift = False Then
                Exit Sub
            End If

            For Each pobjLayer As List(Of Block) In objLayer.Values
                For Each pobjBlock As Block In pobjLayer
                    pobjBlock.Row -= 1
                Next
            Next
        End Sub
    End Class

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

        Public Sub New(pobjBlock As Block)
            With pobjBlock
                Row = .Row
                Col = .Col
                Width = .Width
                Height = .Height
                Rotation = .Rotation
                Color = .Color
            End With
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


    Public Property Layer(strGroup As String, intLayer As Integer) As List(Of Block)
        Get
            Return Group(strGroup).Layer(intLayer)
        End Get
        Set(value As List(Of Block))
            Group(strGroup).Layer(intLayer) = value
        End Set
    End Property

    Public ReadOnly Property MaxLayer() As Integer
        Get
            Dim lintMaxLayer As Integer

            lintMaxLayer = 0

            For Each strBuf As String In Group.Keys
                lintMaxLayer = Math.Max(lintMaxLayer, Group(strBuf).MaxLayer)
            Next

            Return lintMaxLayer
        End Get
    End Property

    Public Sub New()
        Group = New Dictionary(Of String, BlockGroup)
        Clear()
    End Sub
    Public Sub New(pobjModelDataFull As ModelDataFull)
        Me.New()
        SetModelDataFromFull(pobjModelDataFull)
    End Sub

    Public Sub New(pobjModelDataG As ModelDataG)
        With pobjModelDataG
            Name = .Name
            DisplayName = .DisplayName
            Twitter = .Twitter
            Copyright = .Copyright
            PlateWidth = .PlateWidth
            PlateHeight = .PlateHeight
            PlateColor = .PlateColor

            Group = New Dictionary(Of String, BlockGroup)
            For Each lstrKey As String In .Group.Keys
                Group.Add(lstrKey, New BlockGroup(.Group(lstrKey)))
            Next
        End With
    End Sub

    Public Sub Clear()
        Name = ""
        DisplayName = ""
        Twitter = ""
        Copyright = ""
        PlateWidth = 14
        PlateHeight = 14
        PlateColor = BlockColor.ColorName.White.ToString

        Group.Clear()
        AddGroup("デフォルト", 1)
    End Sub

    Public Sub ClearLayer(pintLayer As Integer)
        For Each lobjGroup As BlockGroup In Group.Values
            lobjGroup.ClearLayer(pintLayer)
        Next
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

            Group.Clear()
            Group.Add(lobjGroup.Name, New BlockGroup)
            With Group(lobjGroup.Name)
                .Name = lobjGroup.Name
                .BottomPos = lobjGroup.BottomPos

                For j = 0 To lobjGroup.Layer.Count - 1
                    For Each lobjBlock As ModelDataFull.Position In lobjGroup.Layer(j)
                        AddBlockFromFull(pobjModelDataFull, .Name, .BottomPos + j, lobjBlock)
                    Next
                Next
            End With
        Next
    End Sub

    Public Sub AddBlockFromFull(pobjModelDataFull As ModelDataFull, pstrGroup As String, pintLayer As Integer, pobjBlock As ModelDataFull.Position)
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

            lobjBlock = Group(pstrGroup).AddBlock(pintLayer, lintRow, lintCol, .W, .D, .R, .C)
        End With
    End Sub

    Public Sub AddGroup(pstrName As String, pintBottomPos As Integer)
        If Group.ContainsKey(pstrName) Then
            Return
        End If

        Group.Add(pstrName, New BlockGroup)
        With Group(pstrName)
            .Name = pstrName
            .BottomPos = pintBottomPos
        End With
    End Sub

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
        Dim lobjResult2 As List(Of String)

        lobjResult = New List(Of String)

        For Each lstrGroup As String In Group.Keys
            lobjResult2 = New List(Of String)
            With lobjResult2
                .Add(String.Format("""{0}"":""{1}""", "Name", Group(lstrGroup).Name))
                .Add(String.Format("""{0}"":""{1}""", "BottomPos", 1))
                .Add(Group(lstrGroup).LayerToJSON())
            End With

            lobjResult.Add(String.Concat("{", vbCrLf, String.Join(vbCrLf & ",", lobjResult2.ToArray), vbCrLf, "}"))
        Next

        Return String.Concat("""Block"":[", vbCrLf, String.Join(vbCrLf & ",", lobjResult.ToArray), vbCrLf, "]")
    End Function

    Public Function IsCellBlank(pintLayer As Integer, pintRow As Integer, pintCol As Integer, pintWidth As Integer, pintHeight As Integer) As Boolean
        For Each lstrGroup As String In Group.Keys
            If Group(lstrGroup).IsCellBlank(pintLayer, pintRow, pintCol, pintWidth, pintHeight) = False Then
                Return False
            End If
        Next

        Return True
    End Function
    Public Function IsCellBlank(pintLayer As Integer, pintRow As Integer, pintCol As Integer) As Boolean
        Return IsCellBlank(pintLayer, pintRow, pintCol, 1, 1)
    End Function

    Public Function RemoveCellBlock(pstrGroup As String, pintLayer As Integer, pintRow As Integer, pintCol As Integer) As Block
        Dim lobjRectTarget As Rectangle
        Dim lobjRectBlock As Rectangle

        lobjRectTarget = New Rectangle(pintCol, pintRow, 1, 1)

        For Each lobjBlock As Block In Group(pstrGroup).Layer(pintLayer)
            With lobjBlock
                lobjRectBlock = New Rectangle(.Col, .Row, .RotateWidth, .RotateHeight)
            End With
            If lobjRectTarget.IntersectsWith(lobjRectBlock) Then
                Group(pstrGroup).Layer(pintLayer).Remove(lobjBlock)
                Return lobjBlock
            End If
        Next

        Return Nothing
    End Function

    Public Sub ShiftLayerUp(pintTopLayer As Integer)
        For Each lstrGroup As String In Group.Keys
            Group(lstrGroup).ShiftLayerUp(pintTopLayer)
        Next
    End Sub
    Public Sub ShiftLayerDown(pintTopLayer As Integer)
        For Each lstrGroup As String In Group.Keys
            Group(lstrGroup).ShiftLayerDown(pintTopLayer)
        Next
    End Sub
    Public Sub ShiftColPl(pintMax As Integer)
        For Each lstrGroup As String In Group.Keys
            Group(lstrGroup).ShiftColPl(pintMax)
        Next
    End Sub
    Public Sub ShiftColMi(pintMin As Integer)
        For Each lstrGroup As String In Group.Keys
            Group(lstrGroup).ShiftColMi(pintMin)
        Next
    End Sub
    Public Sub ShiftRowPl(pintMax As Integer)
        For Each lstrGroup As String In Group.Keys
            Group(lstrGroup).ShiftRowPl(pintMax)
        Next
    End Sub
    Public Sub ShiftRowMi(pintMin As Integer)
        For Each lstrGroup As String In Group.Keys
            Group(lstrGroup).ShiftRowMi(pintMin)
        Next
    End Sub

End Class