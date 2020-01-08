Imports System.Data.OleDb

Public Class FormProduk

    Private Sub FormProduk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Koneksi()
        Da = New OleDbDataAdapter("SELECT * FROM Produk", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "Produk")
        DataGridView1.DataSource = Ds.Tables("Produk")
    End Sub

    Private Sub NmProduk_TextChanged(sender As Object, e As EventArgs) Handles NmProduk.TextChanged
        Call Koneksi()
        Cmd = New OleDbCommand("SELECT * FROM Produk where IdProduk='" & NmProduk.Text & "'", Conn)
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Rd.HasRows Then
            Call Koneksi()
            Da = New OleDbDataAdapter("SELECT * FROM Produk where IdProduk='" & NmProduk.Text & "'", Conn)
            Ds = New DataSet
            Da.Fill(Ds, "Ketemu")
            DataGridView1.DataSource = Ds.Tables("Ketemu")
            DataGridView1.ReadOnly = True
        End If
    End Sub

    Private Sub Pilih()
        Dim i As Integer
        i = Me.DataGridView1.CurrentRow.Index
        Try
            With DataGridView1.Rows.Item(i)
                FormTransaksi.TextBox4.Text = .Cells(0).Value
                FormTransaksi.TextBox5.Text = .Cells(1).Value
                FormTransaksi.CmbHarga.Text = .Cells(4).Value
                FormTransaksi.ComboBox3.Text = .Cells(3).Value
                FormTransaksi.Show()
            End With
            Me.Close()
        Catch ex As Exception
            MsgBox("Pilih Salah Satu Field", MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Call Pilih()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Pilih()
    End Sub
End Class