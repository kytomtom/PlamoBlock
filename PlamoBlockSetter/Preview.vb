Imports System.IO
Imports System.Security.Policy
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT

Public Class Preview
    Private Class StreamUriResolver : Implements IUriToStreamResolver

        Private Function UriToStream(uri As Uri) As Stream

            Dim baseDir As Uri = New Uri(AppDomain.CurrentDomain.BaseDirectory)
            Dim target As Uri = New Uri(baseDir, uri.LocalPath.TrimStart("/"))

            Return New FileStream(target.AbsolutePath, FileMode.Open)
        End Function

        Private Function IUriToStreamResolver_UriToStream(uri As Uri) As Stream Implements IUriToStreamResolver.UriToStream
            Throw New NotImplementedException()
        End Function
    End Class

    Private Sub Preview_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim lstrPath As String
        Dim lstrHtml As String

        lstrPath = ("file:///" & My.Application.Info.DirectoryPath).Replace("\", "/")
        lstrHtml = Path.Combine("Data", "modelview.html").Replace("\", "/")

        Dim fileContents As String
        fileContents = My.Computer.FileSystem.ReadAllText(Path.Combine(My.Application.Info.DirectoryPath, "Data", "BlockColor.json"))

        ''WebView1.Navigate(New Url("https://miko.info/?p=1372").Value)
        ''WebView.NavigateToLocalStreamUri(new Uri(mermaidHtmlFileName, UriKind.Relative), new StreamUriResolver());
        'WebView1.NavigateToLocalStreamUri(New Uri(lstrURL), New StreamUriResolver())
        ''While WebBrowser1.IsBusy : Thread.Sleep(1) : End While
        ''While WebBrowser1.Document.Body Is Nothing : Thread.Sleep(1) : End While
        ''WebBrowser1.Document.CreateElement("Body")
        ''WebBrowser1.Document.Body.InnerHtml = "<b>test</b> string"

        WinFormsBrowserView1.Browser.LoadURL(String.Join("/", lstrPath, lstrHtml) & "?path=" & lstrPath)
        'WinFormsBrowserView1.Browser.LoadURL(String.Join("/", lstrPath, lstrHtml) & "?color=" & fileContents)
        'WinFormsBrowserView1.Browser.LoadURL("http://www.kytomtom.site/plamoblock/standalone/EnnoTsuina_PP_2/modelview.html")
    End Sub
End Class
