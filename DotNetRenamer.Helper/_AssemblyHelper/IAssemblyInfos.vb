Imports System.Reflection

Namespace AssemblyHelper
    Public Interface IAssemblyInfos
        Sub GetAssemblyInfo(assembly() As Byte, ByRef AssName$, ByRef AssVersion$, ByRef IsWpfApp As Boolean, ByRef EntryPoint As MethodInfo, ByRef AssemblyReferences As AssemblyName(), ByRef ManifestResourceNames As IEnumerable(Of String), ByRef Result As AssemblyData.Message)
    End Interface

End Namespace

