using UnityEngine;

namespace SchoolRPG.GameMain.Utils
{
    public abstract class NormalMonsterBase: UnitBase
    {
        /// <summary>
        /// Check is player exists on detectable zone and if exists, shoot.
        /// </summary>
        /// <returns>If player exists then returns true</returns>
        private bool SearchPlayer()
        {
            //TODO
            return false;
        }

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
    }
}