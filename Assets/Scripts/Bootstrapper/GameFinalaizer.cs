using Player;
using Screen;
using UnityEngine;

namespace Bootstrapper
{
    public class GameFinalaizer
    {
        private Flappy _flappy;
        private StartingWindow _startingWindow;
        private FinalWindow _finalWindow;
        private IRestart _restart;

        private FinalWindow _finalWindowInstance;

        public GameFinalaizer(StartingWindow startingWindow,FinalWindow finalWindow, IRestart restart )
        {
            _startingWindow = startingWindow;
            _finalWindow = finalWindow;
            _restart = restart;
        }

        public void FinalGame()
        {
            Time.timeScale = 0;
            _startingWindow.gameObject.SetActive(false);
            ShoowScreen();
        }

        private void ShoowScreen()
        {
            _finalWindowInstance = Object.Instantiate(_finalWindow);
            _finalWindowInstance.RestartButton.onClick.AddListener(_restart.Restart);
        }

        public void DisableScreen()
        {
            Object.Destroy(_finalWindowInstance.gameObject);
        }
    }
}