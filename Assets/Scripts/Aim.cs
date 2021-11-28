using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public GameObject Gun;
    public UI_Input input;
    public Transform LaserTarget;
    public float movementSpeed;
    public int clampScreenSize = 300;
    public Vector3 shootPosition;

    public GameObject BulletPrefab, bulletPosition;
    public LayerMask zombie;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        input = new UI_Input();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(LaserTarget.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f, zombie))
            {
                shootBullet();
            }
            Vector3 GunpointPosFar = new Vector3(LaserTarget.position.x, LaserTarget.position.y, Camera.main.farClipPlane);
            Vector3 GunpointPosNear = new Vector3(LaserTarget.position.x, LaserTarget.position.y, Camera.main.nearClipPlane);
            Vector3 gunPointF = Camera.main.ScreenToWorldPoint(GunpointPosFar);
            Vector3 gunPointN = Camera.main.ScreenToWorldPoint(GunpointPosNear);
            Debug.DrawRay(gunPointN, gunPointF - gunPointN, Color.black);
            shootPosition = gunPointF - gunPointN;
            Gun.transform.LookAt(gunPointF - gunPointN);

        }

        Vector2 inputs = input.Joystick.Aim.ReadValue<Vector2>();
        // Debug.Log(inputs);
        LaserTarget.localPosition = LaserTarget.localPosition + new Vector3(inputs.x * movementSpeed, inputs.y * movementSpeed, 0);




        //clamping system
        Vector3 clampedPosition = LaserTarget.transform.localPosition;
        // Now we can manipulte it to clamp the y and X element
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -clampScreenSize, clampScreenSize);
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -clampScreenSize, clampScreenSize);
        // re-assigning the transform's position will clamp it
        LaserTarget.transform.localPosition = clampedPosition;


    }
    public void shootBullet()
    {
       // if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = Instantiate(BulletPrefab, bulletPosition.transform.position, Quaternion.identity);
            projectile.transform.position = bulletPosition.transform.position;
            Vector3 rotation = projectile.transform.rotation.eulerAngles;
            projectile.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
            projectile.GetComponent<Rigidbody>().AddForce(shootPosition * 50 * Time.deltaTime, ForceMode.Impulse);


        }
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
