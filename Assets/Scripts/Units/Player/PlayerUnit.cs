using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using cs4423fp.Move;

namespace cs4423fp.Units.Player
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class PlayerUnit : MonoBehaviour
    {
        private NavMeshAgent agent;

        public StandardUnit unitType;

        [HideInInspector]
        public UnitStatTypes.Base baseStats;

        public UnitStatDisplay statDisplay;
        private Collider2D[] rangeColliders;
        private Transform aggroTarget;
        private UnitStatDisplay aggroUnit;
        private bool hasAggro = false;
        private float distance;
        private float atkCooldown;
        private AudioSource audioSource;

        private void Start(){
            baseStats = unitType.baseStats;
            statDisplay = GetComponentInChildren<UnitStatDisplay>();
            statDisplay.SetStatDisplayStandardUnit(baseStats, false);
            agent = GetComponent<NavMeshAgent>();
            Movement.SetAgentProperties(gameObject, false);
            audioSource = GetComponent<AudioSource>();
        }

        private void Update(){
            atkCooldown -= Time.deltaTime;

            if (!hasAggro){
                CheckForEnemyTargets();
            }
            else{
                Attack();
                //MoveToAggroTarget();
            }
        }

        private void CheckForEnemyTargets(){
            rangeColliders = Physics2D.OverlapCircleAll(transform.position, baseStats.unitAggroRange, UnitHandler.Instance.eUnitLayer);

            for (int i = 0; i < rangeColliders.Length;)
            {
                aggroTarget = rangeColliders[i].gameObject.transform;
                aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                hasAggro = true;
                break;
            }
        }

        private void Attack(){
            if( aggroTarget == null){
                hasAggro = false;
            }
            else{
                if(atkCooldown <= 0 && distance <= baseStats.unitAttackRange + 1){
                    aggroUnit.TakeDamage(baseStats.unitAttack);
                    atkCooldown = baseStats.unitAttackSpeed;
                    audioSource.Play();
                }
            }
        }

        public void MoveUnit(Vector2 destination){
            if (agent == null){
                Movement.SetAgentProperties(gameObject, false);
            }

            Movement.MoveToDestination(gameObject, agent, destination);
        }
    }
}

