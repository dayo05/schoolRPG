using UnityEngine;

namespace SchoolRPG.GameMain.Utils.AtkParticle
{
    public class ShortDistMonsterAtk: AtkParticleBase
    {
        public override double Atk => 10;
        public override float DeltaTime => 1;
        private const float DeleteAfter = 0.5f;

        protected override void Start()
        {
            transform.position = super.transform.position;
            transform.rotation = ((Direction) DataValue).Q();
            if ((Direction) DataValue == Direction.Left) transform.localScale = new Vector3(1, -1, 1); 
            base.Start();
            Destroy(gameObject, DeleteAfter);
        }

        protected override void Move()
        {
            transform.position = super.transform.position + (dt / DeleteAfter) * ((Direction) DataValue).V();
        }

        private void OnDestroy()
        {
            super.SetActive(true);
        }

        protected override bool OnMonsterAtk(NormalMonsterBase shortDistMonster)
            => shortDistMonster != super.GetComponent<NormalMonsterBase>() && base.OnMonsterAtk(shortDistMonster);

        public override float width => 0.9f;
        public override float height => 0.9f;
    }
}