using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;
using System.Windows.Forms;
using GameFramework.BL;
using GameFramework.Movement;
using GameFramework.Collision;
using Guna.UI2.WinForms;

namespace Warzone
{
    public partial class GameGUI : Form
    {
        private Game Game;
        private Label Score = new Label();
        private Label PlayerHealth = new Label();
        private Guna2ProgressBar ProgressBarPlayer = new Guna2ProgressBar();
        private List<Guna2ProgressBar> ProgressBarEnemies = new List<Guna2ProgressBar>();
        private List<Label> EnemiesHealth = new List<Label>();

        public GameGUI()
        {
            InitializeComponent();
        }

        private void GameGUI_Load(object sender, EventArgs e)
        {
            Game = Game.MakeGame(this);
            Point boundary = new Point(this.Width, this.Height);
            if (Game != null)
            {                
                Game.AddGameObject(Images.enemy,GameObjectType.HorizontalEnemy, 100, 70, new HorizontalMovement(10, boundary, HorizontalEnemyDirection.Left));
                Game.AddGameObject(Images.enemy, GameObjectType.VerticalEnemy, 200, 600, new VerticalMovement(10, boundary, VerticalEnemyDirection.Up));
                Game.AddGameObject(Images.enemy, GameObjectType.ZigZagEnemy, 200, 70, new ZigZagMovement(1, boundary, ZigZagEnemyDirection.Left));
                Game.AddGameObject(Images.Screenshot_2024_05_02_190532_removebg_preview, GameObjectType.Player, 200, 70, new KeyboardMovement(10, boundary));
                CollisionDetection collision = new CollisionDetection(GameObjectType.Player, GameObjectType.VerticalEnemy, ActionPerformed.IncreasePoints);
                CollisionDetection collision2 = new CollisionDetection(GameObjectType.Player, GameObjectType.HorizontalEnemy, ActionPerformed.DecreasePoints);
                CollisionDetection collision3 = new CollisionDetection(GameObjectType.Player, GameObjectType.VerticalEnemy, ActionPerformed.IncreaseHealth);
                CollisionDetection collision4 = new CollisionDetection(GameObjectType.Player, GameObjectType.HorizontalEnemy, ActionPerformed.DecreaseHealth);
                CollisionDetection collision5 = new CollisionDetection(GameObjectType.HorizontalEnemy, GameObjectType.Player, ActionPerformed.DecreaseHealth);
                CollisionDetection collision6 = new CollisionDetection(GameObjectType.VerticalEnemy, GameObjectType.Player, ActionPerformed.DecreaseHealth);
                CollisionDetection collision7 = new CollisionDetection(GameObjectType.ZigZagEnemy, GameObjectType.Player, ActionPerformed.DecreaseHealth);

                CollisionDetection collision8 = new CollisionDetection(GameObjectType.HorizontalEnemy, GameObjectType.Ground, ActionPerformed.ChangeState);
                CollisionDetection collision9 = new CollisionDetection(GameObjectType.VerticalEnemy, GameObjectType.Ground, ActionPerformed.ChangeState);

                Game.AddCollision(collision);
                Game.AddCollision(collision2);
                Game.AddCollision(collision3);
                Game.AddCollision(collision4);
                Game.AddCollision(collision5);
                Game.AddCollision(collision6);
                Game.AddCollision(collision7);
                Game.AddCollision(collision8);
                Game.AddCollision(collision9);
                InitializeStats();
            }
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            Game.Update();
            UpdateStats();
        }

        private void UpdateStats() //to be called in tick event
        {
            Score.Text = "Score: " + Game.GetScore();
            ProgressBarPlayer.Value = Game.GetPlayerHealth();
            foreach (GameObject gameObject in Game.GetGameObjects())
            {
                if (gameObject.GetGameObjectType() == GameObjectType.VerticalEnemy || gameObject.GetGameObjectType() == GameObjectType.HorizontalEnemy || gameObject.GetGameObjectType() == GameObjectType.ZigZagEnemy)
                {
                    int index = Game.GetGameObjects().IndexOf(gameObject);
                    ProgressBarEnemies[index].Value = gameObject.GetHealth();
                }
            }
        }
        
