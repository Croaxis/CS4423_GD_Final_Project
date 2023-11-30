using UnityEngine;
using UnityEngine.AI;

namespace cs4423fp.Move
{
    public static class Movement
    {
        public static void SetAgentProperties(GameObject gameObject, bool isPlayer)
        {
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.updateRotation = false;
                agent.updateUpAxis = false;
            }
        }

        public static void MoveToDestination(GameObject gameObject, NavMeshAgent agent, Vector3 destination)
        {
            if (agent != null)
            {
                agent.SetDestination(destination);

                Vector3 direction = (destination - agent.transform.position).normalized;
                float angleRadians = Mathf.Atan2(direction.y, direction.x);
                float angleDegrees = angleRadians * Mathf.Rad2Deg;
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angleDegrees - 90);
            }
        }
    }
}