Public Class MainForm
    Private objDB As Database

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        '初期設定
        ''データベースを開く
        objDB = New Database("PlamoBlock.db", Setting.DatabaseVersion)

        ''ブロック配置エリアの初期化
        'WorkArea.SetWorkAreaSize(32, 32, 16)
        ''カラー選択エリアの初期化
        ColorSelector.SetBlockColor(New BlockColor)

        Dim obj2 As New ModelData
        obj2.LoadJSON(Common.GetResourceText("JSON_ModelTest.json"))

        Debug.WriteLine(obj2.Name)
        obj2.Parts(0).Name = "AAA"
        Debug.WriteLine(obj2.Parts(0).Name)


    End Sub

    Private Sub MainForm_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        If objDB IsNot Nothing Then
            objDB.Close()
            objDB = Nothing
        End If
    End Sub

    Private Sub SetCanvasSize(pintCols As Integer, pintRows As Integer)
    End Sub

    Private Sub ColorSelector_ChangeColor(sender As Object, e As EventArgs) Handles ColorSelector.ChangeColor
        SelectColor.BackColor = ColorSelector.SelectColorSetting.Base
        SelectColor.ForeColor = ColorSelector.SelectColorSetting.Edge
        SelectColor.Text = ColorSelector.SelectColorSetting.Kana

        BlockSelector.SetBlockObject(ColorSelector.SelectColorSetting)
        'BlockImage1.SetBlockSize(Me.ColorSelector.SelectColorSetting, 2, 6, 16)
    End Sub

End Class

