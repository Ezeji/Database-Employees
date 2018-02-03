using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace employees_database
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        System.Data.SqlServerCe.SqlCeConnection con;
        System.Data.SqlServerCe.SqlCeDataAdapter da;

        DataSet ds1;

        int MaxRows = 0;
        int inc = 0;

        // Form loads to start connection to the database and activate NavigateRecords method

        private void Form1_Load(object sender, EventArgs e)
        {

            con = new System.Data.SqlServerCe.SqlCeConnection();
            con.ConnectionString = "Data Source = C:\\Users\\user\\Documents\\Databases\\Employees.sdf";
            con.Open();

            ds1 = new DataSet();

            string sql = "SELECT * From tbl_employees";
            da = new System.Data.SqlServerCe.SqlCeDataAdapter(sql,con);

            //MessageBox.Show("Connection Open");

            da.Fill(ds1,"Workers");
            NavigateRecords();

            con.Close();

        }

        // This method pulls data from the database into a DataRow object for data manipulation

        private void NavigateRecords()

        {
            MaxRows = ds1.Tables["Workers"].Rows.Count;
            DataRow dRow = ds1.Tables["Workers"].Rows[inc];
            textBox1.Text = dRow.ItemArray.GetValue(1).ToString();
            textBox2.Text = dRow.ItemArray.GetValue(2).ToString();
            textBox3.Text = dRow.ItemArray.GetValue(3).ToString();
            textBox4.Text = dRow.ItemArray.GetValue(4).ToString();
        }

        // This button gives access to the next records of the database

        private void button1_Click(object sender, EventArgs e)
        {
            if (inc != MaxRows - 1)
            {
                inc++;
                NavigateRecords();
            }

            else

            {
                MessageBox.Show("No More Rows");
            }
        }

        // This button gives access to previous records of the database

        private void button2_Click(object sender, EventArgs e)
        {
            if (inc > 0)
            {
                inc--;
                NavigateRecords();
            }

            else

            {
                MessageBox.Show("First Record");
            }
        }

        // This button gives access to the last record of the database

        private void button3_Click(object sender, EventArgs e)
        {
            inc = MaxRows - 1;
            NavigateRecords();
        }

        // This button gives access to the first record of the database

        private void button4_Click(object sender, EventArgs e)
        {
            inc = 0;
            NavigateRecords();
        }

        // This button clears the text fields for a new search into the database

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

          
        }

        // This button saves new data entries into the DataRow object

        private void button6_Click(object sender, EventArgs e)
        {
            DataRow dRow = ds1.Tables["Workers"].NewRow();

            dRow[1] = textBox1.Text;
            dRow[2] = textBox2.Text;
            dRow[3] = textBox3.Text;
            dRow[4] = textBox4.Text;

            ds1.Tables["Workers"].Rows.Add(dRow);

            Update();
          

            MaxRows = MaxRows + 1;
            inc = MaxRows - 1;

        }

        // This method updates the database with new data entries

        private void UpdateDB()

        {
            System.Data.SqlServerCe.SqlCeCommandBuilder cb;
            cb = new System.Data.SqlServerCe.SqlCeCommandBuilder(da);
            cb.DataAdapter.Update(ds1.Tables["Workers"]);
        }

        // This button updates the database with new data entries that are already saved in the DataRow object

        private void button7_Click(object sender, EventArgs e)
        {
            DataRow dRow2 = ds1.Tables["Workers"].Rows[inc];

            dRow2[1] = textBox1.Text;
            dRow2[2] = textBox2.Text;
            dRow2[3] = textBox3.Text;
            dRow2[4] = textBox4.Text;

            UpdateDB();

            MessageBox.Show("Data Updated");
        }

        // This button deletes data entries from the database

        private void button8_Click(object sender, EventArgs e)
        {
            ds1.Tables["Workers"].Rows[inc].Delete();
            UpdateDB();
            MaxRows = ds1.Tables["Workers"].Rows.Count;
            inc--;
            NavigateRecords();
            MessageBox.Show("Record Deleted");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string searchFor = "Okeke";
            int results = 0;
            DataRow[] returnedRows;

            returnedRows = ds1.Tables["Workers"].Select("last_name = '" + searchFor + "'");

            results = returnedRows.Length;

            if (results > 0)
            {
                DataRow dr1 = returnedRows[0];

                MessageBox.Show(dr1[1].ToString() + Environment.NewLine + dr1[2].ToString());
            }

            else

            {
                MessageBox.Show("No Such Record");
            }
        }
    }
}
