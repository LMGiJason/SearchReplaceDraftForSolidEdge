<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchReplace
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnReplace = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.BGW = New System.ComponentModel.BackgroundWorker()
        Me.chkMatchCase = New System.Windows.Forms.CheckBox()
        Me.txtReplace = New System.Windows.Forms.TextBox()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.chkBlocks = New System.Windows.Forms.CheckBox()
        Me.chkDimensions = New System.Windows.Forms.CheckBox()
        Me.chkTextBoxes = New System.Windows.Forms.CheckBox()
        Me.chkCallouts = New System.Windows.Forms.CheckBox()
        Me.cboScope = New System.Windows.Forms.ComboBox()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 363)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(266, 25)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(50, 20)
        Me.lblStatus.Text = "Ready"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Scope:"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(15, 45)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(135, 21)
        Me.chkAll.TabIndex = 3
        Me.chkAll.Text = "All annotatations"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(39, 232)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Find:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 260)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Replace:"
        '
        'btnReplace
        '
        Me.btnReplace.Enabled = False
        Me.btnReplace.Location = New System.Drawing.Point(133, 313)
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.Size = New System.Drawing.Size(103, 36)
        Me.btnReplace.TabIndex = 6
        Me.btnReplace.Text = "Replace All"
        Me.btnReplace.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.Enabled = False
        Me.btnFind.Location = New System.Drawing.Point(18, 313)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(88, 36)
        Me.btnFind.TabIndex = 6
        Me.btnFind.Text = "Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'BGW
        '
        Me.BGW.WorkerReportsProgress = True
        Me.BGW.WorkerSupportsCancellation = True
        '
        'chkMatchCase
        '
        Me.chkMatchCase.AutoSize = True
        Me.chkMatchCase.Checked = Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default.matchCase
        Me.chkMatchCase.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default, "matchCase", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkMatchCase.Location = New System.Drawing.Point(15, 180)
        Me.chkMatchCase.Name = "chkMatchCase"
        Me.chkMatchCase.Size = New System.Drawing.Size(104, 21)
        Me.chkMatchCase.TabIndex = 8
        Me.chkMatchCase.Text = "Match Case"
        Me.chkMatchCase.UseVisualStyleBackColor = True
        '
        'txtReplace
        '
        Me.txtReplace.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default, "replaceString", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtReplace.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReplace.Location = New System.Drawing.Point(84, 254)
        Me.txtReplace.Name = "txtReplace"
        Me.txtReplace.Size = New System.Drawing.Size(152, 26)
        Me.txtReplace.TabIndex = 5
        Me.txtReplace.Text = Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default.replaceString
        '
        'txtFind
        '
        Me.txtFind.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default, "findString", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.Location = New System.Drawing.Point(84, 226)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(152, 26)
        Me.txtFind.TabIndex = 5
        Me.txtFind.Text = Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default.findString
        '
        'chkBlocks
        '
        Me.chkBlocks.AutoSize = True
        Me.chkBlocks.Checked = Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default.doBlocks
        Me.chkBlocks.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default, "doBlocks", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkBlocks.Location = New System.Drawing.Point(36, 153)
        Me.chkBlocks.Name = "chkBlocks"
        Me.chkBlocks.Size = New System.Drawing.Size(71, 21)
        Me.chkBlocks.TabIndex = 3
        Me.chkBlocks.Text = "Blocks"
        Me.chkBlocks.UseVisualStyleBackColor = True
        '
        'chkDimensions
        '
        Me.chkDimensions.AutoSize = True
        Me.chkDimensions.Checked = Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default.doDimensions
        Me.chkDimensions.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default, "doDimensions", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkDimensions.Location = New System.Drawing.Point(36, 126)
        Me.chkDimensions.Name = "chkDimensions"
        Me.chkDimensions.Size = New System.Drawing.Size(103, 21)
        Me.chkDimensions.TabIndex = 3
        Me.chkDimensions.Text = "Dimensions"
        Me.chkDimensions.UseVisualStyleBackColor = True
        '
        'chkTextBoxes
        '
        Me.chkTextBoxes.AutoSize = True
        Me.chkTextBoxes.Checked = Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default.doTextBoxes
        Me.chkTextBoxes.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default, "doTextBoxes", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkTextBoxes.Location = New System.Drawing.Point(36, 99)
        Me.chkTextBoxes.Name = "chkTextBoxes"
        Me.chkTextBoxes.Size = New System.Drawing.Size(99, 21)
        Me.chkTextBoxes.TabIndex = 3
        Me.chkTextBoxes.Text = "Text Boxes"
        Me.chkTextBoxes.UseVisualStyleBackColor = True
        '
        'chkCallouts
        '
        Me.chkCallouts.AutoSize = True
        Me.chkCallouts.Checked = Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default.doCallouts
        Me.chkCallouts.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default, "doCallouts", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkCallouts.Location = New System.Drawing.Point(36, 72)
        Me.chkCallouts.Name = "chkCallouts"
        Me.chkCallouts.Size = New System.Drawing.Size(166, 21)
        Me.chkCallouts.TabIndex = 3
        Me.chkCallouts.Text = "Callouts and Balloons"
        Me.chkCallouts.UseVisualStyleBackColor = True
        '
        'cboScope
        '
        Me.cboScope.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default, "scopeString", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.cboScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboScope.FormattingEnabled = True
        Me.cboScope.Items.AddRange(New Object() {"Selection", "ActiveSheet", "WorkingSheets", "BackgroundSheets", "AllSheets"})
        Me.cboScope.Location = New System.Drawing.Point(70, 15)
        Me.cboScope.Name = "cboScope"
        Me.cboScope.Size = New System.Drawing.Size(166, 24)
        Me.cboScope.TabIndex = 1
        Me.cboScope.Text = Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default.scopeString
        '
        'frmSearchReplace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(266, 388)
        Me.Controls.Add(Me.chkMatchCase)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.btnReplace)
        Me.Controls.Add(Me.txtReplace)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtFind)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.chkBlocks)
        Me.Controls.Add(Me.chkDimensions)
        Me.Controls.Add(Me.chkTextBoxes)
        Me.Controls.Add(Me.chkCallouts)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboScope)
        Me.Controls.Add(Me.StatusStrip1)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default, "formLocation", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Location = Global.SearchReplaceDraftForSolidEdge.My.MySettings.Default.formLocation
        Me.Name = "frmSearchReplace"
        Me.Text = "Search Replace Draft for SE"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents cboScope As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chkAll As CheckBox
    Friend WithEvents chkCallouts As CheckBox
    Friend WithEvents chkTextBoxes As CheckBox
    Friend WithEvents chkDimensions As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtFind As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtReplace As TextBox
    Friend WithEvents btnReplace As Button
    Friend WithEvents chkBlocks As CheckBox
    Friend WithEvents chkMatchCase As CheckBox
    Friend WithEvents btnFind As Button
    Friend WithEvents BGW As System.ComponentModel.BackgroundWorker
End Class
