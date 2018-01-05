﻿Imports System.IO

Public Class MainForm
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        '初期設定
        ''データベースを開く
        'Common.DB = New Database("PlamoBlock.db", Setting.DatabaseVersion)

        'モデルデータ初期化
        Common.ModelData = New ModelData

        ''カラー選択エリアの初期化
        ColorSelector.SetBlockColor(Common.BlockColor)
    End Sub

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        LoadTempFile()
    End Sub

    Private Sub MainForm_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        'If Common.DB IsNot Nothing Then
        '    Common.DB.Close()
        '    Common.DB = Nothing
        'End If
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

    Private Sub WorkArea_ChangeModel(sender As Object, e As EventArgs) Handles WorkArea.ChangeModel
        Common.SaveModel(Path.Combine(My.Application.Info.DirectoryPath, Common.ConstTempFileName))

        LayerSelector.Redraw()
    End Sub

    Private Sub LoadTempFile()
        Dim lstrTempFile As String

        lstrTempFile = Path.Combine(My.Application.Info.DirectoryPath, Common.ConstTempFileName)

        If File.Exists(lstrTempFile) = False Then
            Exit Sub
        End If

        If MsgBox("前回の状態を表示しますか？", MsgBoxStyle.Information + MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Common.LoadModel(lstrTempFile)

            LayerSelector.SelectLayer.Value = 1

            WorkArea.SelectLayer = CInt(LayerSelector.SelectLayer.Value)

            LayerSelector.Redraw()
        End If
    End Sub

    Private Sub LoadModel(pstrFileName As String)
        Common.LoadModel(pstrFileName)

        LayerSelector.SelectLayer.Value = 1

        WorkArea.SelectLayer = CInt(LayerSelector.SelectLayer.Value)

        LayerSelector.Redraw()
    End Sub

    'JSONファイル読み込み
    Private Sub MenuItem_File_LoadJson_Click(sender As Object, e As EventArgs) Handles MenuItem_File_LoadJson.Click
        Dim lobjModelDataOldVer As New ModelDataFull

        If OpenFile.ShowDialog() = DialogResult.OK Then
            LoadModel(OpenFile.FileName)
        End If
    End Sub

    Private Sub MenuItem_Output_OutputJSONText_Click(sender As Object, e As EventArgs) Handles MenuItem_Output_OutputJSONText.Click
        Dim lobjForm As ResultJSON

        lobjForm = New ResultJSON

        lobjForm.ResultText.Text = Common.ModelData.ToJSON

        lobjForm.ShowDialog()

        lobjForm.Dispose()
    End Sub

    Private Sub MenuItem_Output_OutputFile_Click(sender As Object, e As EventArgs) Handles MenuItem_Output_OutputFile.Click
        If SaveFile.ShowDialog() = DialogResult.OK Then
            Common.SaveModel(SaveFile.FileName)
        End If
    End Sub

    Private Sub MenuItem_DataClear_Click(sender As Object, e As EventArgs) Handles MenuItem_DataClear.Click
        If MsgBox("表示されているデータを消去しますか？", MsgBoxStyle.Information + MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Common.ModelData.Clear()

            LayerSelector.SelectLayer.Value = 1

            WorkArea.SelectLayer = CInt(LayerSelector.SelectLayer.Value)

            LayerSelector.Redraw()
        End If
    End Sub

    Private Sub MenuItem_ModelInfo_Click(sender As Object, e As EventArgs) Handles MenuItem_ModelInfo.Click
        Dim lobjForm As ModelInfo

        lobjForm = New ModelInfo

        lobjForm.ShowDialog()

        lobjForm.Dispose()
    End Sub
End Class
