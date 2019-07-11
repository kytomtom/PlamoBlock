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
        Me.WinFormsBrowserView1 = New DotNetBrowser.WinForms.WinFormsBrowserView()
        Me.SuspendLayout()
        '
        'WinFormsBrowserView1
        '
        Me.WinFormsBrowserView1.AcceptLanguage = Nothing
        Me.WinFormsBrowserView1.AudioMuted = Nothing
        Me.WinFormsBrowserView1.BrowserType = DotNetBrowser.BrowserType.HEAVYWEIGHT
        Me.WinFormsBrowserView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WinFormsBrowserView1.InitialFocusOwner = False
        Me.WinFormsBrowserView1.Location = New System.Drawing.Point(0, 0)
        Me.WinFormsBrowserView1.Name = "WinFormsBrowserView1"
        Me.WinFormsBrowserView1.Preferences = Nothing
        Me.WinFormsBrowserView1.Size = New System.Drawing.Size(611, 506)
        Me.WinFormsBrowserView1.TabIndex = 0
        Me.WinFormsBrowserView1.URL = Nothing
        Me.WinFormsBrowserView1.ZoomLevel = Nothing
        '
        'Preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 506)
        Me.Controls.Add(Me.WinFormsBrowserView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Preview"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Preview"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents WinFormsBrowserView1 As DotNetBrowser.WinForms.WinFormsBrowserView
End Class
