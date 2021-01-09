using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int _magsRecover;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCombat PC;
        if (PC = other.GetComponent<PlayerCombat>())
        {
            PC.GetAmmo(_magsRecover);
            LeanTween.scale(gameObject , Vector3.zero , 0.2f);
            LeanTween.move(gameObject , other.transform.position , 0.2f).setOnComplete(AmmoTaken);
            //Destroy(gameObject);
        }
    }

    private void AmmoTaken()
    {

        Destroy(gameObject);
    }
}
