using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform rightHand;

    [Header("Gun")]

    [SerializeField] private Transform GunBarrel;
    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem MuzzleFlash;

    [SerializeField] private int _bulletDamage;
    [SerializeField] private int _bulletSpeed;
    [SerializeField] private float _stunChance;
    [SerializeField] private float _stunDuration;

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

    [SerializeField] private string _meleeAttackKeyTrigger;
    [SerializeField] private Transform _meleeAttackPoint;
    [SerializeField] private float _meleeAttackRadius;
    [SerializeField] private int _meleeAttackDamage;
    [SerializeField] private float _meleeAttackStunChance;
    [SerializeField] private float _meleeAttackStunDuration;
    [SerializeField] LayerMask _meleeAttackEnemyMask;
    

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
            if (_timeToShoot <= Time.time && _bulletsLeftInMag > 0 && !_isReloading && Time.timeScale != 0)
            {
                Shoot();
                _timeToShoot = Time.time + _fireSpeed;
            }
        }

        if (Input.GetKeyDown(_meleeAttackKeyTrigger))
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
        Instantiate(MuzzleFlash , GunBarrel.position , GunBarrel.rotation);

        if (_stunChance <= 0)
        {
            bull.Init(_bulletDamage, _bulletSpeed);
        }
        else
        {
            bull.Init(_bulletDamage, _bulletSpeed , _stunChance , _stunDuration);
        }

        _bulletsLeftInMag--;

         _bulletsLeftInMagUI.text = _bulletsLeftInMag.ToString();
    }

    private void MeleeAttack()
    {
        Collider[] cols = Physics.OverlapSphere(_meleeAttackPoint.position , _meleeAttackRadius , _meleeAttackEnemyMask);
        foreach (Collider col in cols)
        {
            EnemyHealth EH;
            if (EH = col.GetComponent<EnemyHealth>())
            {
                Vector3 hitPoint = col.ClosestPoint(_meleeAttackPoint.position);
                if (_meleeAttackStunChance <= 0)
                {
                    EH.GetDamage(_meleeAttackDamage, hitPoint);
                }
                else
                {
                    EH.GetDamage(_meleeAttackDamage, hitPoint , _meleeAttackStunChance , _meleeAttackStunDuration);
                }
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

    public bool TryBuy(int ammount)
    {
        if (_Mags - ammount >= 0)
        {
            _Mags -= ammount;
            _MagsUI.text = _Mags.ToString();
            return true;
        }
        return false;
    }

    public void AddDamage(int ammount)
    {
        if (_bulletDamage + ammount <= 50)
        {
            _bulletDamage += ammount;
            _meleeAttackDamage += ammount;
        }
    }

    public void AddSpeed(float ammount)
    {
        if (_reloadTime - ammount >= 1)
        {
            _reloadTime -= ammount;
            _bulletSpeed += 10;
        }
    }

    public void AddStunChance()
    {
        if (_stunChance + 0.2f <= 1)
        {
            _stunChance += 0.2f;
            _meleeAttackStunChance += 0.2f;
            _stunDuration += 0.4f;
            _meleeAttackStunDuration += 0.4f;
            _bulletDamage -= 4;
            _fireSpeed -= 0.05f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_meleeAttackPoint.position , _meleeAttackRadius);
    }
}
