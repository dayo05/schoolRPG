using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SchoolRPG.GameMain.Entity
{
    public class EntityHandler: MonoBehaviour
    {
        public GameObject player;
        public GameObject map;
        public GameObject monster;

        public List<GameObject> monsters = new();

        public void SpawnMonster((int x, int y) loc)
        {
            var g = Instantiate(monster);
            g.transform.position = EntityBase.GetCurrentWorldPos(loc.x, loc.y);

            if (!g.GetComponent<Monster>().ValidatePos())
            {
                Destroy(g);
                throw new ArgumentOutOfRangeException(nameof(loc), "Not able to spawn monster on that place");
            }
            monsters.Add(g);
        }

        public IEnumerable Entities => monsters.Concat(new[]{player});
    }
}