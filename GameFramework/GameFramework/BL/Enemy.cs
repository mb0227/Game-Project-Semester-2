using GameFramework.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameFramework.BL
{
    public class Enemy
    {
        private PictureBox EnemyImage;
        private EnemyMovement Movement;
        private int Health;

        public Enemy(Image enemyImage, int top, int left, EnemyMovement movement)
        {            
            EnemyImage = new PictureBox();
            EnemyImage.Image = enemyImage;
            EnemyImage.Top = top;
            EnemyImage.Left = left;
            EnemyImage.Visible = true;
            EnemyImage.SizeMode = PictureBoxSizeMode.StretchImage;
            EnemyImage.Width = 100;
            EnemyImage.Height = 70;
            EnemyImage.BackColor = Color.Transparent;
            EnemyImage.BringToFront();
            Movement = movement;
            Health = 100;
        }

        public void Update()
        {
            EnemyImage.Location = Movement.Move(EnemyImage.Location);
        }

        public PictureBox GetImage()
        {
            return EnemyImage;
        }
    }
}
