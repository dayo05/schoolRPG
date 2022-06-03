using UnityEngine;

namespace SchoolRPG.GameMain.Entity.AtkParticle
{
    public class MonsterAtk: AtkParticleBase
    {
        public override double Atk { get; set; }
        public override float DeltaTime { get; set; }
        public GameObject super;
        public override float width { get; set; }
        public override float height { get; set; }
    }
}