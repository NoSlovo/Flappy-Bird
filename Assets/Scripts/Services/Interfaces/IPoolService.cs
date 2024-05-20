using UnityEngine;

namespace Spawner
{
    public interface IPoolService<T>  where T : MonoBehaviour
    {
        public T GetPoolObject(Transform instancePoint);
        public void RemoveObject(T poolObject);

        public void DisableAllElements();
    }
}