using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSClassicTool
{
    public partial class Form1 : Form
    {
        private string _BaseFolder = "";  
        public Form1()
        {
            InitializeComponent();
            
            LoadDrives();
            DatabaseManager.getInstance().GameDeleted += Form1_GameDeleted;
            /*
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                Managers.FileSystemManager.getInstance().SetBasePath(fbd.SelectedPath);
                
                _BaseFolder = fbd.SelectedPath;
                LoadData(fbd.SelectedPath);
            }
            else
            {
                this.Close();
            }*/

        }
        private void LoadDrives()
        {
            List<Managers.FileSystemManager.DriveInfoWrapper> driveInfos = Managers.FileSystemManager.ListDrives();
            mnuDrive.DropDownItems.Clear();
            System.IO.DriveInfo validDi = null;
            foreach(Managers.FileSystemManager.DriveInfoWrapper di in driveInfos)
            {
                if (di.isValid)
                {
                    ToolStripMenuItem mi = new ToolStripMenuItem(di.di.Name + " (" + (di.di.TotalSize / 1024 / 1024).ToString() + "MB)");
                    mi.Enabled = true;
                    mi.Tag = di.di;
                    mi.Click += Mi_Click;
                    if(validDi==null)
                    {
                        validDi = di.di;
                    }
                    mnuDrive.DropDownItems.Add(mi);
                }
                else
                {
                    ToolStripMenuItem mi = new ToolStripMenuItem(di.di.Name + " (" + (di.message)+")");
                    mi.Enabled = false;
                
                    mnuDrive.DropDownItems.Add(mi);
                }
            }
            if(validDi!= null)
            {
                SelectDrive(validDi);
            }
        }
        private void SelectDrive(System.IO.DriveInfo di)
        {
            foreach(ToolStripMenuItem mi in mnuDrive.DropDownItems)
            {
               
                mi.Checked = mi.Tag == di;
            }
            LoadData(di.Name);
        }
        private void Mi_Click(object sender, EventArgs e)
        {

            SelectDrive((System.IO.DriveInfo)((ToolStripMenuItem)sender).Tag);
       
        }

        private void Form1_GameDeleted(object sender, EventArgs e)
        {
            LoadData(_BaseFolder);
        }

        private void LoadData(string baseFolder)
        {
            _BaseFolder = baseFolder;

            listBox1.Items.Clear();
            DatabaseManager.getInstance().LoadDatabase(baseFolder);
            DatabaseManager.GameInfo[] games = DatabaseManager.getInstance().ListGames();
            listBox1.Items.AddRange(games);
            Managers.FileSystemManager.getInstance().SetBasePath(baseFolder);
            txtScript.Text = Managers.FileSystemManager.getInstance().GetScript();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            if(listBox1.SelectedItem!= null)
            {
                GameInfo gi = new GameInfo((DatabaseManager.GameInfo)listBox1.SelectedItem);
                gi.Dock = DockStyle.Fill;
                groupBox1.Controls.Add(gi);
            }
        }

        private void btnAddGame_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() ==  DialogResult.OK)
            {
                long gameId = DatabaseManager.getInstance().GetNextGameId();
                string GamePath = System.IO.Path.Combine(_BaseFolder,"Games\\"+ gameId.ToString()+"\\gamedata\\");
                Managers.FileSystemManager.getInstance().CopyGame(fbd.SelectedPath, GamePath);
                DatabaseManager.getInstance().AddGame(System.IO.Path.GetFileName(fbd.SelectedPath), gameId);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtScript_TextChanged(object sender, EventArgs e)
        {
            Managers.FileSystemManager.getInstance().SaveScript(txtScript.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
