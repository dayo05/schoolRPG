using SchoolRPG.GameMain.Utils;
using UnityEngine;

namespace SchoolRPG.GameStart
{
    public class GameStartSceneManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                Global.Initialize();
        }
    }
}
