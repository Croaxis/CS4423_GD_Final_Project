using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using cs4423fp.Move;

namespace cs4423fp.Units.Enemy
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
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
            statDisplay.SetStatDisplayStandardUnit(unitType.unitName, baseStats, false);
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
                MoveToAggroTarget();
            }
        }

        private void CheckForEnemyTargets(){
            rangeColliders = Physics2D.OverlapCircleAll(transform.position, baseStats.unitAggroRange, UnitHandler.Instance.pUnitLayer);

            for (int i = 0; i < rangeColliders.Length;)
            {
                aggroTarget = rangeColliders[i].gameObject.transform;
                aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                hasAggro = true;
                break;
            }
        }

        private void Attack(){
            if(atkCooldown <= 0 && distance <= baseStats.unitAttackRange + 1){
                aggroUnit.TakeDamage(baseStats.unitAttack);
                atkCooldown = baseStats.unitAttackSpeed;
                audioSource.Play();
            }
        }

        public void MoveToAggroTarget(){
            if( aggroTarget == null){
                Movement.MoveToDestination(gameObject, agent, transform.position);
                hasAggro = false;
            }
            else{
                distance = Vector2.Distance(aggroTarget.position, transform.position);
                agent.stoppingDistance = (baseStats.unitAttackRange + 1);

                if (distance <= baseStats.unitAggroRange){
                    Movement.MoveToDestination(gameObject, agent, aggroTarget.position);
                }
            }
        }
    }
}