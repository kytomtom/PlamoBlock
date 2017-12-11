﻿Imports System.Data.SQLite

Public Class Database_SQLite
    Protected objDBConn As SQLiteConnection
    Protected objDBCmd As SQLiteCommand

    Protected intFailureCount As Integer
    Protected intSuccessCount As Integer

    Public ReadOnly Property FailureCount() As Integer
        Get
            Return Me.intFailureCount
        End Get
    End Property

    Public ReadOnly Property SuccessCount() As Integer
        Get
            Return Me.intSuccessCount
        End Get
    End Property

    Public Sub New(pstrDataSource As String)
        Me.objDBConn = New SQLiteConnection()

        Me.objDBConn.ConnectionString = "Data Source=" + pstrDataSource
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        If Me.objDBConn IsNot Nothing Then
            Me.objDBConn.Dispose()
        End If
        If Me.objDBCmd IsNot Nothing Then
            Me.objDBCmd.Dispose()
        End If
    End Sub

    Public Function Open() As Boolean
        Try
            Me.objDBCmd = Me.objDBConn.CreateCommand()
            Me.objDBConn.Open()

            Return True

        Catch sqlex As SQLiteException
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False
    End Function

    Public Function Close() As Boolean
        Try
            If Me.objDBConn.State = ConnectionState.Open Then
                Me.objDBConn.Close()
                Me.objDBCmd.Dispose()
            End If

            Return True

        Catch sqlex As SQLiteException
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False
    End Function

    Public Function Begin() As Boolean
        Try
            If Me.objDBConn.State = ConnectionState.Closed Then
                Return False
            End If

            Me.objDBCmd.Transaction = Me.objDBConn.BeginTransaction()

            Return True

        Catch sqlex As SQLiteException
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False
    End Function

    Public Function Rollback() As Boolean
        Try
            Me.objDBCmd.Transaction.Rollback()

            Return True

        Catch sqlex As SQLiteException
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False
    End Function

    Public Function Commit() As Boolean
        Try
            Me.objDBCmd.Transaction.Commit()

            Close()

            Return True

        Catch sqlex As SQLiteException
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False
    End Function

    Public Function ExecuteQuery(pstrSQL As String) As DataTable
        Dim lobjDAdapter As SQLiteDataAdapter
        Dim lobjDTable As DataTable

        Try
            If Me.objDBConn.State <> ConnectionState.Open Then
                Return Nothing
            End If

            lobjDTable = New DataTable

            lobjDAdapter = New SQLiteDataAdapter(pstrSQL, Me.objDBConn)

            lobjDAdapter.Fill(lobjDTable)

        Catch sqlex As SQLiteException
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally

        End Try

        Return lobjDTable
    End Function

    Public Function ExecuteQueryFirstRow(pstrSQL As String) As DataRow
        Dim lobjDTable As DataTable
        Dim lobjDRow As DataRow

        lobjDTable = Nothing

        Try
            lobjDTable = Me.ExecuteQuery(pstrSQL)

            If lobjDTable Is Nothing OrElse lobjDTable.Rows.Count = 0 Then
                Return Nothing
            End If

            lobjDRow = lobjDTable.Rows(0)

        Catch sqlex As SQLiteException
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally
            lobjDTable = Nothing
        End Try

        Return lobjDRow
    End Function

    Public Function ExecuteNonQuery(pstrSQL As String) As Integer
        Dim lintCount As Integer

        lintCount = -1

        Try
            If Me.objDBConn.State <> ConnectionState.Open Then
                Return lintCount
            End If

            Me.objDBCmd.CommandText = pstrSQL

            lintCount = Me.objDBCmd.ExecuteNonQuery()

            intSuccessCount += lintCount

        Catch sqlex As SQLiteException
            intFailureCount += Math.Abs(lintCount)
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return lintCount
    End Function

    Public Function TableExists(pstrTableName As String) As Boolean
        Dim lobjDRow As DataRow
        Dim lstrSQL As String

        lstrSQL = String.Format("select count(*) from sqlite_master where type='table' and name='{0}';", pstrTableName)

        lobjDRow = Me.ExecuteQueryFirstRow(lstrSQL)

        If lobjDRow Is Nothing OrElse CInt(lobjDRow.Item(0)) = 0 Then
            Return False
        End If

        Return True
    End Function

    Public Function ClearParameter() As Boolean
        Try
            Me.objDBCmd.Parameters.Clear()

            Return True

        Catch sqlex As SQLiteException
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False
    End Function

    Public Function AddParameter(pstrKey As String, pobjValue As Object) As Boolean
        Try
            Me.objDBCmd.Parameters.AddWithValue(pstrKey, pobjValue)

            Return True

        Catch sqlex As SQLiteException
            Throw New Exception(sqlex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return False
    End Function
End Class
