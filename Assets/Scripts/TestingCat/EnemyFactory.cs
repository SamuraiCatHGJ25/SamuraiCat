using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestingCat
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private GameObject baseEnemy;
        [SerializeField] private GameObject archerEnemy;
        [SerializeField] private TextMeshProUGUI waveText; 
        [SerializeField] private HealthController playerHealth;
        [SerializeField] private GameObject towerRoot;

        private int Wave = 0;

        private List<GameObject> enemies;

        private void Start()
        {
            SpawnWaveInternal();
            enemies = new List<GameObject>();
        }

        public bool IsWaveCleared()
        {
            return !enemies.TrueForAll(s => s);
        }

        public void SpawnWave()
        {
            playerHealth.FullHeal();

            foreach (HealthController tower in towerRoot.GetComponentsInChildren(typeof(HealthController)))
            {
                tower.FullHeal();
            }

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
            var enemy = baseEnemy;
            if (Random.Range(0, 2) == 1)
            {
                enemy = archerEnemy;
            }
            Vector3 pos = new Vector3(Random.Range(-35f, 35f), 2, Random.Range(-35f, 35f));
            GameObject[] go = await InstantiateAsync(enemy, pos, Quaternion.identity);
            foreach (var g in go)
            {
                enemies.Add(g);
                g.layer = LayerMask.NameToLayer("Enemy");
                g.SetActive(true);
            }
        }
        private void Update()
        {
            waveText.text = "Wave: " + Wave;
        }
    }
}
