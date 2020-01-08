Imports System.Data.OleDb

Public Class FormMasterProduct
    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.Text = ""
        TextBox4.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        ComboBox1.Enabled = False
        TextBox4.Enabled = False
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"

        Call Koneksi()
        Da = New OleDbDataAdapter("SELECT * FROM Produk", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "Produk")
        DataGridView1.DataSource = Ds.Tables("Produk")
        DataGridView1.ReadOnly = True

    End Sub
    Sub SiapIsi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        ComboBox1.Enabled = True
        TextBox4.Enabled = True
    End Sub
    Private Sub FormMasterProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
            Call NomorOtomatis()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan Isi Semua Field")
            Else
                Dim InputData As String = "INSERT INTO Produk VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & ComboBox1.Text & "','" & TextBox4.Text & "')"
                Cmd = New OleDbCommand(InputData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Simpan"
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan Isi Field Id Produk, Lalu Tekan ENTER!")
            Else
                Call Koneksi()
                Dim UpdateData As String = "UPDATE INTO Produk set Namaproduk='" & TextBox2.Text & "',Hargaproduk='" & TextBox3.Text & "',Satuanproduk='" & ComboBox1.Text & "', HargaMember='" & TextBox4.Text & "' where Idproduk='" & TextBox1.Text & "'"
                Cmd = New OleDbCommand(UpdateData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Edit Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            Cmd = New OleDbCommand("SELECT * FROM Produk where IdProduk='" & TextBox1.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Id Produk Tidak Ada")
            Else
                TextBox1.Text = Rd.Item("IdProduk")
                TextBox2.Text = Rd.Item("NamaProduk")
                TextBox3.Text = Rd.Item("HargaProduk")
                ComboBox1.Text = Rd.Item("SatuanProduk")
                TextBox4.Text = Rd.Item("HargaMember")
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "Tutup" Then
            Me.Close()
        Else
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Hapus" Then
            Button3.Text = "Delete"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan Isi Field Id Produk, Lalu Tekan ENTER!")
            Else
                Call Koneksi()
                Dim HapusData As String = "DELETE FROM Produk where Idproduk='" & TextBox1.Text & "'"
                Cmd = New OleDbCommand(HapusData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Sub NomorOtomatis()
        Call Koneksi()
        Cmd = New OleDbCommand("Select * from Produk where Idproduk in (Select max(Idproduk) from Produk)", Conn)
        Dim UrutId As String
        Dim Hitung As Long
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            UrutId = "PRD" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(Rd.GetString(0), 3) + 1
            UrutId = "PRD" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutId
    End Sub
End Class