using GameFramework.BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Movement
{
    public class ZigZagMovement : IMovement
    {
        private int count = 0;
        private Point Boundary;
        private int Speed;
        private int Offset = 50;        
        private ZigZagEnemyDirection Direction;

        public ZigZagMovement(int speed, Point boundary, ZigZagEnemyDirection direction) 
        {
            Speed = speed;
            Boundary = boundary;
            Direction = direction;
        }

        public Point Move(Point point)
        {          
            if(Direction == ZigZagEnemyDirection.Right)
            {
                if(count<5)
                {
                    point.X += Speed;
                    point.Y -= Speed;
                }
                else if(count>=5 && count <10)
                {
                    point.X += Speed;
                    point.Y += Speed;
                }
            }
            else if(Direction == ZigZagEnemyDirection.Left)
            {
                if (count < 5)
                {
                    point.X -= Speed;
                    point.Y += Speed;
                }
                else if (count >= 5 && count < 10)
                {
                    point.X -= Speed;
                    point.Y -= Speed;
                }
            }

            if((point.X+Offset)>=Boundary.X)
                Direction = ZigZagEnemyDirection.Left;
            else if((point.X-Offset)<=0)
                Direction = ZigZagEnemyDirection.Right;

            if (count == 10)
                count = 0;

            count++;
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
