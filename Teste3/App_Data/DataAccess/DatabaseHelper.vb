Imports System.Data.SqlClient

Public Class DatabaseHelper
    Private Shared ReadOnly ConnectionString As String = "Data Source=TESTE;Initial Catalog=TESTE;Integrated Security=True"

    Public Shared Function GetConnection() As SqlConnection
        Return New SqlConnection(ConnectionString)
    End Function
End Class
