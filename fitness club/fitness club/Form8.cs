﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace fitness_club
{
    public partial class Form8 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Admin\Documents\gym_db.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        int r1;
        string ff_Code_user;
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            funshow_user();
            total();
        }
        private void funshow_user()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from tbl_purchase";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Visible = true;
            con.Close();
            totshow_user();
        }
        private void totshow_user()
        {
            int counting;
            int no1 = 0;
            counting = dataGridView1.Rows.Count;
            for (int k = 0; k < counting; k++)
            {
                no1 = no1 + 1;
            }

            label15.Text = "No. of Record :" + no1.ToString();
        }
        private void total()
        {
            int sum=0;
           for (int k = 0; k < dataGridView1.Rows.Count; ++k)
            {
                sum+=Convert.ToInt32(dataGridView1.Rows[k].Cells[5].Value);
            }

            label9.Text = "Total Amount :" + sum.ToString();
            //textBox6.Text = sum.ToString();
        }
        

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            r1 = e.RowIndex;
        }
        private void clearing_user()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }
        private void insertin_user()
        {
            long DAY1 = long.Parse(dateTimePicker1.Value.Day.ToString());
            long MONTH1 = long.Parse(dateTimePicker1.Value.Month.ToString());
            long YEAR1 = long.Parse(dateTimePicker1.Value.Year.ToString());
            if (button12.Text == "&Update")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update tbl_purchase set Product='" + textBox1.Text + "',Description='" + textBox5.Text + "',Rate='" + textBox3.Text + "',Qty='" + textBox2.Text + "',Amount='" + textBox4.Text + "',Date='" + dateTimePicker1.Text + "',Day=" + DAY1 + ",Month=" + MONTH1 + ",Year=" + YEAR1 + " where ID=" + ff_Code_user + "";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Update Successfully");
                total();
                button12.Text = "&Save";


            }
            else
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into tbl_purchase(Product,Description,Rate,Qty,Amount,Date,Day,Month,Year) values('" + textBox1.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + dateTimePicker1.Text + "'," + DAY1 + "," + MONTH1 + "," + YEAR1 + ")";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Inserted");
                con.Close();
            }
            clearing_user();
            funshow_user();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            insertin_user();
            total();
        }
        private void calc()
        {
            try
            {
                long totalamount = long.Parse(textBox2.Text) * long.Parse(textBox3.Text);
                textBox4.Text = totalamount.ToString();
            }
            catch(Exception err){}
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            calc();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ff_Code_user = dataGridView1.Rows[r1].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[r1].Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.Rows[r1].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[r1].Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.Rows[r1].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.Rows[r1].Cells[5].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[r1].Cells[6].Value.ToString();

            button12.Text = "&Update";
            textBox1.Focus();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int v2 = int.Parse(dataGridView1.Rows[r1].Cells[0].Value.ToString());
            string na1 = dataGridView1.Rows[r1].Cells[1].Value.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from tbl_purchase where ID=" + v2 + "";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Has Been Deleted Successfully(" + na1.ToUpper() + ")");
            con.Close();
            clearing_user();
            funshow_user();
            total();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            calc();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
