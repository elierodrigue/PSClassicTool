using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSClassicTool
{
    public partial class GameInfo : UserControl
    {
        DatabaseManager.DiscInfo[] discInfos;
        DatabaseManager.GameInfo _gi;
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
            }
        }
        private void LoadData()
        {
            txtGameName.Text = _gi.GAME_TITLE_STRING;
            txtPublisher.Text = _gi.PUBLISHER_NAME;
            nudYear.Value = _gi.RELEASE_YEAR;
            nudPlayers.Value = _gi.PLAYERS;
            nudGameId.Value = _gi.GAME_ID;

            discInfos = DatabaseManager.getInstance().GetDiscInfo(_gi.GAME_ID);
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(discInfos);
           

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
            
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            Managers.FileSystemManager.getInstance().DeleteGame(_gi.GAME_ID);
            DatabaseManager.getInstance().DeleteGame(_gi.GAME_ID);
        }
    }
}
