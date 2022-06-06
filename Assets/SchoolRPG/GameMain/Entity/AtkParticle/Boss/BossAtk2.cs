using System;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity.AtkParticle.Boss
{
    public class BossAtk2: AtkParticleBase
    {
        public override float width => 1.8f;
        public override float height => 2.7f;
        public override double Atk => 100;
        public override float DeltaTime => 0;
        protected override void Move()
        {
            transform.position = new Vector3(DataValue, 18  * (1 - dt), 0);
        }
        
        protected override bool OnBossMonsterAtk(BossMonster boss)
        {
            return false;
        }

        protected override bool ValidatePos(Vector3 vec)
        {
            return dt < 1;
        }

        private void OnDestroy()
        {
            super.transform.position = new Vector3(15.5f, 8.5f, 0);
        }
    }
}