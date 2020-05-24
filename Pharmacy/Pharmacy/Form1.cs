using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy
{
    public partial class Form1 : Form
    {
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=pharmacy.accdb;";
        private OleDbConnection myConnection;
         
        public Form1()
        {
            InitializeComponent();
            //string patch = Environment.CurrentDirectory.ToString().Substring(0, Environment.CurrentDirectory.ToString().Length - 9);
            myConnection = new OleDbConnection(connectString);
            // Відкриває ДБ
            myConnection.Open();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            // текст запиту
            int z;
            z = listBox2.Items.Count;
            if(z==0)
            {     
                string s = DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss");
                listBox2.Items.Add("Чек.");
                listBox2.Items.Add("Дата/Час: " + s);
                listBox2.Items.Add("Товари ------------------------------------------------------------------------");
            }
            string query = "SELECT Inst,Name, Price,Data,Maker,Type,Pok,img FROM pharmacy ";
            label7.Text = "...";
            label8.Text = "...";
            label9.Text = "...";
            label10.Text = "...";
            label11.Text = "...";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            richTextBox1.Clear();
            // створюєм обєкт OleDbCommand для виконання запиту до БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);
            // получаємо обєкт OleDbDataReader для читання табличного результату запиту SELECT
            OleDbDataReader reader = command.ExecuteReader();
            // ощищуємо listBox1
            listBox1.Items.Clear();
            // в циклі по порядку виводжу  дані з БД
            for (int i = 1; reader.Read(); i++)
            {
                // виводимо дані в listBox1
                listBox1.Items.Add(reader[1].ToString());
            }
            // закриваєм OleDbDataReader
            reader.Close();
        }

        //пертворює jpeg в растрове зображення 
        public Bitmap ConvertToBitmap(string fileName)
        {
            Bitmap bitmap;
            using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);

            }
            return bitmap;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = listBox1.SelectedIndex;
                Object selectedItem = listBox1.SelectedItem;
                string q = "select * from pharmacy where Name='" + selectedItem.ToString() + "'";
                OleDbCommand command = new OleDbCommand(q, myConnection);
                OleDbDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    label7.Text = dr[1].ToString();
                    label8.Text = dr[3].ToString() + " UAH";
                    label9.Text = dr[4].ToString();
                    label10.Text = dr[5].ToString();
                    label11.Text = dr[6].ToString();

                    textBox1.Text = dr[1].ToString();
                    textBox2.Text = dr[2].ToString();
                    textBox3.Text = dr[3].ToString();
                    textBox4.Text = dr[4].ToString();
                    textBox5.Text = dr[5].ToString();
                    textBox6.Text = dr[6].ToString();
                    textBox7.Text = dr[7].ToString();
                    
                    richTextBox3.Text = dr[2].ToString();
                    richTextBox1.Text = dr[7].ToString();
                    string patch = Environment.CurrentDirectory.ToString().Substring(0, Environment.CurrentDirectory.ToString().Length - 9);
                    Bitmap image1 = new Bitmap(ConvertToBitmap(patch + "image/" + dr[8].ToString()));
                    pictureBox1.Image = image1;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; 




                }
                
            }
            catch
            {
                
                                MessageBox.Show("Введіть правильно дані",
                                "Помилка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null; 
            richTextBox1.Clear();
            richTextBox3.Clear();
            label9.Text = "...";
            label10.Text = "...";
            label11.Text = "...";
            label8.Text = "...";
            label7.Text = "...";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
             

             
            //-----------------------------------------------------------------------------
            int selectedIndex = listBox1.SelectedIndex;
            Object selectedItem = listBox1.SelectedItem;
            string query = "DELETE * FROM pharmacy  where Name='" + selectedItem.ToString() + "'";
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);
            // выполняем запрос к MS Access
            command.ExecuteNonQuery();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            int selectedIndex = listBox1.SelectedIndex;
            Object selectedItem = listBox1.SelectedItem;

            //----------------------------------------------------------------------------------------------------------------------------------------
            try
            {
                 
                int y = richTextBox1.TextLength;
                if (y == 0)
                {
                    MessageBox.Show("Select medecament");
                }
 


                listBox2.Items.Add(selectedItem.ToString());

            }
            catch {
 
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > 2)
            {
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss");
            MessageBox.Show("Продано (дата продажі: " + s + ")");
            listBox1.Items.Clear();
            richTextBox1.Clear();
            richTextBox3.Clear();
            label9.Text = "...";
            label10.Text = "...";
            label11.Text = "...";
            label8.Text = "...";
            label7.Text = "...";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            pictureBox1.Image = null;
            listBox2.Items.Clear();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                
                int selectedIndex = listBox1.SelectedIndex;
                Object selectedItem = listBox1.SelectedItem;
                string query = "UPDATE pharmacy SET Name ='" + textBox1.Text + "',Inst='" + textBox2.Text + "',Price='" + textBox3.Text + "',Data='" + textBox4.Text + "',Maker='" + textBox5.Text +  "',Type='" + textBox6.Text +  "',Pok='" + textBox7.Text +"'where Name='" + selectedItem.ToString() + "'";
                 
            OleDbCommand command = new OleDbCommand(query, myConnection);
                MessageBox.Show("Дані відредаговано");
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Error");
            } 
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
            try {

                int selectedIndex = listBox1.SelectedIndex;
                Object selectedItem = listBox1.SelectedItem;
                string query = "SELECT pharmacy.[Cod],pharmacy.[Name],pharmacy.[Inst],pharmacy.[Price],pharmacy.[Data],pharmacy.[Maker],pharmacy.[Type],pharmacy.[Pok],pharmacy.[img] FROM pharmacy  where Name='" + selectedItem.ToString() + "'";

                
                OleDbCommand command = new OleDbCommand(query, myConnection);
                OleDbDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    label7.Text = dr[1].ToString();
                    string patch = Environment.CurrentDirectory.ToString().Substring(0, Environment.CurrentDirectory.ToString().Length - 9);
                 

  }
            }
            catch
            {
                MessageBox.Show("Оберіть назву лікарства",
                                "Помилка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label8.Text = "";
            label7.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            richTextBox1.Clear();
            richTextBox3.Clear();
            pictureBox1.Image = null;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
         
            string query = "SELECT * FROM pharmacy where Name like '%" + textBox8.Text + "%'";
           
            listBox1.Items.Clear();
            OleDbCommand command = new OleDbCommand(query, myConnection);// створюєм обєкт OleDbCommand для виконання запиту до БД MS Access
            OleDbDataReader reader = command.ExecuteReader();// получаємо обєкт OleDbDataReader для читання табличного результату запиту SELECT
            listBox1.Items.Clear();// ощищуємо listBox1
            // в циклі по порядку виводжу  дані з БД
            for (int i = 1; reader.Read(); i++)
            {
                // виводимо дані в listBox1
                listBox1.Items.Add(reader[1].ToString());
            }

            // закриваєм OleDbDataReader
            reader.Close();
        }

        
    }
}
