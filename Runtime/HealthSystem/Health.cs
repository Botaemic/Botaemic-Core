using UnityEngine;
using UnityEngine.Events;

namespace Botaemic.Core
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Maximum amount of health")]
        [SerializeField] private float maxHealth = 10f;
        //[Tooltip("Health ratio at which the critical health vignette starts appearing")]
        //[SerializeField] private float criticalHealthRatio = 0.3f;

        [Tooltip("Healthbar to display, can be empty")]
        [SerializeField] private Bar healthBar = null;
        [SerializeField] private float currentHealth = 0;

        public UnityAction onDie;
        public UnityAction<float, GameObject> onDamaged;

        private Stat health = null;
        private bool isDead = false;

        void Start()
        {
            health = new Stat(maxHealth);
            if (healthBar != null) { healthBar.Initialize(health); }
            currentHealth = health.CurrentValue;
        }

        public void Heal(float amount)
        {
            health.AddPoints(amount);
            currentHealth = health.CurrentValue;
        }

        public void TakeDamage(float amount)
        {
            health.RemovePoints(amount);
            if (health.CurrentValue <= 0f)
            {
                Kill();
            }
            currentHealth = health.CurrentValue;
        }

        public void Kill()
        {
            health.RemovePoints(health.CurrentValue);
            HandleDeath();
        }

        private void HandleDeath()
        {
            if (isDead)
                return;

            if (currentHealth <= 0f)
            {
                if (onDie != null)
                {
                    isDead = true;
                    onDie.Invoke();
                }
            }
        }
    }
}