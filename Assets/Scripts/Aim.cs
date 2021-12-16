using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Aim : MonoBehaviour
{
    public int maxAmmo = 12, currentAmmo = -1;
    public TextMeshProUGUI Ammo_text;
    //touch ke liye 
    private Touch touch;
    public float moveSpeed = 0.25f;
    //gun object and to roatate it with the aim point    
    public Transform LaserTarget;
    public GameObject Gun;

    public Vector3 shootPosition;
    public LayerMask zombie, survivor;

    //to include the delay while shooting 
    public float FireRate = 15f;
    public float NextTimeToFire = 0f;
    public Animator Pistol;
    public ParticleSystem blood;
    void Start()
    {
        currentAmmo = maxAmmo;
    }
    void Awake()
    {

    }
    void Update()
    {
        UpdateUI();

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                LaserTarget.transform.position = new Vector3(
                LaserTarget.transform.position.x + touch.deltaPosition.x * moveSpeed,
                LaserTarget.transform.position.y + touch.deltaPosition.y * moveSpeed,
                LaserTarget.transform.position.z + touch.deltaPosition.y * moveSpeed);
            }
            Vector3 GunpointPosFar = new Vector3(LaserTarget.position.x, LaserTarget.position.y, Camera.main.farClipPlane);
            Vector3 GunpointPosNear = new Vector3(LaserTarget.position.x, LaserTarget.position.y, Camera.main.nearClipPlane);
            Vector3 gunPointF = Camera.main.ScreenToWorldPoint(GunpointPosFar);
            Vector3 gunPointN = Camera.main.ScreenToWorldPoint(GunpointPosNear);
            Debug.DrawRay(gunPointN, gunPointF - gunPointN, Color.black);
            shootPosition = gunPointF - gunPointN;
            Gun.transform.LookAt(shootPosition);
            if (Time.time >= NextTimeToFire && currentAmmo != 0)
            {
                var ray = Camera.main.ScreenPointToRay(LaserTarget.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.GetComponent<Limb>())
                    {
                        Limb limb = hit.transform.GetComponent<Limb>();
                        var ParticleSystem = Instantiate(blood, hit.point, Quaternion.identity);
                        Destroy(ParticleSystem.gameObject, 4);
                        currentAmmo--;
                        limb.getHit();
                        
                        hit.collider.gameObject.transform.root.GetComponent<CapsuleCollider>().enabled = false;
                        if(hit.rigidbody!=null)
                        {
                            hit.rigidbody.AddForce(-hit.normal*6000);
                        }
                        
                    }
                }
                if (Physics.Raycast(ray, out hit, 1000f, zombie))
                {
                    currentAmmo--;
                    
                    NextTimeToFire = Time.time + 1f / FireRate;
                    var ParticleSystem = Instantiate(blood, hit.point, Quaternion.identity);
                    Destroy(ParticleSystem.gameObject, 5);

                }
                if (Physics.Raycast(ray, out hit, 1000f, survivor))
                {
                    currentAmmo--;
                    NextTimeToFire = Time.time + 1f / FireRate;
                    Debug.Log("You Killed A survivor");
                }
            }

        }
        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }
        //clamping system
        Vector3 clampedPosition = LaserTarget.transform.localPosition;
        // Now we can manipulte it to clamp the y and X element
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -Screen.height / 2, Screen.height / 2);
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -Screen.width / 2, Screen.width / 2);
        // re-assigning the transform's position will clamp it
        LaserTarget.transform.localPosition = clampedPosition;
    }
    void Reload()
    {
        Pistol.SetTrigger("Reload");
        Invoke("bulletSetup", 1f);
    }
    void bulletSetup()
    {
        currentAmmo = maxAmmo;
    }

    void UpdateUI()
    {
        Ammo_text.text = currentAmmo.ToString();
    }
}
