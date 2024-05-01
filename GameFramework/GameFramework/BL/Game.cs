using GameFramework.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using EZInput;

namespace GameFramework.BL
{
    public class Game
    {
        private List<Enemy> Enemies;
        private Player Player;
        private Form GameForm;

        public Game(Form gameForm)
        {
            GameForm = gameForm;
            Enemies = new List<Enemy>();
            Player = new Player();
        }

        public void AddEnemy(Image image, int top, int left, EnemyMovement movement)
        {
            Enemy enemy = new Enemy(image, top, left, movement);
            Enemies.Add(enemy);
            GameForm.Controls.Add(enemy.GetImage());
        }

        public void AddPlayer(Image image, int top, int left,int speed)
        {
            Player = new Player(image, top, left,speed);
            GameForm.Controls.Add(Player.GetImage());
        }

        public void Update()
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update();
            }
        }

        public void MovePlayer(Key key)
        {
            if (Keyboard.IsKeyPressed(Key.LeftArrow))
                Player.MoveLeft();
            else if (Keyboard.IsKeyPressed(Key.RightArrow))
                Player.MoveRight();
            else if (Keyboard.IsKeyPressed(Key.UpArrow))
                Player.MoveUp();
            else if (Keyboard.IsKeyPressed(Key.DownArrow))
                Player.MoveDown();
            else
                return;
        }
    }
}
