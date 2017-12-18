Imports Newtonsoft.Json

Public Class ModelDataFull
    Private objModelData As ModelData

    Public Class ModelData
        Public Chara As CharaData
    End Class

    Public Class CharaData
        Public Name As String
        Public DisplayName As String
        Public Twitter As String
        Public Copyright As String
        Public Plate As String()
        Public Block As List(Of BlockGroup)

        Public Property BlockGroup() As List(Of BlockGroup)
            Set(value As List(Of BlockGroup))
                Block = value
            End Set
            Get
                Return Block
            End Get
        End Property

        Public Sub New()
            Clear()
        End Sub

        Public Sub Clear()
            Name = ""
            DisplayName = ""
            Twitter = ""
            Copyright = ""
            Plate = {"", "", ""}
            BlockGroup = New List(Of BlockGroup)
        End Sub

        Public Function AddNewBlockGroup() As Integer
            BlockGroup.Add(New BlockGroup())
            Return BlockGroup.Count - 1
        End Function
    End Class

    Public Class BlockGroup
        Public Name As String
        Public BottomPos As Integer
        Public Layer As List(Of List(Of Position))

        Public Sub New()
            Clear()
        End Sub

        Public Sub Clear()
            Name = ""
            BottomPos = 0
            Layer = New List(Of List(Of Position))
        End Sub

        Public Function AddNewLayer() As Integer
            Layer.Add(New List(Of Position))
            Return Layer.Count - 1
        End Function

        Public Function AddNewBlock(pintLayer As Integer) As Integer
            Layer(pintLayer).Add(New Position)
            Return Layer(pintLayer).Count - 1
        End Function
    End Class

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

    Public Property Name() As String
        Set(value As String)
            objModelData.Chara.Name = value
        End Set
        Get
            Return objModelData.Chara.Name
        End Get
    End Property
    Public Property DisplayName() As String
        Set(value As String)
            objModelData.Chara.DisplayName = value
        End Set
        Get
            Return objModelData.Chara.DisplayName
        End Get
    End Property
    Public Property Twitter() As String
        Set(value As String)
            objModelData.Chara.Twitter = value
        End Set
        Get
            Return objModelData.Chara.Twitter
        End Get
    End Property
    Public Property Copyright() As String
        Set(value As String)
            objModelData.Chara.Copyright = value
        End Set
        Get
            Return objModelData.Chara.Copyright
        End Get
    End Property
    Public Property PlateWidth() As Integer
        Set(value As Integer)
            objModelData.Chara.Plate(0) = value.ToString
        End Set
        Get
            Return CInt(Me.objModelData.Chara.Plate(0))
        End Get
    End Property
    Public Property PlateHeight() As Integer
        Set(value As Integer)
            objModelData.Chara.Plate(1) = value.ToString
        End Set
        Get
            Return CInt(Me.objModelData.Chara.Plate(1))
        End Get
    End Property
    Public Property PlateColor() As String
        Set(value As String)
            objModelData.Chara.Plate(2) = value
        End Set
        Get
            Return objModelData.Chara.Plate(2)
        End Get
    End Property

    Public ReadOnly Property PartsNum() As Integer
        Get
            Return objModelData.Chara.Block.Count
        End Get
    End Property
    Public Property Parts(Index As Integer) As BlockGroup
        Set(value As BlockGroup)
            objModelData.Chara.Block(Index) = value
        End Set
        Get
            Return objModelData.Chara.Block(Index)
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Function LoadJSON(pstrJSON As String) As Boolean
        Try
            objModelData = JsonConvert.DeserializeObject(Of ModelData)(pstrJSON)

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Sub ClearData()
        objModelData = Nothing

        objModelData = New ModelData
    End Sub
End Class
