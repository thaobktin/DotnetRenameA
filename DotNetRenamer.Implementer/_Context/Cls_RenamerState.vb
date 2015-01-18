Imports DotNetRenamer.Helper.RandomizeHelper
Imports DotNetRenamer.Helper.RandomizeHelper.Cls_RandomizerType

Namespace Context
    Public Class Cls_RenamerState

#Region " Variables "
        Public Namespaces As Boolean
        Public Types As Boolean
        Public Methods As Boolean
        Public Properties As Boolean
        Public Fields As Boolean
        Public CustomAttributes As Boolean
        Public Events As Boolean
        Public Variables As Boolean
        Public Parameters As Boolean

        Public ReplaceNamespacesSetting As ReplaceNamespaces = CBool(ReplaceNamespaces.ByDefault)
        Public RenameRuleSetting As RenameRule = RenameRule.Full
        Public RenameMainNamespaceSetting As RenameMainNamespace = CBool(RenameMainNamespace.NotOnly)
        Public RenamingType As RenameEnum = RenameEnum.Alphabetic
#End Region

#Region " Enumerations "
        ''' <summary>
        ''' INFO : ByDefault : Namespaces of the assembly stayed on first level of the tree. 
        '''        Empty : Namespaces are renamed by String.Empty value and store the types into the -1 level. 
        ''' </summary>
        Enum ReplaceNamespaces
            ByDefault = 0
            Empty = 1
        End Enum

        ''' <summary>
        ''' INFO : Full : rename all types and members. 
        '''        Medium : set to false events, variables, parameters. It will set the other one automatically to True.
        '''        Personnalize : requires you to set the boolean values manually for each types and members. 
        ''' </summary>
        Enum RenameRule
            Full = 0
            Medium = 1
            Personalize = 2
        End Enum

        ''' <summary>
        ''' INFO : NotOnly : Rename all namespaces.
        '''        Only : It will maybe solve many problems due to rename namespaces of merged assembly(s) !
        ''' </summary>
        Enum RenameMainNamespace
            NotOnly = 0
            Only = 1
        End Enum

#End Region

#Region " Methods "
        Public Sub CleanUp()
            Namespaces = False
            Types = False
            Methods = False
            Properties = False
            Fields = False
            CustomAttributes = False
            Events = False
            Variables = False
            Parameters = False
        End Sub
#End Region

    End Class
End Namespace
