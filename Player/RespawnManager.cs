using UnityEngine;
using System.Collections;
using Game.Player;

namespace Game.Player
{
    /// <summary>
    /// Handles player respawn on death, XP loss and restore health/SP.
    /// </summary>
    public class RespawnManager : MonoBehaviour
    {
        public float respawnDelay = 3f;
        private Vector3 spawnPoint;
        private Health health;
        private Mana mana;
        private PlayerCharacterStats stats;

        void Start()
        {
            health = GetComponent<Health>();
            mana = GetComponent<Mana>();
            stats = GetComponent<PlayerCharacterStats>();
            // initial spawn point at start
            spawnPoint = transform.position;
            health.OnDeath += OnPlayerDeath;
        }

        /// <summary>
        /// Update spawn point (e.g. when reaching a save point)
        /// </summary>
        public void SetSpawnPoint(Vector3 point)
        {
            spawnPoint = point;
        }

        private void OnPlayerDeath()
        {
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(respawnDelay);
            // XP loss: lose 10% of current XP
            stats.currentXP *= 0.9f;
            // Move to spawn
            transform.position = spawnPoint;
            // Restore HP and SP
            health.Initialize(stats.maxHP);
            mana.Initialize(stats.maxSP);
        }
    }
}
