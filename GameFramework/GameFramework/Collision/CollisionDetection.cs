using GameFramework.GL;
using GameFramework.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Collision
{
    public class CollisionDetection
    {
        private readonly GameObjectType ObjectOne;
        private readonly GameObjectType ObjectTwo;
        private readonly ActionPerformed ActionToPerform;

        public CollisionDetection(GameObjectType objectOne, GameObjectType objectTwo, ActionPerformed actionToPerform)
        {
            ObjectOne = objectOne;
            ObjectTwo = objectTwo;
            ActionToPerform = actionToPerform;
        }

        public void DetectCollision(Game game, List<GameObject> gameObjects)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.GetGameObjectType() == ObjectOne)
                {
                    foreach (GameObject otherGameObject in gameObjects)
                    {
                        if (gameObject != otherGameObject && otherGameObject.GetGameObjectType() == ObjectTwo)
                        {
                            if (gameObject.GetImage().Bounds.IntersectsWith(otherGameObject.GetImage().Bounds))
                            {
                                PerformAction(game, gameObject);
                            }
                        }
                    }
                }
            }
        }

        private void PerformAction(Game game, GameObject gameObject)
        {
            if (ActionToPerform == ActionPerformed.IncreasePoints)
            {
                game.AddScore();
            }
            else if (ActionToPerform == ActionPerformed.DecreasePoints)
            {
                game.DecreaseScore();
            }
            else if (ActionToPerform == ActionPerformed.IncreaseHealth)
            {
                gameObject.IncreaseHealth();
            }
            else if (ActionToPerform == ActionPerformed.DecreaseHealth)
            {
                gameObject.DecreaseHealth();
            }
            else if (ActionToPerform == ActionPerformed.ChangeState)
            {
                gameObject.ChangeState();
            }
        }
    }
}
