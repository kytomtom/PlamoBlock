Imports System.IO

Public Class Preview
    Private Const _DataPath As String = "PreviewData"

    Private Sub Preview_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call Reload()
    End Sub

    Private Sub Reload()
        Dim lstrPath As String

        lstrPath = "file:///" & Path.Combine(My.Application.Info.DirectoryPath, _DataPath, "modelview.html").Replace("\", "/")

        Call SetPreviewModel()

        Browser.Browser.LoadURL(lstrPath)
    End Sub

    Private Sub SetPreviewModel()
        Dim llstData As New List(Of String)
        Dim lstrPath As String

        llstData.Add("var gblModelData =")
        llstData.Add(Common.ModelData.ToJSON())
        llstData.Add(";")

        lstrPath = Path.Combine(My.Application.Info.DirectoryPath, _DataPath, "modeldata.json")

        My.Computer.FileSystem.WriteAllText(lstrPath, String.Concat(llstData.ToArray), False)
    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        Call Reload()
    End Sub
End Class
