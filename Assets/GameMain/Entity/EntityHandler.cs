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
        public bool isInversedMap = true;

        private List<GameObject> monsters = new();

        private void Start()
        {
            map.transform.localScale = isInversedMap ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
            map.transform.position = isInversedMap ? new Vector3(15.5f, -8.5f, 0) : new Vector3(15.5f, 8.5f, 0);
            transform.position = isInversedMap ? new Vector3(15.5f, -8.5f, -10) : new Vector3(15.5f, 8.5f, -10);
            SpawnMonster((2, 2));
        }

        public void SpawnMonster((int x, int y) loc)
        {
            var g = Instantiate(monster);
            g.transform.position = g.GetComponent<Monster>().GetCurrentWorldPos(loc.x, loc.y);

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
            SceneManager.LoadScene(gameObject.scene.name);
        }

        public void MoveToPreviousLevel()
        {
            Global.CurrentLevel--;
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }
}