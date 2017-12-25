﻿Public Class MainForm
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        '初期設定
        ''データベースを開く
        Common.DB = New Database("PlamoBlock.db", Setting.DatabaseVersion)

        'モデルデータ初期化
        Common.ModelData = New ModelData

        ''カラー選択エリアの初期化
        ColorSelector.SetBlockColor(Common.BlockColor)
    End Sub

    Private Sub MainForm_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        If Common.DB IsNot Nothing Then
            Common.DB.Close()
            Common.DB = Nothing
        End If
    End Sub

    Private Sub ColorSelector_ChangeColor(sender As Object, e As EventArgs) Handles ColorSelector.ChangeColor
        SelectColor.BackColor = ColorSelector.SelectColorSetting.Base
        SelectColor.ForeColor = ColorSelector.SelectColorSetting.Edge
        SelectColor.Text = ColorSelector.SelectColorSetting.Kana

        BlockSelector.SetBlockObject(ColorSelector.SelectColorSetting)
    End Sub

    Private Sub BlockSelector_ChangeBlockSize(sender As Object, e As EventArgs) Handles BlockSelector.ChangeBlockSize
        SelectBlock.SetBlockSize(BlockSelector.SelectBlockSizeRows, BlockSelector.SelectBlockSizeCols, SelectBlock.CellSize, ColorSelector.SelectColorSetting, 0)
        SetSelectBlock()
    End Sub

    Private Sub SetSelectBlock()
        With SelectBlock
            WorkArea.SetSelectBlock(.Rows, .Cols, .ColorSetting)
        End With
    End Sub

    Private Sub LayerSelector_ChangeLayer(sender As Object, e As EventArgs) Handles LayerSelector.ChangeLayer
        WorkArea.SelectLayer = CInt(LayerSelector.SelectLayer.Value)
    End Sub

    'JSONファイル（旧バージョン）読み込み
    Private Sub MenuItem_File_LoadJsonOldVer_Click(sender As Object, e As EventArgs) Handles MenuItem_File_LoadJsonOldVer.Click
        Dim lobjModelDataOldVer As New ModelDataFull

        If OpenFile.ShowDialog() = DialogResult.OK Then
            lobjModelDataOldVer = New ModelDataFull

            lobjModelDataOldVer.LoadJSON(New System.IO.StreamReader(OpenFile.OpenFile()).ReadToEnd)

            Common.ModelData.SetModelDataFromFull(lobjModelDataOldVer)

            LayerSelector.SelectLayer.Value = 1

            WorkArea.SelectLayer = CInt(LayerSelector.SelectLayer.Value)

            LayerSelector.Redraw()
        End If
    End Sub

    Private Sub MenuItem_Output_OutputJSONText_Click(sender As Object, e As EventArgs) Handles MenuItem_Output_OutputJSONText.Click
        Dim lobjForm As ResultJSON

        lobjForm = New ResultJSON

        lobjForm.ResultText.Text = Common.ModelData.ToJSON

        lobjForm.ShowDialog()

        lobjForm.Dispose()
    End Sub

    Private Sub WorkArea_ChangeModel(sender As Object, e As EventArgs) Handles WorkArea.ChangeModel
        LayerSelector.Redraw()
    End Sub
End Class

