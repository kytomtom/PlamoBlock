<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModelInfo
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.InfoName = New System.Windows.Forms.TextBox()
        Me.InfoDisplayName = New System.Windows.Forms.TextBox()
        Me.InfoTwitter = New System.Windows.Forms.TextBox()
        Me.InfoCopyright = New System.Windows.Forms.TextBox()
        Me.InfoPlateWidth = New System.Windows.Forms.NumericUpDown()
        Me.InfoPlateHeight = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.InfoPlateColor = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.InfoPlateWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InfoPlateHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 252)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 27)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 21)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 21)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "キャンセル"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "モデル名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "モデル表示名"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Twitter"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "コピーライト"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 165)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 12)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "ベースプレート"
        '
        'InfoName
        '
        Me.InfoName.Location = New System.Drawing.Point(144, 10)
        Me.InfoName.Name = "InfoName"
        Me.InfoName.Size = New System.Drawing.Size(173, 19)
        Me.InfoName.TabIndex = 6
        '
        'InfoDisplayName
        '
        Me.InfoDisplayName.Location = New System.Drawing.Point(144, 54)
        Me.InfoDisplayName.Name = "InfoDisplayName"
        Me.InfoDisplayName.Size = New System.Drawing.Size(173, 19)
        Me.InfoDisplayName.TabIndex = 7
        '
        'InfoTwitter
        '
        Me.InfoTwitter.Location = New System.Drawing.Point(144, 88)
        Me.InfoTwitter.Name = "InfoTwitter"
        Me.InfoTwitter.Size = New System.Drawing.Size(173, 19)
        Me.InfoTwitter.TabIndex = 8
        '
        'InfoCopyright
        '
        Me.InfoCopyright.Location = New System.Drawing.Point(144, 119)
        Me.InfoCopyright.Name = "InfoCopyright"
        Me.InfoCopyright.Size = New System.Drawing.Size(173, 19)
        Me.InfoCopyright.TabIndex = 9
        '
        'InfoPlateWidth
        '
        Me.InfoPlateWidth.Location = New System.Drawing.Point(144, 163)
        Me.InfoPlateWidth.Name = "InfoPlateWidth"
        Me.InfoPlateWidth.Size = New System.Drawing.Size(50, 19)
        Me.InfoPlateWidth.TabIndex = 11
        '
        'InfoPlateHeight
        '
        Me.InfoPlateHeight.Location = New System.Drawing.Point(221, 163)
        Me.InfoPlateHeight.Name = "InfoPlateHeight"
        Me.InfoPlateHeight.Size = New System.Drawing.Size(50, 19)
        Me.InfoPlateHeight.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(198, 165)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(17, 12)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "×"
        '
        'InfoPlateColor
        '
        Me.InfoPlateColor.FormattingEnabled = True
        Me.InfoPlateColor.Location = New System.Drawing.Point(280, 162)
        Me.InfoPlateColor.Name = "InfoPlateColor"
        Me.InfoPlateColor.Size = New System.Drawing.Size(121, 20)
        Me.InfoPlateColor.TabIndex = 14
        '
        'ModelInfo
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 291)
        Me.Controls.Add(Me.InfoPlateColor)
        Me.Controls.Add(Me.InfoPlateHeight)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.InfoPlateWidth)
        Me.Controls.Add(Me.InfoCopyright)
        Me.Controls.Add(Me.InfoTwitter)
        Me.Controls.Add(Me.InfoDisplayName)
        Me.Controls.Add(Me.InfoName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ModelInfo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ModelInfo"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.InfoPlateWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InfoPlateHeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents InfoName As TextBox
    Friend WithEvents InfoDisplayName As TextBox
    Friend WithEvents InfoTwitter As TextBox
    Friend WithEvents InfoCopyright As TextBox
    Friend WithEvents InfoPlateWidth As NumericUpDown
    Friend WithEvents InfoPlateHeight As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents InfoPlateColor As ComboBox
End Class
