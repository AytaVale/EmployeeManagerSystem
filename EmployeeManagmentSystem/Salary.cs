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
    public partial class Salary : Form
    {
        public Salary()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-L3JH1U1\SQLEXPRESS;Initial Catalog=EmployeeDb;Integrated Security=True");

        public object EntryHoursTb { get; private set; }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
        private void GetDataFromEmp()
        {
            if (EmpIdTb.Text == "")
            {
                MessageBox.Show("Enter Employee Id");
            }
            else
            {
                try
                {
                    connect.Open();
                    string query = "select * from EmployeeTbl where EmpId = '" + EmpIdTb.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        EmpIdTb.Text = dr["EmpId"].ToString();
                        EmpNameTb.Text = dr["EmpName"].ToString();
                        EmpSurnameTb.Text = dr["EmpSurname"].ToString();
                        connect.Close();
                        GetDataFromWork();
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Employee with this Id is not founded");
                }
            }
        }
        private void GetDataFromWork()
        {
               connect.Open();
                string query = "SELECT COUNT(DayofMonth) as EmpHours FROM WorkTimeTbl WHERE EmpId = '" + EmpIdTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, connect);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                EmpHoursTb.Text = dr["EmpHours"].ToString();
                connect.Close();
                }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetDataFromEmp();
        }

        private void EmpIdlTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void EmpHoursTb_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void EmpNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void SalarySlip_TextChanged(object sender, EventArgs e)
        {
            /*maaş hesablama metodunda erroru həll edə bilmədim...*/
        }

        private void Salary_Load(object sender, EventArgs e)
        {

        }
    }
}