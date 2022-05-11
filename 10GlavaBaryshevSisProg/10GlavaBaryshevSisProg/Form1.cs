using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace _10GlavaBaryshevSisProg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] astrLogicalDrives = System.IO.Directory.GetLogicalDrives();
            foreach (string disk in astrLogicalDrives)
                listBox1.Items.Add(disk);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] astrLogicalDrives = System.Environment.GetLogicalDrives();
            foreach (string disk in astrLogicalDrives)
                listBox1.Items.Add(disk);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Выводим информацию о диске
            System.IO.DriveInfo drv = new System.IO.DriveInfo(@"d:\");
            listBox1.Items.Clear();
            listBox1.Items.Add("Диск: " + drv.Name);

            if (drv.IsReady)
            {
                listBox1.Items.Add("Тип диска: " + drv.DriveType.ToString());
                listBox1.Items.Add("Файловая система: " + drv.DriveFormat.ToString());
                listBox1.Items.Add("Свободное место на диске: " + drv.AvailableFreeSpace.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Получим список папок на диске D:
            listBox1.Items.Clear();
            string[] astrFolders = System.IO.Directory.GetDirectories(@"d:\");
            foreach (string folder in astrFolders)
                listBox1.Items.Add(folder);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Получим список папок, где встречается буквосочетание pro
            listBox1.Items.Clear();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"d:\");
            System.IO.DirectoryInfo[] folders = di.GetDirectories("*pro*");
            foreach (System.IO.DirectoryInfo maskdirs in folders)
                listBox1.Items.Add(maskdirs);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Попытаемся удалить папку C:\WUTEMP
                System.IO.Directory.Delete(@"c:\wutemp");
                MessageBox.Show("Папка удалена.");
            }
            catch (Exception)
            {
                MessageBox.Show("Нельзя удалить непустую папку.");
            }
            finally { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                // задаем папку верхнего уровня
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                // Заголовок в диалоговом окне
                fbd.Description = "Выберите папку";
                // Не выводим кнопку Новая папка
                fbd.ShowNewFolderButton = false;
                // Получаем папку, выбранную пользователем
                if (fbd.ShowDialog() == DialogResult.OK)
                    this.Text = fbd.SelectedPath;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Получим информацию о свойствах папки 
            System.IO.DirectoryInfo dir = new
            System.IO.DirectoryInfo(@"c:\wutemp");
            listBox1.Items.Clear();
            listBox1.Items.Add("Проверка папки: " + dir.Name);
            listBox1.Items.Add("Родительская папка: " + dir.Parent.Name);
            listBox1.Items.Add("Папка существует: ");
            listBox1.Items.Add(dir.Exists.ToString());
            if (dir.Exists)
            {
                listBox1.Items.Add("Папка создана: ");
                listBox1.Items.Add(dir.CreationTime.ToString());
                listBox1.Items.Add("Папка изменена: ");
                listBox1.Items.Add(dir.LastWriteTime.ToString());
                listBox1.Items.Add("Время последнего доступа: ");
                listBox1.Items.Add(dir.LastAccessTime.ToString());
                listBox1.Items.Add("Атрибуты папки: ");
                listBox1.Items.Add(dir.Attributes.ToString());
                listBox1.Items.Add("Папка содержит: " +
                dir.GetFiles().Length.ToString() + " файла");
            }

        }

        static long GetDirectorySize(System.IO.DirectoryInfo directory, bool includeSubdir)
        {
            long totalSize = 0;
            // Считаем все файлы
                System.IO.FileInfo[] files = directory.GetFiles();
            foreach (System.IO.FileInfo file in files)
            {
                totalSize += file.Length;
            }
         // Считаем все подпапки
            if (includeSubdir)
            {
                System.IO.DirectoryInfo[] dirs = directory.GetDirectories();
                foreach (System.IO.DirectoryInfo dir in dirs)
                {
                    totalSize += GetDirectorySize(dir, true);
                }
            }
            return totalSize;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            this.Cursor = Cursors.WaitCursor;
            System.IO.DirectoryInfo dir = new
            System.IO.DirectoryInfo(@"D:\mir");
            textBox1.Text = "Общий размер: " +
            GetDirectorySize(dir, true).ToString() + " байт.";
            this.Cursor = Cursors.Default;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] astrFiles = System.IO.Directory.GetFiles(@"c:\");
            listBox1.Items.Add("Всего файлов: " + astrFiles.Length);
            foreach (string file in astrFiles)
                listBox1.Items.Add(file);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] directoryEntries = System.IO.Directory.GetFileSystemEntries(@"c:\windows");
            foreach (string str in directoryEntries)
            {
                listBox1.Items.Add(str);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] astrFiles = System.IO.Directory.GetFiles(@"c:\", "*.in?");
            listBox1.Items.Add("Всего файлов: " + astrFiles.Length);
            foreach (string file in astrFiles)
                listBox1.Items.Add(file);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Полный путь к файлу
            string fileNamePath = @"c:\windows\system32\notepad.exe";
            // Получим расширение файла
            listBox1.Items.Add(System.IO.Path.GetExtension(fileNamePath));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // путь к тестовому файлу
            string path = @"C:\windows\system32\mspaint.exe";
            // если файлы имел атрибут Скрытый
            if ((System.IO.File.GetAttributes(path) &
            System.IO.FileAttributes.Hidden)
            == System.IO.FileAttributes.Hidden)
            {
                // то устанавливаем атрибут Normal
                System.IO.File.SetAttributes(path,
                System.IO.FileAttributes.Normal);
                MessageBox.Show("Файл больше не является скрытым", path);
            }
            else // если файл не был скрытым
            {
                // то устанавливаем у файла атрибут Скрытый
                System.IO.File.SetAttributes(path,
                System.IO.File.GetAttributes(path) |
                System.IO.FileAttributes.Hidden);
                MessageBox.Show("Файл стал скрытым", path);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // Выводим информацию о файле.
            System.IO.FileInfo file = new
            System.IO.FileInfo(@"c:\wutemp\text.txt");
            listBox1.Items.Clear();
            listBox1.Items.Add("Свойства для файла: " + file.Name);
            listBox1.Items.Add("Наличие файла: " + file.Exists.ToString());
            if (file.Exists)
            {
                listBox1.Items.Add("Время создания файла: ");
                listBox1.Items.Add(file.CreationTime.ToString());
                listBox1.Items.Add("Последнее изменение файла: ");
                listBox1.Items.Add(file.LastWriteTime.ToString());
                listBox1.Items.Add("Файл был открыт в последний раз: ");
                listBox1.Items.Add(file.LastAccessTime.ToString());
                listBox1.Items.Add("Размер файла (в байтах): ");
                listBox1.Items.Add(file.Length.ToString());
                listBox1.Items.Add("Атрибуты файла: ");
                listBox1.Items.Add(file.Attributes.ToString());
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            System.Diagnostics.FileVersionInfo info =System.Diagnostics.FileVersionInfo.GetVersionInfo (@"C:\windows\system32\mspaint.exe");
            // Выводим информацию о выбранном файле
            listBox1.Items.Add("Выбранный файл: " + info.FileName);
            listBox1.Items.Add("Product Name: " + info.ProductName);
            listBox1.Items.Add("Product Version: " + info.ProductVersion);
            listBox1.Items.Add("Company Name: " + info.CompanyName);
            listBox1.Items.Add("File Version: " + info.FileVersion);
            listBox1.Items.Add("File Description: " + info.FileDescription);
            listBox1.Items.Add("Original Filename: " + info.OriginalFilename);
            listBox1.Items.Add("Legal Copyright: " + info.LegalCopyright);
            listBox1.Items.Add("InternalName: " + info.InternalName);
            listBox1.Items.Add("IsDebug: " + info.IsDebug);
            listBox1.Items.Add("IsPatched: " + info.IsPatched);
            listBox1.Items.Add("IsPreRelease: " + info.IsPreRelease);
            listBox1.Items.Add("IsPrivateBuild: " + info.IsPrivateBuild);
            listBox1.Items.Add("IsSpecialBuild: " + info.IsSpecialBuild);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            // Создаем временный файл
            listBox1.Items.Add(System.IO.Path.GetTempFileName());
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(@"C:\windows"))
                label1.Text = "Папка " + @"C:\Windows" + " существует";
            else
                label1.Text = "Папка не существует";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string randomFile = System.IO.Path.GetRandomFileName();
            MessageBox.Show(randomFile); // вернет что-то типа 5wvzx2n0.lby
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string tempText = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), ".txt");
            MessageBox.Show(tempText);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string fileName = @"c:\test.txt";
            FileStream stream = new FileStream(fileName,
             FileMode.Open, FileAccess.Read, FileShare.None);
            //никому не даем доступ к файлу
            // здесь находится ваш код
            // открываем снова файл для доступа
            stream.Close();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string fileName = @"c:\test.txt";
            FileStream stream = File.Open(fileName, FileMode.Open);
            stream.Lock(0, stream.Length); 
            // блокируем файл
            // здесь ваш код
            // снимаем блокировку
            stream.Unlock(0, stream.Length);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string fileName = @"d:\mir\Dlya Raboty.txt";
            // Создадим новый пустой файл
            if (System.IO.File.Exists(fileName))
            {
                MessageBox.Show("Указанный файл уже существует!", fileName);
                return;
            }
            System.IO.FileStream fs = new System.IO.FileStream(fileName,
            System.IO.FileMode.CreateNew);
            // Запишем данные в файл
            System.IO.BinaryWriter w = new System.IO.BinaryWriter(fs);

            for (int i = 0; i < 11; i++)
            {
                w.Write((int)i);
            }
            w.Close();
            fs.Close();
            // Попытаемся прочитать записанное
            fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open,
            System.IO.FileAccess.Read);
            System.IO.BinaryReader binread = new System.IO.BinaryReader(fs);
            // считываем данные из test.bin
            for (int i = 0; i < 11; i++)
            {
                MessageBox.Show(binread.ReadInt32().ToString());
            }
            w.Close();
        }
    }
}