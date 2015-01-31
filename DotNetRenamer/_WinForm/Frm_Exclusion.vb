Imports System.IO
Imports DotNetRenamer.Implementer
Imports DotNetRenamer.Implementer.Exclusion
Imports System.ComponentModel

Public Class Frm_Exclusion

#Region " Fields "
    Private _FilePath$ = String.Empty
    Private _excludeList As Cls_ExcludeList
#End Region

#Region " Events "
    Public Event OnShowingExclusionInfos As ShowingExclusionInfosDelegate
#End Region

#Region " Delegates "
    Public Delegate Sub ShowingExclusionInfosDelegate(e As Cls_ExcludeList)
#End Region

#Region " Constructor "
    Sub New(FilePath$)
        InitializeComponent()
        _FilePath = FilePath
        If Not BgwExclusion.IsBusy Then BgwExclusion.RunWorkerAsync()
    End Sub
#End Region

#Region " Methods "
    Public Sub InitializeExcludeList()
        _excludeList = New Cls_ExcludeList
        RaiseEvent OnShowingExclusionInfos(_excludeList)
    End Sub
    Public Sub FinalizeExcludeList()
        _excludeList.CleanUp()
        RaiseEvent OnShowingExclusionInfos(_excludeList)
    End Sub

    Private Sub Frm_Exclusion_FormClosing(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RaiseEvent OnShowingExclusionInfos(_excludeList)
    End Sub

    Private Sub BgwExclusion_DoWork(sender As Object, e As DoWorkEventArgs) Handles BgwExclusion.DoWork
        e.Result = Cls_Exclusion.LoadTreeNode(_FilePath)
    End Sub

    Private Sub BgwExclusion_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BgwExclusion.RunWorkerCompleted
        If Not e.Result Is Nothing Then TvExclusion.Nodes.Add(e.Result)
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TvExclusion.AfterSelect
        If Not Cls_Exclusion.isRenamable(e.Node.Tag) Then
            ChbExclusion.Checked = False
            ChbAllEntities.Checked = False
            GbxExclusion.Enabled = False
        ElseIf (Not e.Node.Tag Is Nothing) Then
            ChbExclusion.Checked = Cls_Exclusion.isExclude(e.Node.Tag)
            ChbAllEntities.Checked = Cls_Exclusion.getEntitiesVal(e.Node.Tag)
            GbxExclusion.Enabled = Cls_Exclusion.isTypedef(e.Node.Tag)
            ChbAllEntities.Enabled = ChbExclusion.Checked
        Else
            GbxExclusion.Enabled = False
            ChbExclusion.Checked = False
        End If
    End Sub

    Private Sub IncludeAllChildNodes(treeNode As TreeNode, nodeChecked As Boolean)
        For Each node As TreeNode In treeNode.Nodes
            If Cls_Exclusion.isRenamable(node.Tag) Then
                node.Tag.Exclude = nodeChecked
                ColorNode(node, nodeChecked)
                If node.Nodes.Count > 0 Then
                    IncludeAllChildNodes(node, nodeChecked)
                End If
            End If
        Next
    End Sub

    Private Sub IncludeEntitiesChildNodes(treeNode As TreeNode, nodeChecked As Boolean)
        For Each node As TreeNode In treeNode.Nodes
            If Cls_Exclusion.isRenamable(node.Tag) Then
                node.Tag.Exclude = nodeChecked
                node.Tag.AllEntities = nodeChecked
                ColorNode(node, nodeChecked)
                If node.Nodes.Count > 0 Then
                    IncludeEntitiesChildNodes(node, nodeChecked)
                End If
            End If
        Next
    End Sub

    Private Sub ChbAllEntities_Click(sender As Object, e As EventArgs) Handles ChbAllEntities.Click
        If (Not Me.TvExclusion.SelectedNode.Tag Is Nothing) Then
            TvExclusion.SelectedNode.Tag.AllEntities = ChbAllEntities.Checked
            IncludeEntitiesChildNodes(TvExclusion.SelectedNode, ChbAllEntities.Checked)
        End If
    End Sub

    Private Sub ChbExclusion_Click(sender As Object, e As EventArgs) Handles ChbExclusion.Click
        If (Not Me.TvExclusion.SelectedNode.Tag Is Nothing) Then
            TvExclusion.SelectedNode.Tag.exclude = ChbExclusion.Checked
            ColorNode(TvExclusion.SelectedNode, ChbExclusion.Checked)
        End If
        If ChbExclusion.Checked = True Then
            ChbAllEntities.Enabled = True
            IncludeAllChildNodes(TvExclusion.SelectedNode, ChbAllEntities.Checked)
        Else
            ChbAllEntities.Enabled = False
            ColorNode(TvExclusion.SelectedNode, False)
            IncludeAllChildNodes(TvExclusion.SelectedNode, False)
            IncludeEntitiesChildNodes(TvExclusion.SelectedNode, False)
            ChbAllEntities.Checked = False
            'TvExclusion.SelectedNode.Tag.AllEntities = False
        End If
    End Sub

    Private Sub ColorNode(Node As TreeNode, Checked As Boolean)
        If Checked = True Then
            Node.ForeColor = Color.Red
            _excludeList.AddTo(Node.Tag)
        Else
            Node.ForeColor = Color.Black
            _excludeList.RemoveFrom(Node.Tag)
        End If
        LblExcluded.Text = _excludeList.itemsCount.ToString & " items excluded"
    End Sub
#End Region
  
End Class