﻿Imports System.Windows.Forms

Public Class ResultJSON

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ResultJSON_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ResultText.Focus()
        ResultText.SelectAll()
    End Sub
End Class
