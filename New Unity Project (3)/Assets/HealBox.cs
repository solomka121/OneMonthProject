using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBox : MonoBehaviour
{
    [SerializeField] private int _healAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth PH;
        if (PH = other.GetComponent<PlayerHealth>())
        {
            PH.GetHeal(_healAmount);
            Destroy(gameObject);
        }
    }
}
