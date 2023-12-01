using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cs4423fp.Units
{
    public class UnitStatDisplay : MonoBehaviour
    {
        public float maxHealth, armor, currentHealth;
        [SerializeField] private Image healthBarAmount;
        private bool isPlayerUnit = false;
        private AudioSource audioSource;

        private void Start(){
            audioSource = GetComponent<AudioSource>();
        }
        

        public void SetStatDisplayStandardUnit(UnitStatTypes.Base stats, bool isPlayer){
            maxHealth = stats.unitHP;
            armor = stats.unitArmor;
            isPlayerUnit = isPlayer;

            currentHealth = maxHealth;
        }

        public void SetStatDisplayStandardStructure(Structures.StructureStatTypes.Base stats, bool isPlayer){
            maxHealth = stats.structureHP;
            armor = stats.structureArmor;
            isPlayerUnit = isPlayer;

            currentHealth = maxHealth;
        }

        private void Update()
        {
            HandleHealth();
        }

        public void TakeDamage(float damage){
            float totalDamage;

            if(armor != 0f){
                totalDamage = damage / armor;
            }
            else{
                totalDamage = damage;
            }
            
            currentHealth -= totalDamage;

        }

        private void HandleHealth(){
            healthBarAmount.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0){
                HandleDestroy();
            }
        }

        private void HandleDestroy()
        {
            if (isPlayerUnit)
            {
                InputController.InputHandler.Instance.selectedUnits.Remove(gameObject.transform.parent);
            }

            GameObject audioObject = new GameObject("AudioObject");
            AudioSource audioSourceClone = audioObject.AddComponent<AudioSource>();

            audioSourceClone.clip = audioSource.clip;
            audioSourceClone.volume = audioSource.volume;
            audioSourceClone.pitch = audioSource.pitch;
            audioSourceClone.Play();

            Destroy(audioObject, audioSource.clip.length);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}