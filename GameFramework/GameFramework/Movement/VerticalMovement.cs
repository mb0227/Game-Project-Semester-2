using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameFramework.GL;

namespace GameFramework.Movement
{
    public class VerticalMovement : IMovement
    {
        private VerticalEnemyDirection Direction;
        private Point Boundary;
        private int Speed;
        private int Offset = 50;

        public VerticalMovement(int speed, Point boundary, VerticalEnemyDirection direction) 
        {
            Speed = speed;
            Boundary = boundary;
            Direction = direction;
        }

        public Point Move(Point point)
        {
            if ((point.Y + Offset + 10) >= Boundary.Y)
                Direction = VerticalEnemyDirection.Up;
            else if ((point.Y - Offset + 50) <= 0)
            {
                Direction = VerticalEnemyDirection.Down;
            }

            if (Direction == VerticalEnemyDirection.Up)
                point.Y -= Speed;
            else
                point.Y += Speed;

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

        public int GetOffset()
        {
            return Offset;
        }
    }
}
