using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;




namespace CrsGr
{
    public partial class Form1 : Form
    {

        int size;
      
        public Form1()
        {
            InitializeComponent();
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            size = int.Parse(textBox1.Text);
            label1.Text = "Matrix size: " + size;
            
            Random random = new Random();
            int[,] M = new int[size, size];
            for (int i = 1; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    M[i, j] = random.Next(2);
                    M[j, i] = M[i, j];
                }
            }
            Inic(size, M);
        }

        public void Inic(int size, int[,]M)
        {
            label2.Text = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {            
                    label2.Text += M[i, j];
                    label2.Text += "  ";
                }    
                label2.Text += "\n";
            }
            vertex_queue(size, M);
        }


        public void vertex_queue(int size, int[,] M)
        {
            Queue<int> vertex = new Queue<int>();
            Dictionary<int, int> razr = new Dictionary<int, int>(size);
            int sr = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (M[i, j] == 1)
                    {
                        sr++;
                    }
                }
                razr.Add(i, sr);
                sr = 0;
            }

            var list = razr.ToList();
            list.Sort((p1, p2) => p2.Value.CompareTo(p1.Value));
            for (int i = 0; i < size; i++)
            {
                vertex.Enqueue(list.ElementAt(i).Key);
            }
            Colour(M, size, vertex);
        }


        public void Colour(int [,]M,int size, Queue<int> vertex)
        {
            int[] exit = new int[size];
            int w = vertex.Dequeue();
            int color = 1;
            exit[w] = color;
            int e = 0;
        
            while (vertex.Count != 0)
            {
                for (int i = 0; i < size; i++)
                { 
                    for (int j = 0; j < size; j++)
                    {
                        if (M[w, j] == 0 && exit[j] == 0)
                        {
                            exit[j] = color;
                            e = exit[j];
                        }
                    }
                    
                }
                w = vertex.Dequeue();
                color++;
            }
            label3.Text = "";
            label3.Text += "Color: ";
            for (int i = 0; i < size; i++)
            {
                    label3.Text +=+exit[i];
                    label3.Text += "  ";
            }
            chrom_(exit,M);
        }



        public void Button2_Click(object sender, EventArgs e) 
        {
            label1.Text = "";
            int m = richTextBox1.Lines.Length;
            int n = richTextBox1.Lines[0].Split(' ').Length;

            int[,] M = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                string[] s = richTextBox1.Lines[i].Split(' ');
                for (int j = 0; j < n; j++)
                {
                    M[i, j] = Int32.Parse(s[j]);
                }
            }
            size = n;
            Inic(size, M);
            label1.Text = "Matrix size: " + n;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            string records = File.ReadAllText("D:/file.txt");
            form2.setTextToRich(records);
            form2.Show();
        }

        public void saveRecords(int[,] M, int[] exit, int chrom)
        {
            DateTime now = DateTime.Now;
            StreamWriter file = new StreamWriter("D:/file.txt", true);
            try
            {
                file.Write(("Date:  " + now.ToString("dd MMMM yyyy HH.mm.ss")) + "\n");
                file.Write("Size:  " + size + "  \n");
                file.Write("Matrix:   \n");
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        file.Write(M[i, j] + "  ");
                    }
                    file.Write("\n");
                }

                file.Write("Color:   ");

                for (int i = 0; i < size; i++)
                {
                    file.Write(exit[i] + "  ");
                }
                file.Write("\n");
                file.Write("Сhromatic number:  " + chrom);
                file.Write("\n\n\n");
            }

            finally
            {
                file.Flush();
                file.Close();
            }


            string s = DateTime.Now.ToString("dd MMMM yyyy HH.mm.ss");
            StreamWriter save = new StreamWriter(s+".txt", true);
            try
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        save.Write(M[i, j] );
                        if (j == size - 1) {
                            continue;
                        }
                        else
                        {
                            save.Write(" ");
                        }   
                    }
                    if (i == size - 1)
                    {
                        continue;
                    }
                    else
                    {
                        save.Write("\n");
                    }   
                }
            }

            finally
            {
                save.Flush();
                save.Close();
            }
        }

        public void chrom_(int[]exit,int [,]M) {

            int chrom = 0;
            for (int i = 0; i < size; i++)
            {
                if (exit[i] > chrom) {
                    chrom = exit[i];
                }
            }
            label8.Text = "";
            label8.Text = "Сhromatic number: " + chrom;

            label9.Text = "";
            int cl = 0;
            for (int i = 0; i < chrom; i++)
            {
                cl++;
                label9.Text += "Color "+cl+": ";
                for (int j = 0; j < size; j++)
                {
                    if (exit[j] == cl)
                    {
                        label9.Text +=  j ;  
                    }    
                }
                label9.Text += "\n";
            }
            saveRecords(M, exit, chrom);
        }


        private void Button4_Click_1(object sender, EventArgs e)
        {
           string name = textBox2.Text+".txt";
            if (!(File.Exists(name)))
            {
                MessageBox.Show("Input Error.\nTry again");
            }
            else
            {
                string soxr = File.ReadAllText(name);
                richTextBox1.Text = soxr;
            }
            textBox2.Clear();
        }
    }
}   
    
