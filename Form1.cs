using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string line = this.txtName.Text + "," + this.txtLastName.Text + "," + this.txtGrossIncome.Text + "," + this.txtMaritalStatus.Text;

            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:data.txt", true))
                {
                    file.WriteLine(line);
                }
            }

        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:data.txt");

            lines = lines.OrderBy(d => d).ToArray();

            foreach (string line in lines)
            {
                rtBoxRes.Text += (line + "\n");
            }
        }

        static DataTable GetTable()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:data.txt");

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Last Name", typeof(string));
            table.Columns.Add("Gross Income", typeof(double));
            table.Columns.Add("Marital Status", typeof(string));

            foreach (string line in lines)
            {
                int i=0; 
                string[] parts = line.Split(',');
                table.Rows.Add(parts[i]);
            }
           
            return table;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:data.txt");

            this.lblSearch.Text = Array.Find(lines,
                 element => element.StartsWith(this.txtSearch.Text, StringComparison.OrdinalIgnoreCase));
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:data.txt");

            double total = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                total += double.Parse(parts[2]);
            }

            lblAverage.Text = Convert.ToString(total / lines.Length);
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            rtBoxRes.Visible = false;
            dgv.Visible = true;
            DataTable table = GetTable();
        }
    }
}
