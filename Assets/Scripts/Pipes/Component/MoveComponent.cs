using UnityEngine;

namespace Pipes.Component
{
    public class MoveComponent
    {
        private float _speed;
        private Transform _transform;

        public MoveComponent(float speed, Transform transform)
        {
            _speed = speed;
            _transform = transform;
        }

        public void MoveDirection(Vector2 direction)
        {
            var moveDirection = new Vector3(direction.x * _speed * Time.deltaTime, 0, 0);
            _transform.position += moveDirection;
        }
    }
}