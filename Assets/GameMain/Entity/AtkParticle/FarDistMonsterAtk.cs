using UnityEngine;

using static SchoolRPG.GameMain.Utils.Direction;

namespace SchoolRPG.GameMain.Utils.AtkParticle
{
    public class FarDistMonsterAtk: AtkParticleBase
    {
        public override float width => 0.45f;
        public override float height => 0.45f;
        public override double Atk => 10;
        public override float DeltaTime => 1;
        
        private const float deleteTime = 2;
        private Vector3 startPos;
        
        protected override void Start()
        {
            base.Start();
            startPos = transform.position;
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
            transform.position = startPos + ((Direction) DataValue).V() * dt * (12 / deleteTime);
        }
        
        protected override bool OnMonsterAtk(NormalMonsterBase shortDistMonster)
            => shortDistMonster != super.GetComponent<NormalMonsterBase>() && base.OnMonsterAtk(shortDistMonster);
    }
}