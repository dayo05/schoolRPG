using System;
using System.Collections.Generic;
using System.Linq;
using SchoolRPG.GameMain.Entity.AtkParticle;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace SchoolRPG.GameMain.Entity
{
    public class Monster: UnitBase
    {
        /// <summary>
        /// Check is player exists on detectable zone and if exists, shoot.
        /// </summary>
        /// <returns>If player exists then returns true</returns>
        private bool SearchPlayer()
        {
            
            return false;
        }

        protected override GameObject TryShoot(int id, int dat)
        {
            var g = base.TryShoot(id, dat);
            g.GetComponent<MonsterAtk>().super = gameObject;
            return g;
        }

        public override double MaxHp { get; protected set; } = 20;

        protected override float NuckbackDist => MoveDist * 10;
        protected override float MoveDist => 0.005f;

        private new void Update()
        {
            base.Update();
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

        public override float width { get; set; } = 0.9f;
        public override float height { get; set; } = 0.9f;
    }
}