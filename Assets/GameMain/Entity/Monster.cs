using System;
using System.Collections.Generic;
using SchoolRPG.GameMain.Entity.AtkParticle;
using UnityEngine;

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

        public override double Hp { get; set; } = 20;
        protected override float NuckbackDist { get; set; } = moveDist;
        public const float moveDist = 0.05f;

        private void Start()
        {
            //Atk.Add(Resources.Load("MonsterAtk") as GameObject);
        }

        public override float width { get; set; } = 1;
        public override float height { get; set; } = 1;
    }
}