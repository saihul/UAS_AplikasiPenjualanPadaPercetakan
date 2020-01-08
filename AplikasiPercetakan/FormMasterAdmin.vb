Imports System.Data.OleDb

Class FormMasterAdmin
    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.Items.Clear()
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        ComboBox1.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"

        Call Koneksi()
        Da = New OleDbDataAdapter("SELECT * FROM Admin", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "Admin")
        DataGridView1.DataSource = Ds.Tables("Admin")
        DataGridView1.Enabled = True

    End Sub
    Sub SiapIsi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        ComboBox1.Enabled = True
        ComboBox1.Items.Add("ADMIN")
        ComboBox1.Items.Add("USER")
    End Sub
    Private Sub FormMasterAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub
    Sub jalankanSql(ByVal sql As String)
        cmd = New OleDbCommand
        Koneksi()
        Try
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            MsgBox("Data Sudah Disimpan", vbInformation)
        Catch ex As Exception
            MessageBox.Show("Tidak bisa menyimpan data ke Database " & ex.Message)
        End Try
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
                Call Koneksi()
                Dim InputData As String = "INSERT INTO [Admin] ([Kodeadmin], [Namaadmin], [Passwordadmin], [Leveladmin]) VALUES('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & ComboBox1.Text & "')"
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
                MsgBox("Silahkan Isi Field Kodeadmin, Lalu Tekan ENTER!")
            Else
                Call Koneksi()
                Dim UpdateData As String = "UPDATE [Admin] SET [Namaadmin]='" & TextBox2.Text & "', [Passwordadmin]='" & TextBox3.Text & "', [Leveladmin]='" & ComboBox1.Text & "' WHERE Kodeadmin= " & TextBox1.Text & " "
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
            Cmd = New OleDbCommand("SELECT * FROM Admin where Kodeadmin='" & TextBox1.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Kode Admin Tidak Ada")
            Else
                TextBox1.Text = Rd.Item("Kodeadmin")
                TextBox2.Text = Rd.Item("Namaadmin")
                TextBox3.Text = Rd.Item("Passwordadmin")
                ComboBox1.Text = Rd.Item("Leveladmin")
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
                MsgBox("Silahkan Isi Field Kodeadmin, Lalu Tekan ENTER!")
            Else
                Call Koneksi()
                Dim HapusData As String = "DELETE FROM Admin where Kodeadmin='" & TextBox1.Text & "'"
                Cmd = New OleDbCommand(HapusData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil")
                KondisiAwal()
            End If
        End If
    End Sub
   
    Sub NomorOtomatis()
        Call Koneksi()
        Cmd = New OleDbCommand("Select * from Admin where Kodeadmin in (Select max(Kodeadmin) from Admin)", Conn)
        Dim UrutId As String
        Dim Hitung As Long
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            UrutId = "ADM" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(Rd.GetString(0), 3) + 1
            UrutId = "ADM" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutId
    End Sub
End Class