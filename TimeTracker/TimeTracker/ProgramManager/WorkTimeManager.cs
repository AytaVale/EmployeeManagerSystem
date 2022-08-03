using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker
{
    public static class WorkTimeManager
    {
        public static string SqlConnect = "Data Source=DESKTOP-L3JH1U1SQLEXPRESS;Initial Catalog=EMSDB;Integrated Security=True";
        static DataOperation DataOperation = new DataOperation();
        public static void AddWorkTime()
        {
            WorkTime workTime = new WorkTime();
            Console.WriteLine("Ishchi nomresini daxil edin: ");
            workTime.EmployeeNumber = Convert.ToInt32(Console.ReadLine());
            SqlConnection connection = new SqlConnection(SqlConnect);
            connection.Open();
            Console.WriteLine("Ish gunu");
            workTime.Day = DateTime.Now.Day;
            Console.WriteLine("İşə giriş saati:");
            workTime.EntryHours = DateTime.Now.Hour;
            Console.WriteLine("İşə giriş deqiqesi: ");
            workTime.EntryMinute = DateTime.Now.Minute;
            string insertQuery = "insert into [dbo].[WorkTime] ([EmployeeNumber], [Day], [EntryHours], [EntryMinute]" +" values(@EmployeeNumber, @Day, @EntryHours, @EntryMinute)";

            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@EmployeeNumber", workTime.EmployeeNumber);
            insertCommand.Parameters.AddWithValue("@Day", workTime.Day);
            insertCommand.Parameters.AddWithValue("@EntryHours", workTime.EntryHours);
            insertCommand.Parameters.AddWithValue("@EntryMinute", workTime.EntryMinute);
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }
        public static void ShowWorkTime()
        {
            SqlConnection connection = new SqlConnection(SqlConnect);
            connection.Open();
            WorkTime workTime = new WorkTime();
            Console.WriteLine("Ishchi nomresini daxil edin: ");
            workTime.EmployeeNumber = Convert.ToInt32(Console.ReadLine());
            workTime.Day = DateTime.Now.Day;
            Console.WriteLine("İşdən çıxış saatını:");
            workTime.ExitHours = DateTime.Now.Hour;
            Console.WriteLine("İşdən çıxış dəqiqəsini: ");
            workTime.ExitMinute = DateTime.Now.Minute;
            string updateQuery = $"UPDATE [dbo].[WorkTime] SET  [ExitHours] = {workTime.ExitHours}, [ExitMinute] = {workTime.ExitMinute} WHERE [EmployeeNumber] = {workTime.EmployeeNumber}";
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
            updateCommand.ExecuteNonQuery();
            connection.Close();
        }
    }
}
