using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameFramework.GL;
using System.Windows.Forms;

namespace GameFramework.Movement
{
    public class KeyboardMovement : IMovement
    {
        private Point Boundary;
        private int Speed;
        private IPlayer player;
        private int Offset = 50;

        public KeyboardMovement(int speed, Point boundary, IPlayer player)
        {
            Speed = speed;
            Boundary = boundary;
            this.player = player;
        }

        public void SetPlayer(IPlayer player)
        {
            this.player = player;
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
                if (point.X + Speed > 163)
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
            if (EZInput.Keyboard.IsKeyPressed(EZInput.Key.Space))
            {
                player.Fire();
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
    }
}
