using GameFramework.BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Movement
{
    public class EnemyHorizontalMovement : EnemyMovement
    {
        public EnemyHorizontalMovement(int speed, Point boundary, string direction) : base(speed, boundary, direction)
        {
        }
        public override Point Move(Point point)
        {
            if ((point.X + Offset + 30) >= Boundary.X)
            {
                Direction = "left";
            }
            else if ((point.X - Offset + 50) <= 0)
            {
                Direction = "right";
            }
            if (Direction == "left")
            {
                point.X -= Speed;
            }
            else
            {
                point.X += Speed;
            }

            return point;
        }

    }
}
