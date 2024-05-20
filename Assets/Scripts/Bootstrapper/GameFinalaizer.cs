using Player;
using UnityEngine;

namespace Bootstrapper
{
    public class GameFinalaizer
    {
        private Flappy _flappy;
        private MonoBehaviour _behaviour;

        public GameFinalaizer()
        {
            
        }

        public void FinalGame()
        {
            Time.timeScale = 0;
        }
    }
}