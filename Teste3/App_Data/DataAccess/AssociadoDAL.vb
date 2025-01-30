Imports System.Data.SqlClient

Public Class AssociadoDAL
    Public Shared Function Inserir(associado As Associado) As Integer
        Dim query As String = "INSERT INTO Associado (Nome, Cpf, DataNascimento) OUTPUT INSERTED.Id VALUES (@Nome, @Cpf, @DataNascimento)"
        Using conn As SqlConnection = DatabaseHelper.GetConnection()
            Using cmd As SqlCommand = New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Nome", associado.Nome)
                cmd.Parameters.AddWithValue("@Cpf", associado.Cpf)
                cmd.Parameters.AddWithValue("@DataNascimento", If(associado.DataNascimento, DBNull.Value))

                conn.Open()
                Dim id As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return id
            End Using
        End Using
    End Function

    Public Shared Function Listar() As List(Of Associado)
        Dim lista As New List(Of Associado)()
        Dim query As String = "SELECT * FROM Associado"

        Using conn As SqlConnection = DatabaseHelper.GetConnection()
            Using cmd As SqlCommand = New SqlCommand(query, conn)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim associado As New Associado With {
                            .Id = Convert.ToInt32(reader("Id")),
                            .Nome = reader("Nome").ToString(),
                            .Cpf = reader("Cpf").ToString(),
                            .DataNascimento = If(IsDBNull(reader("DataNascimento")), Nothing, Convert.ToDateTime(reader("DataNascimento")))
                        }
                        lista.Add(associado)
                    End While
                End Using
            End Using
        End Using
        Return lista
    End Function

    Public Shared Sub Excluir(id As Integer)
        Dim query As String = "DELETE FROM Associado WHERE Id = @Id"
        Using conn As SqlConnection = DatabaseHelper.GetConnection()
            Using cmd As SqlCommand = New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Id", id)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class
