using UnityEngine;
using System.Collections;

namespace Game.Mobs
{
    /// <summary>
    /// Spawns and respawns mobs at defined spawn points.
    /// </summary>
    public class MobSpawner : MonoBehaviour
    {
        public static MobSpawner Instance { get; private set; }
        void Awake() { Instance = this; }

        [Tooltip("Prefab of the mob to spawn")] public GameObject mobPrefab;
        [Tooltip("Spawn points under this object or assigned manually")] public Transform[] spawnPoints;
        [Tooltip("Delay before respawning a dead mob")] public float respawnDelay = 5f;

        void Start()
        {
            if (spawnPoints == null || spawnPoints.Length == 0)
            {
                // use children as spawn points
                spawnPoints = new Transform[transform.childCount];
                for (int i = 0; i < transform.childCount; i++)
                    spawnPoints[i] = transform.GetChild(i);
            }
            foreach (var point in spawnPoints)
                SpawnAt(point);
        }

        void SpawnAt(Transform point)
        {
            var mob = Instantiate(mobPrefab, point.position, point.rotation);
            var health = mob.GetComponent<Health>();
            if (health != null)
                health.OnDeath += () => StartCoroutine(RespawnRoutine(point));
        }

        IEnumerator RespawnRoutine(Transform point)
        {
            yield return new WaitForSeconds(respawnDelay);
            SpawnAt(point);
        }

        /// <summary>
        /// Schedule a respawn at arbitrary position after delay.
        /// </summary>
        public void ScheduleRespawn(Vector3 position, Quaternion rotation, float delay)
        {
            StartCoroutine(RespawnAfterDelay(position, rotation, delay));
        }

        private IEnumerator RespawnAfterDelay(Vector3 position, Quaternion rotation, float delay)
        {
            yield return new WaitForSeconds(delay);
            var mob = Instantiate(mobPrefab, position, rotation);
            var health = mob.GetComponent<Health>();
            if (health != null)
                health.OnDeath += () => ScheduleRespawn(position, rotation, delay);
        }
    }
}
