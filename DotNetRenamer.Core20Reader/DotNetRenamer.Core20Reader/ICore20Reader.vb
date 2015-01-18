
Public Interface ICore20Reader
    Sub ReadFile(filePath$)

    Function isExecutable() As Boolean
    ReadOnly Property isManagedFile() As Boolean

    ReadOnly Property GetSystemType As String

    ReadOnly Property GetTargetRuntime As String

    ReadOnly Property GetTargetPlatform As String

End Interface
