using System;
using SchoolRPG.GameMain.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SchoolRPG.GameOver
{
    public class GameOverSceneManager : MonoBehaviour
    {
        private void Start()
        {
            GameObject.Find("RestartBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                Global.CurrentLevel = 0;
                SceneManager.LoadScene("GameScene");
            });
        }
    }
}
