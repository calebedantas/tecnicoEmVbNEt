Imports System.Collections.Generic

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarAssociados()
            CarregarEmpresas()
        End If
    End Sub

    Private Sub CarregarAssociados()
        Dim controller As New AssociadoController()
        gvAssociados.DataSource = controller.ConsultarAssociados()
        gvAssociados.DataBind()
    End Sub

    Private Sub CarregarEmpresas()
        Dim controller As New EmpresaController()
        gvEmpresas.DataSource = controller.ConsultarEmpresas()
        gvEmpresas.DataBind()
    End Sub

    Protected Sub btnNovoAssociado_Click(sender As Object, e As EventArgs)
        Response.Redirect("CadastroAssociado.aspx")
    End Sub

    Protected Sub btnNovaEmpresa_Click(sender As Object, e As EventArgs)
        Response.Redirect("CadastroEmpresa.aspx")
    End Sub

    Protected Sub gvAssociados_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Editar" Then
            Response.Redirect("CadastroAssociado.aspx?id=" & e.CommandArgument)
        ElseIf e.CommandName = "Excluir" Then
            Dim controller As New AssociadoController()
            controller.ExcluirAssociado(e.CommandArgument)
            CarregarAssociados()
        End If
    End Sub

    Protected Sub gvEmpresas_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Editar" Then
            Response.Redirect("CadastroEmpresa.aspx?id=" & e.CommandArgument)
        ElseIf e.CommandName = "Excluir" Then
            Dim controller As New EmpresaController()
            controller.ExcluirEmpresa(e.CommandArgument)
            CarregarEmpresas()
        End If
    End Sub
End Class