Imports System.Data.OleDb

Module Module1

    Public Conn As OleDbConnection
    Public Cmd As OleDbCommand
    Public Rd As OleDbDataReader
    Public str As String
    Public Da As OleDbDataAdapter
    Public Ds As DataSet

    Sub Koneksi()
        Dim path As String
        path = ".\db_percetakan.accdb;"
        str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " & path & ";"
        Conn = New OleDbConnection(str)
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        Else
            MsgBox("Koneksi Gagal")
        End If
    End Sub
End Module
