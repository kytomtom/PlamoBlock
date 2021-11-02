Imports System.ComponentModel
Imports System.IO
Imports CefSharp.WinForms

Public Class Preview
    Private _webBrowser As ChromiumWebBrowser

    Private Const _DataPath As String = "PreviewData"

    Private strURL As String

    Private Sub Preview_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim settings As CefSettings

        strURL = Path.Combine(My.Application.Info.DirectoryPath, _DataPath, "modelview.html")

        If Not CefSharp.Cef.IsInitialized Then
            settings = New CefSettings()

            'settings.BrowserSubprocessPath =
            '    Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
            '        If(Environment.Is64BitProcess, "x64", "x86"), "CefSharp.BrowserSubprocess.exe")

            '日本語に設定する
            settings.Locale = Globalization.CultureInfo.CurrentCulture.Parent.ToString()
            settings.AcceptLanguageList = Globalization.CultureInfo.CurrentCulture.Name
            'ユーザーデータの保存先を設定する
            settings.UserDataPath = My.Application.Info.DirectoryPath

            CefSharp.CefSharpSettings.SubprocessExitIfParentProcessClosed = True
            CefSharp.Cef.Initialize(settings, performDependencyCheck:=False, browserProcessHandler:=Nothing)
        End If

        _webBrowser = New ChromiumWebBrowser(New Uri(strURL).ToString)
        PreviewLayout.Controls.Add(_webBrowser, 0, 1)
        _webBrowser.Dock = DockStyle.Fill

        Call Reload()
    End Sub

    Private Sub Reload()
        Call SetPreviewModel()

        _webBrowser.Load(New Uri(strURL).ToString)
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

    Private Sub btnDevTool_Click(sender As Object, e As EventArgs) Handles btnDevTool.Click
        CefSharp.WebBrowserExtensions.ShowDevTools(_webBrowser)
    End Sub
End Class
