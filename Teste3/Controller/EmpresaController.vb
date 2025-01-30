Imports System.Data.SqlClient

Public Class EmpresaController
    Private connectionString As String = "String_De_Conexao"

    Public Function IncluirEmpresa(empresa As Empresa) As Boolean
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim transaction As SqlTransaction = conn.BeginTransaction()
            Try
                Dim cmd As New SqlCommand("INSERT INTO Empresas (Nome, Cnpj) VALUES (@Nome, @Cnpj); SELECT SCOPE_IDENTITY();", conn, transaction)
                cmd.Parameters.AddWithValue("@Nome", empresa.Nome)
                cmd.Parameters.AddWithValue("@Cnpj", empresa.Cnpj)
                Dim empresaId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                For Each associado In empresa.Associados
                    cmd = New SqlCommand("INSERT INTO AssociadosEmpresas (EmpresaId, AssociadoId) VALUES (@EmpresaId, @AssociadoId)", conn, transaction)
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId)
                    cmd.Parameters.AddWithValue("@AssociadoId", associado.Id)
                    cmd.ExecuteNonQuery()
                Next

                transaction.Commit()
                Return True
            Catch ex As Exception
                transaction.Rollback()
                Throw ex
            End Try
        End Using
    End Function

    Public Function AlterarEmpresa(empresa As Empresa) As Boolean
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim transaction As SqlTransaction = conn.BeginTransaction()
            Try
                Dim cmd As New SqlCommand("UPDATE Empresas SET Nome = @Nome, Cnpj = @Cnpj WHERE Id = @Id", conn, transaction)
                cmd.Parameters.AddWithValue("@Id", empresa.Id)
                cmd.Parameters.AddWithValue("@Nome", empresa.Nome)
                cmd.Parameters.AddWithValue("@Cnpj", empresa.Cnpj)
                cmd.ExecuteNonQuery()

                cmd = New SqlCommand("DELETE FROM AssociadosEmpresas WHERE EmpresaId = @EmpresaId", conn, transaction)
                cmd.Parameters.AddWithValue("@EmpresaId", empresa.Id)
                cmd.ExecuteNonQuery()

                For Each associado In empresa.Associados
                    cmd = New SqlCommand("INSERT INTO AssociadosEmpresas (EmpresaId, AssociadoId) VALUES (@EmpresaId, @AssociadoId)", conn, transaction)
                    cmd.Parameters.AddWithValue("@EmpresaId", empresa.Id)
                    cmd.Parameters.AddWithValue("@AssociadoId", associado.Id)
                    cmd.ExecuteNonQuery()
                Next

                transaction.Commit()
                Return True
            Catch ex As Exception
                transaction.Rollback()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ConsultarEmpresaPorId(id As Integer) As Empresa
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Empresas WHERE Id = @Id", conn)
            cmd.Parameters.AddWithValue("@Id", id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                Dim empresa As New Empresa()
                empresa.Id = reader("Id")
                empresa.Nome = reader("Nome")
                empresa.Cnpj = reader("Cnpj")
                empresa.Associados = ConsultarAssociadosPorEmpresaId(empresa.Id)
                Return empresa
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Function ConsultarEmpresas() As List(Of Empresa)
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Empresas", conn)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            Dim empresas As New List(Of Empresa)()
            While reader.Read()
                Dim empresa As New Empresa()
                empresa.Id = reader("Id")
                empresa.Nome = reader("Nome")
                empresa.Cnpj = reader("Cnpj")
                empresa.Associados = ConsultarAssociadosPorEmpresaId(empresa.Id)
                empresas.Add(empresa)
            End While
            Return empresas
        End Using
    End Function

    Public Function ExcluirEmpresa(id As Integer) As Boolean
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim transaction As SqlTransaction = conn.BeginTransaction()
            Try
                Dim cmd As New SqlCommand("DELETE FROM AssociadosEmpresas WHERE EmpresaId = @EmpresaId", conn, transaction)
                cmd.Parameters.AddWithValue("@EmpresaId", id)
                cmd.ExecuteNonQuery()

                cmd = New SqlCommand("DELETE FROM Empresas WHERE Id = @Id", conn, transaction)
                cmd.Parameters.AddWithValue("@Id", id)
                cmd.ExecuteNonQuery()

                transaction.Commit()
                Return True
            Catch ex As Exception
                transaction.Rollback()
                Throw ex
            End Try
        End Using
    End Function

    Private Function ConsultarAssociadosPorEmpresaId(empresaId As Integer) As List(Of Associado)
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT a.* FROM Associados a INNER JOIN AssociadosEmpresas ae ON a.Id = ae.AssociadoId WHERE ae.EmpresaId = @EmpresaId", conn)
            cmd.Parameters.AddWithValue("@EmpresaId", empresaId)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            Dim associados As New List(Of Associado)()
            While reader.Read()
                Dim associado As New Associado()
                associado.Id = reader("Id")
                associado.Nome = reader("Nome")
                associado.Cpf = reader("Cpf")
                associado.DataNascimento = reader("DataNascimento")
                associados.Add(associado)
            End While
            Return associados
        End Using
    End Function
End Class