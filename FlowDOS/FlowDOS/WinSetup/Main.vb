Imports System.Runtime.InteropServices
Imports System.IO

Public Class Main

    Private Sub Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub


    Public Sub CheckVMs()
        If Is64BitOperatingSystem() Then
        Else

        End If
    End Sub

#Region "VMs"
    Public Function VirtualPC() As Boolean
        If (Directory.Exists("%PROGRAMFILES%\Microsoft Virtual PC")) Then

        End If
    End Function

    Public Function VirtualBox() As Boolean
        If (Directory.Exists("%PROGRAMFILES%\Oracle\VirtualBox")) And (Directory.Exists("%userprofile%\VirtualBox VMs")) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function VMWarePlayer() As Boolean

    End Function

    Public Function VMWareWorkstation() As Boolean

    End Function

    Public Function QEMU() As Boolean

    End Function
#End Region


#Region "Plateforme"
    Public Shared Function Is64BitOperatingSystem() As Boolean
        If IntPtr.Size = 8 Then
            ' 64-bit programs run only on Win64
            Return True
        Else
            ' 32-bit programs run on both 32-bit and 64-bit Windows
            ' Detect whether the current process is a 32-bit process 
            ' running on a 64-bit system.
            Dim flag As Boolean
            Return ((DoesWin32MethodExist("kernel32.dll", "IsWow64Process") AndAlso IsWow64Process(GetCurrentProcess(), flag)) AndAlso flag)
        End If
    End Function

    ''' <summary>
    ''' The function determins whether a method exists in the export 
    ''' table of a certain module.
    ''' </summary>
    ''' <param name="moduleName">The name of the module</param>
    ''' <param name="methodName">The name of the method</param>
    ''' <returns>
    ''' The function returns true if the method specified by methodName 
    ''' exists in the export table of the module specified by moduleName.
    ''' </returns>
    Private Shared Function DoesWin32MethodExist(moduleName As String, methodName As String) As Boolean
        Dim moduleHandle As IntPtr = GetModuleHandle(moduleName)
        If moduleHandle = IntPtr.Zero Then
            Return False
        End If
        Return (GetProcAddress(moduleHandle, methodName) <> IntPtr.Zero)
    End Function

    <DllImport("kernel32.dll")> _
    Private Shared Function GetCurrentProcess() As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function GetModuleHandle(moduleName As String) As IntPtr
    End Function

    <DllImport("kernel32", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function GetProcAddress(hModule As IntPtr, <MarshalAs(UnmanagedType.LPStr)> procName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function IsWow64Process(hProcess As IntPtr, wow64Process As Boolean) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

#End Region

    Dim setupstarted = False

    Private Sub KryptonButton1_Click(sender As System.Object, e As System.EventArgs) Handles KryptonButton1.Click
        If setupstarted Then
            setupstarted = False
            Do
                Me.Height = Me.Height + 1
                System.Threading.Thread.Sleep(100)
            Loop Until Me.Height = 103
            statuslbl.Visible = False
        ElseIf Not setupstarted Then
            setupstarted = True
            Do
                Me.Height = Me.Height + 1
                System.Threading.Thread.Sleep(100)
            Loop Until Me.Height = 128
            statuslbl.Visible = True
        End If
    End Sub
End Class
