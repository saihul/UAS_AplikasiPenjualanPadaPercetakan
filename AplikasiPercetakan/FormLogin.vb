Imports System.Data.OleDb

Public Class FormLogin

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub
    Sub Terbuka()
        FormMenuUtama.LoginToolStripMenuItem.Enabled = False
        FormMenuUtama.LogoutToolStripMenuItem.Enabled = True
        FormMenuUtama.MasterToolStripMenuItem.Enabled = True
        FormMenuUtama.TransaksiToolStripMenuItem.Enabled = True
        FormMenuUtama.LaporanToolStripMenuItem.Enabled = True
        FormMenuUtama.UtilityToolStripMenuItem.Enabled = True
        FormMenuUtama.AdminToolStripMenuItem.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Username atau Password Tidak Boleh Kosong OK!", MsgBoxStyle.Question)
        Else
            Call Koneksi()
            Cmd = New OleDbCommand("SELECT * FROM Admin where Namaadmin = '" & TextBox1.Text & "' AND Passwordadmin = '" & TextBox2.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows() Then
                Me.Close()
                Call Terbuka()
                FormMenuUtama.STLabel2.Text = Rd!KodeAdmin
                FormMenuUtama.STLabel4.Text = Rd!NamaAdmin
                FormMenuUtama.STLabel6.Text = Rd!LevelAdmin
                If FormMenuUtama.STLabel6.Text = "USER" Then
                    FormMenuUtama.AdminToolStripMenuItem.Enabled = False
                End If
            Else
                MsgBox("Username atau Password Anda Salah", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class