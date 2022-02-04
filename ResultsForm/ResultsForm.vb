Imports System.Threading

Public Class ResultsForm
    'Publicly accessible items
    Public Language As String = "en"
    Public FormMessage As String = "Output Log:"
    Public DefaultLogFilename As String = "Log"
    Public DefaultSaveFileDialogTitle As String = "Browse for a location to save the log file."
    Public ResultItems As List(Of String)
    Private Shared _DefaultLogFilename As String
    Private Shared _DefaultSaveFileDialogTitle As String
    Private Shared ReadOnly _ResultItems As New List(Of String)
    Private Shared _LogExtension As String = "Log File|*.log"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub SaveLogBtn_Click(sender As Object, e As EventArgs) Handles SaveLogBtn.Click
        _DefaultSaveFileDialogTitle = DefaultSaveFileDialogTitle
        _DefaultLogFilename = DefaultLogFilename
        _ResultItems.AddRange(ResultItems)
        Dim newThread As New Thread(New ThreadStart(AddressOf SaveDialogMethod))
        newThread.SetApartmentState(ApartmentState.STA)
        newThread.Start()
    End Sub

    Private Sub ResultsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.DataSource = ResultItems
        Text = "Results"
        If Language.ToLower() = "es" Then
            Text = "Resultados"
            If FormMessage = "Output Log:" Then FormMessage = "Log de resultados:"
            DefaultSaveFileDialogTitle = "Busque un lugar para guardar el archivo de los resultados."
            SaveLogBtn.Text = "Guardar log de resultados."
            _LogExtension = "Archivo de log|*.log"
        End If
        FormLabelMessage.Text = FormMessage
    End Sub


    Private Shared Sub SaveDialogMethod()
        Dim saveFileDialog As New SaveFileDialog With {
            .Title = _DefaultSaveFileDialogTitle,
            .FileName = _DefaultLogFilename + ".log",
            .Filter = _LogExtension}
        Dim okresult As DialogResult = saveFileDialog.ShowDialog
        If okresult = DialogResult.OK Then
            IO.File.WriteAllLines(saveFileDialog.FileName, _ResultItems)
        End If
    End Sub
End Class