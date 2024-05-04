using GameFramework.GL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Movement
{
    interface IMovementState
    {
        void UpdateState(GameObject gameObject);
    }
}
