using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestingCat
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private GameObject baseEnemy;

        private int Wave = 0;

        private List<GameObject> enemies;

        private void Start()
        {
            SpawnWave();
            enemies = new List<GameObject>();
        }

        public bool IsWaveCleared()
        {
            return !enemies.TrueForAll(s => s);
        }

        public void SpawnWave()
        {
            CancelInvoke(nameof(SpawnWaveInternal));
            Invoke("SpawnWaveInternal", 30f);
        }

        private void SpawnWaveInternal()
        {
            Wave++;

            for (int i = 0; i < Wave * 5; i++)
            {
                SpawnEnemy();
            }
        }

        private async void SpawnEnemy()
        {
            Vector3 pos = new Vector3(Random.Range(-35f, 35f), 2, Random.Range(-35f, 35f));
            GameObject[] go = await InstantiateAsync(baseEnemy, pos, Quaternion.identity);
            foreach (var g in go)
            {
                enemies.Add(g);
                g.layer = LayerMask.NameToLayer("Enemy");
                g.SetActive(true);
            }
        }
    }
}