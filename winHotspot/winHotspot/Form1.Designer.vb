<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ButtonStart = New System.Windows.Forms.Button()
        Me.ButtonStop = New System.Windows.Forms.Button()
        Me.CheckBoxShowPassword = New System.Windows.Forms.CheckBox()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LinkLabelAuthor = New System.Windows.Forms.LinkLabel()
        Me.ButtonRandom = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumbericUpDownUsers = New System.Windows.Forms.NumericUpDown()
        Me.ButtonInfo = New System.Windows.Forms.Button()
        Me.Lamp = New System.Windows.Forms.ProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.NumbericUpDownUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonStart
        '
        Me.ButtonStart.Location = New System.Drawing.Point(12, 101)
        Me.ButtonStart.Name = "ButtonStart"
        Me.ButtonStart.Size = New System.Drawing.Size(260, 32)
        Me.ButtonStart.TabIndex = 0
        Me.ButtonStart.Text = "Start hotspot"
        Me.ButtonStart.UseVisualStyleBackColor = True
        '
        'ButtonStop
        '
        Me.ButtonStop.Location = New System.Drawing.Point(12, 139)
        Me.ButtonStop.Name = "ButtonStop"
        Me.ButtonStop.Size = New System.Drawing.Size(260, 32)
        Me.ButtonStop.TabIndex = 1
        Me.ButtonStop.Text = "Stop hotspot"
        Me.ButtonStop.UseVisualStyleBackColor = True
        '
        'CheckBoxShowPassword
        '
        Me.CheckBoxShowPassword.AutoSize = True
        Me.CheckBoxShowPassword.Location = New System.Drawing.Point(165, 76)
        Me.CheckBoxShowPassword.Name = "CheckBoxShowPassword"
        Me.CheckBoxShowPassword.Size = New System.Drawing.Size(107, 17)
        Me.CheckBoxShowPassword.TabIndex = 2
        Me.CheckBoxShowPassword.Text = "Show password?"
        Me.CheckBoxShowPassword.UseVisualStyleBackColor = True
        '
        'TextBoxName
        '
        Me.TextBoxName.Location = New System.Drawing.Point(113, 16)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(159, 20)
        Me.TextBoxName.TabIndex = 3
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Location = New System.Drawing.Point(113, 42)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPassword.Size = New System.Drawing.Size(159, 20)
        Me.TextBoxPassword.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Hotspot name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Hotspot password:"
        '
        'LinkLabelAuthor
        '
        Me.LinkLabelAuthor.AutoSize = True
        Me.LinkLabelAuthor.Location = New System.Drawing.Point(195, 209)
        Me.LinkLabelAuthor.Name = "LinkLabelAuthor"
        Me.LinkLabelAuthor.Size = New System.Drawing.Size(77, 13)
        Me.LinkLabelAuthor.TabIndex = 7
        Me.LinkLabelAuthor.TabStop = True
        Me.LinkLabelAuthor.Text = "Author website"
        '
        'ButtonRandom
        '
        Me.ButtonRandom.Location = New System.Drawing.Point(12, 72)
        Me.ButtonRandom.Name = "ButtonRandom"
        Me.ButtonRandom.Size = New System.Drawing.Size(147, 23)
        Me.ButtonRandom.TabIndex = 8
        Me.ButtonRandom.Text = "Random Password"
        Me.ButtonRandom.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(47, 182)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Max Users:"
        '
        'NumbericUpDownUsers
        '
        Me.NumbericUpDownUsers.Location = New System.Drawing.Point(113, 180)
        Me.NumbericUpDownUsers.Maximum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.NumbericUpDownUsers.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumbericUpDownUsers.Name = "NumbericUpDownUsers"
        Me.NumbericUpDownUsers.Size = New System.Drawing.Size(62, 20)
        Me.NumbericUpDownUsers.TabIndex = 10
        Me.NumbericUpDownUsers.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'ButtonInfo
        '
        Me.ButtonInfo.Location = New System.Drawing.Point(12, 177)
        Me.ButtonInfo.Name = "ButtonInfo"
        Me.ButtonInfo.Size = New System.Drawing.Size(29, 23)
        Me.ButtonInfo.TabIndex = 11
        Me.ButtonInfo.Text = "?"
        Me.ButtonInfo.UseVisualStyleBackColor = True
        '
        'Lamp
        '
        Me.Lamp.Location = New System.Drawing.Point(247, 180)
        Me.Lamp.Maximum = 1
        Me.Lamp.Name = "Lamp"
        Me.Lamp.Size = New System.Drawing.Size(25, 23)
        Me.Lamp.TabIndex = 12
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 231)
        Me.Controls.Add(Me.Lamp)
        Me.Controls.Add(Me.ButtonInfo)
        Me.Controls.Add(Me.NumbericUpDownUsers)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ButtonRandom)
        Me.Controls.Add(Me.LinkLabelAuthor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxPassword)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.CheckBoxShowPassword)
        Me.Controls.Add(Me.ButtonStop)
        Me.Controls.Add(Me.ButtonStart)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "WinHotSpot"
        CType(Me.NumbericUpDownUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonStart As Button
    Friend WithEvents ButtonStop As Button
    Friend WithEvents CheckBoxShowPassword As CheckBox
    Friend WithEvents TextBoxName As TextBox
    Friend WithEvents TextBoxPassword As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LinkLabelAuthor As LinkLabel
    Friend WithEvents ButtonRandom As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents NumbericUpDownUsers As NumericUpDown
    Friend WithEvents ButtonInfo As Button
    Friend WithEvents Lamp As ProgressBar
    Friend WithEvents Timer1 As Timer
End Class
