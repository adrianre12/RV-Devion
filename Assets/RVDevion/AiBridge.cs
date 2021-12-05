using RVHonorAI;
using UnityEngine;

namespace RVDevion {
public class AiBridge : MonoBehaviour
{
    IDamageable _damageable;

        void Start()
        {
            _damageable = GetComponent<IDamageable>();
            if (_damageable == null)
            {
                Debug.LogWarning("No IDamageable component found, ensure Character of derived component is available.");
                return;
            }
            DevionGames.EventHandler.Register<GameObject, string, float>(gameObject, "OnGetHit", OnGetHit);
        }

        public void OnGetHit(GameObject sender, string statName, float value)
        {
            if (sender.TryGetComponent<PlayerBridge>(out PlayerBridge pc))
                _damageable.ReceiveDamage(value, pc, DamageType.Physical, true);
        }
    }
}
