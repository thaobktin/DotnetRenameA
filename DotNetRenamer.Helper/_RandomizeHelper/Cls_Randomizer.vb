Imports System.Security.Cryptography
Imports System.Text

Namespace RandomizeHelper
    Public NotInheritable Class Cls_Randomizer

#Region " Variables "
        Private Shared _maxLength% = 1
        Private Shared _generatedNames As New List(Of String)()
        Private Shared _alphabeticChars As Char()() = {"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()}
        Private Shared _japenese$ = "れづれなるまゝに日暮らし硯にむかひて心にうりゆくよな事を、こはかとなく書きつくればあやうこそものぐるほけれ。"
        Private Shared _greek$ = "αβγδεζηθικλµνξοπρστυϕχψω"
        Private Shared _rdn As Random = New Random()
#End Region

#Region " Methods "
        ''' <summary>
        ''' Creates a pseudo-random password containing the number of character classes
        ''' </summary>
        Private Shared Function GenerateKey(length%) As String
            Dim csp As New System.Security.Cryptography.RNGCryptoServiceProvider()

            Dim allchars As Char() = _alphabeticChars.Take(1).SelectMany(Function(c) c).ToArray()
            Dim bytes As Byte() = New Byte(allchars.Length - 1) {}
            csp.GetBytes(bytes)

            For i As Integer = 0 To allchars.Length - 1
                Dim tmp As Char = allchars(i)
                allchars(i) = allchars(bytes(i) Mod allchars.Length)
                allchars(bytes(i) Mod allchars.Length) = tmp
            Next
            Array.Resize(bytes, length)
            Dim result As Char() = New Char(length - 1) {}
            While True
                csp.GetBytes(bytes)
                For i% = 0 To length - 1
                    result(i) = allchars(bytes(i) Mod allchars.Length)
                Next
                If Char.IsWhiteSpace(result(0)) OrElse Char.IsWhiteSpace(result((length - 1) Mod length)) Then
                    Continue While
                End If

                Dim testResult As New String(result)
                If 0 <> _alphabeticChars.Take(1).Count(Function(c) testResult.IndexOfAny(c) < 0) Then
                    Continue While
                End If

                Return testResult
            End While
            Return _alphabeticChars.Take(1).ToString
        End Function

        Private Shared Function Alphabetic() As String
            Dim str$ = GenerateKey(_maxLength)
            If _generatedNames.Contains(str.ToLower) Then
                _maxLength += 1
                Return Alphabetic()
            End If
            _generatedNames.Add(str.ToLower)
            Return str
        End Function

        Private Shared Function Japanese() As String
            Dim str$ = New String(Enumerable.Repeat(_japenese, _rdn.Next(1, _maxLength)).Select(Function(s) s(_rdn.Next(s.Length))).ToArray())
            If _generatedNames.Contains(str) Then
                _maxLength += 1
                Return Japanese()
            End If
            _generatedNames.Add(str)
            Return str
        End Function

        Private Shared Function Dots() As String
            Dim builder As New StringBuilder()
            Dim randomize As New Random(Guid.NewGuid().GetHashCode())

            For i% = 0 To _maxLength - 1
                builder.Append(Convert.ToChar(randomize.Next(AscW("⠀"c), AscW("⠎"c))))
            Next

            If _generatedNames.Contains(builder.ToString()) Then
                _maxLength += 1
                Return Dots()
            End If

            _generatedNames.Add(builder.ToString())
            Return builder.ToString()
        End Function

        Private Shared Function Invisible() As String
            Dim builder As New StringBuilder()
            Dim randomize As New Random(Guid.NewGuid().GetHashCode())

            For i% = 0 To _maxLength - 1
                builder.Append(Convert.ToChar(randomize.Next(AscW("​"c), AscW("‏"c))))
            Next

            If _generatedNames.Contains(builder.ToString()) Then
                _maxLength += 1
                Return Invisible()
            End If

            _generatedNames.Add(builder.ToString())
            Return builder.ToString()
        End Function

        ''' <summary>
        ''' by Confuser
        ''' </summary>
        Private Shared Function Chinese() As String
            Dim md5__1 As MD5 = MD5.Create()
            Dim arr As New BitArray(md5__1.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().GetHashCode().ToString())))

            Dim rand As New Random(Guid.NewGuid().GetHashCode())
            Dim xorB As Byte() = New Byte(arr.Length / 8 - 1) {}
            rand.NextBytes(xorB)
            Dim x As New BitArray(xorB)

            Dim result As BitArray = arr.Xor(x)
            Dim ret As Byte() = New Byte(result.Length / 8 - 1) {}
            result.CopyTo(ret, 0)

            Return Encoding.Unicode.GetString(ret).Replace(vbNullChar, "").Replace(".", "").Replace("/", "")
        End Function

        Private Shared Function Greek() As String
            Dim str$ = New String(Enumerable.Repeat(_greek, _rdn.Next(1, _maxLength)).Select(Function(s) s(_rdn.Next(s.Length))).ToArray())
            If _generatedNames.Contains(str) Then
                _maxLength += 1
                Return Greek()
            End If
            _generatedNames.Add(str)
            Return str
        End Function

        Public Shared Function GenerateNew() As String
            Select Case Cls_RandomizerType.RenameSetting
                Case Cls_RandomizerType.RenameEnum.Alphabetic
                    Return Alphabetic()
                Case Cls_RandomizerType.RenameEnum.Dot
                    Return Dots()
                Case Cls_RandomizerType.RenameEnum.Invisible
                    Return Invisible()
                Case Cls_RandomizerType.RenameEnum.Chinese
                    Return Chinese()
                Case Cls_RandomizerType.RenameEnum.Japanese
                    Return Japanese()
                Case Cls_RandomizerType.RenameEnum.Greek
                    Return Greek()
            End Select

            Return Nothing
        End Function

        Public Shared Function GenerateNewAlphabetic() As String
            Return Alphabetic()
        End Function

        Public Shared Sub CleanUp()
            _maxLength = 1
            _generatedNames.Clear()
        End Sub
#End Region

    End Class
End Namespace