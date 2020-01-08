Imports System.Data.OleDb

Public Class UbahPassword

    Private Sub UbahPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Sub KondisiAwal()
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox1.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.PasswordChar = ""
        TextBox2.PasswordChar = ""
        TextBox3.PasswordChar = ""
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            Cmd = New OleDbCommand("select * from Admin where Kodeadmin='" & FormMenuUtama.STLabel2.Text & "' and Passwordadmin ='" & TextBox1.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                TextBox2.Enabled = True
                TextBox3.Enabled = True
                TextBox1.Enabled = False
            Else
                MsgBox("Password Lama Salah", MsgBoxStyle.Exclamation)
                TextBox1.Text = ""
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Pesan As String
        If TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Password Baru Harus di Isi")
        Else
            If TextBox2.Text <> TextBox3.Text Then
                MsgBox("Password Baru dan Konfirmasi Harus Sama!", MsgBoxStyle.Information)
            Else
                Pesan = MsgBox("Yakin Password akan diganti?", vbYesNo, "Informasi")
                If Pesan = vbYes Then
                    Call Koneksi()
                    Dim UpdateData As String = "update Admin set Passwordadmin='" & TextBox3.Text & "' WHERE Kodeadmin= " & FormMenuUtama.STLabel2.Text & " "
                    Cmd = New OleDbCommand(UpdateData, Conn)
                    Cmd.ExecuteNonQuery()
                    MsgBox("Password Berhasil di Update")
                End If
                Call KondisiAwal()
            End If
        End If
    End Sub
End Class