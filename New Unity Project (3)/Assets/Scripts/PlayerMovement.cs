using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public ParticleSystem footSteps;
    private ParticleSystem.EmissionModule footStepsEmission;

    [SerializeField] private float speed;

    private Animator _anim;
    private Rigidbody RB;
    private Vector3 movementVec;
    private float _movement;
    float facing;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();
        footStepsEmission = footSteps.emission;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        movementVec = new Vector3(horizontal, 0 , vertical);

        _movement = movementVec.normalized.magnitude;

        if (_movement > 0.1f)
        {
            footStepsEmission.enabled = true;
        }
        else
        {
            footStepsEmission.enabled = false;
        }

        float facing = 0;
        // is Lerp good here ? meh...
        facing = Mathf.Lerp( facing , Vector3.Angle(transform.forward, movementVec) , 0.01f);
        facing = Mathf.Clamp(facing , 1 , 2);

        /*print(facing);
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
        Debug.DrawRay(transform.position, movementVec.normalized * 2, Color.blue);
*/


        //transform.Translate(vertical , 0 , horizontal);
        RB.velocity = movementVec.normalized * (speed / facing );
        _anim.SetFloat("Speed", _movement * (speed / facing));
        //RB.MovePosition(RB.position + movementVec.normalized * (speed / facing) * Time.deltaTime);
    }

    public void AddSpeed(float ammount)
    {
        if (speed + ammount <= 8)
        {
            speed += ammount;
        }
    }
    private void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(movementVec, facing);
    }

}
