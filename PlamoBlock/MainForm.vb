Public Class MainForm
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WorkArea1.SetWorkAreaSize(Me.WorkArea1.Width, Me.WorkArea1.Height, 24, 24)

        Dim obj As New BlockColor

        Debug.WriteLine(obj.Color(BlockColor.ColorName.Blue).Base)
        Debug.WriteLine(obj.Color(BlockColor.ColorName.Green).Base)

        Dim obj2 As New ModelData
        obj2.LoadJSON(Common.GetResourceText("JSON_ModelTest.json"))

        Debug.WriteLine(obj2.Name)
        obj2.Parts(0).Name = "AAA"
        Debug.WriteLine(obj2.Parts(0).Name)

        Dim db As Database = New Database("PlamoBlock.db", Setting.DatabaseVersion)
    End Sub

    Private Sub SetCanvasSize(pintCols As Integer, pintRows As Integer)
    End Sub
End Class

