Public Class Common
    Public Shared ConstTempFileName As String = "temp.json"

    ''データベース接続
    'Public Shared DB As Database

    '色設定
    Public Shared BlockColor As New BlockColor

    'モデルデータ
    Public Shared ModelData As ModelDataG

    'Undo用データ
    Public Shared UndoIndex As Integer = -1
    Public Shared UndoModelData As New List(Of ModelDataG)

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
        Dim lobjModelDataOldVer As ModelDataFull

        lobjModelDataOldVer = New ModelDataFull

        lobjModelDataOldVer.LoadJSON(New System.IO.StreamReader(pstrFileName).ReadToEnd)

        Common.ModelData.SetModelDataFromFull(lobjModelDataOldVer)

        Common.ClearUndoData()
        Common.SetUndoData()
    End Sub

    Public Shared Sub SetUndoData()
        If Common.UndoIndex <> -1 Then
            For i As Integer = Common.UndoModelData.Count - 1 To Common.UndoIndex Step -1
                Common.UndoModelData.RemoveAt(i)
            Next
        End If
        Common.UndoModelData.Add(New ModelDataG(Common.ModelData))
        Common.UndoIndex = -1
    End Sub
    Public Shared Sub ClearUndoData()
        Common.UndoModelData.Clear()
        Common.UndoIndex = -1
    End Sub

    Public Shared Sub UndoModel()
        If Common.UndoIndex = -1 Then
            Common.SetUndoData()
            Common.UndoIndex = Common.UndoModelData.Count - 2
        ElseIf Common.UndoIndex > 0 Then
            Common.UndoIndex -= 1
        Else
            Return
        End If

        Common.ModelData = New ModelDataG(Common.UndoModelData(Common.UndoIndex))
    End Sub

    Public Shared Sub RedoModel()
        If Common.UndoIndex = -1 Then
            Return
        ElseIf Common.UndoIndex < Common.UndoModelData.Count - 1 Then
            Common.UndoIndex += 1
        Else
            Return
        End If

        Common.ModelData = New ModelDataG(Common.UndoModelData(Common.UndoIndex))
    End Sub

End Class
