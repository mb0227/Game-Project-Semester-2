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
        private Form GameForm;
        private List<CollisionDetection> Collisions;
        private int Score;
        private static bool HasBeenCalled = false;
        private (int VerticalEnemies, int HorizontalEnemies, int ZigZagEnemies, int Players, int Walls) Count;

        private Game(Form gameForm)
        {
            GameForm = gameForm;
            GameObjects = new List<GameObject>();
            Collisions = new List<CollisionDetection>();
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
                entity.Update();
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
    }
}
