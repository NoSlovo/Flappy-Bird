using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class AnimationController
    {
        [SerializeField] private Animator _animator;

        private const string _animationName = "Fly";

        public void PlayFly() => _animator.Play(_animationName);
    }
}