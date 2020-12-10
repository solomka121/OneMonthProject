using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Enemy en;
        if (en = other.GetComponent<Enemy>())
        {
            if (en != null)
            {
                en.GetDamage(1000);
            }
        }
    }
}
