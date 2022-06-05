using MetalGamesSDK;
using UnityEngine;

namespace C_.IObstacles
{
    public class SpeedModifier : MonoBehaviour
    {
        public enum MyEnum
        {
            increasespeed, decreasespead
        }
        private static float minSpeed=15;
        
        public float value;
        public MyEnum modifierstate;
        public bool iscollided;
        private bool gameOver;

                
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController2 playerController))
            {

                if (playerController._forwardSpeed <= minSpeed)
                {
                    if (!gameOver)
                    {
                        gameOver = true;
                        GameManager.Instance.LevelFail();
                        
                    }
                    
                }

                if (modifierstate== MyEnum.decreasespead)
                {
                    playerController._forwardSpeed -= value;
                    playerController._feedBacker.BadFeedback();
                    gameObject.SetActive(false);
                }
               
                else
                {
                    playerController._forwardSpeed += value;
                    playerController._feedBacker.GoodFeedback();
                    AudioManager.instance.Play("powerup");
                }
                
            }
            else if (other.TryGetComponent(out EnemyMovement mv))
            {
                if (modifierstate==MyEnum.decreasespead)
                {
                    mv.mForwardSpeed -= value;
                    mv.FeedBacker.BadFeedback();
                    gameObject.SetActive(false);
                }
                else
                {
                    mv.mForwardSpeed += value;
                    mv.FeedBacker.GoodFeedback();
                  
                }
              
            }
            
        }
    }
}