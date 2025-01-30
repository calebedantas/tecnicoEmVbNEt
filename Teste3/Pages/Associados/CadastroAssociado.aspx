<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="CadastroAssociado.aspx.vb" Inherits="WebApplication1.CadastroAssociado" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Associado</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Cadastro de Associado</h1>
            <asp:HiddenField ID="hdnId" runat="server" />
            <asp:Label ID="lblNome" runat="server" Text="Nome:"></asp:Label>
            <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblCpf" runat="server" Text="CPF:"></asp:Label>
            <asp:TextBox ID="txtCpf" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblDataNascimento" runat="server" Text="Data de Nascimento:"></asp:Label>
            <asp:TextBox ID="txtDataNascimento" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblEmpresas" runat="server" Text="Empresas:"></asp:Label>
            <asp:ListBox ID="lstEmpresas" runat="server" SelectionMode="Multiple"></asp:ListBox>
            <br />
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>
    </form>
</body>
</html>