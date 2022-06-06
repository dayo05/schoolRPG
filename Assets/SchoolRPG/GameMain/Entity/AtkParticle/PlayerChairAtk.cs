using SchoolRPG.GameMain.Utils;
using UnityEngine;
using static SchoolRPG.GameMain.Utils.Direction;

namespace SchoolRPG.GameMain.Entity.AtkParticle
{
    public class PlayerChairAtk: AtkParticleBase
    {
        public override double Atk => 10 * Global.PlayerChairAtk;
        public override float DeltaTime => 1;

        private const float deleteTime = 0.25f;
        private float bias;
        private const float roundAngle = Mathf.PI / 2;
        protected override void Start()
        {
            base.Start();
            bias = (Direction) DataValue switch
            {
                Up => 0 - roundAngle / 2,
                Down => Mathf.PI - roundAngle / 2,
                Right => Mathf.PI / 2 - roundAngle / 2,
                Left => -Mathf.PI / 2 - roundAngle / 2
            };
            Destroy(gameObject, deleteTime);
        }

        protected override void Move()
        {
            var angle = bias + roundAngle * dt / deleteTime;
            transform.position = super.transform.position +
                                 new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
        }

        public override float width => 1;
        public override float height => 2.0f / 3;
        
        protected override bool OnPlayerAtk(Player player)
        {
            return false;
        }
    }
}