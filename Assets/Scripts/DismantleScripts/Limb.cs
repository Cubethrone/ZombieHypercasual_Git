using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    public Enemy enemyScript;
    [SerializeField] Limb[] childLimbs;
    [SerializeField] GameObject limbPrefab, woundhole;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = transform.root.GetComponent<Enemy>();
        if (woundhole != null)
        {
            woundhole.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void getHit()
    {
        if (childLimbs.Length > 0)
        {
            foreach (Limb limb in childLimbs)
            {
                if (limb != null)
                {
                    limb.getHit();
                }
            }
        }
        if (woundhole != null)
        {
            woundhole.SetActive(true);
        }
        if (limbPrefab != null)
        {
            Instantiate(limbPrefab, transform.position, transform.rotation);
        }
        transform.localScale = Vector3.zero;
        enemyScript.GetKilled();
        Destroy(this);
    }
}
