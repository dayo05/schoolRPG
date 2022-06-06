using SchoolRPG.GameMain.Utils;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity
{
    public abstract class NormalMonsterBase : UnitBase
    {
        //#define IS_NATIVE_REGISTERED
        /// <summary>
        /// Check is player exists on detectable zone and if exists, shoot.
        /// </summary>
        /// <returns>If player exists then returns true</returns>
        private bool SearchPlayer()
        {
#if IS_NATIVE_REGISTERED
            var (cx, cy) = GetCurrentMapPos();
            var (px, py) = GetCurrentMapPos(handler.player.transform.position);
            TryMoveBy((Direction)search_player_native(playerMap, cx, cy, px, py));
#endif
            return false;
        }

#if IS_NATIVE_REGISTERED
        [DllImport("native")]
        private extern static int search_player_native(bool[][] map, int cx, int cy, int px, int py);
#endif

        public GameObject atkObject;

        protected override void Start()
        {
            base.Start();
            Atk.Add(atkObject);
        }

        protected virtual void Update()
        {
            if (!SearchPlayer() && !IsNuckbacked)
            {
                var _ = (double) Random.value switch
                {
                    < 0.005 => TryMoveBy(lastMoveDirection.R()),
                    < 0.01 => TryMoveBy(lastMoveDirection.L()),
                    _ => TryMoveBy(lastMoveDirection)
                };
            }
        }

        protected override void OnDie()
        {
            base.OnDie();
            double rv = Random.value;
            var bias = ((Global.CurrentLevel + 1) * 1.5);
            switch (rv)
            {
                case < 0.05:
                    Global.PlayerMaxHealth += 2 * bias;
                    handler.player.GetComponent<Player>().Hp += 2 * bias;
                    break;
                case < 0.2:
                    handler.player.GetComponent<Player>().Hp += 4 * bias;
                    break;
                case < 0.3:
                    Global.PlayerArrowAtk += 0.1 * bias;
                    break;
                case < 0.4:
                    Global.PlayerChairAtk += 0.1 * bias;
                    break;
                case < 0.45:
                    Global.Knowledge += 0.1 * bias;
                    break;
            }
        }
    }
}