using System.Collections;
using System.Collections.Generic;
using SchoolRPG.GameMain.Entity.AtkParticle;
using SchoolRPG.GameMain.Utils;
using UnityEngine;

using static System.Linq.Enumerable;

namespace SchoolRPG.GameMain.Entity
{
    public abstract class UnitBase: EntityBase
    {
        protected List<float> lastShootTime;
        protected Direction lastMoveDirection = Direction.Right;
        
        protected virtual GameObject TryShoot(int atkIdx, int subData = 0)
        {
            lastShootTime ??= new(Repeat<float>(0, Atk.Count));
            
            if (Time.time < lastShootTime[atkIdx] + Atk[atkIdx].GetComponent<AtkParticleBase>().DeltaTime) return null;
            lastShootTime[atkIdx] = Time.time;
            var g = Instantiate(Atk[atkIdx]);
            g.transform.position = transform.position;
            g.GetComponent<AtkParticleBase>().DataValue = subData;
            g.GetComponent<AtkParticleBase>().super = gameObject;
            g.SetActive(true);
            return g;
        }

        private double? _hp;

        public double Hp
        {
            get => _hp ?? MaxHp;
            set
            {
                _hp = value;
                transform.GetChild(0).localPosition = new Vector3((float)(-width / 2 + value / (MaxHp * 2) * width), height / 2 + 0.1f, 0);
                transform.GetChild(0).localScale = new Vector3((float) (value / MaxHp) * width, 0.1f, 0);
                if (_hp <= 0)
                    OnDie();
            }
        }
        
        public abstract double MaxHp { get; }

        protected virtual void OnDie()
        {
            Destroy(gameObject);
        }

        protected List<GameObject> Atk { get; } = new();
        protected abstract float NuckbackDist { get; }
        
        protected bool IsNuckbacked { get; private set; }

        private IEnumerator _Nuckback(Direction d)
        {
            IsNuckbacked = true;
            foreach (var i in Range(0, 5))
            {
                TryMoveBy(d, NuckbackDist);
                yield return new WaitForSeconds(0.01f);
            }

            IsNuckbacked = false;
        }
        
        public void Nuckback(Direction d)
            => StartCoroutine(_Nuckback(d));

        protected abstract float MoveDist { get; }
        protected virtual (bool, Vector3) TryMoveBy(Direction direction, float? dist = null)
        {
            dist ??= MoveDist;
            lastMoveDirection = direction;
            var tryPos = transform.position + dist.Value * direction.V();
            if (!ValidatePos(tryPos)) return (false, tryPos);
            transform.position = tryPos;
            return (true, tryPos);
        }

        protected override void Start()
        {
            base.Start();
            Hp = MaxHp;
        }
    }
}