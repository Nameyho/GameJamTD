using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region  Exposed
    [SerializeField]
    private GameObject _towerPrefab;

    [SerializeField]
    private GameObject _Nose;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _delayshoot;
    private float _nextShotTime;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private float _destroyBullet;
    #endregion


    #region Unity API
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Mob"))
        {
            if ((Time.time >= _nextShotTime))
            {
                FireBullet(other);
                _nextShotTime = Time.time + _delayshoot;
            }
            _towerPrefab.gameObject.transform.LookAt(other.transform);
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mob"))
        {
            _towerPrefab.gameObject.transform.LookAt(other.transform);
        }


    }

    #endregion


    #region Methods

    private void FireBullet(Collider Other)
    {

        GameObject newbullet = Instantiate(_bulletPrefab, _Nose.transform);
        newbullet.gameObject.transform.LookAt(Other.transform);
        Bullet bullet = newbullet.GetComponent<Bullet>();
        bullet.Shoot(_bulletSpeed);
        Destroy(newbullet, _destroyBullet);
    }

    #endregion

}
