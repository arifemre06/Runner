using UnityEngine;

namespace DefaultNamespace
{   
    
    public class Level : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        
        public PlayerManager GetPlayerManager()
        {   
            
            return playerManager;
        }
    }
}