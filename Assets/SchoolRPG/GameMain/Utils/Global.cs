using UnityEngine.SceneManagement;

namespace SchoolRPG.GameMain.Utils
{
    public static class Global
    {
        public static int CurrentLevel;
        public static bool IsMovingNext;
        public static double PlayerHealth;
        public static double PlayerMaxHealth;

        public static double PlayerArrowAtk;
        public static double PlayerChairAtk;
        public static double Knowledge;

        private const int BossFightMap = 10;
        public static bool IsBossFightMap => CurrentLevel == BossFightMap;

        public static void Initialize()
        {
            CurrentLevel = 10;
            IsMovingNext = true;
            PlayerHealth = 20;
            PlayerMaxHealth = 20;
            PlayerArrowAtk = 1;
            PlayerChairAtk = 1;
            
            SceneManager.LoadScene("GameScene");
        }
        
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
            SceneManager.LoadScene("GameScene");
        }
    }
}