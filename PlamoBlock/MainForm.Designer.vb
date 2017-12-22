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
        Dim ColorSetting1 As PlamoBlock.BlockColor.ColorSetting = New PlamoBlock.BlockColor.ColorSetting()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Dim ColorSetting2 As PlamoBlock.BlockColor.ColorSetting = New PlamoBlock.BlockColor.ColorSetting()
        Me.SelectColor = New System.Windows.Forms.Label()
        Me.MenuBar = New System.Windows.Forms.MenuStrip()
        Me.MenuItem_File = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuItem_File_LoadJsonOldVer = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.MenuItem_Output = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItem_Output_OutputJSONText = New System.Windows.Forms.ToolStripMenuItem()
        Me.LayerSelector = New PlamoBlock.LayerSelector()
        Me.SelectBlock = New PlamoBlock.BlockObject()
        Me.BlockSelector = New PlamoBlock.BlockSelector()
        Me.ColorSelector = New PlamoBlock.ColorSelector()
        Me.WorkArea = New PlamoBlock.WorkArea()
        Me.MenuBar.SuspendLayout()
        CType(Me.SelectBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WorkArea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SelectColor
        '
        Me.SelectColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SelectColor.Location = New System.Drawing.Point(569, 456)
        Me.SelectColor.Name = "SelectColor"
        Me.SelectColor.Size = New System.Drawing.Size(96, 23)
        Me.SelectColor.TabIndex = 2
        Me.SelectColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MenuBar
        '
        Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItem_File, Me.MenuItem_Output})
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Size = New System.Drawing.Size(993, 24)
        Me.MenuBar.TabIndex = 10
        Me.MenuBar.Text = "MenuStrip1"
        '
        'MenuItem_File
        '
        Me.MenuItem_File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.MenuItem_File_LoadJsonOldVer})
        Me.MenuItem_File.Name = "MenuItem_File"
        Me.MenuItem_File.Size = New System.Drawing.Size(67, 20)
        Me.MenuItem_File.Text = "ファイル(&F)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(193, 6)
        '
        'MenuItem_File_LoadJsonOldVer
        '
        Me.MenuItem_File_LoadJsonOldVer.Name = "MenuItem_File_LoadJsonOldVer"
        Me.MenuItem_File_LoadJsonOldVer.Size = New System.Drawing.Size(196, 22)
        Me.MenuItem_File_LoadJsonOldVer.Text = "JSONファイル読込(旧Ver)"
        '
        'OpenFile
        '
        Me.OpenFile.Filter = "JSONファイル|*.json|テキストファイル|*.txt"
        '
        'MenuItem_Output
        '
        Me.MenuItem_Output.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItem_Output_OutputJSONText})
        Me.MenuItem_Output.Name = "MenuItem_Output"
        Me.MenuItem_Output.Size = New System.Drawing.Size(60, 20)
        Me.MenuItem_Output.Text = "出力(&O)"
        '
        'MenuItem_Output_OutputJSONText
        '
        Me.MenuItem_Output_OutputJSONText.Name = "MenuItem_Output_OutputJSONText"
        Me.MenuItem_Output_OutputJSONText.Size = New System.Drawing.Size(126, 22)
        Me.MenuItem_Output_OutputJSONText.Text = "JSON出力"
        '
        'LayerSelector
        '
        Me.LayerSelector.Location = New System.Drawing.Point(569, 27)
        Me.LayerSelector.Name = "LayerSelector"
        Me.LayerSelector.Size = New System.Drawing.Size(416, 275)
        Me.LayerSelector.TabIndex = 9
        '
        'SelectBlock
        '
        Me.SelectBlock.ColorSetting = ColorSetting1
        Me.SelectBlock.Image = CType(resources.GetObject("SelectBlock.Image"), System.Drawing.Image)
        Me.SelectBlock.Location = New System.Drawing.Point(569, 308)
        Me.SelectBlock.Name = "SelectBlock"
        Me.SelectBlock.Rows = 2
        Me.SelectBlock.Size = New System.Drawing.Size(16, 32)
        Me.SelectBlock.TabIndex = 8
        Me.SelectBlock.TabStop = False
        '
        'BlockSelector
        '
        Me.BlockSelector.ColorSetting = ColorSetting2
        Me.BlockSelector.Location = New System.Drawing.Point(569, 346)
        Me.BlockSelector.Name = "BlockSelector"
        Me.BlockSelector.Size = New System.Drawing.Size(307, 107)
        Me.BlockSelector.TabIndex = 7
        '
        'ColorSelector
        '
        Me.ColorSelector.ButtonWidth = 52
        Me.ColorSelector.Location = New System.Drawing.Point(569, 482)
        Me.ColorSelector.Name = "ColorSelector"
        Me.ColorSelector.Size = New System.Drawing.Size(416, 96)
        Me.ColorSelector.TabIndex = 6
        '
        'WorkArea
        '
        Me.WorkArea.BackColor = System.Drawing.Color.White
        Me.WorkArea.CellSize = 22
        Me.WorkArea.Cols = 24
        Me.WorkArea.Image = CType(resources.GetObject("WorkArea.Image"), System.Drawing.Image)
        Me.WorkArea.Location = New System.Drawing.Point(12, 27)
        Me.WorkArea.Name = "WorkArea"
        Me.WorkArea.Rows = 24
        Me.WorkArea.SelectLayer = 0
        Me.WorkArea.Size = New System.Drawing.Size(551, 551)
        Me.WorkArea.TabIndex = 5
        Me.WorkArea.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(993, 589)
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
        Me.Text = "PlamoBlock"
        Me.MenuBar.ResumeLayout(False)
        Me.MenuBar.PerformLayout()
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
    Friend WithEvents MenuItem_File_LoadJsonOldVer As ToolStripMenuItem
    Friend WithEvents OpenFile As OpenFileDialog
    Friend WithEvents MenuItem_Output As ToolStripMenuItem
    Friend WithEvents MenuItem_Output_OutputJSONText As ToolStripMenuItem
End Class
