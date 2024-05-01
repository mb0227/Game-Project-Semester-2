using GameFramework.BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Movement
{
    public abstract class EnemyMovement
    {
        protected Point Boundary;
        protected string Direction;
        protected int Speed;
        protected int Offset = 50;

        public EnemyMovement(int speed, Point boundary, string direction)
        {
            Speed = speed;
            Direction = direction;
            Boundary = boundary;
        }

        public string GetDirection()
        {
            return Direction;
        }

        public abstract Point Move(Point point);
    }
}
