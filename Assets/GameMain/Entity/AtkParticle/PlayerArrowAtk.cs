using UnityEngine;

using static SchoolRPG.GameMain.Entity.Direction;

namespace SchoolRPG.GameMain.Entity.AtkParticle
{
    public class PlayerArrowAtk: AtkParticleBase
    {
        public override double Atk { get; set; } = 5;
        public override float DeltaTime { get; set; } = 0.5f;
        public override float width { get; set; } = 1;
        public override float height { get; set; } = 0.2f;
        
        private const float deleteTime = 2;
        private float startTime;
        private Vector3 startPos;
        
        void Start()
        {
            cam = GameObject.Find("Main Camera");
            startTime = Time.time;
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

        protected override void Update()
        {
            base.Update();
            transform.position = startPos + ((Direction) DataValue).V() * (Time.time - startTime) * (12 / deleteTime);
        }

        protected override void OnMonsterAtk(Monster monster)
        {
            monster.Nuckback((Direction) DataValue);
            base.OnMonsterAtk(monster);
        }

        protected override void OnPlayerAtk(Player player)
        {
            //Ignore
        }
    }
}