Imports System.Reflection
Imports System.IO

Namespace AssemblyHelper

    <Serializable> _
    Public Class AssemblyInfos
        Implements IAssemblyInfos

        Public Sub GetAssemblyInfo(assemblyBuffer As Byte(), ByRef AssName$, ByRef AssVersion$, ByRef IsWpfApp As Boolean, ByRef EntryPoint As MethodInfo, ByRef AssemblyReferences As AssemblyName(), ByRef ManifestResourceNames As IEnumerable(Of String), ByRef Result As AssemblyData.Message)
            Try
                Dim assembly As Assembly = AppDomain.CurrentDomain.Load(assemblyBuffer)

                Dim manifest As [Module] = assembly.ManifestModule
                AssName = manifest.ScopeName

                Dim fileVersionAttributes As Object() = assembly.GetCustomAttributes(GetType(AssemblyFileVersionAttribute), True)
                If fileVersionAttributes.Length = 1 Then
                    Dim fileVersion As AssemblyFileVersionAttribute = TryCast(fileVersionAttributes(0), AssemblyFileVersionAttribute)
                    AssVersion = fileVersion.Version
                End If

                Dim isWpfProg As Boolean = assembly.GetReferencedAssemblies().Any(Function(x) x.Name.ToLower = "system.xaml") AndAlso _
        assembly.GetManifestResourceNames().Any(Function(x) x.ToLower.EndsWith(".g.resources"))

                IsWpfApp = isWpfProg
                EntryPoint = assembly.EntryPoint
                AssemblyReferences = assembly.GetReferencedAssemblies
                ManifestResourceNames = assembly.GetManifestResourceNames

                Result = AssemblyData.Message.Success

            Catch ex As ReflectionTypeLoadException
                Result = AssemblyData.Message.Failed
            Catch ex As FileNotFoundException
                Result = AssemblyData.Message.Failed
            Catch ex As FileLoadException
                Result = AssemblyData.Message.Failed
            Catch ex As NotSupportedException
                Result = AssemblyData.Message.Failed
            Catch ex As BadImageFormatException
                Result = AssemblyData.Message.Failed
            End Try
        End Sub

        Public Sub GetAssemblyInfo1(assembly() As Byte, ByRef AssName$, ByRef AssVersion$, ByRef IsWpfApp As Boolean, ByRef EntryPoint As MethodInfo, ByRef AssemblyReferences As AssemblyName(), ByRef ManifestResourceNames As IEnumerable(Of String), ByRef Result As AssemblyData.Message) Implements IAssemblyInfos.GetAssemblyInfo
            GetAssemblyInfo(assembly, AssName, AssVersion, IsWpfApp, EntryPoint, AssemblyReferences, ManifestResourceNames, Result)
        End Sub

    End Class

End Namespace
