using GameFramework.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using EZInput;
using GameFramework.Collision;

namespace GameFramework.BL
{
    public class Game
    {
        private List<GameObject> GameObjects;
        private List<CollisionDetection> Collisions;
        private Form GameForm;
        private int Score;
        private static bool HasBeenCalled = false;
        private (int VerticalEnemies, int HorizontalEnemies, int ZigZagEnemies, int Players, int Walls) Count;

        private List<GameObject> Bullets;

        private Game(Form gameForm)
        {
            GameForm = gameForm;
            GameObjects = new List<GameObject>();
            Collisions = new List<CollisionDetection>();
            Bullets = new List<GameObject>();
            Count = (0, 0, 0,0,0);
        }

        public static Game MakeGame(Form gameForm) //Singleton Pattern
        {
            if (!HasBeenCalled)
            {               
                HasBeenCalled = true;
                return new Game(gameForm);
            }
            return null;
        }

        public void AddGameObject(Image image, GameObjectType gameObjectType, int top, int left, IMovement movement)
        {
            IncreaseCount(gameObjectType);
            GameObject gameObject = new GameObject(image, gameObjectType, top, left, movement);
            GameObjects.Add(gameObject);
            GameForm.Controls.Add(gameObject.GetImage());
        }

        public void Update()
        {
            foreach (GameObject entity in GameObjects)
            {
                if(entity.GetGameObjectType() != GameObjectType.Bullet)
                    entity.Update();
                GenerateBullets();
                MoveBullets();
            }
            foreach (CollisionDetection collision in Collisions)
            {
                collision.DetectCollision(this, GameObjects);
            }
        }

        public void AddCollision(CollisionDetection collision)
        {
            Collisions.Add(collision);
        }

        public void AddScore()
        {
            Score++;
        }   

        public void DecreaseScore()
        {
            if(Score>0)
                Score--;
        }

        public int GetScore()
        {
            return Score;
        }

        public void SetScoreZero()
        {
            Score = 0;
        }

        public void SetScore(int score)
        {
            Score= score;
        }

        public int GetPlayerHealth()
        {
            foreach (GameObject gameObject in GameObjects)
            {
                if (gameObject.GetGameObjectType() == GameObjectType.Player)
                {
                    return gameObject.GetHealth();
                }
            }
            return 0;
        }

        public List<int> GetEnemiesHealth()
        {             
            List<int> healthList = new List<int>();
            foreach (GameObject gameObject in GameObjects)
            {
                if (gameObject.GetGameObjectType() != GameObjectType.Player)
                {
                    healthList.Add(gameObject.GetHealth());
                }
            }
            return healthList;
        }

        private void IncreaseCount(GameObjectType objectType)
        {
            if (objectType == GameObjectType.VerticalEnemy)
                Count.VerticalEnemies++;
            else if (objectType == GameObjectType.HorizontalEnemy)
                Count.HorizontalEnemies++;
            else if (objectType == GameObjectType.ZigZagEnemy)
                Count.ZigZagEnemies++;
            else if (objectType == GameObjectType.Player)
                Count.Players++;
        }

        public void GenerateBullets()
        {
            if (EZInput.Keyboard.IsKeyPressed(EZInput.Key.Space))
            {
                GameObject player = GetPlayer();
                GameObject bullet = GetBullet();
                if (player != null && bullet != null)
                {
                    PictureBox Bullet = new PictureBox();
                    //int playerLocationX = player.GetImageLocation().X;
                    //int playerLocationY = player.GetImageLocation().Y;
                    //Bullet.Location = new System.Drawing.Point(playerLocationX, playerLocationY);
                    Bullet.Left = player.GetImage().Left + 30;
                    Bullet.Top = player.GetImage().Height / 2;
                    Bullet.Size = new Size(25, 20);
                    Bullet.BackColor = Color.Transparent;
                    Bullet.BringToFront();
                    Bullet.SizeMode = PictureBoxSizeMode.StretchImage;
                    Bullet.Visible = true;
                    bullet.SetImage(Bullet.Image);

                    Console.WriteLine(bullet.GetImage().Location + " "+ player.GetImage().Location);
                }
            }
        }

        private void MoveBullets()
        {
            foreach (var item in GameObjects)
            {
                if(item.GetGameObjectType() == GameObjectType.Bullet)
                {
                    item.GetImage().Left += 3;
                }
            }
        }

        public GameObject GetPlayer()
        {
            foreach (var item in GameObjects)
            {
                if (item.GetGameObjectType() == GameObjectType.Player)
                    return item;
            }
            return null;
        }

        public GameObject GetBullet()
        {
            foreach (var item in GameObjects)
            {
                if (item.GetGameObjectType() == GameObjectType.Bullet)
                    return item;
            }
            return null;
        }

        public int GetVerticalEnemiesCount()
        {
            return Count.VerticalEnemies;
        }

        public int GetHorizontalEnemiesCount()
        {
            return Count.HorizontalEnemies;
        }

        public int GetZigZagEnemiesCount()
        {
            return Count.ZigZagEnemies;
        }

        public List<GameObject> GetGameObjects()
        {
            return GameObjects;
        }

        public  int GetPlayersCount()
        {
            return Count.Players;
        }

        public Form GetGame()
        {
            return GameForm;
        }
    }
}
