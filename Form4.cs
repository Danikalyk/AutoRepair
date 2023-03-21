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
    public partial class Form4 : Form
    {
        Form1 f = new Form1();
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Get_CLient();
            Get_Marka();
        }

        private void Get_CLient()
        {
            string command = "select * from Клиент";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView2.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Marka()
        {
            string command = "select Модель.Код_модели, Марка.Название_марки, Модель.Название_модели, Модель.Стоимость from Модель, Марка where Модель.Марка = Марка.Код_марки";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView1.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
