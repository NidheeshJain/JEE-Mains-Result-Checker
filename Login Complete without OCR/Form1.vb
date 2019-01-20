Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WebBrowser1.Navigate("https://jeemain.nic.in/JeeMainApp/Root/AuthCandWithDob.aspx")
        DateTimePicker1.CustomFormat = "dd-MM-yyyy"

    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim a As Integer
        a = Asc(e.KeyChar)
        If Not a = 8 Then
            If Not (a >= 48 And a <= 57) Or (TextBox1.Text.Length > 11) Then
                e.KeyChar = ""
            End If
        End If



    End Sub


    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        If WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then

            For Each Captcha As HtmlElement In WebBrowser1.Document.Images

                If Captcha.GetAttribute("src").Contains("MyHandler/DisplayCaptchaImg.ashx") Then
                    WebBrowser2.Navigate((Captcha.GetAttribute("src")))
                End If


            Next
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text.Length <> 12 Or TextBox2.Text.Length <> 6 Then
            MsgBox("Please enter details correctly")
        Else
            WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_txtRegNo").SetAttribute("value", TextBox1.Text)
            WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_ddlday").SetAttribute("value", DateTimePicker1.Value.Day.ToString("D2"))
            WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_ddlmonth").SetAttribute("value", DateTimePicker1.Value.Month.ToString("D2"))
            WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_ddlyear").SetAttribute("value", DateTimePicker1.Value.Year)
            WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_Secpin").SetAttribute("value", TextBox2.Text)
            WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_Submit1").InvokeMember("click")
        End If
    End Sub
    Dim load_stated As Boolean
    Private Sub WebBrowser1_ProgressChanged(sender As Object, e As WebBrowserProgressChangedEventArgs) _
Handles WebBrowser1.ProgressChanged
        'CODE FOR ACTION UPON PAGE COMPLETE LOAD
        'CODE START
        If e.CurrentProgress = e.MaximumProgress Then
            'The maximun progres is reached
            load_stated = True
        End If
        'The page is confirmed downloaded after the pregres return to 0
        If e.CurrentProgress = 0 Then
            If load_stated Then
                'the page is ready to print or download...
                load_stated = False
                If WebBrowser1.Document.Url.AbsoluteUri = "https://jeemain.nic.in/JeeMainApp/Online/CandidateHome.aspx" Then
                    WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_a4").InvokeMember("click")
                ElseIf WebBrowser1.Document.Url.AbsoluteUri = "https://jeemain.nic.in/JeeMainApp/Result/ViewResult.aspx" Then
                    MsgBox("Dear " + WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_lblName").InnerText + Environment.NewLine + "Your Net Percentile is : " + WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_lbltotalPaper1").InnerText + Environment.NewLine + "Physics : " + WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_lblPaper1Physics").InnerText + Environment.NewLine + "Maths : " + WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_lblPaper1Math").InnerText + Environment.NewLine + "Chemistry : " + WebBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_lblPaper1Chemistry").InnerText)
                    WebBrowser1.Document.GetElementById("ctl00_lnkExit").InvokeMember("click")
                ElseIf WebBrowser1.Document.Url.AbsoluteUri = "https://jeemain.nic.in/JeeMainApp/root/Logout.htm" Then
                    WebBrowser1.Navigate("https://jeemain.nic.in/JeeMainApp/Root/AuthCandWithDob.aspx")
                End If
            End If
        End If
        'CODE END
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim a As Integer
        a = Asc(e.KeyChar)
        If Not a = 8 Then
            If Not ((a >= 48 And a <= 57) Or (a >= 97 And a <= 122) Or (a >= 65 And a <= 90)) Or (TextBox2.Text.Length > 6) Then
                e.KeyChar = ""
            End If
        End If
    End Sub
End Class
