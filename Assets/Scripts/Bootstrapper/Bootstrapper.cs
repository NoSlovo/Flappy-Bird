using System.Collections.Generic;
using Pipes;
using Services;
using Spawner;
using UnityEngine;

namespace Bootstrapper
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Pipe pipe;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _playerInstancePoint;
        
        private IPoolService<Pipe> _poolService;
        private SpawnerPipe _spawnerPipe;

        private int _countInstance = 10;

        private void Start()
        {
            InitServices();
            InstancePlayer();
        }

        private void InitServices()
        {
            _poolService = new PipesPoolService(pipe, 10);
            _spawnerPipe = new SpawnerPipe(_poolService, _spawnPoint);
            _spawnerPipe.Init();
        }

        private void InstancePlayer()
        {
            var player = GameObject.Instantiate(_player);
            player.transform.position = _playerInstancePoint.position;
        }
    }
}