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
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                Managers.FileSystemManager.getInstance().SetBasePath(fbd.SelectedPath);
                DatabaseManager.getInstance().GameDeleted += Form1_GameDeleted;
                _BaseFolder = fbd.SelectedPath;
                LoadData(fbd.SelectedPath);
            }
            else
            {
                this.Close();
            }
            
        }

        private void Form1_GameDeleted(object sender, EventArgs e)
        {
            LoadData(_BaseFolder);
        }

        private void LoadData(string baseFolder)
        {
            listBox1.Items.Clear();
            DatabaseManager.getInstance().LoadDatabase(System.IO.Path.Combine(baseFolder,"custom.db"));
            DatabaseManager.GameInfo[] games = DatabaseManager.getInstance().ListGames();
            listBox1.Items.AddRange(games);
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
                string GamePath = System.IO.Path.Combine(_BaseFolder, gameId.ToString());
                Managers.FileSystemManager.getInstance().CopyGame(fbd.SelectedPath, GamePath);
                DatabaseManager.getInstance().AddGame(System.IO.Path.GetFileName(fbd.SelectedPath), gameId);
            }
        }
    }
}
