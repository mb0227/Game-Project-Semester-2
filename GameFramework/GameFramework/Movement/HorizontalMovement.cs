using GameFramework.BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Movement
{
    public class HorizontalMovement : IMovement
    {
        private HorizontalEnemyDirection Direction;
        private Point Boundary;
        private int Speed;
        private int Offset = 50;

        public HorizontalMovement(int speed, Point boundary, HorizontalEnemyDirection direction)
        {
            Speed = speed;
            Boundary = boundary;
            Direction = direction;
        }

        public Point Move(Point point)
        {
            if ((point.X + Offset + 30) >= Boundary.X)
                Direction = HorizontalEnemyDirection.Left;
            else if ((point.X - Offset + 50) <= 0)
                Direction = HorizontalEnemyDirection.Right;

            if (Direction == HorizontalEnemyDirection.Left)
                point.X -= Speed;
            else
                point.X += Speed;

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
