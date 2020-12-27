using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{

    Quaternion startRotation;

    // Start is called before the first frame update
    void Awake()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = startRotation;
    }
}
