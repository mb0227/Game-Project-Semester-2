using GameFramework.Movement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameFramework.GL
{
    public class Player : GameObject, IPlayer 
    {        
        public Player(){}
        public Player(Image elementImage, GameObjectType gameObjectType, int top, int left) : base(elementImage, gameObjectType, top, left)
        {
        }

        public override void Fire()
        {
            if (GameObjectType == GameObjectType.Player)
            {
                base.Game.AddGameObject(Images.bulletRight, GameObjectType.Bullet, ElementImage.Top-8, ElementImage.Left + 30 , new BulletMovement(30, BulletDirection.Right));
            }
        }
    }
}
