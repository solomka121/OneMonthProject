using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth en;
        if (en = other.GetComponent<EnemyHealth>())
        {
            if (en != null)
            {
                //en.GetDamage(1000);
            }
        }
    }
}
