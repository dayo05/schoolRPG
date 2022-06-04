using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static SchoolRPG.GameMain.Entity.Direction;
using static System.Linq.Enumerable;

namespace SchoolRPG.GameMain.Entity.AtkParticle
{
    public class PlayerChairAtk: AtkParticleBase
    {
        public override double Atk { get; set; } = 10;
        public override float DeltaTime { get; set; } = 1;

        private const float deleteTime = 0.25f;
        private float startTime;
        private float bias;
        private const float roundAngle = Mathf.PI / 2;
        void Start()
        {
            cam = GameObject.Find("Main Camera");
            startTime = Time.time;
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
            var angle = bias + roundAngle * (Time.time - startTime) / deleteTime;
            transform.position = cam.GetComponent<EntityHandler>().player.transform.position +
                                 new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
        }

        protected override void OnMonsterAtk(Monster monster)
        {
            monster.Nuckback((Direction) DataValue);
            base.OnMonsterAtk(monster);
        }

        public override float width { get; set; } = 1;
        public override float height { get; set; } = 2.0f / 3;
        
        protected override void OnPlayerAtk(Player player)
        {
            //Ignore
        }
    }
}