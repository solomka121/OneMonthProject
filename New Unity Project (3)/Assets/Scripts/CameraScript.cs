using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera mainCamera;
    static Vector3 _cursorPoint;
    private Transform _cursorOverObject;

    public Transform _objectToFollow;
    private Vector3 _followOffSet;
    private Vector3 _dir;
    private Vector3 _smoothedDir;

    // Start is called before the first frame update
    void Start()
    {
        _followOffSet += transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CursorCheck();

        _cursorPoint.y = _objectToFollow.position.y; // lock position on y (player.y)

        _dir = Vector3.ClampMagnitude((_cursorPoint - _objectToFollow.position).normalized * (_cursorPoint - _objectToFollow.position).magnitude , 28);
        _smoothedDir = Vector3.Lerp(Vector3.zero, _dir, 0.1f);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, _objectToFollow.position + _followOffSet + _smoothedDir, 0.1f);
        //transform.LookAt(_objectToFollow.position);
        transform.position = smoothedPosition;
    }

    private void CursorCheck()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            _cursorPoint = hit.point;
        }

        Debug.DrawRay(mainCamera.transform.position, ray.direction * 100, Color.yellow);

    }

    public Vector3 GetCursorPoint()
    {
        return _cursorPoint;
    }
}
