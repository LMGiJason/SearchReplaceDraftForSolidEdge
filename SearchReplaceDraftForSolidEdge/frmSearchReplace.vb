Imports System.ComponentModel

Public Class frmSearchReplace
    Private mProcessor As New SearchReplaceProcessor
    Private Sub BGW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGW.DoWork
        mProcessor.DoFindNext(sender, e)
    End Sub

    Private Sub BGW_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BGW.ProgressChanged

    End Sub

    Private Sub BGW_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BGW.RunWorkerCompleted
        EnableDisableControls(True)
    End Sub

    Private Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        chkBlocks.Checked = chkAll.Checked
        chkCallouts.Checked = chkAll.Checked
        chkDimensions.Checked = chkAll.Checked
        chkTextBoxes.Checked = chkAll.Checked
    End Sub

    Private Sub cboScope_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboScope.SelectedIndexChanged
        btnFind.Enabled = (cboScope.Text = "ActiveSheet" Or cboScope.Text = "WorkingSheets") And txtFind.TextLength > 0
    End Sub

    Private Sub frmSearchReplace_Load(sender As Object, e As EventArgs) Handles Me.Load
        cboScope.SelectedIndex = cboScope.FindStringExact(My.Settings.scopeString)
        If mProcessor.InitEdge Then
            EnableDisableControls(True)
            btnFind.Enabled = True
        End If
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        SetInputs()
        If BGW.IsBusy Then
            BGW.CancelAsync()
            mProcessor.DoFindNext()
            Return
        End If
        EnableDisableControls(False)
        BGW.RunWorkerAsync(txtFind.Text)
    End Sub

    Private Sub txtFind_TextChanged(sender As Object, e As EventArgs) Handles txtFind.TextChanged
        btnReplace.Enabled = txtFind.TextLength > 0
        btnFind.Enabled = (cboScope.Text = "ActiveSheet" Or cboScope.Text = "WorkingSheets") And txtFind.TextLength > 0
    End Sub

    Private Sub EnableDisableControls(enable As Boolean)
        chkAll.Enabled = enable
        chkBlocks.Enabled = enable
        chkCallouts.Enabled = enable
        chkDimensions.Enabled = enable
        chkTextBoxes.Enabled = enable
        cboScope.Enabled = enable
    End Sub

    Private Sub SetInputs()
        mProcessor.FindString = txtFind.Text
        mProcessor.ReplaceString = txtReplace.Text
    End Sub
End Class
