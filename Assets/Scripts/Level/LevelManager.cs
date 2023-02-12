using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Level[] levelPrefabs;
        public Action LevelCreated;
        public Action LevelDestroyed;
        private Level currentLevel;
        private int currentLevelIndex;

        public void PrepareCurrentLevel()
        {
            if (currentLevel != null)
            {
                DestroyLevel();
            }
            CreateLevel(currentLevelIndex);
        }

        private void CreateLevel(int levelIndex)
        {
            currentLevel = Instantiate(levelPrefabs[levelIndex], Vector3.zero ,Quaternion.identity);
            LevelCreated?.Invoke();
        }

        private void DestroyLevel()
        {
            Destroy(currentLevel);
            LevelDestroyed?.Invoke();
        }

        public PlayerManager GetPlayerManager()
        {
            return currentLevel.GetPlayerManager();
        }

        public void SetLevelIndexToNextLevel()
        {   
                Debug.Log($"currentLevelIndex {currentLevelIndex}");
            currentLevelIndex += 1;
            
        }
    }
}