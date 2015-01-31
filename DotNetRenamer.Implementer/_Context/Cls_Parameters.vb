Imports DotNetRenamer.Implementer.Analyzer
Imports DotNetRenamer.Implementer.Context.Cls_RenamerState
Imports DotNetRenamer.Implementer.Exclusion

Namespace Context

    ''' <summary>
    ''' INFO : This is the first step of the renamer library. 
    '''        You must pass two arguments (inputFile and outputFile properties) when instantiating this class and calling the ReadAssembly sub.
    '''        This Class inherits Cls_Analyzer.
    ''' </summary>
    Public Class Cls_Parameters
        Inherits Cls_Analyzer

#Region " Variables "
        Public RenamingAccept As Cls_RenamerState
        Public ExcludeList As Cls_ExcludeList
#End Region

#Region " Initialize "
        ''' <summary>
        ''' INFO : Initializes a new instance of the class Context.Cls_Parameters (inherits Context.Cls_Analyzer class) which used to check if the selected inputfile is a valid PE and executable file. 
        ''' </summary>
        Public Sub New(inputFile$, Optional ByVal outputFile As String = "")
            MyBase.New(inputFile, outputFile)
        End Sub
#End Region

    End Class
End Namespace

