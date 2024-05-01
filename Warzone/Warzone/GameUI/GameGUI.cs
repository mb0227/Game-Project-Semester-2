using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameFramework.BL;
using GameFramework.Movement;
using Warzone.Properties;
using EZInput;

namespace Warzone
{
    public partial class GameGUI : Form
    {
        private Game Game;
        private Key key;
        public GameGUI()
        {
            InitializeComponent();
        }

        private void GameGUI_Load(object sender, EventArgs e)
        {
            Game = new Game(this);
            System.Drawing.Point boundary = new System.Drawing.Point(this.Width, this.Height);
            Game.AddEnemy(Images.Enemy1, 100, 70, new EnemyHorizontalMovement(10, boundary, "left"));
            Game.AddEnemy(Images.Enemy2, 200, 70, new EnemyVerticalMovement(10, boundary, "up"));
            Game.AddPlayer(Images.Player, 100, 10, 10);
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            Game.Update();
            Game.MovePlayer(key);
        }
    }
}
