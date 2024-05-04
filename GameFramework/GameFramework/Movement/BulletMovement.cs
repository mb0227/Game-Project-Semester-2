using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameFramework.GL;

namespace GameFramework.Movement
{
    public class BulletMovement : IMovement
    {
        private int Speed;
        private BulletDirection Direction;

        public BulletMovement(int speed, BulletDirection direction)
        {
            Speed = speed;
            Direction = direction;
        }

        public Point Move(Point point)
        {
           if (Direction == BulletDirection.Left)
           {
               Console.WriteLine("Bullet is moving left");
               point.X -= Speed;
           }
           else if (Direction == BulletDirection.Right)
           {
               point.X += Speed;
           }
           return point;
        }
    }
}

