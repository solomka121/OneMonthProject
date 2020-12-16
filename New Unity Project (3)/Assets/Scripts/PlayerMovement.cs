using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody RB;

    [SerializeField] private float speed;

    private Vector3 movementVec;
    float facing;

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

        movementVec = new Vector3(horizontal , 0 , vertical);

        //facing = Vector3.Dot(movementVec , transform.forward);


        float facing = 0;
        // is Lerp good here ? meh...
        facing = Mathf.Lerp( facing , Vector3.Angle(transform.forward, movementVec) , 0.01f);
        facing = Mathf.Clamp(facing , 1 , 2);

        /*print(facing);
        Debug.DrawRay(transform.position, transform.forward * 2 , Color.red);
        Debug.DrawRay(transform.position , movementVec.normalized * 2 , Color.blue);*/

        RB.velocity = movementVec.normalized * (speed / facing );
    }
    private void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(movementVec, facing);
    }

}
