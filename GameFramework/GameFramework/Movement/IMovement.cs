using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameFramework.Movement
{
    public interface IMovement
    {
       Point Move(Point point);
    }
}
