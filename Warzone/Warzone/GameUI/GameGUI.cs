using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using GameFramework.GL;
using GameFramework.Movement;
using GameFramework.Collision;
using Guna.UI2.WinForms;
using EZInput;

namespace Warzone
{
    public partial class GameGUI : Form
    {
        private Game Game;
        private Label Score;
        private Label PlayerHealth;
        private Guna2ProgressBar ProgressBarPlayer;
        private List<Guna2ProgressBar> ProgressBarEnemies;
        private List<Label> EnemiesHealth;

        public GameGUI()
        {
            InitializeComponent();
            Score = new Label();
            PlayerHealth = new Label();
            ProgressBarPlayer = new Guna2ProgressBar();
            ProgressBarEnemies = new List<Guna2ProgressBar>();
            EnemiesHealth = new List<Label>();
            StartGame();
        }

        private void GameGUI_Load(object sender, EventArgs e)
        {
        }

        private void StartGame()
        {
            Game = Game.MakeGame(this);
            System.Drawing.Point boundary = new System.Drawing.Point(this.Width, this.Height);

            Game.AddGameObject(Images.enemy, GameObjectType.HorizontalEnemy, 100, 470, new HorizontalMovement(10, boundary, HorizontalEnemyDirection.Left));
            Game.AddGameObject(Images.enemy, GameObjectType.VerticalEnemy, 100, 600, new VerticalMovement(10, boundary, VerticalEnemyDirection.Down));
            Game.AddGameObject(Images.enemy, GameObjectType.ZigZagEnemy, 200, 70, new ZigZagMovement(6, boundary, ZigZagEnemyDirection.Left));
            Game.AddGameObject(Images.horizontalWall, GameObjectType.HorizontalWall, 300, 600);
            Game.AddGameObject(Images.verticalWall, GameObjectType.VerticalWall, 260, 320);
            Game.AddGameObject(Images.playerRight, GameObjectType.Player, 200, 163, 10, boundary);
            
            CollisionDetection collision1 = new CollisionDetection(GameObjectType.Player, GameObjectType.VerticalEnemy, ActionPerformed.IncreasePoints);
            CollisionDetection collision2 = new CollisionDetection(GameObjectType.Player, GameObjectType.HorizontalEnemy, ActionPerformed.DecreasePoints);
            CollisionDetection collision3 = new CollisionDetection(GameObjectType.Player, GameObjectType.VerticalEnemy, ActionPerformed.IncreaseHealth);
            CollisionDetection collision4 = new CollisionDetection(GameObjectType.Player, GameObjectType.HorizontalEnemy, ActionPerformed.DecreaseHealth);
            CollisionDetection collision5 = new CollisionDetection(GameObjectType.HorizontalEnemy, GameObjectType.Player, ActionPerformed.DecreaseHealth);
            CollisionDetection collision6 = new CollisionDetection(GameObjectType.VerticalEnemy, GameObjectType.Player, ActionPerformed.DecreaseHealth);
            CollisionDetection collision7 = new CollisionDetection(GameObjectType.ZigZagEnemy, GameObjectType.Player, ActionPerformed.DecreaseHealth);
            CollisionDetection collision8 = new CollisionDetection(GameObjectType.VerticalEnemy, GameObjectType.Bullet, ActionPerformed.DecreaseHealth);
            CollisionDetection collision9 = new CollisionDetection(GameObjectType.VerticalEnemy, GameObjectType.HorizontalWall, ActionPerformed.ChangeState);
            CollisionDetection collision10 = new CollisionDetection(GameObjectType.HorizontalEnemy, GameObjectType.VerticalWall, ActionPerformed.ChangeState);

            Game.AddCollision(collision1);
            Game.AddCollision(collision2);
            Game.AddCollision(collision3);
            Game.AddCollision(collision4);
            Game.AddCollision(collision5);
            Game.AddCollision(collision6);
            Game.AddCollision(collision7);
            Game.AddCollision(collision8);
            Game.AddCollision(collision9);
            Game.AddCollision(collision10);
            InitializeStats();
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            CheckStatus();
            Game.Update();
            UpdateStats();
        }

