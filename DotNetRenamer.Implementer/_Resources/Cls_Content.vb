Imports System.Resources
Imports System.IO
Imports DotNetRenamer.Helper.CecilHelper
Imports DotNetRenamer.Helper.RandomizeHelper
Imports DotNetRenamer.Implementer.Processing
Imports System.Text.RegularExpressions
Imports dnlib.DotNet
Imports dnlib.DotNet.Emit

Namespace Resources

    Public Class Cls_Content

#Region " Fields "
        Private Shared _Resources As New List(Of EmbeddedResource)
        Private Shared _embeddedResource As New Dictionary(Of EmbeddedResource, EmbeddedResource)
#End Region

#Region " Methods "

        Public Shared Sub Rename(assdef As AssemblyDef)
            If assdef.ManifestModule.HasResources Then
                For Each EmbRes As Resource In assdef.ManifestModule.Resources
                    _Resources.Add(EmbRes)
                Next
                For Each modul As ModuleDef In assdef.Modules
                    If modul.HasTypes Then
                        For Each type As TypeDef In modul.GetTypes()
                            RenameContent(type)
                        Next
                    End If
                Next
            End If
        End Sub

        Private Shared Sub RenameContent(typeDef As TypeDef)
            For Each pr In typeDef.Properties
                If Not pr.GetMethod Is Nothing Then
                    If pr.GetMethod.Name = "get_ResourceManager" Then
                        If pr.GetMethod.HasBody Then
                            If pr.GetMethod.Body.Instructions.Count <> 0 Then
                                For Each instruction In pr.GetMethod.Body.Instructions
                                    If TypeOf instruction.Operand Is String Then
                                        Dim NewResManagerName$ = instruction.Operand
                                        For Each EmbRes As Resource In _Resources
                                            UpdateResources(typeDef, EmbRes, NewResManagerName)
                                        Next
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
            Next
        End Sub

        Private Shared Sub UpdateResources(TypeDef As TypeDef, OriginalEmbeddedRes As EmbeddedResource, KeyNameOriginal$)
            Try
                Dim NewEmbeddedRes As New ResourceWriter(KeyNameOriginal)

                Using read As New ResourceReader(OriginalEmbeddedRes.GetResourceStream)
                    For Each Dat As System.Collections.DictionaryEntry In read
                        Dim data() As Byte = Nothing
                        Dim dataType = String.Empty
                        Dim originalDataKey$ = Dat.Key
                        read.GetResourceData(Dat.Key, dataType, data)
                        Dim obfuscatedDataKey$ = UpdateKey(NewEmbeddedRes, dataType, data)
                        UpdateResourcesKeys(TypeDef, obfuscatedDataKey, originalDataKey, OriginalEmbeddedRes.Name)
                    Next
                End Using

                NewEmbeddedRes.Generate()
                NewEmbeddedRes.Close()
                NewEmbeddedRes.Dispose()

                UpdateAssembly(TypeDef, KeyNameOriginal, OriginalEmbeddedRes)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End Sub

        Private Shared Sub UpdateResourcesKeys(TypeDef As TypeDef, NewKeyName$, OriginalKeyName$, resName$)
            If resName.EndsWith("Resources.resources") = False Then
                If resName.EndsWith(".resources") Then
                    Dim typeFullName$ = resName.Substring(0, resName.LastIndexOf("."))
                    Dim typeName$ = typeFullName.Replace(".resources", String.Empty).Substring(typeFullName.LastIndexOf(".") + 1)
                    Dim typeSearch As TypeDef = Cls_DnlibHelper.FindType(TypeDef.Module.Assembly.ManifestModule, typeName)
                    If Not typeSearch Is Nothing Then
                        Dim methodSearch As MethodDef = Cls_DnlibHelper.FindMethod(typeSearch, "InitializeComponent")
                        If Not methodSearch Is Nothing Then
                            UpdateMethodBody(methodSearch, NewKeyName, OriginalKeyName)
                        End If
                    End If
                End If
            ElseIf resName.EndsWith("Resources.resources") Then
                For Each pr In TypeDef.Properties
                    If Not pr.GetMethod Is Nothing Then
                        If pr.GetMethod.Name = "get_" & Regex.Replace(OriginalKeyName, "[^\w]+", "_") Then
                            pr.GetMethod.Name = Cls_Mapping.RenameMethodMember(pr.GetMethod, Cls_Randomizer.GenerateNew())
                            UpdateMethodBody(pr.GetMethod, NewKeyName, OriginalKeyName)
                        End If
                    End If
                Next
            End If
        End Sub

        Private Shared Sub UpdateMethodBody(Meth As MethodDef, NewKeyName$, OriginalKeyName$)
            If Meth.HasBody Then
                If Meth.Body.Instructions.Count <> 0 Then
                    For Each instruction As Instruction In Meth.Body.Instructions
                        If TypeOf instruction.Operand Is String Then
                            If CStr(instruction.Operand) = OriginalKeyName Then
                                instruction.Operand = NewKeyName
                            End If
                        End If
                    Next
                End If
            End If
        End Sub

        Private Shared Function UpdateKey(NewEmbeddedRes As ResourceWriter, datatype As Object, data As Byte()) As String
            Dim newdataKey = Cls_Randomizer.GenerateNew()
            NewEmbeddedRes.AddResourceData(newdataKey, datatype, data)
            Return newdataKey
        End Function

        Private Shared Sub UpdateAssembly(TypeDef As TypeDef, resWriterPath$, OriginalEmbeddedResource As Resource)
            Try
                Dim CompressRes As EmbeddedResource = New EmbeddedResource(OriginalEmbeddedResource.Name, File.ReadAllBytes(My.Application.Info.DirectoryPath & "\" & resWriterPath), ManifestResourceAttributes.Private)
                If Not _embeddedResource.ContainsKey(OriginalEmbeddedResource) Then
                    _embeddedResource.Add(OriginalEmbeddedResource, CompressRes)
                    TypeDef.Module.Assembly.ManifestModule.Resources.Remove(OriginalEmbeddedResource)
                    TypeDef.Module.Assembly.ManifestModule.Resources.Add(CompressRes)
                End If
                File.Delete(My.Application.Info.DirectoryPath & "\" & resWriterPath)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End Sub

        Private Shared Sub CleanUpTmpFiles()
            Try
                Dim files = System.IO.Directory.GetFiles(My.Application.Info.DirectoryPath, "*.resources", IO.SearchOption.TopDirectoryOnly)
                For Each f In files
                    System.IO.File.Delete(f)
                Next
            Catch ex As Exception
            End Try
        End Sub

        Public Shared Sub Cleanup()
            _Resources.Clear()
            _embeddedResource.Clear()
            CleanUpTmpFiles()
        End Sub
#End Region

    End Class
End Namespace