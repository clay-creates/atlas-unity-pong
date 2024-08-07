using System.Collections;
using UnityEngine;

namespace ZPong
{
    public class BallSpawner : MonoBehaviour
    {
        public GameObject ballPrefab; // The ball prefab to spawn
        public float initialSpawnDelay = 45f; // Initial delay before the first ball is spawned
        public float spawnInterval = 60f; // Interval between subsequent ball spawns

        private void Start()
        {
            StartCoroutine(SpawnBalls());
        }

        private IEnumerator SpawnBalls()
        {
            yield return new WaitForSeconds(initialSpawnDelay);

            while (true)
            {
                SpawnBall();
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnBall()
        {
            Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
