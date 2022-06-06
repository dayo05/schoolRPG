using UnityEngine;

namespace SchoolRPG.GameMain.Entity.AtkParticle.Boss
{
    public class BossAtk1: AtkParticleBase
    {
        public override float width => 0.7f;
        public override float height => 0.7f;
        public override double Atk => 10;
        public override float DeltaTime => 0;

        protected override void Move()
        {
            transform.position = startPos + dt * 2 * new Vector3(Mathf.Sin(dt / 2 + DataValue / 180.0f * Mathf.PI), Mathf.Cos(dt / 2 + DataValue / 180.0f * Mathf.PI));
        }

        protected override bool OnBossMonsterAtk(BossMonster boss)
        {
            return false;
        }
    }
}