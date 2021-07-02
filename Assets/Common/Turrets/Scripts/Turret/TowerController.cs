using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using ScriptableObjectArchitecture;

public class TowerController : MonoBehaviour
{
    #region  Exposed

    [Header("Prefab")]
    [SerializeField]
    private GameObject _towerModel;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private GameObject _shootZone;
    
    [SerializeField]
    private GameObject _shootZone2;

    [SerializeField]
    private Transform _toTurn;

    [SerializeField]
    private VisualEffect _visualEffect;

    [SerializeField]
    private GameObject _levelUpVisual;

    [SerializeField]
    private AudioSource _audioSource;



    [Header("tweaking")]
    [SerializeField]
    private float _delayShoot;
    private float _nextShotTime;


    [SerializeField]
    private float _bulletSpeed;


    [SerializeField]
    private float _bulletLifeSpan;


    [SerializeField]
    private int _goldCost;

    public int goldCost
    {
        get => _goldCost;
        set => _goldCost = value;
    }

    [SerializeField]
    [Range(10f, 80f)]
    private float angle = 45f;

    public _shootTypes _typeShoot;

    [System.Serializable]
    public enum _shootTypes
    {
        balistique = 1,
        instantanée = 2,

        LanceFlamme = 3
    }

    [SerializeField]
    private IntVariable _gold;

    [SerializeField]
    private float _turretDamage;


    [SerializeField]
    private int _level;

    [SerializeField]
    private int _maxLevel;

    public bool IsMaxLevel
    {
        get
        {
            return (_level < _maxLevel);
        }
    }

    [SerializeField]

    private float _damageMultiplierByLevel;

    #endregion


    #region private

    Collider toAttack = null;

    private bool IsFlameOn;

    #endregion
    #region private 

    private List<Collider> EnemiesList;
    #endregion

    #region Unity API
    private void Update()
    {
        for (int i = EnemiesList.Count; i > 0; i--)
        {
            if (!EnemiesList[i - 1].enabled)
            {
                EnemiesList.RemoveAt(i - 1);
            }
        }

        if (!toAttack || !toAttack.enabled)
        {
            toAttack = null;
        }

        float HighestDistance = -1;

        if ((!toAttack))
        {

            foreach (Collider e in EnemiesList)
            {
                if (e.GetComponentInParent<EnemyWalker>().GetRemainingDistance() > HighestDistance)
                {

                    HighestDistance = e.GetComponentInParent<EnemyWalker>().GetRemainingDistance();
                    toAttack = e;
                    if (_toTurn)
                        _toTurn.LookAt(toAttack.transform);
                }
            }
            if(IsFlameOn)
            {
                IsFlameOn = false;
                _visualEffect.SendEvent("OnStop");
            }
        }
        //else if (!IsFlameOn)
        //{
        //    if (_toTurn)
        //        _toTurn.LookAt(toAttack.transform);
        //    if ((Time.time >= _nextShotTime))
        //    {
        //        FireBullet(toAttack);
        //        _nextShotTime = Time.time + _delayShoot;
        //    }
        //}
        else
        {

            if (toAttack)
            {
                if (_toTurn)
                    _toTurn.LookAt(toAttack.transform);
                FireBullet(toAttack);

            }


        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mob"))
        {

            EnemiesList.Add(other);


        }


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Mob"))
        {
            if (other == toAttack)
            {
                toAttack = null;
            }
            EnemiesList.Remove(other);
        }
    }

    private void PlaySound()
    {
        if(_audioSource)
        {
            _audioSource.pitch = Random.Range(0.8f, 1.2f);
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }

    private void Start()
    {
        EnemiesList = new List<Collider>();
        _gold.Value -= _goldCost;
    }


    private void OnDrawGizmos()
    {

        if (toAttack)
        {
            Gizmos.DrawLine(_shootZone.transform.position, toAttack.transform.position);
        }



    }
    #endregion


    #region Methods

    private void FireBullet(Collider Other)
    {

        float realdamage = _turretDamage + ((_turretDamage * _level) * _damageMultiplierByLevel);
        Vector3 point = Other.transform.position;
        switch (_typeShoot)
        {


            case _shootTypes.balistique:

                GameObject newBullet = Instantiate(_bulletPrefab, _shootZone.transform.position, _shootZone.transform.rotation);
                var velocity = BallisticVelocity(point, angle);
                Debug.Log("Firing at " + point + " velocity " + velocity);
                
                Bullet bullet = newBullet.GetComponent<Bullet>();
                // newBullet = velocity;

                if(_visualEffect)
                    _visualEffect.Play();

                PlaySound();

                bullet.damage = realdamage;
                bullet.Shoot(_bulletSpeed);
                Destroy(newBullet, _bulletLifeSpan);
                break;

            case _shootTypes.instantanée:


                Other.GetComponent<EnemyHealth>().ReceiveDamages(realdamage);
                if (_visualEffect)
                    _visualEffect.Play();
                PlaySound();
                //insérer appel de la fonction pour baisser le point de vie de l'ennemie
                break;

            case _shootTypes.LanceFlamme:


                if (IsFlameOn)
                {

                    if (!toAttack)
                    {
                        // stop lance flamme
                        IsFlameOn = false;
                        _visualEffect.SendEvent("OnStop");
                        _audioSource.Stop();
                        return;
                    }

                    for (int i = 0; i < EnemiesList.Count; i++)
                    {
                        foreach (var item in Physics.OverlapCapsule(_shootZone.transform.position, _shootZone2.transform.position, 3))
                        {
                            EnemyHealth enemy = item.GetComponent<EnemyHealth>();
                            if(enemy)
                                enemy.ReceiveDamages(realdamage * Time.deltaTime);
                        } 
                        //Debug.Log(EnemiesList[i].name + "reçoit des dégats");
                        
                    }
                }
                else
                {
                    IsFlameOn = true;
                    _visualEffect.SendEvent("OnPlay");
                    _audioSource.Play();

                }



                //if pas active je l'actif , lance flamme actif
                break;

        }


    }

    private Vector3 BallisticVelocity(Vector3 destination, float angle)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.
    }

    #endregion

    #region public 
    public void RemoveEnemy(Collider other)
    {
        EnemiesList.Remove(other);
    }

    public void LevelUpTower()
    {
        if (IsMaxLevel)
        {
            _gold.Value -= _goldCost;
            _level++;
            _levelUpVisual.SetActive(true);
        }
    }
    #endregion



}
