using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private int _turretDamage;


    [SerializeField]
    private int _level;

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
        float HighestDistance = -1;

        for (int i = EnemiesList.Count; i > 0; i--)
        {
            if (!EnemiesList[i-1].enabled)
            {
                EnemiesList.RemoveAt(i-1);
            }
        }

        if (!toAttack || !toAttack.enabled)
        {
            toAttack = null;
        }

        if ((!toAttack))
        {

            foreach (Collider e in EnemiesList)
            {
                if (e.GetComponentInParent<EnemyWalker>().GetRemainingDistance() > HighestDistance)
                {

                    HighestDistance = e.GetComponentInParent<EnemyWalker>().GetRemainingDistance();
                    toAttack = e;
                    this.transform.LookAt(toAttack.transform);
                }
            }
        }
        else
        {

        }
        if ((toAttack) && !IsFlameOn)
        {

            this.transform.LookAt(toAttack.transform);
            if ((Time.time >= _nextShotTime))
            {
                FireBullet(toAttack);
                _nextShotTime = Time.time + _delayShoot;
            }
        }
        else
        {

            if (toAttack)
            {
                this.transform.LookAt(toAttack.transform);
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

        float realdamage = (_turretDamage + ((_turretDamage * _level) * _damageMultiplierByLevel));

        switch (_typeShoot)
        {


            case _shootTypes.balistique:

                GameObject newbullet = Instantiate(_bulletPrefab, _shootZone.transform.position, _shootZone.transform.rotation);
                Bullet bullet = newbullet.GetComponent<Bullet>();

                bullet.damage = realdamage;
                bullet.Shoot(_bulletSpeed);
                Destroy(newbullet, _bulletLifeSpan);
                break;

            case _shootTypes.instantanée:


                Other.GetComponent<EnemyHealth>().ReceiveDamages(realdamage);
                //insérer appel de la fonction pour baisser le point de vie de l'ennemie
                break;

            case _shootTypes.LanceFlamme:


                if (IsFlameOn)
                {

                    if (!toAttack)
                    {
                        // stop lance flamme
                        IsFlameOn = false;
                        return;
                    }

                    for (int i = 0; i < EnemiesList.Count; i++)
                    {

                        Debug.Log(EnemiesList[i].name + "reçoit des dégats");
                        Other.GetComponent<EnemyHealth>().ReceiveDamages(realdamage);
                    }
                }
                else
                {
                    IsFlameOn = true;

                }



                //if pas active je l'actif , lance flamme actif
                break;

        }


    }

    #endregion

    #region public 
    public void RemoveEnemy(Collider other)
    {
        EnemiesList.Remove(other);
    }
    #endregion

}
