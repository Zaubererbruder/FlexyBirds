using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Pipe : MonoBehaviour
    {
        [SerializeField] private float MovingSpeed = 3;

        private Rigidbody2D _rigidbody;
        private Transform _pipeTransform;
        private float _deathCoordX = -9;

        public event System.Action OnDestroyed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _pipeTransform = transform;
            
            StartPipe();
        }

        private void Update()
        {
            if (_pipeTransform.position.x < _deathCoordX)
            {
                OnDestroyed?.Invoke();
                Destroy(gameObject);
            }
        }

        public void StopPipe()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        public void StartPipe()
        {
            _rigidbody.velocity = Vector2.left * MovingSpeed;
        }

    }
}
