using System;
using System.Data.SqlClient;
using System.Linq;

namespace TimeTracker
{
    public class EmployeeManager
    {
        public static string SqlConnect = "Data Source=DESKTOP-L3JH1U1SQLEXPRESS;Initial Catalog=EMSDB;Integrated Security=True";
        static DataOperation DataOperation = new DataOperation();
        public static void EmployeeAdder()
        {
            Employee employee = new Employee();
            Console.WriteLine("İshci nomresini daxil edin: ");
            employee.EmployeeNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("İshci adını daxil edin: ");
            employee.Name = Console.ReadLine();
            Console.WriteLine("İshci soyadını daxil edin: ");
            employee.Surname = Console.ReadLine();
            Console.WriteLine("İshci vezifesini qeyd edin: ");
            employee.Position = Console.ReadLine();
            Console.WriteLine("İshcinin emek haqqi emsalini daxil edin: ");
            employee.Salary = int.Parse(Console.ReadLine());
            Console.WriteLine("Ishe girish tarixini daxil edin: ");
            employee.EntryDate = Convert.ToDateTime(Console.ReadLine());
            SqlConnection connect = new SqlConnection(SqlConnect);
            connect.Open();
            string query = "insert into Employee(EmployeeNumber,Name,Surname,EntryDate,Position,Salary) values(@EmployeeNumber,@Name,@Surname,@EntryDate,@Position,@Salary); ";
            SqlCommand cmd = new SqlCommand(query, connect);
            cmd.Parameters.AddWithValue("@EmployeeNumber", employee.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Surname", employee.Surname);
            cmd.Parameters.AddWithValue("@EntryDate", employee.EntryDate);
            cmd.Parameters.AddWithValue("@Position", employee.Position);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.ExecuteNonQuery();
            connect.Close();
        }
        public static void ReadEmployee(int? employeeNumber, int? year, bool? late, int? workday, string position)
        {
            SqlDataReader sqlDataReader;
            SqlConnection sqlConnection = new SqlConnection(SqlConnect);
            sqlConnection.Open();

            string query = "";
            if (employeeNumber != null && year == null)
            {
                query = $"select EmployeeNumber,Name,Surname,Position,Salary,EntryDate from [dbo].[Employee] where EmployeeNumber = {employeeNumber};";
            }
            else if (employeeNumber == null && late != null)
            {
                query = $"select EmployeeNumber,Name,Surname,Position,Salary,EntryDate from [dbo].[Employee] where EntryDate like '{year}%';";
            }
            else if (employeeNumber == null && workday == null && late == true)
            {

                query = $"select [Employee].EmployeeNumber, Name, Surname, Position, Salary, EntryDate from [dbo].[Employee] inner join WorkTime on Employee.EmployeeNumber = WorkTime.EmployeeNumber where WorkTime.EntryHours>9 or (WorkTime.EntryHours=9 and WorkTime.EntryMinute>0)";
            }
            else if (employeeNumber == null && year == null && late == null && workday != null)
            {
                query = $"select Employee.EmployeeNumber, Name, Surname, WorkTime.EntryHours, WorkTime.EntryMinute, WorkTime.ExitHours, WorkTime.ExitMinute from [dbo].[Employee] inner join WorkTime on Employee.EmployeeNumber = WorkTime.EmployeeNumber where WorkTime.Day = {workday} and WorkTime.ExitHours IS NOT NULL AND WorkTime.ExitMinute IS NOT NULL;";
            }
            else if (employeeNumber == null && year == null && late == null && workday == null && position != null)
            {
                query = $"select EmployeeNumber, Name, Surname, Position, Salary, EntryDate from [dbo].[Employee] where Position = '{position}';";
            }

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Employee employee = new Employee();
                WorkTime workTime = new WorkTime();

                if (workday == null)
                {
                    employee.Position = (string)sqlDataReader.GetValue(4);
                    employee.Salary = (decimal)sqlDataReader.GetValue(6);
                    employee.EntryDate = (DateTime)sqlDataReader.GetValue(3);
                }
                else
                {
                    workTime.EmployeeNumber = (int)sqlDataReader.GetValue(0);
                    workTime.EntryHours = (int)sqlDataReader.GetValue(3);
                    workTime.EntryMinute = (int)sqlDataReader.GetValue(4);
                    workTime.ExitHours = (int)sqlDataReader.GetValue(5);
                    workTime.ExitMinute = (int)sqlDataReader.GetValue(6);
                }
                employee.EmployeeNumber = (int)sqlDataReader.GetValue(0);
                employee.Name = (string)sqlDataReader.GetValue(1);
                employee.Surname = (string)sqlDataReader.GetValue(2);


                DataOperation.WorkTimes.Add(workTime);
                DataOperation.Employees.Add(employee);
            }
            sqlConnection.Close();
            foreach (var item in DataOperation.Employees)
            {
                if (year != null)
                {
                    Console.WriteLine($"___________{year} İlinə görə işçilər________");
                }
                else
                {
                    Console.WriteLine("=========================================================");
                }
                Console.WriteLine($"Ishci nomresi: {item.EmployeeNumber}");
                Console.WriteLine($"Ishci adi: {item.Name}");
                Console.WriteLine($"Ishci soyadi: {item.Surname}");
                if (workday == null)
                {
                    Console.WriteLine($"Ish sahesi: {item.Position}");
                    Console.WriteLine($"Emek haqqi: {item.Salary}");
                    Console.WriteLine($"Ishe bashlama tarixi: {item.EntryDate}");
                }
                else
                {
                    foreach (var item2 in DataOperation.WorkTimes.Where(p => p.EmployeeNumber == item.EmployeeNumber))
                    {
                        Console.WriteLine($"Girish saati {item2.EntryHours}:{item2.EntryMinute}");
                        Console.WriteLine($"Chixish saati {item2.ExitHours}:{item2.ExitMinute}");
                        Console.WriteLine($"Bir gun erzinde ishlediyi saat {item2.ExitHours - item2.EntryHours} saat {item2.ExitMinute - item2.EntryMinute} deq");
                    }
                }

                Console.WriteLine("------------");
            }
        }
        public static void UpdateEmployee()
        {
            Console.Write("Ishchi nomresini daxil edin: ");
            int number = Convert.ToInt32(Console.ReadLine());
            SqlConnection connection = new SqlConnection(SqlConnect);
            connection.Open();
            EmployeeManager.ReadEmployee(number,null, null, null, null);
            Console.Write("Yeni adi daxil edin: ");
            string name = Console.ReadLine();
            Console.Write("Yeni emek haqqi emsalini daxil edin: ");
            int WageRate = Convert.ToInt32(Console.ReadLine());
            string updateQuery = $"UPDATE [dbo].[Employee]SET [Name] = '{name}', [Salary] = {WageRate} WHERE Employee.EmployeeNumber = {number}";
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
            updateCommand.ExecuteNonQuery();
            connection.Close();
            Console.Clear();
            Console.WriteLine("________Ishchinin yeni melumatlari");
            EmployeeManager.ReadEmployee(number, null, null, null, null);
        }
        public static void DeleteEmployee()
        {
            Console.Write("Ishchinin nomresini daxil edin: ");
            int number = Convert.ToInt32(Console.ReadLine());
            EmployeeManager.ReadEmployee(number,null, null, null, null);
            Console.WriteLine("Melumatlar silinecek, eminsiniz? He/Yox");
            string result = Console.ReadLine().ToLower();

            if (result == "he")
            {
                SqlConnection connection = new SqlConnection(SqlConnect);
                connection.Open();
                string deleteQuery = $"DELETE FROM [dbo].[Employee] WHERE Employee.EmployeeNumber = {number};";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
