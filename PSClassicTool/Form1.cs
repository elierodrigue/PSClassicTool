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
            GameManager.getInstance().GameDeleted += Form1_GameDeleted;
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
            List<GameManager.DriveInfoWrapper> driveInfos = GameManager.ListDrives();
            mnuDrive.DropDownItems.Clear();
            System.IO.DriveInfo validDi = null;
            foreach(GameManager.DriveInfoWrapper di in driveInfos)
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
            GameManager.getInstance().LoadDatabase(baseFolder);
            GameManager.GameInfo[] games = GameManager.getInstance().ListGames();
            listBox1.Items.AddRange(games);
          
            txtScript.Text = GameManager.getInstance().GetScript();
            FillSizeInfo();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            if(listBox1.SelectedItem!= null)
            {
                GameInfo gi = new GameInfo((GameManager.GameInfo)listBox1.SelectedItem);
                gi.Dock = DockStyle.Fill;
                groupBox1.Controls.Add(gi);
            }
        }

        private void btnAddGame_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() ==  DialogResult.OK)
            {
                long gameId = GameManager.getInstance().GetNextGameId();
                string GamePath = System.IO.Path.Combine(_BaseFolder,"Games\\"+ gameId.ToString()+"\\gamedata\\");

                GameManager.GameInfo gi = new GameManager.GameInfo();
                gi.GAME_ID = gameId;
                string[] splittedPath = fbd.SelectedPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                gi.GAME_TITLE_STRING = splittedPath[splittedPath.Length - 1];
                gi.RELEASE_YEAR = 2018;
                gi.PLAYERS = 2;
                GameCopy gc = new GameCopy(fbd.SelectedPath, GamePath);
                gc.ShowDialog();
                gi.SaveIni();
                LoadData(_BaseFolder);
               
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtScript_TextChanged(object sender, EventArgs e)
        {
            GameManager.getInstance().SaveScript(txtScript.Text);
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
        private void FillSizeInfo()
        {
            try
            {
                string drive = _BaseFolder;
                foreach (System.IO.DriveInfo inf in System.IO.DriveInfo.GetDrives())
                {
                    if (drive.ToLower().StartsWith(inf.RootDirectory.FullName.ToLower()))
                    {
                        long totalSize = inf.TotalSize;
                        long used = totalSize - inf.AvailableFreeSpace;
                        progressBar1.Value = (int)(used * 100 / totalSize);
                        used = used / 1024 / 1024 / 1024;
                        totalSize = totalSize / 1024 / 1024 / 1024;
                        lblSpaceInfo.Text = Math.Round((double)used, 2).ToString() + "gB/" + Math.Round((double)totalSize, 2).ToString() + "gb";
                    }
                }
            }
            catch(Exception exc)
            {

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*Make sure the games are out of the cleanup order*/
            for (int x = 0; x < listBox1.Items.Count; x++)
            {
                GameManager.GameInfo gi = (GameManager.GameInfo)listBox1.Items[x];
              
                    string originalPath = GameManager.getInstance().GetGamePath(gi.GAME_ID);
                gi.GAME_ID = gi.GAME_ID + 9001;
                    string destPath = GameManager.getInstance().GetGamePath(gi.GAME_ID);
                    System.IO.Directory.Move(originalPath, destPath);

                
            }

            /*Fix Game Ids*/
            for (int x =0;x<listBox1.Items.Count;x++)
            {
                GameManager.GameInfo gi = (GameManager.GameInfo)listBox1.Items[x];
                if(gi.GAME_ID != x+1)
                {
                    string originalPath = GameManager.getInstance().GetGamePath(gi.GAME_ID);
                    gi.GAME_ID = x + 1;
                    string destPath = GameManager.getInstance().GetGamePath(gi.GAME_ID);
                    System.IO.Directory.Move(originalPath, destPath);

                }
            }

            foreach(GameManager.GameInfo gi in listBox1.Items)
            {
                gi.SaveIni();
            }
            System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo(System.IO.Path.Combine(_BaseFolder, "BleemSync\\BleemSync.exe"));
            System.Diagnostics.Process.Start(si);
        }

        private void forceOpenFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                LoadData(fbd.SelectedPath);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<GameManager.GameInfo> gis = new List<GameManager.GameInfo>();
            foreach (GameManager.GameInfo gi in listBox1.Items)
            {
                gis.Add(gi);
            }
            gis.Sort();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(gis.ToArray());
        }
    }
}
