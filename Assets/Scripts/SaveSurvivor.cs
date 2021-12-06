using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SaveSurvivor : MonoBehaviour
{
    public LayerMask SurviverLayer;
    public bool targetInViewRadius;
    public float viewRadius;
    public GameObject player;
    public Animator animator;
    public NavMeshAgent agent;
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("findTargetWithDelay", 0.2f);
    }
    IEnumerator findTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }
    void FindVisibleTarget()
    {
        targetInViewRadius = Physics.CheckSphere(transform.position, viewRadius, SurviverLayer);

        if (targetInViewRadius)
        {
            agent.SetDestination(player.transform.position);
            animator.SetFloat("Run", agent.velocity.magnitude);
        }


    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}