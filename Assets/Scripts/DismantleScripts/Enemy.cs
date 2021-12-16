using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator myAnim;
    List<Rigidbody> ragdollRigids;
    // Start is called before the first frame update
    void Start()
    {
        myAnim=GetComponent<Animator>();
        ragdollRigids=new List<Rigidbody>(transform.GetComponentsInChildren<Rigidbody>());
        ragdollRigids.Remove(GetComponent<Rigidbody>());
        DeActivateRagdoll();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ActivateRagdoll()
    {
        myAnim.enabled = false;
        for (int i = 0; i < ragdollRigids.Count; i++)
        {
            ragdollRigids[i].useGravity = true;
            ragdollRigids[i].isKinematic = false;
        }
    }
    void DeActivateRagdoll()
    {
        myAnim.enabled = true;
        for (int i = 0; i < ragdollRigids.Count; i++)
        {
            ragdollRigids[i].useGravity = false;
            ragdollRigids[i].isKinematic = true;
        }
    }
    public void GetKilled()
    {
        ActivateRagdoll();
    }
}
