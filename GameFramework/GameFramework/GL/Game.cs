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

namespace GameFramework.GL
{
    public class Game
    {
        private Form GameForm;
        private List<GameObject> GameObjects;
        private List<CollisionDetection> Collisions;
        private int Score;
        private (int VerticalEnemies, int HorizontalEnemies, int ZigZagEnemies, int Players, int VericalWalls, int HorizontalWalls) Count;
        private static Game GameInstance = null;

        public static Game MakeGame(Form gameForm) //Singleton Pattern
        {
            if (GameInstance == null)
            {
                GameInstance = new Game(gameForm);
            }
            return GameInstance;
        }

        private Game(Form gameForm)
        {
            GameForm = gameForm;
            GameObjects = new List<GameObject>();
            Collisions = new List<CollisionDetection>();
            Count = (0, 0, 0, 0, 0, 0);
        }
        public void AddGameObject(System.Drawing.Image image, GameObjectType gameObjectType, int top, int left)
        {
            IncreaseCount(gameObjectType);
            GameObject gameObject = new GameObject(image, gameObjectType, top, left);
            GameObjects.Add(gameObject);
            GameForm.Controls.Add(gameObject.GetImage());
        }

        public void AddGameObject(System.Drawing.Image image, GameObjectType gameObjectType, int top, int left, IMovement movement)
        {
            IncreaseCount(gameObjectType);
            GameObject gameObject = new GameObject(image, gameObjectType, top, left, movement);
            GameObjects.Add(gameObject);
            GameForm.Controls.Add(gameObject.GetImage());
        }


        public void AddGameObject(System.Drawing.Image image, GameObjectType gameObjectType, int top, int left, int speed,  System.Drawing.Point boundary)
        {
            IncreaseCount(gameObjectType);
            Player gameObject = new Player(image, gameObjectType, top, left);
            gameObject.SetMovement(new KeyboardMovement(speed, boundary, gameObject));
            GameObjects.Add(gameObject);
            GameForm.Controls.Add(gameObject.GetImage());
        }

        public void Update()
        {
            List<GameObject> bulletsToRemove = new List<GameObject>();
            foreach (var entity in GameObjects.ToList())
            {
                if (entity.GetGameObjectType() != GameObjectType.HorizontalWall && entity.GetGameObjectType() != GameObjectType.VerticalWall)
                {
                    entity.Update();
                }
                if (entity.GetGameObjectType() == GameObjectType.Bullet && (entity.GetImage().Location.X > GameForm.Location.X+520 || entity.GetImage().Location.X < 163))
                    bulletsToRemove.Add(entity);
            }

            foreach (var bullet in bulletsToRemove)
            {
                GameForm.Controls.Remove(bullet.GetImage());
                GameObjects.Remove(bullet);
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
            else if (objectType == GameObjectType.VerticalWall)
                Count.VericalWalls++;
            else if (objectType == GameObjectType.HorizontalWall)
                Count.HorizontalWalls++;
        }

        public void Fire(GameObject player)
        {
           player.Fire();
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

        public int GetVerticalWallsCount()
        {
            return Count.VericalWalls;
        }

        public int GetHorizontalWallsCount()
        {
            return Count.HorizontalWalls;
        }

        public Form GetGameForm()
        {
            return GameForm;
        }

        public void UpdateList(GameObject gameObject, int index)
        {
            GameObjects[index] = gameObject;
        }

        public void GameOver()
        {
            GameObjects.Clear();
            Collisions.Clear();
            Count = (0, 0, 0, 0, 0, 0);
            Score = 0;
        }
    }
}