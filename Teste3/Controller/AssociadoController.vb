Imports System.Data.SqlClient

Public Class AssociadoController
    Private connectionString As String = "String_De_Conexao"

    Public Function IncluirAssociado(associado As Associado) As Boolean
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim transaction As SqlTransaction = conn.BeginTransaction()
            Try
                Dim cmd As New SqlCommand("INSERT INTO Associados (Nome, Cpf, DataNascimento) VALUES (@Nome, @Cpf, @DataNascimento); SELECT SCOPE_IDENTITY();", conn, transaction)
                cmd.Parameters.AddWithValue("@Nome", associado.Nome)
                cmd.Parameters.AddWithValue("@Cpf", associado.Cpf)
                cmd.Parameters.AddWithValue("@DataNascimento", associado.DataNascimento)
                Dim associadoId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                For Each empresa In associado.Empresas
                    cmd = New SqlCommand("INSERT INTO AssociadosEmpresas (AssociadoId, EmpresaId) VALUES (@AssociadoId, @EmpresaId)", conn, transaction)
                    cmd.Parameters.AddWithValue("@AssociadoId", associadoId)
                    cmd.Parameters.AddWithValue("@EmpresaId", empresa.Id)
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

    Public Function AlterarAssociado(associado As Associado) As Boolean
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim transaction As SqlTransaction = conn.BeginTransaction()
            Try
                Dim cmd As New SqlCommand("UPDATE Associados SET Nome = @Nome, Cpf = @Cpf, DataNascimento = @DataNascimento WHERE Id = @Id", conn, transaction)
                cmd.Parameters.AddWithValue("@Id", associado.Id)
                cmd.Parameters.AddWithValue("@Nome", associado.Nome)
                cmd.Parameters.AddWithValue("@Cpf", associado.Cpf)
                cmd.Parameters.AddWithValue("@DataNascimento", associado.DataNascimento)
                cmd.ExecuteNonQuery()

                cmd = New SqlCommand("DELETE FROM AssociadosEmpresas WHERE AssociadoId = @AssociadoId", conn, transaction)
                cmd.Parameters.AddWithValue("@AssociadoId", associado.Id)
                cmd.ExecuteNonQuery()

                For Each empresa In associado.Empresas
                    cmd = New SqlCommand("INSERT INTO AssociadosEmpresas (AssociadoId, EmpresaId) VALUES (@AssociadoId, @EmpresaId)", conn, transaction)
                    cmd.Parameters.AddWithValue("@AssociadoId", associado.Id)
                    cmd.Parameters.AddWithValue("@EmpresaId", empresa.Id)
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

    Public Function ConsultarAssociadoPorId(id As Integer) As Associado
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Associados WHERE Id = @Id", conn)
            cmd.Parameters.AddWithValue("@Id", id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                Dim associado As New Associado()
                associado.Id = reader("Id")
                associado.Nome = reader("Nome")
                associado.Cpf = reader("Cpf")
                associado.DataNascimento = reader("DataNascimento")
                associado.Empresas = ConsultarEmpresasPorAssociadoId(associado.Id)
                Return associado
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Function ConsultarAssociados() As List(Of Associado)
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Associados", conn)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            Dim associados As New List(Of Associado)()
            While reader.Read()
                Dim associado As New Associado()
                associado.Id = reader("Id")
                associado.Nome = reader("Nome")
                associado.Cpf = reader("Cpf")
                associado.DataNascimento = reader("DataNascimento")
                associado.Empresas = ConsultarEmpresasPorAssociadoId(associado.Id)
                associados.Add(associado)
            End While
            Return associados
        End Using
    End Function

    Public Function ExcluirAssociado(id As Integer) As Boolean
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim transaction As SqlTransaction = conn.BeginTransaction()
            Try
                Dim cmd As New SqlCommand("DELETE FROM AssociadosEmpresas WHERE AssociadoId = @AssociadoId", conn, transaction)
                cmd.Parameters.AddWithValue("@AssociadoId", id)
                cmd.ExecuteNonQuery()

                cmd = New SqlCommand("DELETE FROM Associados WHERE Id = @Id", conn, transaction)
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

    Private Function ConsultarEmpresasPorAssociadoId(associadoId As Integer) As List(Of Empresa)
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT e.* FROM Empresas e INNER JOIN AssociadosEmpresas ae ON e.Id = ae.EmpresaId WHERE ae.AssociadoId = @AssociadoId", conn)
            cmd.Parameters.AddWithValue("@AssociadoId", associadoId)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            Dim empresas As New List(Of Empresa)()
            While reader.Read()
                Dim empresa As New Empresa()
                empresa.Id = reader("Id")
                empresa.Nome = reader("Nome")
                empresa.Cnpj = reader("Cnpj")
                empresas.Add(empresa)
            End While
            Return empresas
        End Using
    End Function
End Class