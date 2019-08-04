Imports System.IO
Imports System.Reflection

Module Start

    <STAThread()>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf Resolve
        Application.Run(New MainForm())
    End Sub

    Function Resolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As [Assembly]
        If args.Name.StartsWith("CefSharp") Then
            Dim assemblyName As String = args.Name.Split({","c}, 2)(0) & ".dll"
            Dim archSpecificPath As String = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                          If(Environment.Is64BitProcess, "x64", "x86"), assemblyName)
            Return If(File.Exists(archSpecificPath), Assembly.LoadFile(archSpecificPath), Nothing)
        End If

        Return Nothing
    End Function

End Module