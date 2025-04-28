using UnityEngine;
using UnityEngine.UI;
using Game.Player;

namespace Game.Player
{
    /// <summary>
    /// NPC or object to set player's save/respawn point when interacted.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class SavePoint : MonoBehaviour
    {
        [Tooltip("Transform to use as new spawn location; defaults to this object")] public Transform spawnLocation;
        [Tooltip("Key to press for interaction")] public KeyCode interactKey = KeyCode.E;
        [Tooltip("Optional UI Text for feedback")] public Text feedbackText;

        private RespawnManager respawnManager;
        private bool playerInRange;

        void Start()
        {
            if (spawnLocation == null) spawnLocation = transform;
            var col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                respawnManager = other.GetComponent<RespawnManager>();
                playerInRange = respawnManager != null;
                if (playerInRange && feedbackText)
                    feedbackText.text = "Press [" + interactKey + "] to set save point";
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
                if (feedbackText) feedbackText.text = "";
            }
        }

        void Update()
        {
            if (playerInRange && Input.GetKeyDown(interactKey))
            {
                respawnManager.SetSpawnPoint(spawnLocation.position);
                if (feedbackText) feedbackText.text = "Save point updated!";
            }
        }
    }
}
