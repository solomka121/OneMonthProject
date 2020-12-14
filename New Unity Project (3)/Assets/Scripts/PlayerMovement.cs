using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody RB;

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
    }
    private void Update()
    {

    }
    
}
