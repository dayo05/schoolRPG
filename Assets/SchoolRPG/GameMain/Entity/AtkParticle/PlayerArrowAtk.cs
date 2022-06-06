using SchoolRPG.GameMain.Utils;
using UnityEngine;
using static SchoolRPG.GameMain.Utils.Direction;

namespace SchoolRPG.GameMain.Entity.AtkParticle
{
    public class PlayerArrowAtk: AtkParticleBase
    {
        public override double Atk => 5 * Global.PlayerArrowAtk;
        public override float DeltaTime => 0.5f;
        public override float width => 0.9f;
        public override float height => 0.2f;
        
        private const float deleteTime = 1;
        
        protected override void Start()
        {
            base.Start();
            transform.rotation = Quaternion.Euler(0, 0, (Direction)DataValue switch
            {
                Up => 45,
                Right => -45,
                Down => -135,
                Left => 135
            });
            Destroy(gameObject, deleteTime);
        }

        protected override void Move()
        {
            transform.position = startPos + ((Direction) DataValue).V() * dt * (6 / deleteTime);
        }

        protected override bool OnPlayerAtk(Player player)
        {
            return false;
        }
    }
}