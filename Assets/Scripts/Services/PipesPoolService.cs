using System.Collections.Generic;
using Pipes;
using UnityEngine;

namespace Spawner
{
    public class PipesPoolService : IPoolService<Pipe>
    {
        private readonly float _maxHeight = 70f;
        private readonly float _minHeight = 30f;

        private int _initObjectsCount = 10;

        private Pipe _pipen;

        private Stack<Pipe> _pipeses;

        public PipesPoolService(Pipe pipens, int countObjects)
        {
            _pipen = pipens;
            _initObjectsCount = countObjects;
            _pipeses = new Stack<Pipe>(_initObjectsCount);
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

        public Pipe GetPoolObject(Transform instancePoint)
        {
            var poolObject = GetDisablePipes();
            float offsetY = Random.Range(_maxHeight, _minHeight);
            var showPosition = instancePoint.position +  new Vector3(0, offsetY,0);
            poolObject.transform.position = showPosition;
            poolObject.Init();
            poolObject.SwitchActiveState(true);
            return poolObject;
        }

        private Pipe GetDisablePipes()
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

        private Pipe CreateObject() => Object.Instantiate(_pipen);

        public void RemoveObject(Pipe poolObject) => poolObject.SwitchActiveState(false);
    }
}