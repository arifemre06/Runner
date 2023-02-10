using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAnimController: MonoBehaviour
    {
        [SerializeField] private Animator[] playerModelAnimators;
        private int currentAnimatorIndex = 0;
        

        public void UpdateAnimator(float playerPoint)
        {
            if (playerPoint <= 5)
            {
                ChangeAnimator(0);
            }
            else if (playerPoint > 5 && playerPoint <= 10)
            {
                ChangeAnimator(1);
            }
            else if (playerPoint > 10 && playerPoint < 15)
            {
                ChangeAnimator(2);
            }
        }
        
        public void PlayRunningAnimation()
        {
            
        }

        public void PlayIdleAnimation()
        {
            
        }

        public void PlayFightAnimation()
        {
            
        }

        private void ChangeAnimator(int animatorIndex)
        {
            
        }
    }
}