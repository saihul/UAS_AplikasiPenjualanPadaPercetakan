Public Class FormFaktur

    Private Sub FormStruk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Focus()
        Dim waktu As Date = Now
        TextBoxTT.Text = waktu.ToString("dd/MMMM/yyyy")
    End Sub

    Private Sub TextBoxN_TextChanged(sender As Object, e As EventArgs) Handles TextBoxN.TextChanged
        TextBoxN.Text = FormTransaksi.LBLNmPembeli.Text
    End Sub

    Private Sub TextBoxJU_TextChanged(sender As Object, e As EventArgs) Handles TextBoxJU.TextChanged
        TextBoxJU.Text = FormTransaksi.TextBox3.Text
    End Sub

    Private Sub TextBoxK_TextChanged(sender As Object, e As EventArgs) Handles TextBoxK.TextChanged
        TextBoxK.Text = FormTransaksi.LBLKembali.Text
    End Sub

    Private Sub DataGridViewFaktur_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewFaktur.CellContentClick
        FormTransaksi.DataGridView1.Rows.Add(New String() {FormTransaksi.LBLNmPembeli.Text, FormTransaksi.TextBox4.Text, FormTransaksi.TextBox1.Text, FormTransaksi.CmbHarga.Text, FormTransaksi.txtQTY.Text, FormTransaksi.txt_Panjang.Text, FormTransaksi.txt_Lebar.Text, FormTransaksi.txtHasil.Text, FormTransaksi.ComboBox3.Text, Val(FormTransaksi.CmbHarga.Text) * Val(FormTransaksi.txtQTY.Text) * Val(FormTransaksi.txtHasil.Text)})
    End Sub

    Private Sub TextBoxTT_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTT.TextChanged
        TextBoxTT.Text = FormTransaksi.LBLTgl.Text
    End Sub

    Private Sub TextBoxT_TextChanged(sender As Object, e As EventArgs) Handles TextBoxT.TextChanged
        TextBoxT.Text = FormTransaksi.Label16.Text
    End Sub
End Class
