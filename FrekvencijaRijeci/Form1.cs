using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrekvencijaRijeci
{
    public partial class Form1 : Form
    {
        string filepath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text File Only|*.txt";
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK)) {
                label1.Text = "";
                dataGridView1.Rows.Clear();
                filepath = openFileDialog1.FileName;
                label1.Text = filepath;
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> rijeci = new Dictionary<string, int>();
            List<string> lista = new List<string>();
          
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line;
                   
                    while ((line = sr.ReadLine()) != null)
                    {
                    
                        lista = line.Split(' ').ToList();

                        foreach (var s in lista)
                        {
                        string malaSlova = s.ToLower();

                        //removing non letters from string
                        Regex rgx = new Regex("[^a-zA-Z0-9]");
                        malaSlova = rgx.Replace(malaSlova, "");

                        //checking if string is null or whitespace
                        if (string.IsNullOrWhiteSpace(malaSlova)) {
                            continue;
                        }

                        
                        if (rijeci.TryGetValue(malaSlova, out int value))
                        {
                            rijeci[malaSlova]++;
                        }
                        else
                        {
                           rijeci[malaSlova] = 1;
                        }
                    }
                     
                    }
                }
            var ordered = rijeci.OrderByDescending(x => x.Value);
            
            foreach (var i in ordered)
            {
                dataGridView1.Rows.Add(i.Key, i.Value);
            }
            button2.Enabled = false;


        }
    }
}
