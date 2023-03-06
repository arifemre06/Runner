using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAnimController: MonoBehaviour
    {
        [SerializeField] private Animator[] playerModelAnimators;
        //[SerializeField] private Animator soldierAnimator;
        private Animator[] playerAnimators;
        private int currentAnimatorIndex = 2;
        
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsEnemyInFightRange = Animator.StringToHash("IsEnemyInFightRange");
        private static readonly int IsJump = Animator.StringToHash("IsJumping");

        private void Start()
        {
            //soldierAnimator = soldierAnimator.GetComponent<Animator>();
            playerAnimators = new Animator[playerModelAnimators.Length];
            for (var i = 0; i < playerModelAnimators.Length; i++)
            {
               playerAnimators[i] = playerModelAnimators[i].GetComponent<Animator>();
            }
        }

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
            Debug.Log("run");
            playerAnimators[currentAnimatorIndex].SetBool("IsEnemyInFightRange",false);
            //soldierAnimator.SetBool("IsEnemyInFightRange",false);
            //soldierAnimator.SetBool("IsMoving",true);
            //soldierAnimator.SetBool("IsJumping", false);
            //Debug.Log("IsEnemyInFightRange" + //soldierAnimator.GetBool("IsEnemyInFightRange") + "IsMoving" +
                      //soldierAnimator.GetBool("IsMoving") + "IsJumping" +
                      //soldierAnimator.GetBool("IsJumping"));
            playerAnimators[currentAnimatorIndex].SetBool("IsMoving",true);

            playerAnimators[currentAnimatorIndex].SetBool(IsJump, false);

        }

        public void PlayIdleAnimation()
        {   
            Debug.Log("idle");
            playerAnimators[currentAnimatorIndex].SetBool("IsEnemyInDetectionRange",true);
            //soldierAnimator.SetBool("IsEnemyInDetectionRange",true);
            //soldierAnimator.SetBool(IsMoving,false);
            
            playerAnimators[currentAnimatorIndex].SetBool(IsMoving,false);
            
        }

        public void PlayFightAnimation()
        {   
            Debug.Log("fight");
            //soldierAnimator.SetBool(IsEnemyInFightRange,true);
            //Debug.Log("IsMoving " + soldierAnimator.GetBool(IsMoving) + "IsEnemyInFightRange" +
            //soldierAnimator.GetBool(IsEnemyInFightRange)
                      //);
            playerAnimators[currentAnimatorIndex].SetBool(IsEnemyInFightRange,true);
        }

        public void PlayJumpAnimation()
        {
            playerAnimators[currentAnimatorIndex].SetBool(IsJump,true);
            //soldierAnimator.SetBool("IsJumping",true);
            //Debug.Log("IsJumping" + soldierAnimator.GetBool("IsJumping")
            
            //);
        }

        public void PlayDieAnimation()
        { 
            
        }
        private void ChangeAnimator(int animatorIndex)
        {
            
            PrintManager.Print("animator index "+animatorIndex);
            currentAnimatorIndex = animatorIndex;
        }
    }
}