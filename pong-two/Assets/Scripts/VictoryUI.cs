using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ZPong
{

    public class VictoryUI : MonoBehaviour
    {
        public Button playAgain;
        public Button quitGame;

        private void Start()
        {
            playAgain.onClick.AddListener(ReloadScene);
            quitGame.onClick.AddListener(Quit);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene("Singleplayer");
        }
        public void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}