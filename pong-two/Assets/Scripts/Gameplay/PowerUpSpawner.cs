using System.Collections;
using UnityEngine;

namespace ZPong
{
    public class PowerUpSpawner : MonoBehaviour
    {
        public GameObject speedUpPowerUpPrefab;
        public float minSpawnTime = 20f;
        public float maxSpawnTime = 30f;
        public RectTransform gameplayCanvas;

        private void Start()
        {
            StartCoroutine(SpawnPowerUp());
        }

        private IEnumerator SpawnPowerUp()
        {
            while (true)
            {
                float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(waitTime);

                // Calculate random position within the canvas bounds
                Vector2 canvasSize = gameplayCanvas.rect.size;
                Vector2 spawnPosition = new Vector2(
                    Random.Range(-canvasSize.x / 2, canvasSize.x / 2),
                    Random.Range(-canvasSize.y / 2, canvasSize.y / 2)
                );

                // Instantiate power-up as a child of the gameplay canvas
                GameObject powerUp = Instantiate(speedUpPowerUpPrefab, gameplayCanvas);
                RectTransform powerUpRectTransform = powerUp.GetComponent<RectTransform>();
                powerUpRectTransform.anchoredPosition = spawnPosition;
            }
        }
    }
}
