Public Class MainForm
    Private objDB As Database

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        '初期設定
        ''データベースを開く
        Me.objDB = New Database("PlamoBlock.db", Setting.DatabaseVersion)
        ''ブロック配置エリアの初期化
        Me.WorkArea.SetWorkAreaSize(Me.WorkArea.Width, Me.WorkArea.Height, 24, 24)
        ''カラー選択エリアの初期化
        Me.ColorSelector.SetBlockColor(New BlockColor)

        Dim obj2 As New ModelData
        obj2.LoadJSON(Common.GetResourceText("JSON_ModelTest.json"))

        Debug.WriteLine(obj2.Name)
        obj2.Parts(0).Name = "AAA"
        Debug.WriteLine(obj2.Parts(0).Name)

    End Sub

    Private Sub MainForm_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        If Me.objDB IsNot Nothing Then
            Me.objDB.Close()
            Me.objDB = Nothing
        End If
    End Sub

    Private Sub SetCanvasSize(pintCols As Integer, pintRows As Integer)
    End Sub

    Private Sub ColorSelector_ChangeColor(sender As Object, e As EventArgs) Handles ColorSelector.ChangeColor
        Me.SelectColor.BackColor = Me.ColorSelector.SelectColorSetting.Base
        Me.SelectColor.Text = Me.ColorSelector.SelectColorSetting.Kana
    End Sub
End Class

