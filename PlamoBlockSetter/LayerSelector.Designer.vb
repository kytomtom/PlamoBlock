﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LayerSelector
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
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
        Me.BaseLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.HeaderPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.SelectLayer = New System.Windows.Forms.NumericUpDown()
        Me.IsEdgeView = New System.Windows.Forms.CheckBox()
        Me.PanelBack = New System.Windows.Forms.Panel()
        Me.PanelLeft = New System.Windows.Forms.Panel()
        Me.PanelFront = New System.Windows.Forms.Panel()
        Me.PanelRight = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BaseLayout.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.SelectLayer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BaseLayout
        '
        Me.BaseLayout.ColumnCount = 3
        Me.BaseLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.BaseLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.BaseLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.BaseLayout.Controls.Add(Me.PanelRight, 2, 2)
        Me.BaseLayout.Controls.Add(Me.HeaderPanel, 0, 0)
        Me.BaseLayout.Controls.Add(Me.PanelBack, 1, 1)
        Me.BaseLayout.Controls.Add(Me.PanelFront, 1, 3)
        Me.BaseLayout.Controls.Add(Me.PanelLeft, 0, 2)
        Me.BaseLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BaseLayout.Location = New System.Drawing.Point(0, 0)
        Me.BaseLayout.Margin = New System.Windows.Forms.Padding(0)
        Me.BaseLayout.Name = "BaseLayout"
        Me.BaseLayout.RowCount = 5
        Me.BaseLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.BaseLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.BaseLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.BaseLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.BaseLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.BaseLayout.Size = New System.Drawing.Size(360, 240)
        Me.BaseLayout.TabIndex = 0
        '
        'HeaderPanel
        '
        Me.BaseLayout.SetColumnSpan(Me.HeaderPanel, 3)
        Me.HeaderPanel.Controls.Add(Me.SelectLayer)
        Me.HeaderPanel.Controls.Add(Me.IsEdgeView)
        Me.HeaderPanel.Controls.Add(Me.Label1)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(360, 24)
        Me.HeaderPanel.TabIndex = 0
        '
        'SelectLayer
        '
        Me.SelectLayer.Font = New System.Drawing.Font("MS UI Gothic", 12.0!)
        Me.SelectLayer.Location = New System.Drawing.Point(0, 0)
        Me.SelectLayer.Margin = New System.Windows.Forms.Padding(0)
        Me.SelectLayer.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.SelectLayer.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.SelectLayer.Name = "SelectLayer"
        Me.SelectLayer.Size = New System.Drawing.Size(58, 23)
        Me.SelectLayer.TabIndex = 0
        Me.SelectLayer.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'IsEdgeView
        '
        Me.IsEdgeView.Checked = True
        Me.IsEdgeView.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IsEdgeView.Location = New System.Drawing.Point(61, 3)
        Me.IsEdgeView.Name = "IsEdgeView"
        Me.IsEdgeView.Size = New System.Drawing.Size(84, 21)
        Me.IsEdgeView.TabIndex = 1
        Me.IsEdgeView.Text = "境界線表示"
        Me.IsEdgeView.UseVisualStyleBackColor = True
        '
        'PanelBack
        '
        Me.PanelBack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelBack.Location = New System.Drawing.Point(120, 24)
        Me.PanelBack.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelBack.Name = "PanelBack"
        Me.BaseLayout.SetRowSpan(Me.PanelBack, 2)
        Me.PanelBack.Size = New System.Drawing.Size(120, 108)
        Me.PanelBack.TabIndex = 1
        '
        'PanelLeft
        '
        Me.PanelLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelLeft.Location = New System.Drawing.Point(0, 78)
        Me.PanelLeft.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelLeft.Name = "PanelLeft"
        Me.BaseLayout.SetRowSpan(Me.PanelLeft, 2)
        Me.PanelLeft.Size = New System.Drawing.Size(120, 108)
        Me.PanelLeft.TabIndex = 4
        '
        'PanelFront
        '
        Me.PanelFront.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelFront.Location = New System.Drawing.Point(120, 132)
        Me.PanelFront.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelFront.Name = "PanelFront"
        Me.BaseLayout.SetRowSpan(Me.PanelFront, 2)
        Me.PanelFront.Size = New System.Drawing.Size(120, 108)
        Me.PanelFront.TabIndex = 3
        '
        'PanelRight
        '
        Me.PanelRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelRight.Location = New System.Drawing.Point(240, 78)
        Me.PanelRight.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelRight.Name = "PanelRight"
        Me.BaseLayout.SetRowSpan(Me.PanelRight, 2)
        Me.PanelRight.Size = New System.Drawing.Size(120, 108)
        Me.PanelRight.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(151, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(206, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "※Shift+マウスホイールでレイヤーの上下"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LayerSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BaseLayout)
        Me.Name = "LayerSelector"
        Me.Size = New System.Drawing.Size(360, 240)
        Me.BaseLayout.ResumeLayout(False)
        Me.HeaderPanel.ResumeLayout(False)
        CType(Me.SelectLayer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BaseLayout As TableLayoutPanel
    Friend WithEvents HeaderPanel As FlowLayoutPanel
    Friend WithEvents SelectLayer As NumericUpDown
    Friend WithEvents PanelBack As Panel
    Friend WithEvents PanelFront As Panel
    Friend WithEvents PanelLeft As Panel
    Friend WithEvents PanelRight As Panel
    Friend WithEvents IsEdgeView As CheckBox
    Friend WithEvents Label1 As Label
End Class
