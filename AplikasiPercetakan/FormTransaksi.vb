Imports System.Data.OleDb

Public Class FormTransaksi

    Dim YesNo As MsgBoxResult

    Private Property PowerPacks As Object

    Sub KondisiAwal()
        LBLNmPembeli.Text = ""
        LBLAlamat.Text = ""
        LBLTelp.Text = ""
        LBLTgl.Text = Today
        LBLAdmin.Text = FormMenuUtama.STLabel4.Text
        LBLKembali.Text = ""
        ComboBox1.Text = ""
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        CmbHarga.Text = ""
        txtQTY.Text = ""
        txt_Panjang.Text = ""
        txt_Lebar.Text = ""
        txtHasil.Text = ""
        ComboBox3.Text = ""

    End Sub
    Private Sub FormTransaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
        Call MunculKodePembeli()
        Call NomorOtomatis()
        Call BuatKolom()
        Label16.Text = "0"
        LBLKembali.Text = "0"
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LBLJam.Text = TimeOfDay
    End Sub
    Sub MunculKodePembeli()
        Call Koneksi()
        ComboBox1.Items.Clear()
        Cmd = New OleDbCommand("Select * from Pembeli", Conn)
        Rd = Cmd.ExecuteReader
        Do While Rd.Read
            ComboBox1.Items.Add(Rd.Item(0))
        Loop
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call Koneksi()
        Cmd = New OleDbCommand("Select * from Pembeli where Idpembeli='" & ComboBox1.Text & "'", Conn)
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Rd.HasRows Then
            LBLNmPembeli.Text = Rd!NamaPembeli
            LBLAlamat.Text = Rd!AlamatPembeli
            LBLTelp.Text = Rd!TeleponPembeli
        End If
    End Sub
    Sub NomorOtomatis()
        Call Koneksi()
        Cmd = New OleDbCommand("Select * from Transaksi where Idtransaksi in (Select max(Idtransaksi) from Transaksi)", Conn)
        Dim UrutId As String
        Dim Hitung As Long
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            UrutId = "JL" + Format(Now, "yyMMdd") + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(Rd.GetString(0), 9) + 1
            UrutId = "JL" = Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        LBLIdJual.Text = UrutId
    End Sub

    Sub BuatKolom()
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("Id", "Id Produk")
        DataGridView1.Columns.Add("Nama", "Nama Produk")
        DataGridView1.Columns.Add("Harga", "Harga")
        DataGridView1.Columns.Add("Quantity", "Quantity")
        DataGridView1.Columns.Add("Panjang", "Panjang")
        DataGridView1.Columns.Add("Lebar", "Lebar")
        DataGridView1.Columns.Add("Jumlah", "Jumlah Ukuran")
        DataGridView1.Columns.Add("Satuan", "Satuan")
        DataGridView1.Columns.Add("Subtotal", "Subtotal")
        DataGridView1.Columns.Add("Keterangan", "Keterangan")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Or txtQTY.Text = "" Or txt_Panjang.Text = "" Or txt_Lebar.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Tidak Boleh Kosong", MsgBoxStyle.Information)
        Else
            DataGridView1.Rows.Add(New String() {TextBox4.Text, TextBox5.Text, CmbHarga.Text, txtQTY.Text, txt_Panjang.Text, txt_Lebar.Text, txtHasil.Text, ComboBox3.Text, Val(CmbHarga.Text) * Val(txtQTY.Text) * Val(txtHasil.Text), TextBox1.Text})
            FormFaktur.DataGridViewFaktur.Rows.Add(New String() {TextBox4.Text, TextBox5.Text, CmbHarga.Text, txtQTY.Text, txt_Panjang.Text, txt_Lebar.Text, txtHasil.Text, ComboBox3.Text, Val(CmbHarga.Text) * Val(txtQTY.Text) * Val(txtHasil.Text)})
        End If

        Call Subtotal()
        KondisiAwal()

    End Sub
    Sub Subtotal()
        Dim Hitung As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Hitung = Hitung + DataGridView1.Rows(i).Cells(8).Value
            Label16.Text = Hitung
        Next
    End Sub

    Private Sub txt_Lebar_Click(sender As Object, e As EventArgs) Handles txt_Lebar.Click
        txtHasil.Text = FormatNumber(txt_Panjang.Text) * FormatNumber(txt_Lebar.Text)
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        LBLKembali.Text = Val(TextBox3.Text) - Val(Label16.Text)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        YesNo = MsgBox("Apakah Anda Ingin Menutup Transaksi Penjualan ", vbQuestion + vbYesNo + vbDefaultButton2, "Tutup")
        If YesNo = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        FormProduk.ShowDialog()
    End Sub
    Private Sub Print()
        FormFaktur.TextBoxTT.Text = LBLTgl.Text
        FormFaktur.TextBoxN.Text = LBLNmPembeli.Text
        FormFaktur.TextBoxT.Text = Label16.Text
        FormFaktur.TextBoxJU.Text = TextBox3.Text
        FormFaktur.TextBoxK.Text = LBLKembali.Text
        Dim YesNo As Integer
        YesNo = MsgBox("Simpan / Cetak Nota", vbQuestion + vbYesNo + vbDefaultButton2, "Informasi")
        If YesNo = vbYes Then
            FormFaktur.Show()
            FormFaktur.PrintFormFaktur.PrintAction = Printing.PrintAction.PrintToPrinter
            FormFaktur.PrintFormFaktur.Print(FormFaktur, Microsoft.VisualBasic.PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)
            FormFaktur.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Print()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        Call KondisiAwal()
        Label16.Text = "0"
    End Sub

End Class