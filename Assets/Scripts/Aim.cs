using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    //touch ke liye 
    private Touch touch;
    public float moveSpeed = 0.25f;

    //gun object and to roatate it with the aim point    
    public Transform LaserTarget;
    public GameObject Gun;
    public UI_Input input;
    public Vector3 shootPosition;
    public GameObject BulletPrefab, bulletPosition;
    public LayerMask zombie, survivor;

    //to include the delay while shooting 
    public float defaultTime = 0;
    public float delayTimeShoot = 0.25f;
    public Animator Pistol;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Screen Width : " + Screen.width);
        Debug.Log("Screen Width : " + Screen.height);
    }
    void Awake()
    {
        input = new UI_Input();
    }
    // Update is called once per frame
    void Update()
    {
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
            var ray = Camera.main.ScreenPointToRay(LaserTarget.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f, zombie))
            {
                defaultTime = defaultTime + Time.deltaTime;

                if (defaultTime >= delayTimeShoot)
                {
                    shootBullet();
                    defaultTime = 0;
                }
            }
            if (Physics.Raycast(ray, out hit, 1000f, survivor))
            {
                shootBullet();
                Debug.Log("You Killed A survivor");
            }
            Vector3 GunpointPosFar = new Vector3(LaserTarget.position.x, LaserTarget.position.y, Camera.main.farClipPlane);
            Vector3 GunpointPosNear = new Vector3(LaserTarget.position.x, LaserTarget.position.y, Camera.main.nearClipPlane);
            Vector3 gunPointF = Camera.main.ScreenToWorldPoint(GunpointPosFar);
            Vector3 gunPointN = Camera.main.ScreenToWorldPoint(GunpointPosNear);
            Debug.DrawRay(gunPointN, gunPointF - gunPointN, Color.black);
            shootPosition = gunPointF - gunPointN;
            Gun.transform.LookAt(shootPosition);
        }
        //clamping system
        Vector3 clampedPosition = LaserTarget.transform.localPosition;
        // Now we can manipulte it to clamp the y and X element
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -Screen.height / 2, Screen.height / 2);
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -Screen.width / 2, Screen.width / 2);
        // re-assigning the transform's position will clamp it
        LaserTarget.transform.localPosition = clampedPosition;


    }
    public void shootBullet()
    {
        {
            Pistol.SetTrigger("Fire");
            Invoke("InstanciateBullet", 0.15f);
        }
    }
    public void InstanciateBullet()
    {
        GameObject projectile = Instantiate(BulletPrefab, bulletPosition.transform.position, Quaternion.identity);
        projectile.transform.position = bulletPosition.transform.position;
        Vector3 rotation = projectile.transform.rotation.eulerAngles;
        projectile.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        projectile.GetComponent<Rigidbody>().AddForce(shootPosition * 50 * Time.deltaTime, ForceMode.Impulse);
    }
    private void OnEnable()
    {
        input.Enable();

    }
    private void OnDisable()
    {
        input.Disable();
    }
}
