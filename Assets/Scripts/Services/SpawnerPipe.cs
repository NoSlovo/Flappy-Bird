using System;
using Cysharp.Threading.Tasks;
using Pipes;
using Spawner;
using UI;
using UnityEngine;

namespace Services
{
    public class SpawnerPipe
    {
        private IPoolService<Pipe> _poolService;
        private Transform _spawnPoint;
        private Counter _counter;
        private bool _isWork = false;

        public SpawnerPipe(IPoolService<Pipe> poolService,Counter counter, Transform spawnPoint)
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
                ShowPipe();
               await UniTask.Delay(deley);
            }
        }

        public void Stop()
        {
            _isWork = false;
        }
        
        private void ShowPipe()
        {
            var showObject = _poolService.GetPoolObject(_spawnPoint);
            showObject.SwitchActiveState(true);
            showObject.CheckZone.SetCounter(_counter);
        }
    }
}