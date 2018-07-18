Imports SolidEdgeFramework
Imports System.Runtime.InteropServices
Imports SolidEdgeFrameworkSupport
Imports SolidEdgeConstants.ObjectType
Imports System.Runtime.InteropServices.ComTypes

Public Enum SearchReplaceScope
    Selection
    ActiveSheet
    WorkingSheets
    BackgroundSheets
    AllSheets
End Enum

Public Class MsgEventArg
    Inherits EventArgs
    Sub New(m As String)
        Me.Msg = m
    End Sub
    Property Msg As String
End Class

Public Class SearchReplaceProcessor
    Implements SolidEdgeFramework.ISEApplicationEvents

    Private mSolidApp As SolidEdgeFramework.Application
    Private mDraftDoc As SolidEdgeDraft.DraftDocument
    Private mWindow As SolidEdgeDraft.SheetWindow
    Private dblx1 As Double, dbly1 As Double, dblx2 As Double, dbly2 As Double

    Public Event OnMsg(sender As Object, e As MsgEventArg)
    Public Function InitEdge() As Boolean
        Try
            Try
                mSolidApp = TryCast(Marshal.GetActiveObject("SolidEdge.Application"), SolidEdgeFramework.Application)
            Catch ex As Exception
                ReportStatus("Solid Edge is not running!")
                Return False
            End Try

            If mSolidApp.ActiveDocumentType <> DocumentTypeConstants.igDraftDocument Then
                ReportStatus("Drafting only!")
                Return False
            Else
                OleMessageFilter.Register()

                'Just so we get an undo mark
                HookUnHookCommandEventHandlers(True)
                mDraftDoc = mSolidApp.ActiveDocument
            End If
        Catch ex As Exception
            ReportStatus(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private mMatchCase As Boolean
    Public Property MatchCase() As Boolean
        Get
            Return mMatchCase
        End Get
        Set(ByVal value As Boolean)
            mMatchCase = value
        End Set
    End Property

    Private mSearchScope As SearchReplaceScope
    Public Property SearchScope() As SearchReplaceScope
        Get
            Return mSearchScope
        End Get
        Set(ByVal value As SearchReplaceScope)
            mSearchScope = value
        End Set
    End Property

    Private mFindString As String
    Public Property FindString() As String
        Get
            Return mFindString
        End Get
        Set(ByVal value As String)
            mFindString = value
        End Set
    End Property

    Private mReplaceString As String
    Public Property ReplaceString() As String
        Get
            Return mReplaceString
        End Get
        Set(ByVal value As String)
            mReplaceString = value
        End Set
    End Property

    Private mDoBalloons As Boolean
    Public Property DoBalloons() As Boolean
        Get
            Return mDoBalloons
        End Get
        Set(ByVal value As Boolean)
            mDoBalloons = value
        End Set
    End Property

    Private mDoTextBoxes As Boolean
    Public Property DoTextBoxes() As Boolean
        Get
            Return mDoTextBoxes
        End Get
        Set(ByVal value As Boolean)
            mDoTextBoxes = value
        End Set
    End Property

    Private mDoDimensions As Boolean
    Public Property DoDimensions() As Boolean
        Get
            Return mDoDimensions
        End Get
        Set(ByVal value As Boolean)
            mDoDimensions = value
        End Set
    End Property

    Private mBGW As System.ComponentModel.BackgroundWorker
    Public Sub DoFindNext(ByVal worker As System.ComponentModel.BackgroundWorker, ByVal e As System.ComponentModel.DoWorkEventArgs)
        mBGW = worker
        BeginFind()
    End Sub

    Private Sub SpinWait()
        If IsSearching() Then
            Do While Not break And IsSearching()
            Loop
        End If
        break = False
    End Sub

    Private Function IsSearching() As Boolean
        Return mBGW IsNot Nothing AndAlso mBGW.IsBusy
    End Function

    Private break As Boolean = False
    Public Sub DoFindNext()
        break = True
    End Sub

    Private Sub BeginFind()
        Dim sht As SolidEdgeDraft.Sheet
        Dim shts As SolidEdgeDraft.SectionSheets = mDraftDoc.Sections.WorkingSection.Sheets

        For s As Integer = 1 To shts.Count
            sht = shts.Item(s)
            ProcessSheet(sht)
            ReleaseRCW(sht)
        Next
        ReleaseRCW(shts)
    End Sub

    Public Sub DoReplacements()
        Dim sht As SolidEdgeDraft.Sheet
        Dim shts As SolidEdgeDraft.Sheets = mDraftDoc.Sheets

        mSolidApp.StatusBar = "Replacing ..."
        For s As Integer = 1 To shts.Count
            sht = shts.Item(s)
            ProcessSheet(sht)
            ReleaseRCW(sht)
        Next
        ReleaseRCW(shts)
    End Sub

    Private Function IsMatch(txt As String) As Boolean
        If String.IsNullOrEmpty(txt) Then Return False
        Return String.Compare(txt, FindString, Not MatchCase) = 0
    End Function

    Private Sub ProcessSheet(sht As SolidEdgeDraft.Sheet)
        Dim objTextboxes As SolidEdgeFrameworkSupport.TextBoxes
        Dim objTextBox As SolidEdgeFrameworkSupport.TextBox
        Dim objBalloons As SolidEdgeFrameworkSupport.Balloons
        Dim objBalloon As SolidEdgeFrameworkSupport.Balloon
        Dim objDimensions As SolidEdgeFrameworkSupport.Dimensions
        Dim objDimension As SolidEdgeFrameworkSupport.Dimension

        FindInPartsLists()


        objBalloons = sht.Balloons
        'Balloons
        For c As Integer = 1 To objBalloons.Count
            objBalloon = objBalloons.Item(c)
            If objBalloon.BalloonText IsNot Nothing Then
                If IsMatch(objBalloon.BalloonText) Then
                    sht.Activate() 'This ends the command!!!!!!!!!
                    objBalloon.Range(dblx1, dbly1, dblx2, dbly2)
                    ZoomTo2DObject(dblx1, dbly1, dblx2, dbly2, 1)
                    SpinWait()
                End If

                objBalloon.BalloonText = objBalloon.BalloonDisplayedText
                objBalloon.BalloonTextLower = objBalloon.BalloonDisplayedTextLower
            End If
            ReleaseRCW(objBalloon)
        Next
        ReleaseRCW(objBalloons)

        objTextboxes = sht.TextBoxes
        For t As Integer = 1 To objTextboxes.Count
            objTextBox = objTextboxes.Item(t)
            'objTextBox.Text = Replace(FindString, ReplaceString)
            ReleaseRCW(objTextBox)
        Next
        ReleaseRCW(objTextBox)





    End Sub

    Private Sub FindInPartsLists()
        Dim partsList As SolidEdgeDraft.PartsList = Nothing
        'Dim partsLists As SolidEdgeDraft.PartsLists = Nothing
        Dim page As SolidEdgeDraft.TablePage = Nothing
        Dim pages As SolidEdgeDraft.TablePages = Nothing
        Dim columns As SolidEdgeDraft.TableColumns = Nothing
        Dim rows As SolidEdgeDraft.TableRows = Nothing
        Dim cell As SolidEdgeDraft.TableCell = Nothing

        Using partsLists = mDraftDoc.PartsLists.WithComCleanup
            For p As Integer = 1 To partsLists.Resource.Count
                partsList = partsLists.Resource.Item(p)
                Using partsList.WithComCleanup
                    pages = partsList.Pages
                    Using pages.WithComCleanup
                        For pg As Integer = 1 To pages.Count
                            page = pages.Item(pg)
                            Using page.WithComCleanup
                                'we would need to calculate the cell location based on column count of the page
                            End Using
                        Next
                    End Using

                    cell = partsList.Cell(10, 5)
                    Using cell.WithComCleanup
                        Debug.Print(cell.value)
                    End Using
                End Using
            Next
        End Using

    End Sub

    Private Function GetCellPos(plistidx As Integer, pageidx As Integer, rowidx As Integer, colidx As Integer) As Point2d

    End Function

    Public Sub ZoomTo2DObject(dblx1 As Double, dbly1 As Double, dblx2 As Double, dbly2 As Double, adjust As Double)
        Dim WindowX1 As Long, WindowY1 As Long, WindowX2 As Long, WindowY2 As Long
        Dim midX As Double, midY As Double, y As Double, x As Double

        mWindow = mDraftDoc.Windows(0)

        'Set scale
        x = (dblx2 - dblx1) * adjust
        y = (dbly2 - dbly1) * adjust

        midX = (dblx2 + dblx1) / 2
        midY = (dbly2 + dbly1) / 2

        dblx1 = midX - (x / 2)
        dblx2 = midX + (x / 2)
        dbly1 = midY - (x / 2)
        dbly2 = midY + (x / 2)

        'Convert coordinates.
        mWindow.ModelToWindow(dblx1, dbly1, WindowX1, WindowY1)
        mWindow.ModelToWindow(dblx2, dbly2, WindowX2, WindowY2)
        mWindow.ZoomArea(WindowX1, WindowY1, WindowX2, WindowY2)
        ReleaseRCW(mWindow)

    End Sub

    Private Sub FixBlockLabels(ByVal find As String, ByVal replace As String)

        Dim mySheet As SolidEdgeDraft.Sheet = Nothing
        Dim myBlock As SolidEdgeDraft.BlockOccurrence = Nothing
        Dim myBlockLable As SolidEdgeDraft.BlockLabelOccurrence = Nothing
        Dim oldtext() As String = Split(find, ",")
        Dim newtext() As String = Split(replace, ",")
        Try
            For Each mySheet In mDraftDoc.Sheets
                For Each myBlock In mySheet.BlockOccurrences
                    For Each myBlockLable In myBlock.BlockLabelOccurrences
                        For r As Integer = 0 To oldtext.Length - 1
                            If myBlockLable.value.Contains(oldtext(r).Trim) Then
                                myBlockLable.value = myBlockLable.value.Replace(oldtext(r).Trim, newtext(r).Trim)
                            End If
                        Next
                    Next
                Next
            Next
        Catch
            Throw
        Finally

            If Not (myBlockLable Is Nothing) Then
                Debug.Assert(0 = Marshal.FinalReleaseComObject(myBlockLable))
            End If
            myBlockLable = Nothing

            If Not (myBlock Is Nothing) Then
                Debug.Assert(0 = Marshal.FinalReleaseComObject(myBlock))
            End If
            myBlock = Nothing

            If Not (mySheet Is Nothing) Then
                Debug.Assert(0 = Marshal.FinalReleaseComObject(mySheet))
            End If
            mySheet = Nothing
        End Try

    End Sub


    Private Command_CP As IConnectionPoint
    Private Command_CP_Cookie As Integer

    Private Sub HookUnHookCommandEventHandlers(hook As Boolean)
        Dim i As Type = GetType(SolidEdgeFramework.ISEApplicationEvents)
        Dim EventGuid As Guid = i.GUID
        Command_CP_Cookie = -1
        HookUnhookEvents(Command_CP, hook, mSolidApp, Command_CP_Cookie, EventGuid)
    End Sub

    Private Sub HookUnhookEvents(ByRef CP As IConnectionPoint, ByVal Add As Boolean, ByVal obj As Object, ByRef Cookie As Integer, ByVal EventGuid As Guid)
        Dim CPC As IConnectionPointContainer

        CPC = obj

        If Not CPC Is Nothing Then

            If Add Then
                CPC.FindConnectionPoint(EventGuid, CP)
                If Not CP Is Nothing Then
                    CP.Advise(Me, Cookie)
                End If
            Else
                If Not CP Is Nothing Then
                    If Not Cookie = -1 Then
                        Try
                            CP.Unadvise(Cookie)
                            Marshal.ReleaseComObject(CP)
                        Catch ex As Exception
                            'swallow all exceptions on unadvise
                            'the host may not be available at this point
                            ' What kind of host leaves the party before the guests?
                        Finally
                            CP = Nothing
                            Cookie = -1
                        End Try
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub ReportStatus(msg As String)
        RaiseEvent OnMsg(Me, New MsgEventArg(msg))
    End Sub

    Public Sub FinalCleanup()
        HookUnHookCommandEventHandlers(False)
        ReleaseRCW(mDraftDoc)
        ReleaseRCW(mSolidApp)
        OleMessageFilter.Unregister()
    End Sub

    Private Sub ReleaseRCW(ByRef o As Object)
        If o IsNot Nothing Then
            Debug.Assert(0 = Marshal.ReleaseComObject(o))
            o = Nothing
        End If
    End Sub

    Public Sub AfterActiveDocumentChange(theDocument As Object) Implements ISEApplicationEvents.AfterActiveDocumentChange
        Throw New NotImplementedException()
    End Sub

    Public Sub AfterCommandRun(theCommandID As Integer) Implements ISEApplicationEvents.AfterCommandRun
        Throw New NotImplementedException()
    End Sub

    Public Sub AfterDocumentOpen(theDocument As Object) Implements ISEApplicationEvents.AfterDocumentOpen
        Throw New NotImplementedException()
    End Sub

    Public Sub AfterDocumentPrint(theDocument As Object, hDC As Integer, ByRef ModelToDC As Double, ByRef Rect As Integer) Implements ISEApplicationEvents.AfterDocumentPrint
        Throw New NotImplementedException()
    End Sub

    Public Sub AfterDocumentSave(theDocument As Object) Implements ISEApplicationEvents.AfterDocumentSave
        Throw New NotImplementedException()
    End Sub

    Public Sub AfterEnvironmentActivate(theEnvironment As Object) Implements ISEApplicationEvents.AfterEnvironmentActivate
        Throw New NotImplementedException()
    End Sub

    Public Sub AfterNewDocumentOpen(theDocument As Object) Implements ISEApplicationEvents.AfterNewDocumentOpen
        Throw New NotImplementedException()
    End Sub

    Public Sub AfterNewWindow(theWindow As Object) Implements ISEApplicationEvents.AfterNewWindow
        Throw New NotImplementedException()
    End Sub

    Public Sub AfterWindowActivate(theWindow As Object) Implements ISEApplicationEvents.AfterWindowActivate
        Throw New NotImplementedException()
    End Sub

    Public Sub BeforeCommandRun(theCommandID As Integer) Implements ISEApplicationEvents.BeforeCommandRun
        Throw New NotImplementedException()
    End Sub

    Public Sub BeforeDocumentClose(theDocument As Object) Implements ISEApplicationEvents.BeforeDocumentClose
        Throw New NotImplementedException()
    End Sub

    Public Sub BeforeDocumentPrint(theDocument As Object, hDC As Integer, ByRef ModelToDC As Double, ByRef Rect As Integer) Implements ISEApplicationEvents.BeforeDocumentPrint
        Throw New NotImplementedException()
    End Sub

    Public Sub BeforeEnvironmentDeactivate(theEnvironment As Object) Implements ISEApplicationEvents.BeforeEnvironmentDeactivate
        Throw New NotImplementedException()
    End Sub

    Public Sub BeforeWindowDeactivate(theWindow As Object) Implements ISEApplicationEvents.BeforeWindowDeactivate
        Throw New NotImplementedException()
    End Sub

    Public Sub BeforeQuit() Implements ISEApplicationEvents.BeforeQuit
        Throw New NotImplementedException()
    End Sub

    Public Sub BeforeDocumentSave(theDocument As Object) Implements ISEApplicationEvents.BeforeDocumentSave
        Throw New NotImplementedException()
    End Sub
End Class
