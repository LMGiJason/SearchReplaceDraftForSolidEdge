
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Text
Imports System.Drawing.Imaging

Public Enum ManipMode
    NONE
    FENCE
    PAN
    ZOOM
    SELECTION
End Enum
Public Class SearchByEMF
    Private mEmfTemp As String

    Private mMetafile As Metafile
    Private metafileDelegate As Graphics.EnumerateMetafileProc
    Private destPoint As New Point(0, 0)
    Private mAspectRatio As Double
    Private mFitScale As Single = 1.0F
    Private mGfx As Graphics
    Private mMtxDraw As New Drawing2D.Matrix
    Private mViewportCenter As New PointF
    Private mViewRect As New RectangleF
    Private mClientRect As New Rectangle
    Private mLongside As Single = 2.0
    Private mDpiX As Single = 0
    Private mDpiY As Single = 0
    Private mMetaVres As Single = 0
    Private mMetaHres As Single = 0
    Private mMetaSize As Size

    Public Sub New()
        mEmfTemp = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "SE_DraftDataAPI.emf")
        metafileDelegate = New Graphics.EnumerateMetafileProc(AddressOf MetafileCallback)
    End Sub

    Public Sub EnumMetaFile(ByVal g As Graphics)
        mGfx = g
        mDpiX = mGfx.DpiX
        mDpiY = mGfx.DpiY

        Using mMetafile = New Metafile(mEmfTemp)
            mMetaHres = mMetafile.HorizontalResolution
            mMetaVres = mMetafile.VerticalResolution
            mMetaSize = mMetafile.Size

            'destPoint.X = CInt(mViewRect.X)
            'destPoint.Y = CInt(mViewRect.Y)
            mGfx.MultiplyTransform(mMtxDraw)
            mGfx.EnumerateMetafile(mMetafile, destPoint, metafileDelegate, System.IntPtr.Zero)
        End Using
    End Sub

    Private Function MetafileCallback(ByVal recordType As EmfPlusRecordType, ByVal flags As Integer, ByVal dataSize As Integer, ByVal data As IntPtr, ByVal callbackData As PlayRecordCallback) As Boolean

        Dim dataArray As Byte() = Nothing
        If data <> IntPtr.Zero Then
            ' Copy the unmanaged record to a managed byte buffer that can be used by PlayRecord.
            dataArray = New Byte(dataSize) {}
            Marshal.Copy(data, dataArray, 0, dataSize)


            '        Int32 nChars = Marshal.ReadInt32(data, 36);          
            'Int32 rclBounds_left = Marshal.ReadInt32(data, 0);
            'Int32 rclBounds_top = Marshal.ReadInt32(data, 4);
            'Int32 rclBounds_right = Marshal.ReadInt32(data, 8);
            'Int32 rclBounds_bottom = Marshal.ReadInt32(data, 12);

            'Int32 exScale = Marshal.ReadInt32(data, 20);
            'Int32 eyScale = Marshal.ReadInt32(data, 24);

            'Int32 ptlReferencex = Marshal.ReadInt32(data, 28);
            'Int32 ptlReferencey = Marshal.ReadInt32(data, 32);

            'Int32 rcl_left = Marshal.ReadInt32(data, 48);
            'Int32 rcl_top = Marshal.ReadInt32(data, 52);
            'Int32 rcl_right = Marshal.ReadInt32(data, 56);
            'Int32 rcl_bottom = Marshal.ReadInt32(data, 60);




        End If
        'mMetafile.PlayRecord(recordType, flags, dataSize, dataArray)
        Select Case recordType
            Case EmfPlusRecordType.EmfExtTextOutW
                If GetStringRecord(dataArray, dataSize).StartsWith("SOLID EDGE RESELLER COPY") Then
                    Return True
                Else
                    'mGfx.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
                    'mMetafile.PlayRecord(EmfPlusRecordType.SetTextRenderingHint, Drawing.Text.TextRenderingHint.ClearTypeGridFit, 0, New Byte() {})
                    mMetafile.PlayRecord(recordType, flags, dataSize, dataArray)
                End If
            Case Else
                mMetafile.PlayRecord(recordType, flags, dataSize, dataArray)
        End Select
        Return True
    End Function

    Private TextUTF16 As New System.Text.UnicodeEncoding
    Private Function GetStringRecord(ByVal dataArray As Byte(), ByVal dataSize As Integer) As String
        Return TextUTF16.GetString(dataArray, dataArray(40) - 8, dataArray(36) * 2)
    End Function

#Region "Properties"

    Private mCanvasSize As New Size(0, 0)
    Public Property CanvasSize() As Size
        Get
            Return mCanvasSize
        End Get
        Set(ByVal value As Size)
            mCanvasSize = value
            mClientRect.Width = mCanvasSize.Width
            mClientRect.Height = mCanvasSize.Height
        End Set
    End Property

    Public ReadOnly Property EmfTempFile() As String
        Get
            Return mEmfTemp
        End Get
    End Property

    Private mUnitsDescription As String = "nounits"
    Public ReadOnly Property UnitsDescription() As String
        Get
            Return mUnitsDescription
        End Get
    End Property

    Private mSheetCount As Integer = 0
    Public ReadOnly Property SheetCount() As Integer
        Get
            Return mSheetCount
        End Get
    End Property

    Private mSheetSize As SheetSize
    Public ReadOnly Property CurSheetSize() As SheetSize
        Get
            Return mSheetSize
        End Get
    End Property

    Private mEmfSize As Size
    Public ReadOnly Property EmfSize() As Size
        Get
            Return mMetaSize
        End Get
    End Property
#End Region

End Class

Public Structure SheetSize
    Dim Width As Double
    Dim Height As Double
End Structure

