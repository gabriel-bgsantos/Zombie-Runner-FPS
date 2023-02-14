using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f; 

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked) {
            EngageTarget(); // chase target
        }
        else if (distanceToTarget <= chaseRange) {
            isProvoked = true;
            navMeshAgent.SetDestination(target.position);
        }
        else{
            return;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void EngageTarget() {
        if (distanceToTarget >= navMeshAgent.stoppingDistance) {
            GetComponent<Animator>().SetBool("attack", false);
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance) {
            GetComponent<Animator>().SetBool("attack", true);
            AttackTarget();
        }
    }

    void ChaseTarget() {
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget() {
        Debug.Log(name + " is attacking " + target.name);
    }
}
