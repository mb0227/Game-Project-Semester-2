using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Movement
{
    public class EnemyVerticalMovement : EnemyMovement
    {
        public EnemyVerticalMovement(int speed, Point boundary, string direction) : base(speed, boundary, direction)
        {
        }
        public override Point Move(Point point)
        {
            if ((point.Y + Offset + 10) >= Boundary.Y)
            {
                Direction = "up";
            }
            else if ((point.Y - Offset + 50) <= 0)
            {
                Direction = "down";
            }
            if (Direction == "up")
            {
                point.Y -= Speed;
            }
            else
            {
                point.Y += Speed;
            }

            return point;
        }
    }
}
