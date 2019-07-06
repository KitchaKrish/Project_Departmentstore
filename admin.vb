Imports System.Data.SqlClient
Imports System.IO
Public Class admin
    Dim path As String = (Microsoft.VisualBasic.Left(Application.StartupPath, Len(Application.StartupPath) - 9))
    Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & Path & "Database1.mdf;Integrated Security=True;User Instance=True")
    Dim cmd As SqlCommand
    Dim aa As String
    Dim i, j As Integer
    Dim rd As SqlDataReader
    Dim bool As Char

    Private Sub admin_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Form3.Hide()
    End Sub

    Private Sub admin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.Open()
        ComboBox1.Items.Add("DME")
        ComboBox1.Items.Add("DEEE")
        ComboBox1.Items.Add("DECE")
        ComboBox1.Items.Add("DCE")
        For i = 1 To 3
            ComboBox2.Items.Add(i)
        Next
        For i = 1 To 6
            ComboBox3.Items.Add(i)
        Next
        'def
        cmd = New SqlCommand("select photo from Information where name='df'", con)
        Dim imageData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())
        If Not imageData Is Nothing Then
            Using ms As New MemoryStream(imageData, 0, imageData.Length)
                ms.Write(imageData, 0, imageData.Length)
                PictureBox1.BackgroundImage = Image.FromStream(ms, True)
            End Using
        End If
        'com box
        ComboBox1.Text = "Select"
        ComboBox2.Text = "Select"
        ComboBox3.Text = "Select"
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Form3.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PictureBox1.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If OpenFileDialog2.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PictureBox2.BackgroundImage = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ComboBox1.Text = "Select"
        ComboBox2.Text = "Select"
        ComboBox3.Text = "Select"
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Then
            MsgBox("Fill the Name Field")
        Else
            Dim cmd As New SqlCommand("INSERT INTO marksheet VALUES(@sem,@regno,@name,@dob,@s1,@s2,@s3,@s4,@s5,@s6,@s7,@photo)", con)
            cmd.Parameters.AddWithValue("@sem", ComboBox3.SelectedItem)
            cmd.Parameters.AddWithValue("@regno", TextBox2.Text)
            cmd.Parameters.AddWithValue("@name", TextBox1.Text)
            cmd.Parameters.AddWithValue("@dob", TextBox3.Text)
            cmd.Parameters.AddWithValue("@s1", TextBox4.Text)
            cmd.Parameters.AddWithValue("@s2", TextBox5.Text)
            cmd.Parameters.AddWithValue("@s3", TextBox6.Text)
            cmd.Parameters.AddWithValue("@s4", TextBox7.Text)
            cmd.Parameters.AddWithValue("@s5", TextBox8.Text)
            cmd.Parameters.AddWithValue("@s6", TextBox9.Text)
            cmd.Parameters.AddWithValue("@s7", TextBox10.Text)
            'photo 
            Dim ms As New MemoryStream()
            PictureBox1.BackgroundImage.Save(ms, PictureBox1.BackgroundImage.RawFormat)
            Dim data As Byte() = ms.GetBuffer()
            Dim p As New SqlParameter("@photo", SqlDbType.Image)
            p.Value = data
            cmd.Parameters.Add(p)
            cmd.ExecuteNonQuery()
            Dim cmd1 As New SqlCommand("INSERT INTO mssign VALUES(@regno,@signphoto)", con)
            cmd1.Parameters.AddWithValue("@regno", TextBox2.Text)
            'sign photo
            Dim ms1 As New MemoryStream()
            PictureBox2.BackgroundImage.Save(ms1, PictureBox2.BackgroundImage.RawFormat)
            Dim data1 As Byte() = ms1.GetBuffer()
            Dim p1 As New SqlParameter("@signphoto", SqlDbType.Image)
            p1.Value = data1
            cmd1.Parameters.Add(p1)
            cmd1.ExecuteNonQuery()
            MessageBox.Show("data has been saved in database", "Save", MessageBoxButtons.OK)
            con.Close()

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form3.Dispose()
        con.Close()
        End
    End Sub
End Class