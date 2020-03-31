using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Botaemic.Core
{
    public class EnergyShield : MonoBehaviour
    {
        [Tooltip("Maximum amount of health")]
        [SerializeField] private float maximumShieldPoints = 10f;
        [Tooltip("Recharge rate of energy shield")]
        [SerializeField] private float rechargeRate = 0f;
        //[Tooltip("Health ratio at which the critical health vignette starts appearing")]
        //[SerializeField] private float criticalShieldRatio = 0.3f;

        [Tooltip("Healthbar to display, can be empty")]
        [SerializeField] private Bar shieldBar = null;
        [SerializeField] private float currentHealth = 0;

        public UnityAction<float, GameObject> onDamaged;

        private Stat shield = null;
        //private bool isDead = false;

        public float CurrentValue { get => shield.CurrentValue; }

        void Start()
        {
            shield = new Stat(maximumShieldPoints);
            if (shieldBar != null) { shieldBar.Initialize(shield); }
            currentHealth = shield.CurrentValue;
        }

        public void Heal(float amount)
        {
            shield.AddPoints(amount);
            currentHealth = shield.CurrentValue;
        }

        public void TakeDamage(float amount)
        {
            shield.RemovePoints(amount);
            if (shield.CurrentValue <= 0f)
            {
            }
            currentHealth = shield.CurrentValue;
        }

        private void Update()
        {
            Heal(rechargeRate * Time.deltaTime);
        }
    }
}
