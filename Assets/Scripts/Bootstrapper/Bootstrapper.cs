using AppsFlyerSDK;
using Pipes;
using Player;
using Screen;
using Services;
using Spawner;
using UnityEngine;

namespace Bootstrapper
{
    public class Bootstrapper : MonoBehaviour, IRestarter
    {
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private FinalScreen _finalScreen;
        [SerializeField] private Pipe pipe;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Flappy _player;
        [SerializeField] private Transform _playerInstancePoint;

        private IPoolService<Pipe> _poolService;
        private SpawnerPipe _spawnerPipe;
        private StartScreen _instanceScreen;
        private GameFinalaizer _finalaizer;
        private Flappy _playerInstance;
        private SaveSystem _saveSystem;

        private const string KeyDev = "Test";
        private const string AppId = "TestID";

        private int _countInstance = 10;

        public void Start()
        {
            DontDestroyOnLoad(this);
            _instanceScreen = Instantiate(_startScreen);
            _instanceScreen.HideObjects(true);
            _instanceScreen.ShowBestScore();
            _instanceScreen.ButtonStartGame.onClick.AddListener(StartGame);
            _instanceScreen.Counter.Init();
            Time.timeScale = 1;
        }

        public void StartGame()
        {
            AppsFlyer.initSDK("devkey", "appID");
            AppsFlyer.startSDK();
            InitServices();
            InstancePlayer();
            _instanceScreen.ButtonStartGame.onClick.RemoveListener(StartGame);
            _instanceScreen.HideObjects(false);
        }

        private void InitServices()
        {
            _poolService = new PipesPoolService(pipe, _countInstance);
            _spawnerPipe = new SpawnerPipe(_poolService, _instanceScreen.Counter, _spawnPoint);
            _finalaizer = new GameFinalaizer(_instanceScreen, _finalScreen, this);
            _spawnerPipe.Init();
        }

        private void InstancePlayer()
        {
            _playerInstance = Instantiate(_player);
            _playerInstance.transform.position = _playerInstancePoint.position;
            _playerInstance.OnDead += _finalaizer.FinalGame;
        }

        public void Restart()
        {
            _instanceScreen.ButtonStartGame.onClick.RemoveListener(StartGame);
            _finalaizer.DisableScreen();
            Destroy(_instanceScreen.gameObject);
            _playerInstance.Destroy();
            _poolService.DisableAllElements();
            _spawnerPipe.Stop();
            Start();
        }
    }

    public struct SaveSystem
    {
        private const string _keySave = "SaveBestScore";

        public static void SaveValue(int score)
        {
            var lasScore = PlayerPrefs.GetInt(_keySave);

            if (score > lasScore)
                PlayerPrefs.SetInt(_keySave, score);
        }

        public static int GetBestScore()
        {
            var loadValue = PlayerPrefs.GetInt(_keySave);
            return loadValue;
        }
    }
}