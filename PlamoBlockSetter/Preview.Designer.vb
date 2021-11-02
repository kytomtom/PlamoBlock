<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Preview
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
        Me.PreviewLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnReload = New System.Windows.Forms.Button()
        Me.btnDevTool = New System.Windows.Forms.Button()
        Me.PreviewLayout.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PreviewLayout
        '
        Me.PreviewLayout.ColumnCount = 1
        Me.PreviewLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.PreviewLayout.Controls.Add(Me.FlowLayoutPanel1, 0, 0)
        Me.PreviewLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PreviewLayout.Location = New System.Drawing.Point(0, 0)
        Me.PreviewLayout.Margin = New System.Windows.Forms.Padding(0)
        Me.PreviewLayout.Name = "PreviewLayout"
        Me.PreviewLayout.RowCount = 2
        Me.PreviewLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.PreviewLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.PreviewLayout.Size = New System.Drawing.Size(630, 669)
        Me.PreviewLayout.TabIndex = 1
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.btnReload)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnDevTool)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(624, 34)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'btnReload
        '
        Me.btnReload.Location = New System.Drawing.Point(0, 0)
        Me.btnReload.Margin = New System.Windows.Forms.Padding(0)
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Size = New System.Drawing.Size(80, 34)
        Me.btnReload.TabIndex = 0
        Me.btnReload.Text = "リロード"
        Me.btnReload.UseVisualStyleBackColor = True
        '
        'btnDevTool
        '
        Me.btnDevTool.Location = New System.Drawing.Point(80, 0)
        Me.btnDevTool.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDevTool.Name = "btnDevTool"
        Me.btnDevTool.Size = New System.Drawing.Size(80, 34)
        Me.btnDevTool.TabIndex = 1
        Me.btnDevTool.Text = "開発者ツール"
        Me.btnDevTool.UseVisualStyleBackColor = True
        '
        'Preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(630, 669)
        Me.Controls.Add(Me.PreviewLayout)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Preview"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Preview"
        Me.PreviewLayout.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PreviewLayout As TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents btnReload As Button
    Friend WithEvents btnDevTool As Button
End Class
