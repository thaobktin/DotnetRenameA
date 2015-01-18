Imports System.Reflection
Imports System.IO
Imports DotNetRenamer.Helper.RandomizeHelper

Namespace AssemblyHelper
    Public Class AssemblyHelper

        Public Shared Function LoadMinimal(AssPath$) As AssemblyData

            Dim tempAppDomain As AppDomain = Nothing
            Dim tempPath As String = Environment.GetEnvironmentVariable("TEMP")
            Dim AssemblyPath As String = AssPath
            Dim tempAssemblyPath As String = Path.Combine(tempPath, New FileInfo(AssemblyPath).Name)
            File.Copy(AssPath, tempAssemblyPath, True)

            Dim AssData As New AssemblyData
            Try
                tempAppDomain = AppDomain.CreateDomain(Cls_Randomizer.GenerateNew)

                Dim assemblyBuffer As Byte() = File.ReadAllBytes(tempAssemblyPath)

                Dim anObject As Object = tempAppDomain.CreateInstanceAndUnwrap("DotNetRenamer.Helper", "DotNetRenamer.Helper.AssemblyHelper.AssemblyInfos")
                Dim assemblyInspector As IAssemblyInfos = TryCast(anObject, IAssemblyInfos)

                Dim AssName$ = String.Empty
                Dim AssVersion$ = String.Empty
                Dim IsWpf As Boolean
                Dim Location$ = String.Empty
                Dim EntryPoint As MethodInfo = Nothing
                Dim AssemblyReferences As AssemblyName() = Nothing
                Dim ManifestResourceNames As IEnumerable(Of String) = Nothing
                Dim Result As AssemblyData.Message

                assemblyInspector.GetAssemblyInfo(assemblyBuffer, AssName, AssVersion, IsWpf, EntryPoint, AssemblyReferences, ManifestResourceNames, Result)

                With AssData
                    .AssName = AssName
                    .AssVersion = AssVersion
                    .IsWpf = IsWpf
                    .Location = AssPath
                    .EntryPoint = EntryPoint
                    .AssemblyReferences = AssemblyReferences
                    .Result = Result
                End With

            Catch ex As Exception
                AssData.Result = AssemblyData.Message.Failed
            Finally
                If tempAppDomain IsNot Nothing Then
                    AppDomain.Unload(tempAppDomain)
                End If
            End Try
            Return AssData
        End Function
    End Class
End Namespace