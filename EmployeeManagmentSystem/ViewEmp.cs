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
    public partial class ViewEmp : Form
    {
        public ViewEmp()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-L3JH1U1\SQLEXPRESS;Initial Catalog=EmployeeDb;Integrated Security=True");
        private void GetData()
        {
            try
            {
                connect.Open();
            string query = "select * from EmployeeTbl where EmpId = '" + EmpIdTb.Text + "'";
            SqlCommand cmd = new SqlCommand(query,connect);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    EmpIdlbl.Text = dr["EmpId"].ToString();
                    EmpNamelbl.Text = dr["EmpName"].ToString();
                    EmpSurnamelbl.Text = dr["EmpSurname"].ToString();
                    EmpPositionlbl.Text = dr["EmpPosition"].ToString();
                    EmpDateofJoinlbl.Text = dr["EmpDateOfJoin"].ToString();
                    EmpDateofJoinlbl.Text = dr["EmpDepartment"].ToString();
                    empWageRatelbl.Text = dr["EmpWageRate"].ToString();
                    EmpHourslbl.Text = dr["EmpHours"].ToString();
                    EmpIdlbl.Visible = true;
                    EmpNamelbl.Visible = true;
                    EmpSurnamelbl.Visible = true;
                    EmpPositionlbl.Visible = true;
                    EmpDateofJoinlbl.Visible = true;
                    EmpDateofJoinlbl.Visible = true;
                    empWageRatelbl.Visible = true;
                    EmpHourslbl.Visible = true;
                    connect.Close();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Employee with this Id is not founded");
            }
            
            
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void ViewEmp_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
