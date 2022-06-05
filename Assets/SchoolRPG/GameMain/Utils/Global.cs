using UnityEngine.SceneManagement;

namespace SchoolRPG.GameMain.Utils
{
    public static class Global
    {
        public static int CurrentLevel = 10;
        public static bool IsMovingNext = true;
        public static double PlayerHealth = 20;

        private const int BossFightMap = 10;
        public static bool IsBossFightMap => CurrentLevel == BossFightMap;
        
        public static void MoveToPreviousLevel()
        {
            if (CurrentLevel == 0) return;
            CurrentLevel--;
            IsMovingNext = false;
            SceneManager.LoadScene("GameScene");
        }
        
        public static void MoveToNextLevel()
        {
            CurrentLevel++;
            IsMovingNext = true;
            SceneManager.LoadScene(CurrentLevel == BossFightMap ? "BossFightScene" : "GameScene");
        }
    }
}