Imports System.Data.SqlClient

Public Class EmpresaDAL
    Public Shared Function Inserir(empresa As Empresa) As Integer
        Dim query As String = "INSERT INTO Empresa (Nome, Cnpj) OUTPUT INSERTED.Id VALUES (@Nome, @Cnpj)"
        Using conn As SqlConnection = DatabaseHelper.GetConnection()
            Using cmd As SqlCommand = New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Nome", empresa.Nome)
                cmd.Parameters.AddWithValue("@Cnpj", empresa.Cnpj)

                conn.Open()
                Dim id As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return id
            End Using
        End Using
    End Function

    Public Shared Function Listar() As List(Of Empresa)
        Dim lista As New List(Of Empresa)()
        Dim query As String = "SELECT * FROM Empresa"

        Using conn As SqlConnection = DatabaseHelper.GetConnection()
            Using cmd As SqlCommand = New SqlCommand(query, conn)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim empresa As New Empresa With {
                            .Id = Convert.ToInt32(reader("Id")),
                            .Nome = reader("Nome").ToString(),
                            .Cnpj = reader("Cnpj").ToString()
                        }
                        lista.Add(empresa)
                    End While
                End Using
            End Using
        End Using
        Return lista
    End Function

    Public Shared Sub Excluir(id As Integer)
        Dim query As String = "DELETE FROM Empresa WHERE Id = @Id"
        Using conn As SqlConnection = DatabaseHelper.GetConnection()
            Using cmd As SqlCommand = New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Id", id)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class
