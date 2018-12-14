using System;
using System.Drawing;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
namespace PSClassicTool
{
    public partial class GameInfo : UserControl
    {
        DatabaseManager.DiscInfo[] discInfos;
        DatabaseManager.GameInfo _gi;
        bool loadingData = false;
        public GameInfo(DatabaseManager.GameInfo gi)
        {
            InitializeComponent();
            _gi = gi;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            LoadData();
            if(comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                LoadDisc(null);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDisc((DatabaseManager.DiscInfo)comboBox1.SelectedItem);
        }
        private void LoadDisc(DatabaseManager.DiscInfo di)
        {
            if(di == null)
            {
                grpDisc.Visible = false;
            }
            else
            {
                grpDisc.Visible = true;
                grpDisc.Text = di.ToString();
                txtBaseName.Text = di.BASENAME;
                _CurrentDisc = di;
                try
                {
                    pictureBox1.Image = Bitmap.FromFile(Managers.FileSystemManager.getInstance().GetBoxArtPath(di.GAME_ID, di.BASENAME));
                }
                catch(Exception exc)
                {
                    pictureBox1.Image = null;
                }
            }
        }
        private DatabaseManager.DiscInfo _CurrentDisc;
        private void LoadData()
        {
            loadingData = true;
            txtGameName.Text = _gi.GAME_TITLE_STRING;
            txtPublisher.Text = _gi.PUBLISHER_NAME;
            nudYear.Value = _gi.RELEASE_YEAR;
            nudPlayers.Value = _gi.PLAYERS;
            nudGameId.Value = _gi.GAME_ID;

            discInfos = DatabaseManager.getInstance().GetDiscInfo(_gi.GAME_ID);
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(discInfos);

            this.chkConfig.Checked = System.IO.File.Exists(Managers.FileSystemManager.getInstance().GetConfigFilePath(_gi.GAME_ID));
            chkDoubleResolution.Checked = false;
            chkDoubleResolution.Enabled = chkConfig.Checked;
            if (chkDoubleResolution.Enabled)
            {

                
                chkDoubleResolution.Checked = ConfigFileHandler.GetSettingValue(Managers.FileSystemManager.getInstance().GetConfigFilePath(_gi.GAME_ID), "gpu_neon.enhancement_enable") == "1";
                
            }
            loadingData = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {


        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            Managers.FileSystemManager.getInstance().DeleteGame(_gi.GAME_ID);
            DatabaseManager.getInstance().DeleteGame(_gi.GAME_ID);
        }

        private void btnDefaultConfig_Click(object sender, EventArgs e)
        {
            System.IO.File.Copy("pcsx.cfg", Managers.FileSystemManager.getInstance().GetConfigFilePath(_gi.GAME_ID));
            LoadData();
        }
        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {

                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }

            }

            return destImage;
        }
        private void btnReplace_Click(object sender, EventArgs e)
        {
            if(txtUrl.Text == "")
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "PNG|*.png";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.Copy(ofd.FileName, Managers.FileSystemManager.getInstance().GetBoxArtPath(_CurrentDisc.GAME_ID, _CurrentDisc.BASENAME));
                    Console.Write("");
                    try
                    {
                        pictureBox1.Image = Bitmap.FromFile(Managers.FileSystemManager.getInstance().GetBoxArtPath(_CurrentDisc.GAME_ID, _CurrentDisc.BASENAME));
                    }
                    catch (Exception exc)
                    {
                        //pictureBox1.Image = null;
                    }

                }
            }
            else
            {
                System.Net.WebClient cli = new System.Net.WebClient();
                byte[] data = cli.DownloadData(txtUrl.Text);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                ms.Write(data, 0, data.Length);
                ms.Seek(0,System.IO.SeekOrigin.Begin);
                Image img = Bitmap.FromStream(ms);

                Bitmap resized = ResizeImage(img, 500, 500);
                resized.Save(Managers.FileSystemManager.getInstance().GetBoxArtPath(_CurrentDisc.GAME_ID, _CurrentDisc.BASENAME), System.Drawing.Imaging.ImageFormat.Png);

                try
                {
                    pictureBox1.Image = Bitmap.FromFile(Managers.FileSystemManager.getInstance().GetBoxArtPath(_CurrentDisc.GAME_ID, _CurrentDisc.BASENAME));
                }
                catch (Exception exc)
                {
                    pictureBox1.Image = null;
                }

            }
        }

        private void chkDoubleResolution_CheckedChanged(object sender, EventArgs e)
        {
          
            if(!loadingData)
            {
                string targetValue = "0";
                if(chkDoubleResolution.Checked)
                {
                    targetValue = "1";
                }
                ConfigFileHandler.SetSettingValue(Managers.FileSystemManager.getInstance().GetConfigFilePath(_gi.GAME_ID), "gpu_neon.enhancement_enable", targetValue);
            }
        }
        private void SaveGame()
        {
            if (!loadingData)
            {
                DatabaseManager.getInstance().UpdateGame(_gi.GAME_ID, _gi.GAME_TITLE_STRING, _gi.PUBLISHER_NAME, _gi.RELEASE_YEAR, _gi.PLAYERS);
            }
        }
        private void txtGameName_TextChanged(object sender, EventArgs e)
        {
            _gi.GAME_TITLE_STRING = txtGameName.Text;
            SaveGame();
        }

        private void txtPublisher_TextChanged(object sender, EventArgs e)
        {
            _gi.PUBLISHER_NAME = txtPublisher.Text;
            SaveGame();
        }

        private void nudYear_ValueChanged(object sender, EventArgs e)
        {
            _gi.RELEASE_YEAR =(long) nudYear.Value;
            SaveGame();
        }

        private void nudPlayers_ValueChanged(object sender, EventArgs e)
        {
            _gi.PLAYERS = (long)nudPlayers.Value;
            SaveGame();
        }
    }
}
