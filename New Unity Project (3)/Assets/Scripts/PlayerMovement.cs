using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody RB;

    private float SpawnTime = 0.5f;
    private float TimeToSpawn;

    public GameObject gg;
    public GameObject spawnEffect;

    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Invoke("mg" , 3);
         
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horszontal");

        Vector3 movementVec = new Vector3(horizontal , 0 , vertical);

        //gameObject.transform.Translate(movementVec * speed * Time.deltaTime);
        //RB.velocity = movementVec * speed;
        RB.AddForce(movementVec * speed * 100);

        if (TimeToSpawn <= 0)
        {
            GameObject box = Instantiate(gg , transform.position + new Vector3(Random.Range(1 , 8) , 20, Random.Range(1, 8)) , Quaternion.identity);
            Instantiate(spawnEffect, box.transform.position, Quaternion.Euler(new Vector3(-90 , 0 , 0)));
            TimeToSpawn = SpawnTime;

            Instantiate(box , transform.position , Quaternion.identity);
        }
        else
        {
            TimeToSpawn -= Time.deltaTime;
        }
    }
    public void mg()
    {
        print("hh");
    }
}
