using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f; 
    [SerializeField] float turnSpeed = 5f;

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

    private void FaceTarget() { // could use transform.LookAt() but it is here in case we need faceTarget over time or smt like this
        Vector3 direction = (target.position - transform.position).normalized; // .normalized gets just the direction of the vector (it just looks to the target)
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // gets the rotation from the line above (just x and z axis)
        // slerp let us rotate smoothly between 2 vectors
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed); // current rotation, our look rotation and the speed we want to change the rotation
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void EngageTarget() {
        
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance) {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance) {
            //transform.LookAt(target); kind of bugged, it also considers the z.axis
            AttackTarget();
        }
    }

    void ChaseTarget() {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget() {
        GetComponent<Animator>().SetBool("attack", true);
        Debug.Log(name + " is attacking " + target.name);
    }
}
