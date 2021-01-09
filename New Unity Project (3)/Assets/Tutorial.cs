using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutor;
    private Vector3 _tutorScale;
    void Start()
    {
        _tutorScale = tutor.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutor.transform.localScale = Vector3.zero;
            tutor.SetActive(true);
            LeanTween.scale(tutor, _tutorScale, 0.5f).setEaseOutExpo();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            LeanTween.scale(tutor, Vector3.zero, 0.5f).setEaseOutExpo().setOnComplete(DisableTutor);
        }
    }
    private void DisableTutor()
    {
        tutor.SetActive(false);
    }
}
