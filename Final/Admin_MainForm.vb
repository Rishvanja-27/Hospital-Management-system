Public Class Admin_MainForm
    ' Property to store reference to the parent form
    Private Property par As Patients_MainForm

    ' Constructor to initialize the form with a reference to the parent form
    Public Sub New(ByVal parent As Patients_MainForm)
        InitializeComponent()
        par = parent
    End Sub

    ' Method to switch the displayed panel in the form
    Sub SwitchPanel(ByVal panel As Form)
        ' Clear existing controls in the panel
        Panel1.Controls.Clear()
        ' Set the panel as non-top level to embed it within the form
        panel.TopLevel = False
        ' Add the panel to the main panel control
        Panel1.Controls.Add(panel)
        ' Show the panel
        panel.Show()
    End Sub

    ' Event handler for the "Add Doctors" button click
    Private Sub AddDoctors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Switch to the "Admin_AddDoctor" panel/form
        SwitchPanel(Admin_AddDoctor)
    End Sub

    ' Event handler for the "Doctors List" button click
    Private Sub DoctorsList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' Switch to the "Admin_DoctorsList" panel/form
        SwitchPanel(Admin_DoctorsList)
    End Sub

    ' Event handler for the "Generate Bill" button click
    Private Sub GenerateBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ' Switch to the "Admin_GenerateBill" panel/form
        SwitchPanel(Admin_GenrateBill)
        ' Alternative: SwitchPanel(Admin_EditDoctor("Rashaad", "10:20"))
    End Sub

    ' Event handler for the "Logout" button click
    Private Sub Logout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        ' Hide the current form (Admin_MainForm)
        Me.Hide()
        ' Show the parent form (Patients_MainForm)
        par.Show()
    End Sub

    ' Event handler for the form's closed event
    Private Sub ChildForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Close the parent form (Patients_MainForm)
        par.Close()
    End Sub

    ' Event handler for the "Complaints" button click
    Private Sub Complaints_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        ' Switch to the "Admin_Complaints" panel/form
        SwitchPanel(Admin_Complaints)
    End Sub

    ' Event handler for the "Leave Requests" button click
    Private Sub LeaveRequests_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        ' Switch to the "Admin_LeaveRequests" panel/form
        SwitchPanel(Admin_LeaveRequests)
    End Sub

    ' Event handler for the "Security Log" button click
    Private Sub SecurityLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' Switch to the "Admin_Security" panel/form
        SwitchPanel(Admin_Security)
    End Sub

    ' Event handler for the "Cleanliness Log" button click
    Private Sub CleanlinessLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ' Switch to the "Admin_Cleanliness" panel/form
        SwitchPanel(Admin_Cleanliness)
    End Sub

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
