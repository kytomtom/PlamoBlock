﻿Public Class BlockObject
    Inherits PictureBox

    'プロパティの既定値
    Private Const _Default_Rows As Integer = 1
    Private Const _Default_Cols As Integer = 1
    Private Const _Default_CellSize As Integer = 16

    Private intCols As Integer
    Private intRows As Integer
    Private intCellSize As Integer
    Private objColorSetting As BlockColor.ColorSetting

    Private objBlockImage As BlockImage

    <System.ComponentModel.Category("エリアサイズ設定")>
    <System.ComponentModel.DefaultValue(_Default_Rows)>
    Public Property Rows() As Integer
        Get
            Return intRows
        End Get
        Set(ByVal value As Integer)
            intRows = value
            SetBlockSize(intRows, intCols, intCellSize, objColorSetting)
        End Set
    End Property
    <System.ComponentModel.Category("エリアサイズ設定")>
    <System.ComponentModel.DefaultValue(_Default_Cols)>
    Public Property Cols() As Integer
        Get
            Return intCols
        End Get
        Set(ByVal value As Integer)
            intCols = value
            SetBlockSize(intRows, intCols, intCellSize, objColorSetting)
        End Set
    End Property
    <System.ComponentModel.Category("エリアサイズ設定")>
    <System.ComponentModel.DefaultValue(_Default_CellSize)>
    Public Property CellSize() As Integer
        Get
            Return intCellSize
        End Get
        Set(ByVal value As Integer)
            intCellSize = value
            SetBlockSize(intRows, intCols, intCellSize, objColorSetting)
        End Set
    End Property

    Public Property ColorSetting() As BlockColor.ColorSetting
        Get
            Return objColorSetting
        End Get
        Set(value As BlockColor.ColorSetting)
            objColorSetting = value
            SetBlockSize(intRows, intCols, intCellSize, objColorSetting)
        End Set
    End Property

    Public ReadOnly Property BaseColor() As Color
        Get
            Return objBlockImage.BaseColor
        End Get
    End Property
    Public ReadOnly Property EdgeColor() As Color
        Get
            Return objBlockImage.EdgeColor
        End Get
    End Property
    Public ReadOnly Property Opacity() As Single
        Get
            Return objBlockImage.Opacity
        End Get
    End Property

    Public Sub New(pintRows As Integer, pintCols As Integer, pintCellSize As Integer, pobjColorSetting As BlockColor.ColorSetting)
        intRows = pintRows
        intCols = pintCols
        intCellSize = pintCellSize
        objColorSetting = pobjColorSetting

        SetBlockSize(intRows, intCols, intCellSize, objColorSetting)
    End Sub
    Public Sub New()
        Me.New(_Default_Rows, _Default_Cols, _Default_CellSize, New BlockColor.ColorSetting())
    End Sub

    Private Sub BlockObject_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetControlSize()
    End Sub

    Public Sub SetBlockSize(pintRows As Integer, pintCols As Integer, pintCellSize As Integer, pobjColorSetting As BlockColor.ColorSetting)
        intRows = pintRows
        intCols = pintCols
        intCellSize = pintCellSize
        objColorSetting = pobjColorSetting

        SetControlSize()

        objBlockImage = New BlockImage(intRows, intCols, intCellSize, objColorSetting)

        Image = objBlockImage.Image
    End Sub

    Private Sub SetControlSize()
        Width = intCols * intCellSize
        Height = intRows * intCellSize
    End Sub
End Class
