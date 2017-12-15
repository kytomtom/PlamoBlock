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
        Me.SelectColor = New System.Windows.Forms.Label()
        Me.ColorSelector = New PlamoBlock.ColorSelector()
        Me.WorkArea1 = New PlamoBlock.WorkArea()
        CType(Me.WorkArea1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SelectColor
        '
        Me.SelectColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SelectColor.Location = New System.Drawing.Point(580, 452)
        Me.SelectColor.Name = "SelectColor"
        Me.SelectColor.Size = New System.Drawing.Size(96, 23)
        Me.SelectColor.TabIndex = 2
        Me.SelectColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ColorSelector
        '
        Me.ColorSelector.ButtonWidth = 52
        Me.ColorSelector.Location = New System.Drawing.Point(580, 478)
        Me.ColorSelector.Name = "ColorSelector"
        Me.ColorSelector.Size = New System.Drawing.Size(416, 96)
        Me.ColorSelector.TabIndex = 6
        '
        'WorkArea1
        '
        Me.WorkArea1.BackColor = System.Drawing.Color.White
        Me.WorkArea1.CellSize = 17
        Me.WorkArea1.Cols = 32
        Me.WorkArea1.Image = CType(resources.GetObject("WorkArea1.Image"), System.Drawing.Image)
        Me.WorkArea1.Location = New System.Drawing.Point(12, 12)
        Me.WorkArea1.Name = "WorkArea1"
        Me.WorkArea1.Rows = 32
        Me.WorkArea1.Size = New System.Drawing.Size(562, 562)
        Me.WorkArea1.TabIndex = 5
        Me.WorkArea1.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1007, 583)
        Me.Controls.Add(Me.ColorSelector)
        Me.Controls.Add(Me.WorkArea1)
        Me.Controls.Add(Me.SelectColor)
        Me.DoubleBuffered = True
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PlamoBlock"
        CType(Me.WorkArea1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SelectColor As Label
    Friend WithEvents WorkArea1 As WorkArea
    Friend WithEvents ColorSelector As ColorSelector
End Class
