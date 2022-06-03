using System;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity.AtkParticle
{
    public abstract class AtkParticleBase: EntityBase
    {
        public abstract double Atk { get; set; }
        public abstract float DeltaTime { get; set; }
        public int DataValue { get; set; } = -1;

        public GameObject cam;
        private void Start()
        {
            cam = GameObject.Find("Main Camera");
        }

        protected virtual void Update()
        {
            if (!ValidatePos())
                Destroy(gameObject);
            else
            {
                foreach (var x in cam.GetComponent<EntityHandler>().monsters)
                {
                    if (x.TryGetComponent(out Monster monster)) OnMonsterAtk(monster);
                    if (x.TryGetComponent(out Player player)) OnPlayerAtk(player);
                }
            }
        }

        protected virtual void OnMonsterAtk(Monster monster)
        {
            monster.Hp -= Atk;
            Destroy(gameObject);
        }

        protected virtual void OnPlayerAtk(Player player)
        {
            player.Hp -= Atk;
            Destroy(gameObject);
        }
    }
}