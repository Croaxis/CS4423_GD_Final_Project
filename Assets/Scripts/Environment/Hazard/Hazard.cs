using UnityEngine;
using System.Collections.Generic;

namespace cs4423fp.Environment.Hazard
{
    public class Hazard : MonoBehaviour
    {
        [SerializeField] float damageAmount = 10f;
        [SerializeField] float atkCooldown = 1f;

        private List<Units.UnitStatDisplay> unitsInCollider = new List<Units.UnitStatDisplay>();
        private float cooldownTimer = 0f;

        private void Update(){
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                DealDamageToUnits();
                cooldownTimer = atkCooldown;
            }
        }

        private void OnTriggerEnter2D(Collider2D other){
            Units.UnitStatDisplay unitStatDisplay = other.GetComponentInChildren<Units.UnitStatDisplay>();

            if (unitStatDisplay != null)
            {
                unitsInCollider.Add(unitStatDisplay);
            }
        }

        private void OnTriggerExit2D(Collider2D other){
            Units.UnitStatDisplay unitStatDisplay = other.GetComponentInChildren<Units.UnitStatDisplay>();

            if (unitStatDisplay != null)
            {
                unitsInCollider.Remove(unitStatDisplay);
            }
        }

        private void DealDamageToUnits()
        {
            foreach (Units.UnitStatDisplay unitStatDisplay in unitsInCollider)
            {
                unitStatDisplay.TakeDamage(damageAmount);
            }
        }
    }
}
