using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warzone.GameUI
{
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void PlayGameBtn_Click(object sender, EventArgs e)
        {
            GameGUI gameGUI = new GameGUI();
            gameGUI.ShowDialog();
            this.Hide();
        }
    }
}
