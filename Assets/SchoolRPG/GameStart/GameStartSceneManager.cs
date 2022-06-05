using SchoolRPG.GameMain.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SchoolRPG.GameStart
{
    public class GameStartSceneManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameObject.Find("PlayBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                Global.CurrentLevel = 0;
                SceneManager.LoadScene("GameScene");
            });
        }
    }
}
