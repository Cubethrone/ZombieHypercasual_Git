using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovementInPlayer : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform trackPoint1;
    public Transform trackPoint2;

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(trackPoint1.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
