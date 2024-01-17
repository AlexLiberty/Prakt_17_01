using Microsoft.Data.SqlClient;


string connectionString = "Server=LIBERTY; Database=Office; Trusted_Connection=True; TrustServerCertificate=True; Integrated Security=True;";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    //Department(connection, 1);
    //LengthAndSalary(connection, 2, 10000);
    //MinSalary(connection);
    //IncreaseSalaryForDepartment(connection, 1, 5000);
    AddNewColumnToWorkerTable(connection, "NewColumn");
}
    

   static void Department(SqlConnection connection, int departmentId)
{
    string query = $"SELECT * FROM Worker WHERE DepartmentId = {departmentId}";

    using (SqlCommand command = new SqlCommand(query, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            Console.WriteLine("Employees in Department {0}:", departmentId);
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1} - {2} - {3}", reader["id"], reader["Name"], reader["Position"], reader["Salary"]);
            }
        }
    }
}

static void LengthAndSalary(SqlConnection connection, int nameLength, decimal minSalary)
{
    string query = $"SELECT * FROM Worker WHERE LEN(Name) = {nameLength} AND Salary >= {minSalary}";

    using (SqlCommand command = new SqlCommand(query, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            Console.WriteLine("Employees with Name Length {0} and Salary >= {1}:", nameLength, minSalary);
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1} - {2} - {3}", reader["id"], reader["Name"], reader["Position"], reader["Salary"]);
            }
        }
    }
}

static void MinSalary(SqlConnection connection)
{
    string query = "SELECT TOP 1 * FROM Worker ORDER BY Salary";

    using (SqlCommand command = new SqlCommand(query, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            Console.WriteLine("Employee with Min Salary:");
            while (reader.Read())
            {
                Console.WriteLine("{0} - {1} - {2} - {3}", reader["id"], reader["Name"], reader["Position"], reader["Salary"]);
            }
        }
    }
}

static void IncreaseSalaryForDepartment(SqlConnection connection, int departmentId, decimal increaseAmount)
{
    string query;

    if (departmentId == 0)
    {
        query = $"UPDATE Worker SET Salary = Salary + {increaseAmount}";
    }
    else
    {
        query = $"UPDATE Worker SET Salary = Salary + {increaseAmount} WHERE DepartmentId = {departmentId}";
    }

    using (SqlCommand command = new SqlCommand(query, connection))
    {
        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine("{0} rows updated.", rowsAffected);
    }
}

static void AddNewColumnToWorkerTable(SqlConnection connection, string newColumnName)
{
    string query = $"ALTER TABLE Worker ADD {newColumnName} VARCHAR(255) NULL";

    using (SqlCommand command = new SqlCommand(query, connection))
    {
        command.ExecuteNonQuery();
        Console.WriteLine("New column added to Worker table.");
    }
}
