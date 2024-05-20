using Pipes;
using Player;
using Screen;
using Services;
using Spawner;
using UnityEngine;

namespace Bootstrapper
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private GameObject _finalScreen;
        [SerializeField] private Pipe pipe;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Flappy _player;
        [SerializeField] private Transform _playerInstancePoint;

        private IPoolService<Pipe> _poolService;
        private SpawnerPipe _spawnerPipe;
        private StartScreen _instanceScreen;
        private GameFinalaizer _finalaizer;
        private Flappy _playerInstance;

        private int _countInstance = 10;

        public void Start()
        {
            _instanceScreen = Instantiate(_startScreen);
            _instanceScreen.ButtonStartGame.onClick.AddListener(StartGame);
            _instanceScreen.Counter.Init();
        }

        public void StartGame()
        {
            InitServices();
            InstancePlayer();
            _instanceScreen.ButtonStartGame.onClick.RemoveListener(StartGame);
            _instanceScreen.ButtonStartGame.gameObject.SetActive(false);
        }

        private void InitServices()
        {
            _poolService = new PipesPoolService(pipe, _countInstance);
            _spawnerPipe = new SpawnerPipe(_poolService,_instanceScreen.Counter, _spawnPoint);
            _finalaizer = new GameFinalaizer();
            _spawnerPipe.Init();
        }

        private void InstancePlayer()
        {
            _playerInstance = Instantiate(_player);
            _playerInstance.transform.position = _playerInstancePoint.position;
            _playerInstance.OnDead += _finalaizer.FinalGame;
        }

        public void Reset()
        {
            _instanceScreen.ButtonStartGame.onClick.RemoveListener(StartGame);
            Destroy(_instanceScreen);
            Destroy(_playerInstance);
            InstancePlayer();
            _poolService.DisableAllElements();
            _spawnerPipe.Stop();
            Start();
        }
    }
}