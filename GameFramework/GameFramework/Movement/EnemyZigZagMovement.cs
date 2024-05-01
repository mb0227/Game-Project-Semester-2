using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Movement
{
    public class EnemyZigZagMovement : EnemyMovement
    {
        private int stepCount = 0;
        private int stepsBeforeDirectionChange = 10; 

        public EnemyZigZagMovement(int speed, Point boundary, string direction) : base(speed, boundary, direction)
        {
        }

        public override Point Move(Point point)
        {           
            return point;
        }

    }
}
