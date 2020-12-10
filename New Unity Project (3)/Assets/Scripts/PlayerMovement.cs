using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody RB;
    
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform GunBarrel;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float _fireSpeed = 0.4f;
    private float _timeToShoot;

    public Camera mainCamera;
    private Vector3 cursorPoint = new Vector3(0 , 0 , 0);
    private Transform cursorOverObject;

    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movementVec = new Vector3(horizontal , 0 , vertical);

        //gameObject.transform.Translate(movementVec * speed * Time.deltaTime);
        RB.velocity = movementVec * speed;
        //RB.AddForce(movementVec * speed * 100);

        if (Input.GetButton("Fire1"))
        {
            if (Time.time >= _timeToShoot)
            {
                Instantiate(bullet, GunBarrel.position, GunBarrel.rotation);
                _timeToShoot = Time.time + _fireSpeed;
            }
        }

        CursorCheck();
        Aim();
    }
    private void Update()
    {

    }
    private void CursorCheck()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            cursorPoint = hit.point;
        }

        Debug.DrawRay(mainCamera.transform.position, ray.direction * 100, Color.yellow);
    
    }
    private void Aim()
    {
        Vector3 lookAt = cursorPoint;
        lookAt.y = transform.position.y;

        rightHand.LookAt(cursorPoint);
        transform.LookAt(lookAt);


        Debug.DrawRay(GunBarrel.position, GunBarrel.forward * 5); 

        /*RaycastHit hit;
        if (Physics.Raycast(GunBarrel.position , cursorPoint , out hit))
        {
            print("Hit :" + hit.point);
        }*/

        //Debug.DrawRay(GunBarrel.position, cursorPoint , Color.red );
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(cursorPoint , 0.3f);
    }
}
