using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
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
                    try
                    {
                        ToolStripMenuItem mi = new ToolStripMenuItem(di.di.Name + " (" + (di.di.TotalSize / 1024 / 1024).ToString() + "MB)");
                        mi.Enabled = true;
                        mi.Tag = di.di;
                        mi.Click += Mi_Click;
                        if (validDi == null)
                        {
                            validDi = di.di;
                        }
                        mnuDrive.DropDownItems.Add(mi);
                    }
                    catch(Exception exc)
                    {

                    }
                }
                else
                {
                    try
                    {
                        ToolStripMenuItem mi = new ToolStripMenuItem(di.di.Name + " (" + (di.message) + ")");
                        mi.Enabled = false;

                        mnuDrive.DropDownItems.Add(mi);
                    }
                    catch(Exception exc)
                    {

                    }
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
                

                GameCopy gc = new GameCopy(fbd.SelectedPath, GamePath);
                gc.ShowDialog();
                DatabaseManager.getInstance().AddGame(System.IO.Path.GetFileName(fbd.SelectedPath), gameId,"-",2018,2);
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

        private void downloadBleemSyncToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            downloadBleemSyncToolStripMenuItem.DropDownItems.Clear();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                /*   System.Net.WebClient cli = new System.Net.WebClient();
               System.Net.ServicePointManager.Expect100Continue = true;
              ;
               string json = cli.DownloadString("https://api.github.com/repos/pathartl/BleemSync/releases");*/
            HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create("https://api.github.com/repos/pathartl/BleemSync/releases");
            req.UserAgent = "PSClassicTool";
            HttpWebResponse resp =(HttpWebResponse) req.GetResponse();
            System.IO.StreamReader rdr = new   System.IO.StreamReader(resp.GetResponseStream());
            string respData= rdr.ReadToEnd();
            rdr.Close();
            resp.Close();


            Newtonsoft.Json.Linq.JArray obj = Newtonsoft.Json.Linq.JArray.Parse(respData);
            foreach(Newtonsoft.Json.Linq.JObject o in obj)
            {
                ToolStripMenuItem itm = new ToolStripMenuItem();
                itm.Text = o["name"].ToString();
                itm.Tag = o;
                downloadBleemSyncToolStripMenuItem.DropDownItems.Add(itm);
                foreach(Newtonsoft.Json.Linq.JObject subObj in o["assets"])
                {
                    ToolStripMenuItem subItm = new ToolStripMenuItem();
                    subItm.Text = subObj["name"].ToString();
                    subItm.Tag = subObj;
                    subItm.Click += SubItm_Click;
                    itm.DropDownItems.Add(subItm);
                }

            }
            






            if (downloadBleemSyncToolStripMenuItem.DropDownItems.Count == 0)
            {
                downloadBleemSyncToolStripMenuItem.DropDownItems.Add("-");
            }
        }

        private void SubItm_Click(object sender, EventArgs e)
        {
            string theUrl = ((Newtonsoft.Json.Linq.JObject)((ToolStripMenuItem)sender).Tag)["browser_download_url"].ToString();
            System.Diagnostics.Process.Start(theUrl);

        }
    }
}
