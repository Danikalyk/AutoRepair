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

namespace STO
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            Get_Marka();
        }

        private void Get_Marka()
        {
            string command = "select Название_марки from Марка";

            Form1 f = new Form1();
            SqlConnection connRC = new SqlConnection(f.connectionString);
            SqlDataAdapter da2 = new SqlDataAdapter(command, connRC);

            DataSet ds2 = new DataSet();
            connRC.Open();
            da2.Fill(ds2);
            connRC.Close();

            comboBox1.DataSource = ds2.Tables[0];
            comboBox1.DisplayMember = "Название_марки";
            comboBox1.Text = " ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            D.name = comboBox1.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
