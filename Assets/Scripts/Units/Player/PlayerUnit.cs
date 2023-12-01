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
        private bool isAttackCommand = false;
        private float distance;
        private float atkCooldown;
        private AudioSource audioSource;

        private void Start(){
            baseStats = unitType.baseStats;
            statDisplay = GetComponentInChildren<UnitStatDisplay>();
            statDisplay.SetStatDisplayStandardUnit(baseStats, true);
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
                if(isAttackCommand){
                    Attack();
                    MoveToAggroTarget();
                }
                else{
                    Attack();
                }
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
                isAttackCommand = false;
            }
            else{
                if(atkCooldown <= 0 && distance <= baseStats.unitAttackRange + 1){
                    if(aggroUnit != null){
                        aggroUnit.TakeDamage(baseStats.unitAttack);
                        audioSource.Play();
                    }
                    
                    atkCooldown = baseStats.unitAttackSpeed;
                }
            }
        }

        public void MoveUnit(Vector2 destination){
            if(isAttackCommand){
                isAttackCommand = false;
                agent.stoppingDistance = 0;
            }
            if (agent == null){
                Movement.SetAgentProperties(gameObject, false);
            }

            Movement.MoveToDestination(gameObject, agent, destination);
        }

        public void AttackSpecificTarget(Transform target)
        {
            aggroTarget = target;
            aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
            hasAggro = true;
            isAttackCommand = true;
        }

        private void MoveToAggroTarget()
        {
            if (aggroTarget == null)
            {
                agent.stoppingDistance = 0;
                Movement.MoveToDestination(gameObject, agent, transform.position);
                hasAggro = false;
                isAttackCommand = false;
            }
            else
            {
                distance = Vector2.Distance(aggroTarget.position, transform.position);
                agent.stoppingDistance = (baseStats.unitAttackRange + 1);

                if (distance <= baseStats.unitAggroRange)
                {
                    Movement.MoveToDestination(gameObject, agent, aggroTarget.position);
                }
                else
                {
                    hasAggro = false;
                    isAttackCommand = false;
                }
            }
        }
    }
}

