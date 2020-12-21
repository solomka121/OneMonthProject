using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetActive : MonoBehaviour
{
    [SerializeField] private float _doorSpeed;

    [SerializeField] private GameObject _UpBorder;
    [SerializeField] private Color _openedColor;
    [SerializeField] private Color _blockedColor;
    [SerializeField] private bool _isOpen = true;

    private void Start()
    {
        
    }

    public void CloseDoor()
    {
        if (_isOpen)
        {
            LeanTween.moveLocalY(gameObject, gameObject.transform.position.y + gameObject.transform.localScale.y, _doorSpeed);
            _isOpen = false;
            LeanTween.color(_UpBorder , _blockedColor , _doorSpeed / 2);
        }
    }

    public void OpenDoor()
    {
        if (!_isOpen)
        {
            LeanTween.moveLocalY(gameObject, gameObject.transform.position.y - gameObject.transform.localScale.y, _doorSpeed);
            _isOpen = true;
            LeanTween.color(_UpBorder, _openedColor , _doorSpeed / 2);
        }
    }

    public float GetSpeed()
    {
        return _doorSpeed;
    }
}
