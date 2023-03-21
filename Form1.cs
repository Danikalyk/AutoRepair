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
    public partial class Form1 : Form
    {
        public int act_table;
        public string connectionString = @"Data Source=DESKTOP-A6KEL66\SQLEXPRESS;Initial Catalog=СТО;User ID=sa;Password=EmmojoyProoo123";
        int kod;
        int kod2;
        int cost;

        public Form1()
        {
            InitializeComponent();
        }

        public void My_Execute_Non_Query(string CommandText)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = CommandText;
            myCommand.ExecuteNonQuery();
            conn.Close();
        }

        private void Get_Client()
        {
            string command = "select * from Клиент";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView2.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Auto()
        {
            string command = "select Авто.Код_авто, Модель.Код_модели, (select Марка.Название_марки from Марка where Модель.Марка = Марка.Код_марки) as Марка, Модель.Название_модели as Модель, Клиент.Фамилия_клиента, Клиент.Имя_клиента from Авто, Клиент, Модель where Авто.Марка = Модель.Код_модели and Авто.Клиент = Клиент.Код_клиента";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView2.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Oplatatype()
        {
            string command = "select * from Тип_оплаты";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView2.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Usluga()
        {
            string command = "select * from Услуга";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView2.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Master()
        {
            string command = "select * from Мастер";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView2.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Marka()
        {
            string command = "select * from Марка";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView2.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Model()
        {
            string command = "select Модель.Код_модели, Марка.Название_марки, Модель.Название_модели, Модель.Стоимость from Модель, Марка where Модель.Марка = Марка.Код_марки";
            
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView2.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Get_Dogovor()
        {
            string command = "select Договор.Код_договора, Услуга.Название_услуги, (select Клиент.Фамилия_клиента from Клиент where Авто.Клиент = Клиент.Код_клиента) as Фамилия_клиента, (select Марка.Название_марки from Марка where Авто.Марка = Марка.Код_марки) as Марка_авто, (select Клиент.Имя_клиента from Клиент where Авто.Клиент = Клиент.Код_клиента) as Имя_клиента, Мастер.Фамилия_мастера, Мастер.Имя_мастера, Тип_оплаты.Название_типа, Договор.Цена from Договор, Авто, Тип_оплаты, Мастер, Услуга where Договор.Мастер = Мастер.Код_мастера and Договор.Авто = Авто.Код_авто and Договор.Тип_оплаты = Тип_оплаты.Код_типа and Договор.Услуга = Услуга.Код_услуги";
            
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command, connectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Table");
            dataGridView1.DataSource = ds.Tables["Table"].DefaultView;
        }

        private void Add_Client(string surname, string name, string phone)
        {
            string command = $"insert into Клиент (Фамилия_клиента, Имя_клиента, Номер_телефона) values ('{surname}','{name}','{phone}')";
            My_Execute_Non_Query(command);
            Get_Client();
        }

        private void Add_Auto(int marka, int client)
        {
            string command = $"insert into Авто (Марка, Клиент) values ({marka},{client})";
            My_Execute_Non_Query(command);
            Get_Auto();
        }

        private void Add_Oplatatype(string name)
        {
            string command = $"insert into Тип_оплаты (Название_типа) values ('{name}')";
            My_Execute_Non_Query(command);
            Get_Oplatatype();
        }

        private void Add_Master(string surname, string name, string phone)
        {
            string command = $"insert into Мастер (Фамилия_мастера, Имя_мастера, Номер_телефона) values ('{surname}','{name}','{phone}')";
            My_Execute_Non_Query(command);
            Get_Master();
        }

        private void Add_Marka(string name)
        {
            string command = $"insert into Марка (Название_марки) values ('{name}')";
            My_Execute_Non_Query(command);
            Get_Marka();
        }

        private void Add_Model(string name, int marka, int stoim)
        {
            string command = $"insert into Модель (Название_модели, Марка, Стоимость) values ('{name}', {marka}, {stoim})";
            My_Execute_Non_Query(command);
            Get_Model();
        }

        private void Add_Usluga(string name, int cost)
        {
            string command = $"insert into Услуга (Название_услуги, Цена_услуги) values ('{name}', {cost})";
            My_Execute_Non_Query(command);
            Get_Usluga();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Get_Dogovor();
            Get_Client();
        }

        
        private void button11_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            Form4 f4 = new Form4();
            Form5 f5 = new Form5();
            Form6 f6 = new Form6();
            Form7 f7 = new Form7();
            Form8 f8 = new Form8();
            Form9 f9 = new Form9();

            switch (act_table)
            {
                case 1:
                    try
                    {
                        if (f3.ShowDialog() == DialogResult.OK)
                        {
                            Add_Client(f3.textBox1.Text, f3.textBox2.Text, Convert.ToString(f3.maskedTextBox1.Text));
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Что-то пошло не так!");
                    }
                    break;
                case 2:
                    try
                    {
                        if (f4.ShowDialog() == DialogResult.OK)
                        {
                            int row = f4.dataGridView1.CurrentCell.RowIndex;
                            int ID = Convert.ToInt32(f4.dataGridView1[0, row].Value);

                            int row2 = f4.dataGridView2.CurrentCell.RowIndex;
                            int ID2 = Convert.ToInt32(f4.dataGridView2[0, row].Value);

                            Add_Auto(ID, ID2);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Что-то пошло не так!");
                    }
                    break;
                case 3:
                    try
                    {
                        if (f5.ShowDialog() == DialogResult.OK)
                        {
                            Add_Oplatatype(f5.textBox1.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Что-то пошло не так!");
                    }
                    break;
                case 4:
                    try
                    {
                        if (f6.ShowDialog() == DialogResult.OK)
                        {
                            Add_Master(f6.textBox1.Text, f6.textBox2.Text, Convert.ToString(f6.maskedTextBox1.Text));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Что-то пошло не так!");
                    }
                    break;
                case 5:
                    try
                    {
                        if (f7.ShowDialog() == DialogResult.OK)
                        {
                            Add_Marka(f7.textBox1.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Что-то пошло не так!");
                    }
                    break;
                case 6:
                    try
                    {
                        if (f8.ShowDialog() == DialogResult.OK)
                        {
                            string sqlExpression = $"SELECT Код_марки from Марка where Название_марки = '{D.name}'";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                SqlDataReader reader = command.ExecuteReader();

                                if (reader.HasRows) // если есть данные
                                {
                                    reader.Read();
                                    kod = Convert.ToInt32(reader.GetValue(0));
                                }
                            }

                            Add_Model(f8.textBox1.Text, kod, Convert.ToInt32(f8.maskedTextBox1.Text));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Что-то пошло не так!");
                    }
                    break;
                case 7:
                    try
                    {
                        if (f9.ShowDialog() == DialogResult.OK)
                        {
                            Add_Usluga(f9.textBox1.Text, Convert.ToInt32(f9.maskedTextBox1.Text));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Что-то пошло не так!");
                    }
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            act_table = 1;
            Get_Client();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            act_table = 2;
            Get_Auto();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            act_table = 3;
            Get_Oplatatype();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            act_table = 4;
            Get_Master();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            act_table = 5;
            Get_Marka();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            act_table = 6;
            Get_Model();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            act_table = 7;
            Get_Usluga();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();

            string command;

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int row = dataGridView2.CurrentCell.RowIndex; 
                    int ID = Convert.ToInt32(dataGridView2[0, row].Value);

                    switch (act_table)
                    {
                        case 1:
                            command = $"delete from Клиент where Код_клиента = {ID}";
                            My_Execute_Non_Query(command);
                            Get_Client();
                            break;

                        case 2:
                            command = $"delete from Авто where Код_авто = {ID}";
                            My_Execute_Non_Query(command);
                            Get_Auto();
                            break;

                        case 3:
                            command = $"delete from Тип_оплаты where Код_типа = {ID}";
                            My_Execute_Non_Query(command);
                            Get_Oplatatype();
                            break;

                        case 4:
                            command = $"delete from Мастер where Код_мастера = {ID}";
                            My_Execute_Non_Query(command);
                            Get_Master();
                            break;

                        case 5:
                            command = $"delete from Марка where Код_марки = {ID}";
                            My_Execute_Non_Query(command);
                            Get_Marka();
                            break;

                        case 6:
                            command = $"delete from Модель where Код_модели = {ID}";
                            My_Execute_Non_Query(command);
                            Get_Model();
                            break;

                        case 7:
                            command = $"delete from Услуга where Код_услуги = {ID}";
                            My_Execute_Non_Query(command);
                            Get_Usluga();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Что-то пошло не так!");
                }
            }
        }

        private void Add_Dogovor(int auto, int usluga, int master, int type, int cost)
        {
            string command = $"insert into Договор (Авто, Услуга, Мастер, Тип_оплаты, Цена) values ({auto},{usluga},{master},{type},{cost})";
            My_Execute_Non_Query(command);
            Get_Dogovor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Form10 f = new Form10();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    int row = f.dataGridView1.CurrentCell.RowIndex;
                    int ID = Convert.ToInt32(f.dataGridView1[0, row].Value);

                    int row2 = f.dataGridView2.CurrentCell.RowIndex;
                    int ID2 = Convert.ToInt32(f.dataGridView2[0, row].Value);

                    string sqlExpression = $"SELECT Код_услуги from Услуга where Название_услуги = '{D.usluga}'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            reader.Read();
                            kod = Convert.ToInt32(reader.GetValue(0));
                        }
                    }

                    string sqlExpression2 = $"SELECT Код_типа from Тип_оплаты where Название_типа = '{D.type}'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression2, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            reader.Read();
                            kod2 = Convert.ToInt32(reader.GetValue(0));
                        }
                    }

                    string sqlExpression3 = $"SELECT Цена_услуги from Услуга where Название_услуги = '{D.usluga}'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression3, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            reader.Read();
                            cost = Convert.ToInt32(reader.GetValue(0));
                        }
                    }

                    int cena = cost + (cost / 2);

                    Add_Dogovor(ID, kod, ID2, kod2, cena);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();

            string command;

            if (f.ShowDialog() == DialogResult.OK)
            {
                int row = dataGridView1.CurrentCell.RowIndex;
                int ID = Convert.ToInt32(dataGridView1[0, row].Value);

                command = $"delete from Договор where Код_договора = {ID}";
                My_Execute_Non_Query(command);
                Get_Dogovor();
            }
        }
    }
}
