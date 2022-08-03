using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EmployeeManagmentSystem
{
    public partial class WorkTime : Form
    {
        public WorkTime()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-L3JH1U1\SQLEXPRESS;Initial Catalog=EmployeeDb;Integrated Security=True");
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpIdTb.Text == "" || DayOfMonthTb.Text == "" || EntryHoursTb.Text == "" || EntryMinuteTb.Text == "" || ExitHoursTb.Text == "" || ExitMinuteTb.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    connect.Open();
                    string queryString = "INSERT INTO WorkTimeTbl(EmpId, DayOfMonth, EntryHours, EntryMinute, ExitHours, ExitMinute) VALUES ("+EmpIdTb.Text+",'"+DayOfMonthTb.Text+"', '"+EntryHoursTb.Text+"', '"+EntryMinuteTb.Text+"', '"+ExitHoursTb.Text+"', '"+ExitMinuteTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(queryString, connect);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("WorkTime Seccessfully Added");
                    connect.Close();
                    Settle2();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Settle2()
        {
            connect.Open();
            string query = "select * from WorkTimeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, connect);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var dataSet = new DataSet();
            sda.Fill(dataSet);
            WorkTimeData.DataSource = dataSet.Tables[0];
            connect.Close();
        }
        private void WorkTime_Load(object sender, EventArgs e)
        {
            Settle2();
        }
        //public void TotalWorkedHours()
        //{
        //    int StarHour = int.Parse(EntryHoursTb.Text);
        //    int StartMinute = int.Parse(EntryMinuteTb.Text);
        //    int EndHour = int.Parse(ExitHoursTb.Text);
        //    connect.Open();
        //    string query = "select EntryHours, EntryMinute, ExitHours from WorkTimeTbl";
        //    SqlCommand cmd = new SqlCommand(query, connect);
        //    cmd.Parameters.Add("@EntryHours", SqlDbType.Int);
        //    cmd.Parameters["@EntryHours"].Value = StarHour;
        //    cmd.Parameters.Add("@EntryMinute", SqlDbType.Int);
        //    cmd.Parameters["@EntryMinute"].Value = StartMinute;
        //    cmd.Parameters.Add("@ExitHours", SqlDbType.Int);
        //    cmd.Parameters["@ExitHours"].Value = EndHour;
        //    connect.Close();
        //    if (StarHour > 9 || StartMinute > 0)
        //    {
        //        StarHour++;
        //    }
        //    int TotalTime = EndHour - StarHour;
            
        //}
        private void WorkTimeData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpIdTb.Text = WorkTimeData.SelectedRows[0].Cells[0].Value.ToString();
            DayOfMonthTb.Text = WorkTimeData.SelectedRows[0].Cells[0].Value.ToString();
            EntryHoursTb.Text = WorkTimeData.SelectedRows[0].Cells[1].Value.ToString();
            EntryMinuteTb.Text = WorkTimeData.SelectedRows[0].Cells[1].Value.ToString();
            ExitHoursTb.Text = WorkTimeData.SelectedRows[0].Cells[2].Value.ToString();
            ExitMinuteTb.Text = WorkTimeData.SelectedRows[0].Cells[1].Value.ToString();
        }
       
        private void EntryHoursTb_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void EntryMinuteTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
