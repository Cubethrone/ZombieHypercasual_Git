using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NewShoot : MonoBehaviour
{
    public int maxAmmo = 10, currentAmmo = -1;
    private bool isReloading;
    public Transform Gun;
    private Touch touch;
    [SerializeField]
    private float moveSpeed = 1f, reloadTime = 1f;
    //gun object and to roatate it with the aim point    
    public Transform LaserTarget;
    public ParticleSystem blood, hitmisk;
    public GameObject head, lower, upper;
    public LayerMask zombie, Deadzombie, survivor, anything, Head, LowerLeg_Hand, UpperLeg_Hand;
    public Animator gun;
    public TextMeshProUGUI Ammo_text;
    void Start()
    {
        currentAmmo = maxAmmo;
    }
    void Update()
    {
        UpdateUI();
        // if (currentAmmo <= 0)
        // {
        //     StartCoroutine(Reload());
        //     return;
        // }
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
            if (isReloading)
            {

                return;
            }
            if (Physics.Raycast(ray, out hit, 1000f, zombie))
            {

                hit.collider.gameObject.layer = LayerMask.NameToLayer("DeadZombie");
                var ParticleSystem = Instantiate(blood, hit.point, Quaternion.identity);

                Destroy(ParticleSystem.gameObject, 5);
                Destroy(hit.collider.gameObject, 10);
                hit.collider.gameObject.GetComponent<Animator>().enabled = false;
                hit.collider.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            if (Physics.Raycast(ray, out hit, 1000f, Deadzombie))
            {
                hit.collider.gameObject.transform.localScale = new Vector3(0, 0, 0);
                currentAmmo--;
                var ParticleSystem = Instantiate(blood, hit.point, Quaternion.identity);
                Destroy(ParticleSystem.gameObject, 3);
            }
            if (Physics.Raycast(ray, out hit, 1000f, anything))
            {
                currentAmmo--;
                var ParticleSystem = Instantiate(hitmisk, hit.point, Quaternion.identity);
                Destroy(ParticleSystem.gameObject, 3);
            }
            if (Physics.Raycast(ray, out hit, 1000f, Head))
            {
                currentAmmo--;
                var ParticleSystem = Instantiate(blood, hit.point, Quaternion.identity);
                Instantiate(head, hit.point, Quaternion.identity);
                Destroy(ParticleSystem.gameObject, 3);
                return;
            }
            if (Physics.Raycast(ray, out hit, 1000f, LowerLeg_Hand))
            {
                currentAmmo--;
                var ParticleSystem = Instantiate(blood, hit.point, Quaternion.identity);
                Instantiate(lower, hit.point, Quaternion.identity);
                Destroy(ParticleSystem.gameObject, 3);
                return;
            }
            if (Physics.Raycast(ray, out hit, 1000f, UpperLeg_Hand))
            {
                currentAmmo--;
                var ParticleSystem = Instantiate(blood, hit.point, Quaternion.identity);
                Instantiate(upper, hit.point, Quaternion.identity);
                Destroy(ParticleSystem.gameObject, 3);
                return;
            }
            clampAim();
        }
        RotateGunWithAim();
    }
    void RotateGunWithAim()
    {
        {   //for rotating the gun with the aim
            Vector3 GunpointPosFar = new Vector3(LaserTarget.position.x, LaserTarget.position.y, Camera.main.farClipPlane);
            Vector3 GunpointPosNear = new Vector3(LaserTarget.position.x, LaserTarget.position.y, Camera.main.nearClipPlane);
            Vector3 gunPointF = Camera.main.ScreenToWorldPoint(GunpointPosFar);
            Vector3 gunPointN = Camera.main.ScreenToWorldPoint(GunpointPosNear);
            Debug.DrawRay(gunPointN, gunPointF - gunPointN, Color.black);
            var shootPosition = gunPointF - gunPointN;
            Gun.transform.LookAt(shootPosition);
        }
    }
    void clampAim()
    {
        {   //for clamping
            Vector3 clampedPosition = LaserTarget.transform.localPosition;
            // Now we can manipulte it to clamp the y and X element
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -Screen.height / 2, Screen.height / 2);
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -Screen.width / 2, Screen.width / 2);
            // re-assigning the transform's position will clamp it
            LaserTarget.transform.localPosition = clampedPosition;
        }
    }
    void UpdateUI()
    {
        Ammo_text.text = currentAmmo.ToString();
    }



}
