using System;
using Cysharp.Threading.Tasks;
using GameElements;
using Spawner;
using UI;
using UnityEngine;

namespace Services
{
    public class SpawnerPipe
    {
        private IPoolService<Pipes> _poolService;
        private Transform _spawnPoint;
        private Counter _counter;
        private bool _isWork;

        public SpawnerPipe(IPoolService<Pipes> poolService,Counter counter, Transform spawnPoint)
        {
            _poolService = poolService;
            _spawnPoint = spawnPoint;
            _counter = counter;
        }

        public async void Init()
        {
            _isWork = true;
            var deley = TimeSpan.FromSeconds(3f);
            
            while ( _isWork)
            {
                ShowPipes();
               await UniTask.Delay(deley);
            }
        }

        public void Stop() => _isWork = false;

        private void ShowPipes()
        {
            var showObject = _poolService.GetPoolObject(_spawnPoint);
            showObject.SwitchActiveState(true);
            showObject.CheckZone.SetCounter(_counter);
        }
    }
}