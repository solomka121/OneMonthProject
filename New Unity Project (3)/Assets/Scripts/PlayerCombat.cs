﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform rightHand;

    [Header("Gun Stats")]

    [SerializeField] private Transform GunBarrel;
    [SerializeField] private GameObject bullet;

    [SerializeField] private int _bulletDamage;
    [SerializeField] private int _bulletSpeed;

    [SerializeField] private float _fireSpeed = 0.4f;
    private float _timeToShoot;

    [SerializeField] private float _reloadTime;
    private float _timeToReload;
    private float _imageFill;
    private bool _isReloading;

    [SerializeField] private int _Mags;
    [SerializeField] private int _maxBulletsInMag;
    private int _bulletsLeftInMag;

    [Header("Melee")]

    [SerializeField] private string meleeAttackKeyTrigger;
    [SerializeField] private Transform meleeAttackPoint;
    [SerializeField] private float meleeAttackRadius;
    [SerializeField] private int meleeAttackDamage;
    [SerializeField] private float meleeAttackStunChance;
    [SerializeField] LayerMask meleeAttackEnemyMask;
    

    [Header("Gun UI")]
    [SerializeField] private Image _reloadImgUI;
    [SerializeField] private TextMeshProUGUI _MagsUI;
    [SerializeField] private TextMeshProUGUI _maxBulletsInMagUI;
    [SerializeField] private TextMeshProUGUI _bulletsLeftInMagUI;

    private Animator _anim;
    private Rigidbody _RB;
    private CameraScript _camData;
    private Vector3 cursorPoint;
    private Vector3 smoothedLookAt;

    void Awake()
    {
        smoothedLookAt = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {

        // any other way except public GameObj camera ?
        _camData = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        _RB = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        _bulletsLeftInMag = _maxBulletsInMag;

        _isReloading = false;

        _reloadImgUI.enabled = false;

        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        cursorPoint = _camData.GetCursorPoint();

        if (Input.GetButton("Fire1"))
        {
            if (_timeToShoot <= Time.time && _bulletsLeftInMag > 0 && !_isReloading)
            {
                Shoot();
                _timeToShoot = Time.time + _fireSpeed;
            }
        }

        if (Input.GetKeyDown(meleeAttackKeyTrigger))
        {
            _anim.SetTrigger("MeleeAttack");
        }

        if (Input.GetKeyDown("r") && _Mags > 0)
        {
            if (_timeToReload <= 0)
            {
                StartCoroutine(TryReload());

                _isReloading = true;

                _imageFill = 0;
                _timeToReload = _reloadTime;
                _reloadImgUI.enabled = true;
            }
        }
        else if(_isReloading)
        {
            _reloadImgUI.fillAmount = _imageFill;
            _imageFill += Time.deltaTime / _reloadTime;
            _timeToReload -= Time.deltaTime;
        }

        if (_bulletsLeftInMag == 0 && _Mags > 0 && !_isReloading)
        {
            StartCoroutine(TryReload());

            _isReloading = true;

            _imageFill = 0;
            _timeToReload = _reloadTime;
            _reloadImgUI.enabled = true;
        }

        Aim();
    }

    private void Shoot()
    {
        _anim.SetTrigger("Shoot");

        BulletScript bull = Instantiate(bullet, GunBarrel.position, GunBarrel.rotation).GetComponent<BulletScript>();
        bull.Init(_bulletDamage , _bulletSpeed);
        _bulletsLeftInMag--;

         _bulletsLeftInMagUI.text = _bulletsLeftInMag.ToString();
    }

    private void MeleeAttack()
    {
        Collider[] cols = Physics.OverlapSphere(meleeAttackPoint.position , meleeAttackRadius , meleeAttackEnemyMask);
        foreach (Collider col in cols)
        {
            EnemyHealth EH;
            if (EH = col.GetComponent<EnemyHealth>())
            {
                Vector3 hitPoint = col.ClosestPoint(meleeAttackPoint.position);
                EH.GetDamage( meleeAttackDamage , hitPoint);
            }
        }
    }

    private void Aim()
    {
        Vector3 lookAt = cursorPoint;
        lookAt.y = transform.position.y;

        //lookAt.Normalize();
        //print(Vector3.Angle(lookAt, smoothedLookAt));

        //smoothedLookAt = Vector3.Lerp(smoothedLookAt, lookAt , 0.01f * (Vector3.Angle(lookAt, smoothedLookAt) / 3));

        rightHand.LookAt(lookAt);
        transform.LookAt(lookAt);

        Debug.DrawRay(GunBarrel.position, GunBarrel.forward * 5);

        /*RaycastHit hit;
        if (Physics.Raycast(GunBarrel.position , cursorPoint , out hit))
        {
            print("Hit :" + hit.point);
        }*/

        //Debug.DrawRay(GunBarrel.forward, cursorPoint , Color.red );
    }
    private IEnumerator TryReload()
    {
        yield return new WaitForSeconds(_reloadTime);
        Reload();
    }
    private void Reload()
    {
        _bulletsLeftInMag = _maxBulletsInMag;
        _Mags--;

        _timeToReload = 0;

        _isReloading = false;

        _reloadImgUI.enabled = false;
        _reloadImgUI.fillAmount = 0;

        _MagsUI.text = _Mags.ToString();
        _bulletsLeftInMagUI.text = _bulletsLeftInMag.ToString();
    }
    public void GetAmmo(int mags)
    {
        _Mags += mags;
        _MagsUI.text = _Mags.ToString();
    }

    private void UpdateAmmoUI()
    {
        _MagsUI.text = _Mags.ToString();
        _maxBulletsInMagUI.text = _maxBulletsInMag.ToString();
        _bulletsLeftInMagUI.text = _bulletsLeftInMag.ToString();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleeAttackPoint.position , meleeAttackRadius);
    }
}
