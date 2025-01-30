Imports System.Collections.Generic

Public Class CadastroEmpresas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarAssociados()
            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                CarregarEmpresa(Request.QueryString("id"))
            End If
        End If
    End Sub

    Private Sub CarregarAssociados()
        Dim controller As New AssociadoController()
        lstAssociados.DataSource = controller.ConsultarAssociados()
        lstAssociados.DataTextField = "Nome"
        lstAssociados.DataValueField = "Id"
        lstAssociados.DataBind()
    End Sub

    Private Sub CarregarEmpresa(id As String)
        Dim controller As New EmpresaController()
        Dim empresa As Empresa = controller.ConsultarEmpresaPorId(id)
        hdnId.Value = empresa.Id
        txtNome.Text = empresa.Nome
        txtCnpj.Text = empresa.Cnpj
        For Each associado In empresa.Associados
            lstAssociados.Items.FindByValue(associado.Id).Selected = True
        Next
    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs)
        Dim empresa As New Empresa()
        empresa.Id = If(String.IsNullOrEmpty(hdnId.Value), 0, Convert.ToInt32(hdnId.Value))
        empresa.Nome = txtNome.Text
        empresa.Cnpj = txtCnpj.Text
        empresa.Associados = New List(Of Associado)()
        For Each item As ListItem In lstAssociados.Items
            If item.Selected Then
                Dim associado As New Associado()
                associado.Id = Convert.ToInt32(item.Value)
                empresa.Associados.Add(associado)
            End If
        Next

        Dim controller As New EmpresaController()
        If empresa.Id = 0 Then
            controller.IncluirEmpresa(empresa)
        Else
            controller.AlterarEmpresa(empresa)
        End If

        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Default.aspx")
    End Sub
End Class