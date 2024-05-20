using System;
using Cysharp.Threading.Tasks;
using Pipes;
using Spawner;
using UnityEngine;

namespace Services
{
    public class SpawnerPipe
    {
        private IPoolService<Pipe> _poolService;
        private Transform _spawnPoint;

        public SpawnerPipe(IPoolService<Pipe> poolService, Transform spawnPoint)
        {
            _poolService = poolService;
            _spawnPoint = spawnPoint;
        }

        public async void Init()
        {
            var deley = TimeSpan.FromSeconds(5f);
            while (true)
            {
                ShowPipe();
               await UniTask.Delay(deley);
            }
        }

        private void ShowPipe()
        {
            var showObject = _poolService.GetPoolObject(_spawnPoint);
            showObject.SwitchActiveState(true);
        }
    }
}