using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform _objectToFollow;
    [SerializeField] private Vector3 _followOffSet;
    private Vector3 _cursorPoint;
    private Vector3 _dir = Vector3.zero;
    private Vector3 _smoothedDir;

    // Start is called before the first frame update
    void Start()
    {
        _followOffSet += transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _cursorPoint = _objectToFollow.GetComponent<PlayerMovement>().GetCursorPoint();

        _dir = Vector3.ClampMagnitude(_cursorPoint.normalized * _cursorPoint.magnitude, 24);
        print(_dir);
        _smoothedDir = Vector3.Lerp(_dir , transform.position , 0.2f);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position , _objectToFollow.position + _followOffSet + _smoothedDir , 0.1f);
        //transform.LookAt(_objectToFollow.position);
        transform.position = smoothedPosition;
    }
}
