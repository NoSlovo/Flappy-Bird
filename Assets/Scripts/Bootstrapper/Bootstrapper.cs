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

        private const string _keyDev = "Test";
        private const string _appId = "TestID";

        private int _countInstance = 10;

        public void Start()
        {
            _instanceScreen = Instantiate(_startScreen);
            _instanceScreen.ShowBestScore();
            _instanceScreen.HideObjects(true);
            DontDestroyOnLoad(this);
        }

        public void StartGame()
        {
            InitSDK();
            InitServices();
            InstancePlayer();
            RegisterActions();
            HideScreen();
        }

        private static void InitSDK()
        {
            AppsFlyer.initSDK(_keyDev, _appId);
            AppsFlyer.startSDK();
        }

        private void InitServices()
        {
            _poolService = new PipesPoolService(pipe, _countInstance);
            _spawnerPipe = new SpawnerPipe(_poolService, _instanceScreen.Counter, _spawnPoint);
            _finalaizer = new GameFinalaizer(_instanceScreen, _finalScreen, this);
            _instanceScreen.Counter.Init();
            _spawnerPipe.Init();
        }

        private void InstancePlayer()
        {
            _playerInstance = Instantiate(_player);
            _playerInstance.transform.position = _playerInstancePoint.position;
            
        }

        private void HideScreen()
        {
            _instanceScreen.ButtonStartGame.onClick.RemoveListener(StartGame);
            _instanceScreen.HideObjects(false);
        }

        private void RegisterActions()
        {
            _playerInstance.OnDead += _finalaizer.FinalGame;
            _instanceScreen.ButtonStartGame.onClick.AddListener(StartGame);
        }

        public void Restart()
        {
            _instanceScreen.ButtonStartGame.onClick.RemoveListener(StartGame);
            Destroy(_instanceScreen.gameObject);
            _finalaizer.DisableScreen();
            _playerInstance.Destroy();
            _poolService.DisableAllElements();
            _spawnerPipe.Stop();
            Start(); 
            Time.timeScale = 1;
        }
    }
}