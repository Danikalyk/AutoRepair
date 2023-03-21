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
    public partial class Form10 : Form
    {
        Form1 f = new Form1();

        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            Get_Master();
            Get_Usluga();
            Get_Oplatatype();
            Get_Auto();
        }

        private void Get_Master()
        {
            string command = "select * from Мастер";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView2.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Usluga()
        {
            string command = "select Название_услуги from Услуга";

            Form1 f = new Form1();
            SqlConnection connRC = new SqlConnection(f.connectionString);
            SqlDataAdapter da2 = new SqlDataAdapter(command, connRC);

            DataSet ds2 = new DataSet();
            connRC.Open();
            da2.Fill(ds2);
            connRC.Close();

            comboBox1.DataSource = ds2.Tables[0];
            comboBox1.DisplayMember = "Название_услуги";
            comboBox1.Text = " ";
        }

        private void Get_Auto()
        {
            string command = "select Авто.Код_авто, Модель.Код_модели, (select Марка.Название_марки from Марка where Модель.Марка = Марка.Код_марки) as Марка, Модель.Название_модели as Модель, Клиент.Фамилия_клиента, Клиент.Имя_клиента from Авто, Клиент, Модель where Авто.Марка = Модель.Код_модели and Авто.Клиент = Клиент.Код_клиента";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, f.connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView1.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Oplatatype()
        {
            string command = "select Название_типа from Тип_оплаты";

            Form1 f = new Form1();
            SqlConnection connRC = new SqlConnection(f.connectionString);
            SqlDataAdapter da2 = new SqlDataAdapter(command, connRC);

            DataSet ds2 = new DataSet();
            connRC.Open();
            da2.Fill(ds2);
            connRC.Close();

            comboBox2.DataSource = ds2.Tables[0];
            comboBox2.DisplayMember = "Название_типа";
            comboBox2.Text = " ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            D.usluga = comboBox1.Text;
            D.type = comboBox2.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
