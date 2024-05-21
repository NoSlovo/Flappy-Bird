using Player;
using Screen;
using UnityEngine;

namespace Bootstrapper
{
    public class GameFinalaizer
    {
        private Flappy _flappy;
        private StartScreen _startScreen;
        private FinalScreen _finalScreen;
        private IRestarter _restarter;

        private FinalScreen _finalScreenInstance;

        public GameFinalaizer(StartScreen startScreen,FinalScreen finalScreen, IRestarter restarter )
        {
            _startScreen = startScreen;
            _finalScreen = finalScreen;
            _restarter = restarter;
        }

        public void FinalGame()
        {
            Time.timeScale = 0;
            _startScreen.gameObject.SetActive(false);
            ShoowScreen();
        }

        private void ShoowScreen()
        {
            _finalScreenInstance = Object.Instantiate(_finalScreen);
            _finalScreenInstance.RestartButton.onClick.AddListener(_restarter.Restart);
        }

        public void DisableScreen()
        {
            Object.Destroy(_finalScreenInstance.gameObject);
        }
    }
}