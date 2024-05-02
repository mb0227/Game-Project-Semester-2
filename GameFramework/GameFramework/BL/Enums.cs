using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace GameFramework.BL
{
    public enum GameObjectType
    {
        Player,
        HorizontalEnemy,
        VerticalEnemy,
        ZigZagEnemy,
        Ground
    }

    public enum ActionPerformed
    {
        IncreasePoints,
        DecreasePoints,
        IncreaseHealth,
        DecreaseHealth,
        ChangeState
    }

    public enum MovementType
    {
        Vertical,
        Horizontal,
        ZigZag,
        Keyboard
    }

    public enum VerticalEnemyDirection
    {
        Up,
        Down
    }

    public enum HorizontalEnemyDirection
    {
        Left,
        Right
    }

    public enum ZigZagEnemyDirection
    {
        Right,
        Left
    }
}
