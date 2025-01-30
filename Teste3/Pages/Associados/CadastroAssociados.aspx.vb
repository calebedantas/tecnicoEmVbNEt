Imports System.Collections.Generic

Public Class CadastroAssociado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarEmpresas()
            If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                CarregarAssociado(Request.QueryString("id"))
            End If
        End If
    End Sub

    Private Sub CarregarEmpresas()
        Dim controller As New EmpresaController()
        lstEmpresas.DataSource = controller.ConsultarEmpresas()
        lstEmpresas.DataTextField = "Nome"
        lstEmpresas.DataValueField = "Id"
        lstEmpresas.DataBind()
    End Sub

    Private Sub CarregarAssociado(id As String)
        Dim controller As New AssociadoController()
        Dim associado As Associado = controller.ConsultarAssociadoPorId(id)
        hdnId.Value = associado.Id
        txtNome.Text = associado.Nome
        txtCpf.Text = associado.Cpf
        txtDataNascimento.Text = associado.DataNascimento.ToString("yyyy-MM-dd")
        For Each empresa In associado.Empresas
            lstEmpresas.Items.FindByValue(empresa.Id).Selected = True
        Next
    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs)
        Dim associado As New Associado()
        associado.Id = If(String.IsNullOrEmpty(hdnId.Value), 0, Convert.ToInt32(hdnId.Value))
        associado.Nome = txtNome.Text
        associado.Cpf = txtCpf.Text
        associado.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text)
        associado.Empresas = New List(Of Empresa)()
        For Each item As ListItem In lstEmpresas.Items
            If item.Selected Then
                Dim empresa As New Empresa()
                empresa.Id = Convert.ToInt32(item.Value)
                associado.Empresas.Add(empresa)
            End If
        Next

        Dim controller As New AssociadoController()
        If associado.Id = 0 Then
            controller.IncluirAssociado(associado)
        Else
            controller.AlterarAssociado(associado)
        End If

        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Default.aspx")
    End Sub
End Class