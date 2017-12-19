Public Class Common
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

End Class
