Imports System.Runtime.CompilerServices
Imports System.Collections.Generic
Module COM_Extensions
    Public refCollection As New Dictionary(Of Object, Object)
    <System.Runtime.CompilerServices.Extension()> _
    Public Function WithComCleanup(Of T As Class)(ByVal comObj As T) As RefCounted(Of T)
        Dim theObject As Object = Nothing
        refCollection.TryGetValue(comObj, theObject)
        If theObject Is Nothing Then
            theObject = New RefCounted(Of T)(comObj, refCollection)
            refCollection(comObj) = theObject
        Else
            DirectCast(theObject, RefCounted(Of T)).AddRef()
        End If
        Return theObject
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ReleaseComObject(Of T As Class)(ByVal comObj As T) As Boolean
        Dim retIndex As Integer
        If comObj IsNot Nothing Then
            retIndex = System.Runtime.InteropServices.Marshal.ReleaseComObject(comObj)
            comObj = Nothing
        End If
        Return retIndex
    End Function

End Module


''' <summary>
''' Base Class for a RefCountedUnmanagedResourceProtectingClass
''' Caution with cyclic use - however
''' </summary>
Public Class RefCounted(Of T As Class)
    Implements IDisposable
    Private comObj As T
    Private IsFinalDisposed As Boolean = False
    Private RefCounter As Integer = 0
    Private refCollection As Dictionary(Of Object, Object)

    Public Sub New(ByVal inResource As T, ByRef refColl As Dictionary(Of Object, Object))
        comObj = inResource
        refCollection = refColl
        System.Threading.Interlocked.Increment(RefCounter)
    End Sub
    Public Function AddRef() As RefCounted(Of T)
        System.Threading.Interlocked.Increment(RefCounter)
        Return Me
    End Function

    Public ReadOnly Property Resource() As T
        Get
            Return comObj
        End Get
    End Property

    Protected Overrides Sub Finalize()
        Try
            If Not IsFinalDisposed Then
                Dispose()
            End If
        Finally
            MyBase.Finalize()
        End Try
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        System.Threading.Interlocked.Decrement(RefCounter)
        If (RefCounter = 0) AndAlso (Not IsFinalDisposed) Then
            FinalDispose()
        End If
    End Sub
    Public Overridable Sub FinalDispose()
        If comObj.[GetType]().IsCOMObject Then
            If comObj IsNot Nothing Then
                refCollection.Remove(comObj)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(comObj)
                comObj = Nothing
            End If
        Else
            DirectCast(comObj, IDisposable).Dispose()
        End If
        IsFinalDisposed = True
    End Sub
End Class


