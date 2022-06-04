using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SchoolRPG.GameMain.Entity
{
    public class EntityHandler: MonoBehaviour
    {
        public GameObject player;
        public GameObject map;
        public GameObject monster;
        public static bool IsInversedMap => Global.CurrentLevel % 2 == 1;

        private List<GameObject> monsters = new();

        private void Start()
        {
            map.transform.localScale = IsInversedMap ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
            map.transform.position = IsInversedMap ? new Vector3(15.5f, -8.5f, 0) : new Vector3(15.5f, 8.5f, 0);
            transform.position = IsInversedMap ? new Vector3(15.5f, -8.5f, -10) : new Vector3(15.5f, 8.5f, -10);
            player.transform.position = Global.IsMovingNext
                ? EntityBase.GetCurrentWorldPos(2, 2)
                : EntityBase.GetCurrentWorldPos(26, 2);
            SpawnMonster((2, 2));
        }

        public void SpawnMonster((int x, int y) loc)
        {
            var g = Instantiate(monster);
            g.transform.position = EntityBase.GetCurrentWorldPos(loc.x, loc.y);

            if (!g.GetComponent<Monster>().ValidatePos())
            {
                Debug.Log("Destroyed");
                Destroy(g);
                throw new ArgumentOutOfRangeException(nameof(loc), "Not able to spawn monster on that place");
            }
            g.SetActive(true);
            monsters.Add(g);
        }

        public IEnumerable<GameObject> Entities => monsters.Where(x => !x.IsDestroyed()).Concat(new[]{player});

        public void MoveToNextLevel()
        {
            Global.CurrentLevel++;
            Global.IsMovingNext = true;
            SceneManager.LoadScene(gameObject.scene.name);
        }

        public void MoveToPreviousLevel()
        {
            if (Global.CurrentLevel == 0) return;
            Global.CurrentLevel--;
            Global.IsMovingNext = false;
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }
}