<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Dim ColorSetting1 As PlamoBlockSetter.BlockColor.ColorSetting = New PlamoBlockSetter.BlockColor.ColorSetting()
        Dim ColorSetting2 As PlamoBlockSetter.BlockColor.ColorSetting = New PlamoBlockSetter.BlockColor.ColorSetting()
        Me.SelectColor = New System.Windows.Forms.Label()
        Me.MenuBar = New System.Windows.Forms.MenuStrip()
        Me.MenuItem_File = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuItem_File_LoadJson = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Output = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Output_OutputJSONText = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Output_OutputFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_ModelInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Operation = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Operation_ShiftLayerUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Operation_ShiftLayerDown = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Operation_Clear = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Operation_ShiftColPl = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Operation_ShiftColMi = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Operation_ShiftRowMi = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Operation_ShiftRowPl = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.MoeCharaPic = New System.Windows.Forms.PictureBox()
        Me.SaveFile = New System.Windows.Forms.SaveFileDialog()
        Me.LayerSelector = New PlamoBlockSetter.LayerSelector()
        Me.SelectBlock = New PlamoBlockSetter.BlockObject()
        Me.BlockSelector = New PlamoBlockSetter.BlockSelector()
        Me.ColorSelector = New PlamoBlockSetter.ColorSelector()
        Me.WorkArea = New PlamoBlockSetter.WorkArea()
        Me.MenuBar.SuspendLayout()
        CType(Me.MoeCharaPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SelectBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WorkArea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SelectColor
        '
        Me.SelectColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SelectColor.Location = New System.Drawing.Point(716, 589)
        Me.SelectColor.Name = "SelectColor"
        Me.SelectColor.Size = New System.Drawing.Size(96, 26)
        Me.SelectColor.TabIndex = 2
        Me.SelectColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MenuBar
        '
        Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItem_File, Me.MenuItem_Output, Me.MenuItem_ModelInfo, Me.MenuItem_Operation})
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Size = New System.Drawing.Size(1224, 24)
        Me.MenuBar.TabIndex = 10
        Me.MenuBar.Text = "MenuStrip1"
        '
        'MenuItem_File
        '
        Me.MenuItem_File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.MenuItem_File_LoadJson})
        Me.MenuItem_File.Name = "MenuItem_File"
        Me.MenuItem_File.Size = New System.Drawing.Size(67, 20)
        Me.MenuItem_File.Text = "ファイル(&F)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(157, 6)
        '
        'MenuItem_File_LoadJson
        '
        Me.MenuItem_File_LoadJson.Name = "MenuItem_File_LoadJson"
        Me.MenuItem_File_LoadJson.Size = New System.Drawing.Size(160, 22)
        Me.MenuItem_File_LoadJson.Text = "JSONファイル読込"
        '
        'MenuItem_Output
        '
        Me.MenuItem_Output.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItem_Output_OutputJSONText, Me.MenuItem_Output_OutputFile})
        Me.MenuItem_Output.Name = "MenuItem_Output"
        Me.MenuItem_Output.Size = New System.Drawing.Size(60, 20)
        Me.MenuItem_Output.Text = "出力(&O)"
        '
        'MenuItem_Output_OutputJSONText
        '
        Me.MenuItem_Output_OutputJSONText.Name = "MenuItem_Output_OutputJSONText"
        Me.MenuItem_Output_OutputJSONText.Size = New System.Drawing.Size(132, 22)
        Me.MenuItem_Output_OutputJSONText.Text = "JSON出力"
        '
        'MenuItem_Output_OutputFile
        '
        Me.MenuItem_Output_OutputFile.Name = "MenuItem_Output_OutputFile"
        Me.MenuItem_Output_OutputFile.Size = New System.Drawing.Size(132, 22)
        Me.MenuItem_Output_OutputFile.Text = "ファイル出力"
        '
        'MenuItem_ModelInfo
        '
        Me.MenuItem_ModelInfo.Name = "MenuItem_ModelInfo"
        Me.MenuItem_ModelInfo.Size = New System.Drawing.Size(71, 20)
        Me.MenuItem_ModelInfo.Text = "モデル情報"
        '
        'MenuItem_Operation
        '
        Me.MenuItem_Operation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItem_Operation_ShiftLayerUp, Me.MenuItem_Operation_ShiftLayerDown, Me.MenuItem_Operation_ShiftRowMi, Me.MenuItem_Operation_ShiftRowPl, Me.MenuItem_Operation_ShiftColPl, Me.MenuItem_Operation_ShiftColMi, Me.MenuItem_Operation_Clear})
        Me.MenuItem_Operation.Name = "MenuItem_Operation"
        Me.MenuItem_Operation.Size = New System.Drawing.Size(69, 20)
        Me.MenuItem_Operation.Text = "データ操作"
        '
        'MenuItem_Operation_ShiftLayerUp
        '
        Me.MenuItem_Operation_ShiftLayerUp.Name = "MenuItem_Operation_ShiftLayerUp"
        Me.MenuItem_Operation_ShiftLayerUp.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.Up), System.Windows.Forms.Keys)
        Me.MenuItem_Operation_ShiftLayerUp.Size = New System.Drawing.Size(264, 22)
        Me.MenuItem_Operation_ShiftLayerUp.Text = "上のレイヤーへシフト"
        '
        'MenuItem_Operation_ShiftLayerDown
        '
        Me.MenuItem_Operation_ShiftLayerDown.Name = "MenuItem_Operation_ShiftLayerDown"
        Me.MenuItem_Operation_ShiftLayerDown.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.Down), System.Windows.Forms.Keys)
        Me.MenuItem_Operation_ShiftLayerDown.Size = New System.Drawing.Size(264, 22)
        Me.MenuItem_Operation_ShiftLayerDown.Text = "下のレイヤーへシフト"
        '
        'MenuItem_Operation_Clear
        '
        Me.MenuItem_Operation_Clear.Name = "MenuItem_Operation_Clear"
        Me.MenuItem_Operation_Clear.Size = New System.Drawing.Size(264, 22)
        Me.MenuItem_Operation_Clear.Text = "クリア"
        '
        'MenuItem_Operation_ShiftColPl
        '
        Me.MenuItem_Operation_ShiftColPl.Name = "MenuItem_Operation_ShiftColPl"
        Me.MenuItem_Operation_ShiftColPl.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Right), System.Windows.Forms.Keys)
        Me.MenuItem_Operation_ShiftColPl.Size = New System.Drawing.Size(264, 22)
        Me.MenuItem_Operation_ShiftColPl.Text = "右へシフト"
        '
        'MenuItem_Operation_ShiftColMi
        '
        Me.MenuItem_Operation_ShiftColMi.Name = "MenuItem_Operation_ShiftColMi"
        Me.MenuItem_Operation_ShiftColMi.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Left), System.Windows.Forms.Keys)
        Me.MenuItem_Operation_ShiftColMi.Size = New System.Drawing.Size(264, 22)
        Me.MenuItem_Operation_ShiftColMi.Text = "左へシフト"
        '
        'MenuItem_Operation_ShiftRowMi
        '
        Me.MenuItem_Operation_ShiftRowMi.Name = "MenuItem_Operation_ShiftRowMi"
        Me.MenuItem_Operation_ShiftRowMi.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Up), System.Windows.Forms.Keys)
        Me.MenuItem_Operation_ShiftRowMi.Size = New System.Drawing.Size(264, 22)
        Me.MenuItem_Operation_ShiftRowMi.Text = "上へシフト"
        '
        'MenuItem_Operation_ShiftRowPl
        '
        Me.MenuItem_Operation_ShiftRowPl.Name = "MenuItem_Operation_ShiftRowPl"
        Me.MenuItem_Operation_ShiftRowPl.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Down), System.Windows.Forms.Keys)
        Me.MenuItem_Operation_ShiftRowPl.Size = New System.Drawing.Size(264, 22)
        Me.MenuItem_Operation_ShiftRowPl.Text = "下へシフト"
        '
        'OpenFile
        '
        Me.OpenFile.Filter = "JSONファイル|*.json|テキストファイル|*.txt"
        '
        'MoeCharaPic
        '
        Me.MoeCharaPic.Image = CType(resources.GetObject("MoeCharaPic.Image"), System.Drawing.Image)
        Me.MoeCharaPic.Location = New System.Drawing.Point(1022, 447)
        Me.MoeCharaPic.Margin = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.MoeCharaPic.Name = "MoeCharaPic"
        Me.MoeCharaPic.Size = New System.Drawing.Size(190, 168)
        Me.MoeCharaPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.MoeCharaPic.TabIndex = 11
        Me.MoeCharaPic.TabStop = False
        '
        'SaveFile
        '
        Me.SaveFile.DefaultExt = "json"
        Me.SaveFile.Filter = "JSONファイル|*.json|テキストファイル|*.txt"
        Me.SaveFile.InitialDirectory = "Sample"
        '
        'LayerSelector
        '
        Me.LayerSelector.Cols = 32
        Me.LayerSelector.Layers = 32
        Me.LayerSelector.Location = New System.Drawing.Point(716, 27)
        Me.LayerSelector.Name = "LayerSelector"
        Me.LayerSelector.Rows = 32
        Me.LayerSelector.Size = New System.Drawing.Size(496, 414)
        Me.LayerSelector.TabIndex = 9
        '
        'SelectBlock
        '
        Me.SelectBlock.ColorSetting = ColorSetting1
        Me.SelectBlock.Cols = 2
        Me.SelectBlock.Image = CType(resources.GetObject("SelectBlock.Image"), System.Drawing.Image)
        Me.SelectBlock.Location = New System.Drawing.Point(716, 444)
        Me.SelectBlock.Name = "SelectBlock"
        Me.SelectBlock.Rotation = 1
        Me.SelectBlock.Rows = 8
        Me.SelectBlock.Size = New System.Drawing.Size(128, 32)
        Me.SelectBlock.TabIndex = 8
        Me.SelectBlock.TabStop = False
        '
        'BlockSelector
        '
        Me.BlockSelector.CellSize = 14
        Me.BlockSelector.ColorSetting = ColorSetting2
        Me.BlockSelector.Location = New System.Drawing.Point(716, 482)
        Me.BlockSelector.Name = "BlockSelector"
        Me.BlockSelector.Size = New System.Drawing.Size(275, 104)
        Me.BlockSelector.TabIndex = 7
        '
        'ColorSelector
        '
        Me.ColorSelector.ButtonWidth = 52
        Me.ColorSelector.Location = New System.Drawing.Point(716, 618)
        Me.ColorSelector.MaxCols = 9
        Me.ColorSelector.Name = "ColorSelector"
        Me.ColorSelector.Size = New System.Drawing.Size(468, 96)
        Me.ColorSelector.TabIndex = 6
        '
        'WorkArea
        '
        Me.WorkArea.BackColor = System.Drawing.Color.White
        Me.WorkArea.CellSize = 20
        Me.WorkArea.Cols = 32
        Me.WorkArea.Image = CType(resources.GetObject("WorkArea.Image"), System.Drawing.Image)
        Me.WorkArea.Location = New System.Drawing.Point(12, 27)
        Me.WorkArea.Name = "WorkArea"
        Me.WorkArea.Rows = 32
        Me.WorkArea.SelectLayer = 1
        Me.WorkArea.Size = New System.Drawing.Size(661, 661)
        Me.WorkArea.TabIndex = 5
        Me.WorkArea.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1224, 729)
        Me.Controls.Add(Me.MoeCharaPic)
        Me.Controls.Add(Me.LayerSelector)
        Me.Controls.Add(Me.SelectBlock)
        Me.Controls.Add(Me.BlockSelector)
        Me.Controls.Add(Me.ColorSelector)
        Me.Controls.Add(Me.WorkArea)
        Me.Controls.Add(Me.SelectColor)
        Me.Controls.Add(Me.MenuBar)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PlamoBlock Setter"
        Me.MenuBar.ResumeLayout(False)
        Me.MenuBar.PerformLayout()
        CType(Me.MoeCharaPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SelectBlock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WorkArea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SelectColor As Label
    Friend WithEvents WorkArea As WorkArea
    Friend WithEvents ColorSelector As ColorSelector
    Friend WithEvents BlockSelector As BlockSelector
    Friend WithEvents SelectBlock As BlockObject
    Friend WithEvents LayerSelector As LayerSelector
    Friend WithEvents MenuBar As MenuStrip
    Friend WithEvents MenuItem_File As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents MenuItem_File_LoadJson As ToolStripMenuItem
    Friend WithEvents OpenFile As OpenFileDialog
    Friend WithEvents MenuItem_Output As ToolStripMenuItem
    Friend WithEvents MenuItem_Output_OutputJSONText As ToolStripMenuItem
    Friend WithEvents MoeCharaPic As PictureBox
    Friend WithEvents MenuItem_Output_OutputFile As ToolStripMenuItem
    Friend WithEvents SaveFile As SaveFileDialog
    Friend WithEvents MenuItem_Operation As ToolStripMenuItem
    Friend WithEvents MenuItem_ModelInfo As ToolStripMenuItem
    Friend WithEvents MenuItem_Operation_Clear As ToolStripMenuItem
    Friend WithEvents MenuItem_Operation_ShiftLayerUp As ToolStripMenuItem
    Friend WithEvents MenuItem_Operation_ShiftLayerDown As ToolStripMenuItem
    Friend WithEvents MenuItem_Operation_ShiftColPl As ToolStripMenuItem
    Friend WithEvents MenuItem_Operation_ShiftColMi As ToolStripMenuItem
    Friend WithEvents MenuItem_Operation_ShiftRowMi As ToolStripMenuItem
    Friend WithEvents MenuItem_Operation_ShiftRowPl As ToolStripMenuItem
End Class
