'Imports System.Data.SQLite

'Public Class Database
'    Inherits Database_SQLite

'    Private decVersion As Decimal

'    Public Sub New(pstrDataSource As String, pdecVersion As Decimal)
'        MyBase.New(pstrDataSource)

'        decVersion = pdecVersion

'        Initialize()
'    End Sub

'    Private Function Initialize() As Boolean
'        Try
'            Open()

'            Begin()

'            '設定テーブルの確認
'            If TableExists("Setting") = False Then
'                CreateTable_Setting()
'            End If

'            'データベースバージョンの確認
'            If CheckVersion() = False Then
'                'データベースアップデート時の処理
'            End If

'            'モデルデータテーブルの確認
'            If TableExists("ModelData") = False Then
'                CreateTable_ModelData()
'            End If

'            Commit()

'        Catch sqlex As SQLiteException
'            Throw New Exception(sqlex.Message)
'            Rollback()

'        Catch ex As Exception
'            Throw New Exception(ex.Message)
'            Rollback()

'        Finally
'            Close()
'        End Try

'        Return True
'    End Function

'    '設定テーブルの作成
'    Private Function CreateTable_Setting() As Boolean
'        Dim lstrSQL As String

'        lstrSQL = Common.GetResourceText("Create_Setting.txt")

'        ClearParameter()
'        If ExecuteNonQuery(lstrSQL) <= -1 Then
'            Return False
'        End If

'        Return True
'    End Function

'    'モデルデータテーブルの作成
'    Private Function CreateTable_ModelData() As Boolean
'        Dim lstrSQL As String

'        lstrSQL = Common.GetResourceText("Create_ModelData.txt")

'        ClearParameter()
'        If ExecuteNonQuery(lstrSQL) <= -1 Then
'            Return False
'        End If

'        Return True
'    End Function

'    '設定データの初期値設定
'    Private Function InitializeSettingData() As Boolean
'        Dim lstrSQL As String

'        lstrSQL = Common.GetResourceText("Isert_Setting_Init.txt")

'        ClearParameter()
'        AddParameter("@Version", decVersion)
'        If ExecuteNonQuery(lstrSQL) <= -1 Then
'            Return False
'        End If

'        Return True
'    End Function

'    Private Function GetDatabaseVersion() As Decimal
'        Dim lobjDRow As DataRow
'        Dim lstrSQL As String

'        lstrSQL = Common.GetResourceText("Select_Version.txt")

'        lobjDRow = ExecuteQueryFirstRow(lstrSQL)

'        If lobjDRow Is Nothing OrElse CInt(lobjDRow.Item(0)) = 0 Then
'            Return -1
'        End If

'        Return CDec(lobjDRow.Item("Version"))
'    End Function

'    Private Function CheckVersion() As Boolean
'        Dim ldecDBVersion As Decimal

'        ldecDBVersion = GetDatabaseVersion()

'        '設定データがない場合はデータを作成
'        If ldecDBVersion = -1 Then
'            InitializeSettingData()
'            Return True
'        End If

'        '設定データのバージョンが古い場合はFalse（要データベースアップデート）
'        If decVersion > ldecDBVersion Then
'            Return False
'        End If

'        Return True
'    End Function
'End Class
