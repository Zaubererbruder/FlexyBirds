using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _thrust = 4.4f;

        private Rigidbody2D _rigidbody;
        private bool _isAlive;

        public event Action OnPipeTrigger;
        public event Action OnCollisionEnter;
        public event Action OnDeath;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.isPressed)
            {
                Application.Quit();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Player trigger an object.");
            if (collision.tag == "ScorePointer")
            {
                OnPipeTrigger?.Invoke();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(_isAlive)
            {
                _isAlive = false;
                OnDeath?.Invoke();
            }
            OnCollisionEnter?.Invoke();
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector2.up * _thrust;
            Debug.Log("Jump!!!");
        }

        public void JumpInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("Jump initiated");
                Jump();
            }
        }

        public void Launch()
        {
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.identity;
            _rigidbody.angularVelocity = 0;
            _rigidbody.velocity = new Vector2(0, 0);
            _rigidbody.simulated = true;
            _isAlive = true;
            Jump();
            Debug.Log("Player launched");
        }

        public void RelocateOnStart()
        {
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.identity;
            _rigidbody.angularVelocity = 0;
            _rigidbody.velocity = new Vector2(0, 0);
            _rigidbody.simulated = false;
        }
    }
}
