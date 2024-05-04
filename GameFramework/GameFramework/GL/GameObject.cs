using GameFramework.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Remoting;

namespace GameFramework.GL
{
    public class GameObject
    {
        protected PictureBox ElementImage;
        protected IMovement Movement;
        protected GameObjectType GameObjectType;
        protected int Health;
        protected Game Game;

        public GameObject(){}

        public GameObject(Image elementImage, GameObjectType gameObjectType, int top, int left)
        {            
            if(left<163)
                left = 163;
            Game = Game.MakeGame(new Form());  
            ElementImage = new PictureBox();
            ElementImage.Image = elementImage;
            ElementImage.Top = top;
            ElementImage.Left = left;
            ElementImage.Visible = true;
            ElementImage.SizeMode = PictureBoxSizeMode.StretchImage;
            ElementImage.Width = 45;
            ElementImage.Height = 52;
            ElementImage.BackColor = Color.Transparent;
            ElementImage.BackgroundImageLayout = ImageLayout.None;
            ElementImage.BringToFront();
            GameObjectType = gameObjectType;
            Health = 100;
        }

        public GameObject(Image elementImage, GameObjectType gameObjectType, int top, int left, IMovement movement) : this(elementImage, gameObjectType, top, left)
        {
            Movement = movement;
        }


        public void Update()
        {
            ElementImage.Location = Movement.Move(ElementImage.Location);
        }

        public bool ChangePlayerImage()
        {
            if(GameObjectType == GameObjectType.Player && EZInput.Keyboard.IsKeyPressed(EZInput.Key.LeftArrow))
            {
                this.ElementImage.Image = Images.playerLeft;
                ChangeBulletImage(BulletDirection.Left);
                return true;
            }
            else if(GameObjectType == GameObjectType.Player && EZInput.Keyboard.IsKeyPressed(EZInput.Key.RightArrow))
            {
                this.ElementImage.Image = Images.playerRight;
                ChangeBulletImage(BulletDirection.Right);
                return true;
            }
            return false;
        }

        public void ChangeBulletImage(BulletDirection direction)
        {
            var gameObjectsCopy = Game.GetGameObjects().ToList(); 
            foreach (var gameObject in gameObjectsCopy)
            {
                int index = Game.GetGameObjects().IndexOf(gameObject);

                if (gameObject.GetGameObjectType() == GameObjectType.Bullet && direction == BulletDirection.Left)
                {
                    IMovement movement = new BulletMovement(30, BulletDirection.Left);
                    gameObject.SetMovement(movement);
                    gameObject.SetImage(Images.bulletLeft);
                    gameObject.ElementImage.Left -= 30;
                    Game.UpdateList(gameObject, index);
                }
                else if (gameObject.GetGameObjectType() == GameObjectType.Bullet && direction == BulletDirection.Right)
                {
                    IMovement movement = new BulletMovement(30, BulletDirection.Right);
                    gameObject.SetMovement(movement);
                    gameObject.SetImage(Images.bulletRight);
                    gameObject.ElementImage.Left += 30;
                    Game.UpdateList(gameObject, index);
                }
            }
        }

        public IMovement GetMovement()
        {
            return Movement;
        }

        public PictureBox GetImage()
        {
            return ElementImage;
        }

        public GameObjectType GetGameObjectType()
        {
            return this.GameObjectType;
        }

        public void IncreaseHealth()
        {
            if(Health < 100)
                Health++;
        }

        public void DecreaseHealth()
        {
            if (Health > 0)
                Health--;
        }

        public int GetHealth()
        {
            return Health;
        }

        public void SetImage(Image image)
        {
            ElementImage.Image = image;
        }

        public void SetMovement(IMovement movement)
        {
            Movement = movement;
        }

        public Point GetImageLocation()
        {
            return ElementImage.Location;
        }

        public virtual void Fire()
        {
        }

        public void ChangeState()
        {
            Form form = Game.GetGameForm();
            System.Drawing.Point boundary = new System.Drawing.Point(form.Width, form.Height);
            if (this.GameObjectType == GameObjectType.HorizontalEnemy)
            {
                this.Movement = new VerticalMovement(10, boundary, VerticalEnemyDirection.Up);
                this.GameObjectType = GameObjectType.VerticalEnemy;
            }
            else if (this.GameObjectType == GameObjectType.VerticalEnemy)
            {
                this.Movement = new HorizontalMovement(10, boundary, HorizontalEnemyDirection.Left);
                this.GameObjectType = GameObjectType.HorizontalEnemy;
            }
        }
    }
}
