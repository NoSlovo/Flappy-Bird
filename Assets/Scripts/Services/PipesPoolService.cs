using System.Collections.Generic;
using Spawner;
using UnityEngine;

namespace Services
{
    public class PipesPoolService : IPoolService<GameElements.Pipes>
    {
        private readonly float _maxHeight = 70f;
        private readonly float _minHeight = 30f;

        private int _initObjectsCount = 10;

        private GameElements.Pipes _pipen;

        private Stack<GameElements.Pipes> _pipeses;

        public PipesPoolService(GameElements.Pipes pipens, int countObjects)
        {
            _pipen = pipens;
            _initObjectsCount = countObjects;
            _pipeses = new Stack<GameElements.Pipes>(_initObjectsCount);
            Init();
        }

        private void Init()
        {
            for (int i = 0; i <= _initObjectsCount; i++)
            {
                var objectInstance = CreateObject();
                objectInstance.Init();
                objectInstance.SwitchActiveState(false);
                _pipeses.Push(objectInstance);
            }
        }

        public GameElements.Pipes GetPoolObject(Transform instancePoint)
        {
            var poolObject = GetDisablePipes();
            float offsetY = Random.Range(_maxHeight, _minHeight);
            var showPosition = instancePoint.position +  new Vector3(0, offsetY,0);
            poolObject.transform.position = showPosition;
            poolObject.Init();
            poolObject.SwitchActiveState(true);
            return poolObject;
        }

        private GameElements.Pipes GetDisablePipes()
        {
            foreach (var pipese in _pipeses)
            {
                if (!pipese.IsActive)
                {
                    return pipese;
                }
            }

            return CreateObject();
        }

        private GameElements.Pipes CreateObject() => Object.Instantiate(_pipen);

        public void RemoveObject(GameElements.Pipes poolObject) => poolObject.SwitchActiveState(false);
        public void DisableAllElements()
        {
            foreach (var pipese in _pipeses)
            {
                pipese.SwitchActiveState(false);
            }
        }
    }
}