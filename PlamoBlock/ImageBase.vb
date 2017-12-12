Public Class ImageBase
    Inherits PictureBox

    Protected intMaxWidth As Integer
    Protected intMaxHeight As Integer
    Protected intMaxCols As Integer
    Protected intMaxRows As Integer
    Protected intCellSize As Integer

    Public ReadOnly Property MaxWidth() As Integer
        Get
            Return Me.intMaxWidth
        End Get
    End Property
    Public ReadOnly Property MaxHeight() As Integer
        Get
            Return Me.intMaxHeight
        End Get
    End Property
    Public ReadOnly Property MaxCols() As Integer
        Get
            Return Me.intMaxCols
        End Get
    End Property
    Public ReadOnly Property MaxRows() As Integer
        Get
            Return Me.intMaxRows
        End Get
    End Property
    Public ReadOnly Property CellSize() As Integer
        Get
            Return Me.intCellSize
        End Get
    End Property

    Protected Sub SetCellSize()
        Me.intCellSize = CInt(Math.Truncate(Math.Min((Me.intMaxWidth - 1) / (Me.intMaxCols + 1), (Me.intMaxHeight - 1) / (Me.intMaxRows + 1))))
    End Sub
End Class