<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="CadastroEmpresas.aspx.vb" Inherits="WebApplication1.CadastroEmpresas" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Empresa</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Cadastro de Empresa</h1>
            <asp:HiddenField ID="hdnId" runat="server" />
            <asp:Label ID="lblNome" runat="server" Text="Nome:"></asp:Label>
            <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblCnpj" runat="server" Text="CNPJ:"></asp:Label>
            <asp:TextBox ID="txtCnpj" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblAssociados" runat="server" Text="Associados:"></asp:Label>
            <asp:ListBox ID="lstAssociados" runat="server" SelectionMode="Multiple"></asp:ListBox>
            <br />
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>
    </form>
</body>
</html>