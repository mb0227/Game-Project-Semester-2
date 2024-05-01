using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameFramework.BL
{
    public class Player
    {
        private PictureBox PlayerImage;
        private int Speed;
        private int Health;

        public Player()
        {            
        }
        public Player(Image image, int top, int left, int speed)
        {
            PlayerImage = new PictureBox();
            PlayerImage.Image = image;
            PlayerImage.Top = top;
            PlayerImage.Left = left;
            PlayerImage.Visible = true;
            PlayerImage.SizeMode = PictureBoxSizeMode.StretchImage;
            PlayerImage.Width = 100;
            PlayerImage.Height = 70;
            PlayerImage.BackColor = Color.Transparent;
            PlayerImage.BringToFront();
            Speed = speed;
            Health = 100;
        }

        public PictureBox GetImage()
        {
            return PlayerImage;
        }

        public void MoveLeft()
        {
            PlayerImage.Left -= Speed;
        }

        public void MoveRight()
        {
            PlayerImage.Left += Speed;
        }

        public void MoveUp()
        {
            PlayerImage.Top -= Speed;
        }

        public void MoveDown()
        {
            PlayerImage.Top += Speed;
        }

    }
}
