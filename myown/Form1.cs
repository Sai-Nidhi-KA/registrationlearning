using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace myown
{
    public partial class Form1 : Form
    {
        string connectionstring = "Data Source=LAPTOP-UDFFUK18\\SQLEXPRESS;Initial Catalog=myown;Integrated Security=True";
        string mailpattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        string passpattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,}$";


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!Regex.IsMatch(textBox1.Text,mailpattern))
            {
                MessageBox.Show("Invalid Email", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Regex.IsMatch(textBox2.Text, passpattern))
            {
                MessageBox.Show("Invalid password", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Not matched", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO registermyown VALUES(@email,@password,@cpassword)", con);
                    cmd.Parameters.AddWithValue("@email", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);
                    cmd.Parameters.AddWithValue("@cpassword", textBox3.Text);
                    
                    int rows =cmd.ExecuteNonQuery();
                    con.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("Registered successfully");
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid register details");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
    }
}