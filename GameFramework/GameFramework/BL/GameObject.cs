using GameFramework.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Remoting;

namespace GameFramework.BL
{
    public class GameObject
    {
        private PictureBox ElementImage;
        private IMovement Movement;
        private GameObjectType GameObjectType;
        private int Health;

        public GameObject(Image elementImage, GameObjectType gameObjectType, int top, int left, IMovement movement)
        {            
            ElementImage = new PictureBox();
            ElementImage.Image = elementImage;
            ElementImage.Top = top;
            ElementImage.Left = left;
            ElementImage.Visible = true;
            ElementImage.SizeMode = PictureBoxSizeMode.StretchImage;
            ElementImage.Width = 45;
            ElementImage.Height = 40;
            ElementImage.BackColor = Color.Transparent;
            ElementImage.BringToFront();
            Movement = movement;
            GameObjectType = gameObjectType;
            Health = 100;
        }

        public void Update()
        {
            ElementImage.Location = Movement.Move(ElementImage.Location);
            ChangePlayerImage();
        }

        public void ChangePlayerImage()
        {
            if(GameObjectType == GameObjectType.Player && EZInput.Keyboard.IsKeyPressed(EZInput.Key.LeftArrow))
            {
                ElementImage.Image = im.Player;
            }
            else if(GameObjectType == GameObjectType.Player && EZInput.Keyboard.IsKeyPressed(EZInput.Key.RightArrow))
            {
                ElementImage.Image = im.Enemy2;
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
    }
}
