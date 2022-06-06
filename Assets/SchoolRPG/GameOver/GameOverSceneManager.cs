using SchoolRPG.GameMain.Utils;
using UnityEngine;

namespace SchoolRPG.GameOver
{
    public class GameOverSceneManager : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
                Global.Initialize();
        }
    }
}
