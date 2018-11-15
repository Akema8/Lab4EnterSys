using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;


namespace Lab4
{
    public partial class Form1 : Form
    {
        string Path;
        string Path2;
        shifr gr;
        List<string> Lst;
        List<string> Lst2;

        public Form1()
        {
            InitializeComponent();
            Path = "OS1";
            Path2 = "OS2";
            gr = new shifr();
            Lst = new List<string>();
            Lst2 = new List<string>();

            if (!System.IO.File.Exists(Path))
                File.Create(Path);

            if (!System.IO.File.Exists(Path2))
                File.Create(Path2);

            if (System.IO.File.Exists(Path))
            {
                ReadFromFile(Path, Lst);
                if (Lst[0] != "true")
                    InFil(Path);
            }

            Lst = new List<string>();
        }

        private List<string> InfoFromPK(string WIN32_Class, string ClassItemField)
        {
            List<string> result = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);

            try
            {
                foreach (ManagementObject obj in searcher.Get())
                    result.Add(gr.GetShifr(obj[ClassItemField].ToString(), "rita"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        private void OutRes(string info, List<string> result, string path)
        {
            FileStream fl = new FileStream(path, FileMode.Append); //открываем поток на добавление
            StreamWriter sw = new StreamWriter(fl); //поток для записи
            if (info.Length > 0)
                sw.WriteLine(gr.GetShifr(info, "rita"));

            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; ++i)
                    sw.WriteLine(result[i]);
            }
            sw.Close();
            fl.Close();
        }

        private void InFil(string path)
        {
            string[] str = { "true" };
            File.WriteAllLines(path, str);
            OutRes("CPU:", InfoFromPK("Win32_Processor", "Name"), path); //процессор 
            OutRes("Manufacturer:", InfoFromPK("Win32_Processor", "Manufacturer"), path); //производитель
            OutRes("Description:", InfoFromPK("Win32_Processor", "Description"), path); //описание
            OutRes("Videocard:", InfoFromPK("Win32_VideoController", "Name"), path); //видеокарта
            OutRes("VP:", InfoFromPK("Win32_VideoController", "VideoProcessor"), path); //видеопроцессор            
            OutRes("Memory size:", InfoFromPK("Win32_VideoController", "AdapterRAM"), path); //объем памяти в байтах
            OutRes("HDD:", InfoFromPK("Win32_DiskDrive", "Caption"), path); //ЖД
            OutRes("Size:", InfoFromPK("Win32_DiskDrive", "Size"), path); //объем в байтах
        }

        private void ReadFromFile(string path, List<string> lst)
        {
            FileStream opfl = new FileStream(path, FileMode.Open);
            StreamReader rdfl = new StreamReader(opfl);
            int i = 0;

            while (i < 18)
            {
                lst.Add(rdfl.ReadLine());
                i++;
            }
            rdfl.Close();
            opfl.Close();
        }

        private int Sravn(List<string> lst1, List<string> lst2)
        {
            ReadFromFile(Path, lst1);
            ReadFromFile(Path2, lst2);
            int count = 0;

            for (int i = 0; i < lst1.Count; i++)
                if (lst1[i] != lst2[i]) count++;

            return count;
        }

        int count = 0;
        
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (!System.IO.File.Exists(Path2))
                File.Create(Path2);
            InFil(Path2);

            if ((Sravn(Lst, Lst2) != 0))
            {
                button1.BackColor = Color.Red;
                if (count == 0) button1.Enabled = false;
                button1.Visible = false;
                label1.Visible = true;
            }
            else
            {
                
                Form2 fr2 = new Form2();
                fr2.Show();         
          //      button1.Enabled = false;
               button1.BackColor = Color.Blue;
           //     this.Hide(); //скрывает элемент от пользователя
                
            }
            count++;
        }
    }
}
