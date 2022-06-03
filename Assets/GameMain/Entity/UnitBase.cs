using System.Collections;
using System.Collections.Generic;
using SchoolRPG.GameMain.Entity.AtkParticle;
using UnityEngine;

using static System.Linq.Enumerable;

namespace SchoolRPG.GameMain.Entity
{
    public abstract class UnitBase: EntityBase
    {
        private List<float> lastShootTime;
        
        protected virtual GameObject TryShoot(int atkIdx, int subData = 0)
        {
            lastShootTime ??= new(Repeat<float>(0, Atk.Count));
            
            if (Time.time < lastShootTime[atkIdx] + Atk[atkIdx].GetComponent<AtkParticleBase>().DeltaTime) return null;
            lastShootTime[atkIdx] = Time.time;
            var g = Instantiate(Atk[atkIdx]);
            g.transform.position = transform.position;
            g.GetComponent<AtkParticleBase>().DataValue = subData;
            g.SetActive(true);
            return g;
        }

        private double _hp;

        public double Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                if (_hp <= 0)
                    OnDie();
            }
        }

        protected virtual void OnDie()
        {
            Destroy(gameObject);
        }

        protected List<GameObject> Atk { get; } = new();
        protected abstract float NuckbackDist { get; set; }

        private IEnumerator _Nuckback(Direction d)
        {
            foreach (var i in Range(0, 25))
            {
                if (ValidatePos(transform.position + NuckbackDist * d.V()))
                    transform.position += NuckbackDist * d.V();
                yield return new WaitForSeconds(0.01f);
            }
        }
        
        public void Nuckback(Direction d)
            => StartCoroutine(_Nuckback(d));
    }
}