using UnityEngine;

namespace Botaemic.Core
{
    [RequireComponent(typeof(Health))]
    public class Damageable : MonoBehaviour
    {
        [Tooltip("Multiplier to apply to the received damage")]
        [SerializeField]
        private float damageMultiplier = 1f;
        [Range(0, 1)]
        [Tooltip("Multiplier to apply to self damage")]
        [SerializeField]
        private float sensibilityToSelfdamage = 0.5f;

        public Health health { get; private set; }
        public EnergyShield shield { get; private set; }

        void Awake()
        {
            // find the health component either at the same level, or higher in the hierarchy
            health = GetComponent<Health>();
            if (health == null)
            {
                health = GetComponentInParent<Health>();
            }

            shield = GetComponent<EnergyShield>();
            if (shield == null)
            {
                shield = GetComponentInParent<EnergyShield>();
            }
        }

        public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
        {
            if (health == null && shield == null) { return; }

            //TODO Better damage sharing
            if (shield != null)
            {
                if (shield.CurrentValue > 0f)
                {
                    shield.TakeDamage(damage);
                }
                else
                {
                    if (health != null)
                    {
                        var totalDamage = damage;

                        // skip the crit multiplier if it's from an explosion
                        if (!isExplosionDamage)
                        {
                            totalDamage *= damageMultiplier;
                        }

                        // potentially reduce damages if inflicted by self
                        if (health.gameObject == damageSource)
                        {
                            totalDamage *= sensibilityToSelfdamage;
                        }

                        // apply the damages
                        //health.TakeDamage(totalDamage, damageSource);
                        health.TakeDamage(totalDamage);
                    }
                }

            }

        }
    }
}
