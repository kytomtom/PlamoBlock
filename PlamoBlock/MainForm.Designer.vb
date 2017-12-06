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
        Me.WorkArea1 = New PlamoBlock.WorkArea()
        CType(Me.WorkArea1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WorkArea1
        '
        Me.WorkArea1.BackColor = System.Drawing.Color.White
        Me.WorkArea1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.WorkArea1.Location = New System.Drawing.Point(12, 12)
        Me.WorkArea1.Name = "WorkArea1"
        Me.WorkArea1.Size = New System.Drawing.Size(482, 482)
        Me.WorkArea1.TabIndex = 0
        Me.WorkArea1.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(937, 518)
        Me.Controls.Add(Me.WorkArea1)
        Me.DoubleBuffered = True
        Me.Name = "MainForm"
        Me.Text = "PlamoBlock"
        CType(Me.WorkArea1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents WorkArea1 As WorkArea
End Class
