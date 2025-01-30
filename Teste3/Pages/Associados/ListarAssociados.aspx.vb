Partial Class ListarAssociados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            gvAssociados.DataSource = AssociadoDAL.Listar()
            gvAssociados.DataBind()
        End If
    End Sub
End Class
