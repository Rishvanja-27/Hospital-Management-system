Public Class Patients_MainPage

    Private Property par As Patients_MainForm
    Public patientId As Integer
    Public Sub New(ByVal parent As Patients_MainForm, ByVal pID As Integer)
        InitializeComponent()
        par = parent
        patientId = pID
        MessageBox.Show(patientId)
    End Sub

    Sub SwitchPanel(ByVal panel As Form)
        Panel1.Controls.Clear()
        panel.TopLevel = False
        Panel1.Controls.Add(panel)
        panel.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Show the other form (Form1)
        Dim Patients_BookAppointment As New Patients_BookAppointment(patientId)
        SwitchPanel(Patients_BookAppointment)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Show the other form (Form1)
        Dim Patients_AppointmentHistory As New Patients_AppointmentHistory(patientId)
        SwitchPanel(Patients_AppointmentHistory)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' Show the other form (Form1)
        Dim Patients_Complaints As New Patients_Complaints()
        SwitchPanel(Patients_Complaints)
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ' Show the other form (Form1)
        Me.Hide()
        par.Show()
    End Sub
    Private Sub ChildForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Close the parent form
        par.Close()
    End Sub


    Private Sub Patients_MainPage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class