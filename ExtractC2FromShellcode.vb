    Sub Main()

        Dim sArg() As String = System.Environment.GetCommandLineArgs

        Dim sFN As String = ""
        Try
            sFN = sArg(1)
        Catch ex As Exception
        End Try
        If sFN = "" Then
            Console.WriteLine("Provide a file containing raw shellcode.")
            End
        End If

        If System.IO.File.Exists(sFN) = False Then End ' File does not exist
        If FileLen(sFN) = 0 Then End ' File is zero length or locked

        Dim bFileContent() As Byte = System.IO.File.ReadAllBytes(sFN)

        Dim sIP As String = ""
        Dim iPort As Integer = 0

        For n = 0 To (UBound(bFileContent) - 10)
            If bFileContent(n) = 73 Then
                If bFileContent(n + 1) = 188 And bFileContent(n + 2) = 2 And bFileContent(n + 3) = 0 And bFileContent(n + 10) = 65 Then ' dec ecx, mov esp 0x0100007f

                    'PORT: bFileContent(n + 4) and bFileContent(n + 5), preceeds ip address
                    iPort = bFileContent(n + 4) * 256
                    iPort = iPort + bFileContent(n + 5)

                    'IP: bFileContent(n + 6) and bFileContent(n + 7) and bFileContent(n + 8) and bFileContent(n + 9)
                    sIP = bFileContent(n + 6).ToString & "." & bFileContent(n + 7).ToString & "." & bFileContent(n + 8).ToString & "." & bFileContent(n + 9).ToString
                End If
            End If

        Next

        Console.WriteLine(sIP & ":" & iPort)

    End Sub
