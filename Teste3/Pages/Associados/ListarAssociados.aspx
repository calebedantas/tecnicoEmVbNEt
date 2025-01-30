<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="WebApplication1._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Associados e Empresas</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Cadastro de Associados</h1>
            <asp:GridView ID="gvAssociados" runat="server" AutoGenerateColumns="False" OnRowCommand="gvAssociados_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                    <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                    <asp:BoundField DataField="DataNascimento" HeaderText="Data de Nascimento" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnNovoAssociado" runat="server" Text="Novo Associado" OnClick="btnNovoAssociado_Click" />
        </div>
        <div>
            <h1>Cadastro de Empresas</h1>
            <asp:GridView ID="gvEmpresas" runat="server" AutoGenerateColumns="False" OnRowCommand="gvEmpresas_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                    <asp:BoundField DataField="Cnpj" HeaderText="CNPJ" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnNovaEmpresa" runat="server" Text="Nova Empresa" OnClick="btnNovaEmpresa_Click" />
        </div>
    </form>
</body>
</html>