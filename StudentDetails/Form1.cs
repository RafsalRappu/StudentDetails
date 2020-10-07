using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentDetails
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                                        //SAVE DATA

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-F842VI4\SQLEXPRESS; Database = StudentInformation; Trusted_Connection = True");
            con.Open();

            SqlCommand cmd = new SqlCommand("StudentSave", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", txtSaveName.Text);
            cmd.Parameters.AddWithValue("@Mobile", txtSaveMobile.Text);
            cmd.Parameters.AddWithValue("@Email", txtSaveEmail.Text);
            cmd.Parameters.AddWithValue("@Grade", txtSaveGrade.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Your Data Saved Successfully", "Student Information-Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
                                    //GET DATA

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-F842VI4\SQLEXPRESS; Database = StudentInformation; Trusted_Connection = True");
            con.Open();

            SqlCommand cmd = new SqlCommand("StudentGet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RollNo", txtGetRollNo.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblGetName.Text = reader["Name"].ToString();
                lblGetMobile.Text = reader["Mobile"].ToString();
                lblGetEmail.Text = reader["Email"].ToString();
                lblGetGrade.Text = reader["Grade"].ToString();
            }
            else
            {
                MessageBox.Show("No Data To Display", "Student Information-Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                                       //DELETE DATA

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-F842VI4\SQLEXPRESS; Database = StudentInformation; Trusted_Connection = True");
            con.Open();

            SqlCommand cmd = new SqlCommand("StudentGet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RollNo", txtDelete.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Deleted", "Student Information-Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lstBoxShowData.Items.Remove(txtDelete.Text);
            lstboxNames.Items.Remove(txtDelete.Text);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
                                       //UPDATE DATA

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-F842VI4\SQLEXPRESS; Database = StudentInformation; Trusted_Connection = True");
            con.Open();

            SqlCommand cmd = new SqlCommand("StudentUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RollNo", txtUpdateRollNo.Text);
            cmd.Parameters.AddWithValue("@Name", txtUpdateName.Text);
            cmd.Parameters.AddWithValue("@Email", txtUpdateEmail.Text);
            cmd.Parameters.AddWithValue("@Mobile", txtUpdateMobile.Text);
            cmd.Parameters.AddWithValue("@Grade", txtUpdateGrade.Text);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Updated", "Student Information-Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            {
                SqlConnection con1 = new SqlConnection(@"Data Source = DESKTOP-F842VI4\SQLEXPRESS; Database = StudentInformation; Trusted_Connection = True");
                con1.Open();


                SqlCommand cmd1 = new SqlCommand("Select RollNo From StudentInfo", con1);
                SqlDataReader reader1 = cmd1.ExecuteReader();

                while (reader1.Read())
                {
                    String Id = reader1["RollNo"].ToString();
                    lstBoxShowData.Items.Add(Id);
                }
                reader1.Close();
                SqlCommand cmd = new SqlCommand("Select Name From StudentInfo", con1);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    String Name = reader["Name"].ToString();
                    lstboxNames.Items.Add(Name);
                }

                con1.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSaveEmail.Text = txtSaveGrade.Text = txtSaveMobile.Text = txtSaveName.Text = "";
        }

        private void lstBoxShowData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGetClear_Click(object sender, EventArgs e)
        {
            txtUpdateEmail.Text = txtUpdateGrade.Text = txtUpdateMobile.Text = txtUpdateName.Text = txtUpdateRollNo.Text = "";
        }

        private void btnShowClear_Click(object sender, EventArgs e)
        {
            lblGetEmail.Text = "Email :";
            lblGetGrade.Text = "Grade :";
            lblGetMobile.Text = "Mobile :";
            lblGetName.Text = "Name :";
            txtGetRollNo.Text = "";
        }
    }
}
