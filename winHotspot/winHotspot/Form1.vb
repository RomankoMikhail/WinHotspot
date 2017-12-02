Imports System.Text

Public Class Form1
    Private Sub CheckBoxShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowPassword.CheckedChanged
        If (CheckBoxShowPassword.Checked = True) Then
            TextBoxPassword.PasswordChar = ""
        Else
            TextBoxPassword.PasswordChar = "*"
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxName.Text = System.Net.Dns.GetHostName
    End Sub

    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        Dim StartHotspot As New ProcessStartInfo("whc.exe")
        StartHotspot.WindowStyle = ProcessWindowStyle.Hidden
        StartHotspot.Arguments = "/start """ + TextBoxName.Text + """ """ + TextBoxPassword.Text + """ " + NumbericUpDownUsers.Value.ToString() + " /noverbose"
        StartHotspot.RedirectStandardOutput = True
        StartHotspot.UseShellExecute = False
        StartHotspot.CreateNoWindow = True
        Dim prc = Process.Start(StartHotspot)
        prc.WaitForExit()

        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = prc.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
        End Using

        If sOutput.Length > 0 Then
            MessageBox.Show(sOutput)
        End If

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ButtonRandom_Click(sender As Object, e As EventArgs) Handles ButtonRandom.Click
        TextBoxPassword.Text = ""
        CheckBoxShowPassword.Checked = True
        TextBoxPassword.PasswordChar = ""
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_"
        Dim r As New Random
        Dim len As Integer = r.Next(8, 12)
        Dim sb As New StringBuilder
        For i As Integer = 1 To len
            Dim idx As Integer = r.Next(0, s.Length - 1)
            sb.Append(s.Substring(idx, 1))
        Next
        TextBoxPassword.Text = sb.ToString()
    End Sub

    Private Sub ButtonStop_Click(sender As Object, e As EventArgs) Handles ButtonStop.Click
        Dim StartHotspot As New ProcessStartInfo("whc.exe")
        StartHotspot.WindowStyle = ProcessWindowStyle.Hidden
        StartHotspot.Arguments = "/stop /noverbose"
        StartHotspot.RedirectStandardOutput = True
        StartHotspot.UseShellExecute = False
        StartHotspot.CreateNoWindow = True
        Dim prc = Process.Start(StartHotspot)
        prc.WaitForExit()

        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = prc.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
        End Using

        If sOutput.Length > 0 Then
            MessageBox.Show(sOutput)
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim StartHotspot As New ProcessStartInfo("whc.exe")
        StartHotspot.WindowStyle = ProcessWindowStyle.Hidden
        StartHotspot.Arguments = "/info /noverbose"
        StartHotspot.RedirectStandardOutput = True
        StartHotspot.UseShellExecute = False
        StartHotspot.CreateNoWindow = True
        Dim prc = Process.Start(StartHotspot)
        prc.WaitForExit()

        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = prc.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
        End Using

        If sOutput.Length > 0 Then
            Dim symbols As String() = sOutput.Split(New Char() {";"c})
            If Integer.Parse(symbols(1)) = 1 Then
                Lamp.Value = 1
            Else
                Lamp.Value = 0
            End If
            ' MessageBox.Show(sOutput)
        End If
    End Sub

    Private Sub ButtonInfo_Click(sender As Object, e As EventArgs) Handles ButtonInfo.Click
        Dim StartHotspot As New ProcessStartInfo("whc.exe")
        StartHotspot.WindowStyle = ProcessWindowStyle.Hidden
        StartHotspot.Arguments = "/info"
        StartHotspot.RedirectStandardOutput = True
        StartHotspot.UseShellExecute = False
        StartHotspot.CreateNoWindow = True
        Dim prc = Process.Start(StartHotspot)
        prc.WaitForExit()

        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = prc.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
        End Using

        If sOutput.Length > 0 Then
            MessageBox.Show(sOutput)
        End If
    End Sub
End Class
