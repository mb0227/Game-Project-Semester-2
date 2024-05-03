using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameFramework.BL;
using System.Windows.Forms;

namespace GameFramework.Movement
{
    public class KeyboardMovement : IMovement
    {
        private Point Boundary;
        private int Speed;
        private int Offset = 50;

        public KeyboardMovement(int speed, Point boundary)
        {
            Speed = speed;
            Boundary = boundary;
        }

        public Point Move(Point point)
        {
            if (EZInput.Keyboard.IsKeyPressed(EZInput.Key.UpArrow))
            {
                if(point.Y + Speed > 7)
                {
                    point.Y -= Speed;
                }
            }
            if (EZInput.Keyboard.IsKeyPressed(EZInput.Key.DownArrow))
            {
                if (point.Y + Offset < Boundary.Y + 3)
                {
                    point.Y += Speed;
                }
            }
            if (EZInput.Keyboard.IsKeyPressed(EZInput.Key.LeftArrow))
            {
                if (point.X + Speed > 7)
                {
                    point.X -= Speed;
                }
            }
            if (EZInput.Keyboard.IsKeyPressed(EZInput.Key.RightArrow))
            {
                if (point.X + Offset < Boundary.X + 5)
                {
                    point.X += Speed;
                }
            }
            return point;
        }

        public int GetSpeed()
        {
            return Speed;
        }

        public Point GetBoundary()
        {
            return Boundary;
        }

        private void CreateBullet()
        {
            PictureBox bullet = new PictureBox();

        }
    }
}
