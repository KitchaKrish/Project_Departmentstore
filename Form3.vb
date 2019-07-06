Imports System.Data.SqlClient
Imports System.IO
Public Class Form3
    Dim path As String = (Microsoft.VisualBasic.Left(Application.StartupPath, Len(Application.StartupPath) - 9))
    Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & Path & "Database1.mdf;Integrated Security=True;User Instance=True")
    Dim cmd As SqlCommand
    Dim rd As SqlDataReader
    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
      Public Shared Function BitBlt(ByVal hdcDest As IntPtr, ByVal nXDest As Integer, ByVal nYDest As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hdcSrc As IntPtr, _
     ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As Integer) As Long
    End Function
    'get the screenshot
    Private memoryImage As Bitmap
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Database1DataSet.Information' table. You can move, or remove it, as needed.

        con.Open()
        'GroupBox1.Visible = False
        'GroupBox2.Visible = True
    End Sub
    Private Sub CaptureScreen()
        Dim mygraphics As Graphics = Me.Panel1.CreateGraphics()
        Dim s As Size = Me.Panel1.Size
        memoryImage = New Bitmap(s.Width, s.Height, mygraphics)
        Dim memoryGraphics As Graphics = Graphics.FromImage(memoryImage)
        Dim dc1 As IntPtr = mygraphics.GetHdc()
        Dim dc2 As IntPtr = memoryGraphics.GetHdc()
        BitBlt(dc2, 0, 0, Me.Panel1.ClientRectangle.Width, Me.Panel1.ClientRectangle.Height, dc1, _
         0, 0, 13369376)
        mygraphics.ReleaseHdc(dc1)
        memoryGraphics.ReleaseHdc(dc2)
    End Sub
    Private Sub printDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawImage(memoryImage, 0, 0)
    End Sub
    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text = "admin" And TextBox2.Text = "007" And TextBox3.Text = "admin" Then
            admin.Show()
            Exit Sub
        End If
        GroupBox1.BringToFront()
        GroupBox2.Visible = False
        GroupBox1.Visible = True
        cmd = New SqlCommand("select name,regno,dob from marksheet where regno='" & TextBox2.Text & "'", con)
        rd = cmd.ExecuteReader
        rd.Read()
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("fill data")
            GroupBox2.BringToFront()
            GroupBox1.Visible = False
            GroupBox2.Visible = True
            Exit Sub
        End If
        If TextBox1.Text = rd.GetString(0) And TextBox2.Text = rd.GetString(1) And TextBox3.Text = rd.GetString(2) Then
            rd.Close()
            cmd = New SqlCommand("select sem,regno,name,dob,s1,s2,s3,s4,s5,s6,s7 from marksheet where regno='" & TextBox2.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            Label19.Text = rd.GetString(0) & " sem"
            Label20.Text = rd.GetString(1)
            Label21.Text = rd.GetString(2)
            Label22.Text = rd.GetString(3)
            Label23.Text = Label19.Text
            Label24.Text = rd.GetString(4)
            Label25.Text = rd.GetString(5)
            Label26.Text = rd.GetString(6)
            Label27.Text = rd.GetString(7)
            Label28.Text = rd.GetString(8)
            Label29.Text = rd.GetString(9)
            Label30.Text = rd.GetString(10)
            Label31.Text = Integer.Parse(Label24.Text) + Integer.Parse(Label25.Text) + Integer.Parse(Label26.Text) + Integer.Parse(Label27.Text) + Integer.Parse(Label28.Text) + Integer.Parse(Label29.Text) + Integer.Parse(Label30.Text)
            Label32.Text = Integer.Parse(Label31.Text) / 7 & " %"
            rd.Close()
            'photo to the form
            cmd = New SqlCommand("select photo from marksheet where regno='" & TextBox2.Text & "'", con)
            Dim imageData1 As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())
            If Not imageData1 Is Nothing Then
                Using ms1 As New MemoryStream(imageData1, 0, imageData1.Length)
                    ms1.Write(imageData1, 0, imageData1.Length)
                    PictureBox2.BackgroundImage = Image.FromStream(ms1, True)
                End Using
            End If
            'signphoto
            Dim cmd1 = New SqlCommand("select signphoto from mssign where regno='" & TextBox2.Text & "'", con)
            Dim imageData2 As Byte() = DirectCast(cmd1.ExecuteScalar(), Byte())
            If Not imageData2 Is Nothing Then
                Using ms2 As New MemoryStream(imageData2, 0, imageData2.Length)
                    ms2.Write(imageData2, 0, imageData2.Length)
                    PictureBox6.BackgroundImage = Image.FromStream(ms2, True)
                End Using
            End If
            'end photo to form
        Else
            MsgBox("no data is found")
        End If
        rd.Close()
    End Sub

   
    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        con.Close()
        End
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        CaptureScreen()
        PrintDocument1.Print()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GroupBox2.BringToFront()
        GroupBox1.Visible = False
        GroupBox2.Visible = True
    End Sub
End Class