        private void InitializeStats() //to be called after defining collisions
        {
            InitializeScore();
            InitializePlayerHealth();
            InitializeEnemiesHealth();
        }
        
        private void InitializeScore()
        {
            Score.Anchor = AnchorStyles.None;
            Score.AutoSize = true;
            Score.BackColor = Color.Transparent;
            Score.Font = new Font("Tahoma", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            Score.ForeColor = Color.Red;
            Score.Location = new Point(625, 5);
            Score.Name = "Score";
            Score.Size = new Size(117, 39);
            Score.Text = "Score: ";
            Score.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(Score);
        }

        private void InitializePlayerHealth()
        {
            PlayerHealth.Anchor = AnchorStyles.None;
            PlayerHealth.AutoSize = true;
            PlayerHealth.BackColor = Color.Transparent;
            PlayerHealth.Font = new Font("Tahoma", 7F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            PlayerHealth.ForeColor = Color.Lime;
            PlayerHealth.Location = new Point(551, 33);
            PlayerHealth.Name = "PlayerHealth";
            PlayerHealth.Size = new Size(40, 20);
            PlayerHealth.Text = "Player Health: ";
            PlayerHealth.Visible = true;

            ProgressBarPlayer.Anchor = AnchorStyles.None;
            ProgressBarPlayer.BackColor = SystemColors.ActiveCaptionText;
            ProgressBarPlayer.Location = new Point(640, 30);
            ProgressBarPlayer.Name = "ProgressBarPlayer";
            ProgressBarPlayer.ProgressColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            ProgressBarPlayer.ProgressColor2 = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            ProgressBarPlayer.Size = new Size(80, 18);
            ProgressBarPlayer.Style = ProgressBarStyle.Continuous;
            ProgressBarPlayer.TabIndex = 0;
            ProgressBarPlayer.Value = 100;
            ProgressBarPlayer.Text = "ProgressBar";
            ProgressBarPlayer.TextRenderingHint = TextRenderingHint.SystemDefault;
            Controls.Add(PlayerHealth);
            Controls.Add(ProgressBarPlayer);
        }

        private void InitializeEnemiesHealth()
        {
            int enemiesCount = Game.GetVerticalEnemiesCount() + Game.GetHorizontalEnemiesCount()+ Game.GetZigZagEnemiesCount();
            int x = 551, y=55;
            for (int i = 0; i < enemiesCount; i++)
            {
                Label enemyHealth = new Label();
                enemyHealth.Anchor = AnchorStyles.None;
                enemyHealth.AutoSize = true;
                enemyHealth.BackColor = Color.Transparent;
                enemyHealth.Font = new Font("Tahoma", 7F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                enemyHealth.ForeColor = Color.Red;
                enemyHealth.Location = new Point(x, y+3);
                enemyHealth.Name = "EnemyHealth";
                enemyHealth.Size = new Size(40, 20);
                enemyHealth.Text = "Enemy " + (i+1) +" Health: ";
                enemyHealth.Visible = true;

                Guna2ProgressBar enemyProgressBar = new Guna2ProgressBar();
                enemyProgressBar.Anchor = AnchorStyles.None;
                enemyProgressBar.BackColor = SystemColors.ActiveCaptionText;
                enemyProgressBar.Location = new Point(x+89, y);
                enemyProgressBar.Name = "ProgressBarEnemy";
                enemyProgressBar.ProgressColor = Color.Red;
                enemyProgressBar.ProgressColor2 = Color.Red;
                enemyProgressBar.Size = new Size(80, 18);
                enemyProgressBar.Style = ProgressBarStyle.Continuous;
                enemyProgressBar.TabIndex = 0;
                enemyProgressBar.Value = 100;
                enemyProgressBar.Text = "ProgressBarEnemy";
                enemyProgressBar.TextRenderingHint = TextRenderingHint.SystemDefault;
                y += 25;
                ProgressBarEnemies.Add(enemyProgressBar);
                EnemiesHealth.Add(enemyHealth);
                Controls.Add(enemyHealth);
                Controls.Add(enemyProgressBar);
            }
        }
    }
}
