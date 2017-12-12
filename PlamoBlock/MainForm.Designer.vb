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
        Me.ColorSelector = New PlamoBlock.ColorSelector()
        Me.WorkArea = New PlamoBlock.WorkArea()
        Me.SelectColor = New System.Windows.Forms.Label()
        Me.BlockImage1 = New PlamoBlock.BlockImage()
        CType(Me.WorkArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BlockImage1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColorSelector
        '
        Me.ColorSelector.Location = New System.Drawing.Point(496, 301)
        Me.ColorSelector.Name = "ColorSelector"
        Me.ColorSelector.Size = New System.Drawing.Size(429, 205)
        Me.ColorSelector.TabIndex = 1
        '
        'WorkArea
        '
        Me.WorkArea.BackColor = System.Drawing.Color.White
        Me.WorkArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.WorkArea.Location = New System.Drawing.Point(12, 12)
        Me.WorkArea.Name = "WorkArea"
        Me.WorkArea.Size = New System.Drawing.Size(478, 494)
        Me.WorkArea.TabIndex = 0
        Me.WorkArea.TabStop = False
        '
        'SelectColor
        '
        Me.SelectColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SelectColor.Location = New System.Drawing.Point(496, 266)
        Me.SelectColor.Name = "SelectColor"
        Me.SelectColor.Size = New System.Drawing.Size(96, 32)
        Me.SelectColor.TabIndex = 2
        Me.SelectColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BlockImage1
        '
        Me.BlockImage1.Location = New System.Drawing.Point(530, 83)
        Me.BlockImage1.Name = "BlockImage1"
        Me.BlockImage1.Size = New System.Drawing.Size(51, 41)
        Me.BlockImage1.TabIndex = 3
        Me.BlockImage1.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(937, 518)
        Me.Controls.Add(Me.BlockImage1)
        Me.Controls.Add(Me.SelectColor)
        Me.Controls.Add(Me.ColorSelector)
        Me.Controls.Add(Me.WorkArea)
        Me.DoubleBuffered = True
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PlamoBlock"
        CType(Me.WorkArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BlockImage1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents WorkArea As WorkArea
    Friend WithEvents ColorSelector As ColorSelector
    Friend WithEvents SelectColor As Label
    Friend WithEvents BlockImage1 As BlockImage
End Class
