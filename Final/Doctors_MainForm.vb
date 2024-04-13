Public Class Doctors_MainForm
    Private originalColor As Color = Color.FromArgb(217, 217, 217)
    Private selectedColor As Color = Color.Gray

    ' Your existing code...
    Private Property par As Patients_MainForm
    Private doctorID As Integer

    ' Constructor modified to accept doctor's ID as parameter
    Public Sub New(ByVal parent As Patients_MainForm, ByVal dID As Integer)
        InitializeComponent()
        par = parent
        Me.doctorID = dID ' Store the doctor's ID
    End Sub

    ' Function to switch panels
    Sub SwitchPanel(ByVal panel As Form)
        Panel1.Controls.Clear()
        panel.TopLevel = False
        Panel1.Controls.Add(panel)
        panel.Show()
    End Sub

    ' Event handler for the Appointments label
    Private Sub Appointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Appointment.Click
        ' Pass the doctor's ID to the Doctors_Appointment form
        Dim appointmentForm As New Doctors_Appointment(par, doctorID)
        SwitchPanel(appointmentForm)

        ' Change the background color of the clicked label to indicate selection
        ResetButtonColors()
        Appointment.BackColor = selectedColor
    End Sub

    ' Event handler for the Medicines label
    Private Sub Medicines_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Medicines.Click
        Dim medicineForm As New Doctors_Medicine(par, doctorID)
        SwitchPanel(medicineForm)

        ' Change the background color of the clicked label to indicate selection
        ResetButtonColors()
        Medicines.BackColor = selectedColor
    End Sub

    ' Event handler for the Leave label
    Private Sub Leave_Form_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Leave_Form.Click
        ' Pass the doctor's ID to the Doctors_LeaveForm form
        Dim leaveForm As New Doctors_LeaveForm(par, doctorID)
        SwitchPanel(leaveForm)

        ' Change the background color of the clicked label to indicate selection
        ResetButtonColors()
        Leave_Form.BackColor = selectedColor
    End Sub

    ' Event handler for the Complaints label
    Private Sub Complaints_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Complaints.Click
        SwitchPanel(Doctors_Complaints)

        ' Change the background color of the clicked label to indicate selection
        ResetButtonColors()
        Complaints.BackColor = selectedColor
    End Sub

    ' Event handler for the Logout label
    Private Sub Logout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Logout.Click
        ' Show the other form (Form1)
        Me.Hide()
        par.Show()
    End Sub

    ' Event handler for the Form Closed event
    Private Sub ChildForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Close the parent form
        par.Close()
    End Sub

    ' Function to reset button background colors
    Private Sub ResetButtonColors()
        ' Reset the background color of all buttons to original color
        Appointment.BackColor = originalColor
        Medicines.BackColor = originalColor
        Leave_Form.BackColor = originalColor
        Complaints.BackColor = originalColor
    End Sub

    ' Load event handler for the Doctors_MainForm
    Private Sub Doctors_MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set initial button background colors
        ResetButtonColors()
    End Sub
End Class