        private void UpdateStats() //to be called in tick event
        {
            Score.Text = "Score: " + Game.GetScore();
            ProgressBarPlayer.Value = Game.GetPlayerHealth();
            foreach (var gameObject in Game.GetGameObjects())
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
            Score.ForeColor = Color.Aquamarine;
            Score.Location = new System.Drawing.Point(0, 5);
            Score.Name = "Score";
            Score.Size = new Size(117, 39);
            Score.Text = "Score: ";
            Score.TextAlign = ContentAlignment.MiddleCenter;
            Sidebar.Controls.Add(Score);
        }

        private void InitializePlayerHealth()
        {
            PlayerHealth.Anchor = AnchorStyles.None;
            PlayerHealth.AutoSize = true;
            PlayerHealth.BackColor = Color.Transparent;
            PlayerHealth.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            PlayerHealth.ForeColor = Color.Lime;
            PlayerHealth.Location = new System.Drawing.Point(0, 32);
            PlayerHealth.Name = "PlayerHealth";
            PlayerHealth.Size = new Size(40, 20);
            PlayerHealth.Text = "Player HP: ";
            PlayerHealth.Visible = true;

            ProgressBarPlayer.Anchor = AnchorStyles.None;
            ProgressBarPlayer.BackColor = SystemColors.ActiveCaptionText;
            ProgressBarPlayer.Location = new System.Drawing.Point(85, 30);
            ProgressBarPlayer.Name = "ProgressBarPlayer";
            ProgressBarPlayer.ProgressColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            ProgressBarPlayer.ProgressColor2 = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            ProgressBarPlayer.Size = new Size(80, 18);
            ProgressBarPlayer.Style = ProgressBarStyle.Continuous;
            ProgressBarPlayer.TabIndex = 0;
            ProgressBarPlayer.Value = 100;
            ProgressBarPlayer.Text = "ProgressBar";
            ProgressBarPlayer.TextRenderingHint = TextRenderingHint.SystemDefault;
            Sidebar.Controls.Add(PlayerHealth);
            Sidebar.Controls.Add(ProgressBarPlayer);
        }

        private void InitializeEnemiesHealth()
        {
            int enemiesCount = Game.GetVerticalEnemiesCount() + Game.GetHorizontalEnemiesCount()+ Game.GetZigZagEnemiesCount();
            int x = 0, y=55;
            for (int i = 0; i < enemiesCount; i++)
            {
                Label enemyHealth = new Label();
                enemyHealth.Anchor = AnchorStyles.None;
                enemyHealth.AutoSize = true;
                enemyHealth.BackColor = Color.Transparent;
                enemyHealth.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                enemyHealth.ForeColor = Color.Red;
                enemyHealth.Location = new System.Drawing.Point(x, y+3);
                enemyHealth.Name = "EnemyHealth";
                enemyHealth.Size = new Size(20, 15);
                enemyHealth.Text = "Enemy " + (i+1) +" HP: ";
                enemyHealth.Visible = true;

                Guna2ProgressBar enemyProgressBar = new Guna2ProgressBar();
                enemyProgressBar.Anchor = AnchorStyles.None;
                enemyProgressBar.BackColor = SystemColors.ActiveCaptionText;
                enemyProgressBar.Location = new System.Drawing.Point(x+84, y);
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
                Sidebar.Controls.Add(enemyHealth);
                Sidebar.Controls.Add(enemyProgressBar);
            }
        }

        private void CheckStatus()
        {
            CheckLose();
            CheckWin();
            CheckConditions();
        }

        private void CheckLose()
        {
            if (Game.GetPlayerHealth() <= 0)
            {
                GameLoop.Stop();
                GameOver gameOver = new GameOver(Images.lose);
                Game.GameOver();
                gameOver.Show();
                this.Hide();
            }
        }

        private void CheckWin()
        {
            if (Game.GetScore() >= 100 && EnemiesDead())
            {
                GameLoop.Stop();
                GameOver gameOver = new GameOver(Images.win);
                Game.GameOver();
                gameOver.Show();
            }
        }

        private bool EnemiesDead()
        {
            List<int> enemiesHealth = Game.GetEnemiesHealth();
            foreach (int health in enemiesHealth)
            {
                if (health > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void CheckConditions()
        {
            if (Keyboard.IsKeyPressed(Key.Escape))
            {
                GameLoop.Stop();
                this.Hide();
            }
        }
    }
}
