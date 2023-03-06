using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerModelSwitchController : MonoBehaviour
    {
        //[SerializeField] private Renderer[] models;
        [SerializeField] private GameObject[] models;
        private int currentModelIndex = 0;
       

        private void Awake()
        {
            foreach (var model in models)
            {
                model.SetActive(false);
            }
            
        }
        
        public void UpdateModel(float playerPoint)
        {
            if (playerPoint <= 5)
            {
                ChangeModel(0);
            }
            else if (playerPoint > 5 && playerPoint <= 10)
            {
                ChangeModel(1);
            }
            else if (playerPoint > 10 && playerPoint <= 15)
            {
                ChangeModel(2);
            }
        }


        private void ChangeModel(int modelIndex)
        {
           
            models[currentModelIndex].SetActive(false);
            models[modelIndex].SetActive(true);
            currentModelIndex = modelIndex;
        }
    }
}