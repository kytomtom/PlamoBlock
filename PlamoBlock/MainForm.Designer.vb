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
        Dim ModelData1 As PlamoBlock.ModelData = New PlamoBlock.ModelData()
        Dim ColorSetting2 As PlamoBlock.BlockColor.ColorSetting = New PlamoBlock.BlockColor.ColorSetting()
        Me.SelectColor = New System.Windows.Forms.Label()
        Me.BlockSelector = New PlamoBlock.BlockSelector()
        Me.ColorSelector = New PlamoBlock.ColorSelector()
        Me.WorkArea = New PlamoBlock.WorkArea()
        Me.SelectBlock = New PlamoBlock.BlockObject()
        CType(Me.WorkArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SelectBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SelectColor
        '
        Me.SelectColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SelectColor.Location = New System.Drawing.Point(569, 441)
        Me.SelectColor.Name = "SelectColor"
        Me.SelectColor.Size = New System.Drawing.Size(96, 23)
        Me.SelectColor.TabIndex = 2
        Me.SelectColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BlockSelector
        '
        Me.BlockSelector.ColorSetting = ColorSetting1
        Me.BlockSelector.Location = New System.Drawing.Point(569, 331)
        Me.BlockSelector.Name = "BlockSelector"
        Me.BlockSelector.Size = New System.Drawing.Size(307, 107)
        Me.BlockSelector.TabIndex = 7
        '
        'ColorSelector
        '
        Me.ColorSelector.ButtonWidth = 52
        Me.ColorSelector.Location = New System.Drawing.Point(569, 467)
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
        Me.WorkArea.Location = New System.Drawing.Point(12, 12)
        Me.WorkArea.ModelData = ModelData1
        Me.WorkArea.Name = "WorkArea"
        Me.WorkArea.Rows = 24
        Me.WorkArea.Size = New System.Drawing.Size(551, 551)
        Me.WorkArea.TabIndex = 5
        Me.WorkArea.TabStop = False
        '
        'SelectBlock
        '
        Me.SelectBlock.ColorSetting = ColorSetting2
        Me.SelectBlock.Image = CType(resources.GetObject("SelectBlock.Image"), System.Drawing.Image)
        Me.SelectBlock.Location = New System.Drawing.Point(569, 293)
        Me.SelectBlock.Name = "SelectBlock"
        Me.SelectBlock.Rows = 2
        Me.SelectBlock.Size = New System.Drawing.Size(16, 32)
        Me.SelectBlock.TabIndex = 8
        Me.SelectBlock.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(993, 571)
        Me.Controls.Add(Me.SelectBlock)
        Me.Controls.Add(Me.BlockSelector)
        Me.Controls.Add(Me.ColorSelector)
        Me.Controls.Add(Me.WorkArea)
        Me.Controls.Add(Me.SelectColor)
        Me.DoubleBuffered = True
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PlamoBlock"
        CType(Me.WorkArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SelectBlock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SelectColor As Label
    Friend WithEvents WorkArea As WorkArea
    Friend WithEvents ColorSelector As ColorSelector
    Friend WithEvents BlockSelector As BlockSelector
    Friend WithEvents SelectBlock As BlockObject
End Class
