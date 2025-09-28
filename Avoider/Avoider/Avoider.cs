using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Profiling;

namespace Avoider
{
    public class Avoider : MonoBehaviour
    {
        public GameObject objectToAvoid;
        public float avoidanceRange = 5f;
        public float avoidanceSpeed = 2f;
        public bool showAvoidanceGizmos = true;
        public NavMeshAgent navMeshAgent;

        void Update()
        {
            if (navMeshAgent == null || objectToAvoid == null)
            {
                return;
            }
            float distanceToAvoid = Vector3.Distance(transform.position, objectToAvoid.transform.position);
            if (distanceToAvoid < avoidanceRange)
            {
                Vector3 avoidDirection = (transform.position - objectToAvoid.transform.position).normalized;
                Vector3 targetPosition = transform.position + avoidDirection * avoidanceRange;
                navMeshAgent.speed = avoidanceSpeed;
                navMeshAgent.SetDestination(targetPosition);
            }
            else
            {
                if (navMeshAgent.hasPath && navMeshAgent.remainingDistance < 0.1f)
                {
                    navMeshAgent.ResetPath();
                }
            }
        }

        void OnDrawGizmos()
        {
            if (showAvoidanceGizmos)
            {

                Gizmos.DrawWireSphere(transform.position, avoidanceRange);

                Gizmos.DrawLine(transform.position, objectToAvoid.transform.position);
            }
        }
    }
}
