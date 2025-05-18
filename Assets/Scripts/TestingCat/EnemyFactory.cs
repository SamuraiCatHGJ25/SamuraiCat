using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestingCat
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private GameObject baseEnemy;

        private int Wave = 0;

        private void Start()
        {
            CancelInvoke(nameof(Wave));
            InvokeRepeating(nameof(SpawnEnemies), 30f, 30f);
        }

        private void SpawnEnemies()
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
                g.layer = LayerMask.NameToLayer("Enemy");
                g.SetActive(true);
            }
        }
    }
}