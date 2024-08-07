using System.Collections;
using UnityEngine;

namespace ZPong
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float introDuration = 3f; // Total duration of the intro animation
        [SerializeField] private float ballInstantiateTime = 0f; // Time at which to instantiate the ball during the intro
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private GameObject canvasParent;

        public Ball activeBall;

        public static GameManager Instance { get; private set; }

        private Goal[] goals;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            goals = new Goal[2];
        }

        private void Start()
        {
            StartCoroutine(IntroSequence());
        }

        IEnumerator IntroSequence()
        {
            // Wait until it's time to instantiate the ball
            yield return new WaitForSeconds(ballInstantiateTime);

            // Instantiate the ball part way through the intro
            SetGame();

            // Wait for the remainder of the intro animation
            yield return new WaitForSeconds(introDuration - ballInstantiateTime);

            // Set bounds and start the game
            SetBounds();
            StartGame();
        }

        void SetGame()
        {
            if (activeBall == null)
            {
                activeBall = Instantiate(ballPrefab, Vector3.zero, this.transform.rotation, canvasParent.transform)
                    .GetComponent<Ball>();
                activeBall.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }

            activeBall.SetBallActive(false);
        }

        void StartGame()
        {
            // Debug.Log("Starting game!");
            activeBall.SetBallActive(true);
        }

        public void Reset()
        {
            StartCoroutine(StartTimer());
        }

        IEnumerator StartTimer()
        {
            if (activeBall != null)
            {
                activeBall.SetBallActive(false);
                activeBall.transform.position = Vector3.zero;
                activeBall.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }

            yield return new WaitForSeconds(introDuration);

            SetBounds();

            StartGame();
        }

        void SetBounds()
        {
            if (activeBall != null)
            {
                activeBall.SetHeightBounds();
            }
            foreach (var g in goals)
            {
                g?.SetHeightBounds();
            }
        }

        public void ResetBall()
        {
            StartCoroutine(ResetBallCoroutine());
        }

        private IEnumerator ResetBallCoroutine()
        {
            // Simply reset the ball's position and state instead of destroying it
            if (activeBall != null)
            {
                activeBall.transform.position = Vector3.zero;
                activeBall.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                activeBall.SetBallActive(false);
            }

            yield return null;

            StartCoroutine(StartTimer());
        }

        public void SetGoalObj(Goal g)
        {
            if (goals[0] != null)
            {
                goals[1] = g;
            }
            else
            {
                goals[0] = g;
            }
        }
    }
}
