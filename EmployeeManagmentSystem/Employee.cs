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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-L3JH1U1\SQLEXPRESS;Initial Catalog=EmployeeDb;Integrated Security=True");
        private void Employee_Load(object sender, EventArgs e)
        {
            Settle();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EmpIdTb.Text == "" || EmpNameTb.Text == "" || EmpSurnameTb.Text == "" || EmpPositionTb.Text == "" || EmpWageRateTb.Text == "" || EmpHoursTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    connect.Open();
                    string query = "update EmployeeTbl set EmpName = '" + EmpNameTb.Text + "', EmpSurname = '" + EmpSurnameTb.Text + "', EmpPosition = '" + EmpPositionTb.Text + "', EmpDateOfJoin = '" + EmpDateofJoin.Value.Date + "', EmpDepartment = '" + EmpDepartmentCb.SelectedItem.ToString() + "', EmpWageRate = '" + EmpWageRateTb.Text + "', EmpHours = '" + EmpHoursTb.Text + "'where EmpId= '"+EmpIdTb.Text+"';";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Successfully Updated");
                    connect.Close();
                    Settle();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpIdTb.Text == "" || EmpNameTb.Text == "" || EmpSurnameTb.Text == "" || EmpPositionTb.Text == "" || EmpWageRateTb.Text == "" || EmpHoursTb.Text == "" )
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    connect.Open();
                    string query = "insert into EmployeeTbl values ("+EmpIdTb.Text+",'"+EmpNameTb.Text+"', '"+EmpSurnameTb.Text+"','"+EmpPositionTb.Text+"','"+EmpDateofJoin.Value.Date+"', '"+EmpDepartmentCb.SelectedItem.ToString()+"', '"+EmpWageRateTb.Text+"','"+EmpHoursTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Seccessfully Added");
                    connect.Close();
                    Settle();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show (Ex.Message);
                }
            }
        }
        private void Settle()
        {
            connect.Open();
            string query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, connect);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmpData.DataSource = ds.Tables[0];
            connect.Close();
        }

        private void EmpPositionTb_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (EmpIdTb.Text == "")
            {
                MessageBox.Show("Enter the Employee Id");
            }
            else
            {
                try
                {
                    connect.Open();
                    string query = "delete from EmployeeTbl where EmpId= '" + EmpIdTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Successfully Deleted");
                    connect.Close();
                    Settle();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EmpData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpIdTb.Text = EmpData.SelectedRows[0].Cells[0].Value.ToString();
            EmpNameTb.Text = EmpData.SelectedRows[0].Cells[1].Value.ToString();
            EmpSurnameTb.Text = EmpData.SelectedRows[0].Cells[2].Value.ToString();
            EmpPositionTb.Text = EmpData.SelectedRows[0].Cells[3].Value.ToString();
            EmpWageRateTb.Text = EmpData.SelectedRows[0].Cells[4].Value.ToString();
            EmpHoursTb.Text = EmpData.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
