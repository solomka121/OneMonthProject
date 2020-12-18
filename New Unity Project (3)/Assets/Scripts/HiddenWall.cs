using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWall : MonoBehaviour
{
    [SerializeField] private GameObject _roof;
    private MeshRenderer _roof_MR;
    private Color _startColor;
    private Color _smoothedColor;

    private bool _isOpen;

    // Start is called before the first frame update
    void Start()
    {
       _roof_MR = _roof.GetComponent<MeshRenderer>();
        _startColor = _roof_MR.material.color;
        _smoothedColor = _startColor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isOpen)
        {
            _smoothedColor.a = Mathf.Lerp(_smoothedColor.a, 1, 0.02f);
            _roof_MR.material.color = _smoothedColor;   
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _smoothedColor.a = Mathf.Lerp(_smoothedColor.a , 0.4f , 0.02f);
        _roof_MR.material.color = _smoothedColor;
        _isOpen = true;
    }
    private void OnTriggerExit(Collider other) 
    {
        float alpha = _roof_MR.material.color.a;
        alpha += Time.deltaTime;
        _isOpen = false;
    }
}
