using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerModelSwitchController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] models;
        private int modelCurrentIndex = 0;

        private void Awake()
        {
            foreach (var model in models)
            {
                model.gameObject.SetActive(false);
            }
            
        }

        public void UpdateModel(float playerPoint)
        {
            if (playerPoint <= 5 && playerPoint != 0)
            {
                ChangeModel(0);
            }
            else if (playerPoint > 5 && playerPoint <= 10)
            {
                ChangeModel(1);
            }
            else if (playerPoint > 10 && playerPoint < 15)
            {
                ChangeModel(2);
            }
        }
        
        

        private void ChangeModel(int modelIndex)
        {
            models[modelCurrentIndex].gameObject.SetActive(false);
            models[modelIndex].gameObject.SetActive(true);
        }
    }
}