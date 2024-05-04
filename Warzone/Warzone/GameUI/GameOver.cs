using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warzone
{
    public partial class GameOver : Form
    {
        public GameOver()
        {
            InitializeComponent();
        }
        
        public GameOver(Image image)
        {
            InitializeComponent();
            this.BackgroundImage = image;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            PlayAgainBtn.BringToFront();
            ExitBtn.BringToFront();
        }

        private void GameOver_Load(object sender, EventArgs e)
        {
        }

        private void PlayAgainBtn_Click(object sender, EventArgs e)
        {
            GameGUI game = new GameGUI();
            game.Show();
            this.Hide();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
