using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTab : MonoBehaviour
{
    public GameObject menu;
    public GameObject UpgradeEffect;
    private Vector3 _menuScale;
    private bool InTrigger;
    private bool tryBuy;

    // Start is called before thes first frame update
    void Start()
    {
        _menuScale = menu.transform.localScale;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && InTrigger)
        {
            InTrigger = false;
            tryBuy = true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            InTrigger = true;
            if (tryBuy)
            {
                tryBuy = false;
                if (other.GetComponent<PlayerCombat>().TryBuy(8))
                {
                    other.GetComponent<PlayerCombat>().AddSpeed(0.3f);
                    other.GetComponent<PlayerMovement>().AddSpeed(0.5f);
                    Instantiate(UpgradeEffect, other.transform.position, UpgradeEffect.transform.rotation);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InTrigger = false;
            tryBuy = false;

            menu.transform.localScale = Vector3.zero;
            menu.SetActive(true);
            LeanTween.scale(menu, _menuScale, 0.2f).setEaseOutExpo();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            LeanTween.scale(menu, Vector3.zero, 0.2f).setEaseOutExpo().setOnComplete(DisableMenu);
        }
    }
    private void DisableMenu()
    {
        menu.SetActive(false);
    }
}
