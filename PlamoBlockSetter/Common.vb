Public Class Common
    Public Shared ConstTempFileName As String = "temp.json"

    ''データベース接続
    'Public Shared DB As Database

    '色設定
    Public Shared BlockColor As New BlockColor

    'モデルデータ
    Public Shared ModelData As ModelData

    '埋め込まれたリソースからテキストの内容を取得
    Public Shared Function GetResourceText(pstrFileName As String) As String
        Dim lobjStream As IO.StreamReader
        Dim lstrFilePath As String
        Dim lstrResult As String
        Dim llstRead As List(Of String)

        lstrFilePath = String.Join(".", My.Application.Info.Title, pstrFileName)

        With Reflection.Assembly.GetCallingAssembly()
            lobjStream = New IO.StreamReader(.GetManifestResourceStream(lstrFilePath))
        End With

        lstrResult = lobjStream.ReadToEnd()

        lobjStream.Close()

        llstRead = Nothing

        Return lstrResult
    End Function

    Public Shared Sub SaveModel(pstrFileName As String)
        Dim lobjENC As System.Text.Encoding

        Try
            lobjENC = System.Text.Encoding.GetEncoding("utf-8")
            System.IO.File.WriteAllText(pstrFileName, Common.ModelData.ToJSON, lobjENC)

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub LoadModel(pstrFileName As String)
        Dim lobjModelDataOldVer As New ModelDataFull

        lobjModelDataOldVer = New ModelDataFull

        lobjModelDataOldVer.LoadJSON(New System.IO.StreamReader(pstrFileName).ReadToEnd)

        Common.ModelData.SetModelDataFromFull(lobjModelDataOldVer)
    End Sub
End Class
