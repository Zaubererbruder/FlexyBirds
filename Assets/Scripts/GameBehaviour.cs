using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private GameObject _playerObj;
        [SerializeField] private List<Animator> _stoppableAnimations;
        
        private PlayerInput _input;
        private PipeSpawner _spawner;
        private Player _player;
        private ScoreCounter _scoreCounter;
        private bool _isGameOver;

        private void Awake()
        {
            var factory = new PipeSpawnerFactory();
            _spawner = factory.Create(pipePrefab);
            StartCoroutine(_spawner.SpawnCoroutine());

            _player = _playerObj.GetComponent<Player>();
            _scoreCounter = GetComponent<ScoreCounter>();
            _input = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            _player.OnPipeTrigger += _scoreCounter.AddScore;
            _player.OnCollisionEnter += GameOver;
        }

        private void OnDisable()
        {
            _player.OnPipeTrigger -= _scoreCounter.AddScore;
            _player.OnCollisionEnter -= GameOver;
        }

        private void GameOver()
        {
            if(!_isGameOver)
                StopGame();
        }

        private void SwitchActionMap(string actionMapName)
        {
            if (_input.currentActionMap.name == actionMapName)
                return;

            _input.SwitchCurrentActionMap(actionMapName);
            Debug.Log($"Switched PlayerInput on a '{actionMapName}'");

        }

        private void SwitchAnimation(bool enabled)
        {
            foreach (var anim in _stoppableAnimations)
            {
                anim.enabled = enabled;
            }
        }

        public void StartGamePressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SwitchActionMap("Player");
                LaunchGame();
            }
        }
        public void RestartGamePressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SwitchActionMap("UI");
                _scoreCounter.RestartScore();
                _spawner.ClearSpawner();
                _player.RelocateOnStart();
                _isGameOver = false;
            }
        }

        public void LaunchGame()
        {
            _spawner.StartSpawner();
            _player.Launch();
            SwitchAnimation(true);
        }

        public void StopGame()
        {
            _spawner.StopSpawner();
            SwitchAnimation(false);
            SwitchActionMap("GameOverUI");
            _isGameOver = true;
        }
    }
}