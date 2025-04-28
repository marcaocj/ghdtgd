using System.Collections;
using UnityEngine;

namespace Game.Monsters {
    public class MobSpawner : MonoBehaviour
    {
        public static MobSpawner Instance;
        public GameObject monsterPrefab;
        public Transform[] spawnPoints;
        public float respawnDelay = 5f;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        void Start()
        {
            foreach (var sp in spawnPoints)
                SpawnAt(sp.position, sp.rotation);
        }

        public void SpawnAt(Vector3 pos, Quaternion rot)
        {
            Instantiate(monsterPrefab, pos, rot);
        }

        public void ScheduleRespawn(Vector3 pos, Quaternion rot, float delay)
        {
            StartCoroutine(RespawnCoroutine(pos, rot, delay));
        }

        IEnumerator RespawnCoroutine(Vector3 pos, Quaternion rot, float delay)
        {
            yield return new WaitForSeconds(delay);
            SpawnAt(pos, rot);
        }
    }
}
