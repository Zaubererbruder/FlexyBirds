using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PipeSpawner
    {
        private GameObject _pipePrefab; 
        private bool _active = false;
        private float _maxSpawnPoint = 1.5f;
        private float _minSpawnPoint = -0.9f;
        
        public PipeSpawner(GameObject prefab)
        {
            _pipePrefab = prefab;
        }

        public Queue<Pipe> _pipes = new Queue<Pipe>();

        public IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                var spawnTime = 1.5f;
                while (spawnTime > 0)
                {
                    if (_active)
                        spawnTime -= Time.deltaTime;
                    yield return null;
                }

                var interval = Random.Range(_minSpawnPoint, _maxSpawnPoint);

                var createdPipe = GameObject.Instantiate(_pipePrefab, new Vector3(10, interval, 0), Quaternion.identity);
                var pipeMoving = createdPipe.GetComponent<Pipe>();
                pipeMoving.OnDestroyed += RemovePipeFromQueue;
                _pipes.Enqueue(pipeMoving);
                
            }
        }

        public void RemovePipeFromQueue()
        {
            var pipe = _pipes.Dequeue();
            pipe.OnDestroyed -= RemovePipeFromQueue;
        }

        public void StopSpawner()
        {
            _active = false;
            foreach(var pipe in _pipes)
            {
                pipe.StopPipe();
            }
        }

        public void StartSpawner()
        {
            _active = true;
            foreach (var pipe in _pipes)
            {
                pipe.StartPipe();
            }
        }

        public void ClearSpawner()
        {
            foreach (var pipe in _pipes)
            {
                pipe.OnDestroyed -= RemovePipeFromQueue;
                Object.Destroy(pipe.gameObject);
            }
            _pipes.Clear();
        }
    }
}
