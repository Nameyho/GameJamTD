using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region  Exposed
    [SerializeField]
    private GameObject _towerPrefab;

    [SerializeField]
    private GameObject _shotArea;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _delayshoot;
    private float _nextShotTime;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private float _destroyBullet;

    [System.Serializable]
     public enum _shootTypes {
        balistique = 1,
        instantanée = 2
    }

    public _shootTypes _typeshoot;

    #endregion

    #region private methods

    public List<Collider> EnemiesList;
    #endregion 

    #region Unity API
    private void OnTriggerStay(Collider other)
    {
          
        if (other.CompareTag("Mob"))
        {
               
                int  highestscore =-1 ;
                Collider toAttack =null;
                
                foreach (Collider e in EnemiesList)
                {
                    if(e.GetComponentInParent<EnemyHP>().distanceParcourue > highestscore){
                        highestscore = e.GetComponentInParent<EnemyHP>().distanceParcourue;
                        toAttack = e;
                    }
                }
                if(toAttack!= null)
                 _towerPrefab.transform.LookAt(toAttack.transform);
                if ((Time.time >= _nextShotTime))
                {
               
                if(toAttack!= null){
                FireBullet(toAttack);
                _nextShotTime = Time.time + _delayshoot;
                }

            }
           
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mob"))
        {
            
            EnemiesList.Add(other);
            Debug.Log(EnemiesList.Count);
            
        }


    }

    private void OnTriggerExit(Collider other) {

        if (other.CompareTag("Mob"))
        {
            
            
            Debug.Log(EnemiesList.Count);
            EnemiesList.Remove(other);
            
        }
    }
    

    private void Awake() {
        EnemiesList = new List<Collider>();
    }

    #endregion


    #region Methods

    private void FireBullet(Collider Other)
    {
        
        
        switch(_typeshoot)
        {
            //tir balistique
            case _shootTypes.balistique : 
               
                GameObject newbullet = Instantiate(_bulletPrefab, _shotArea.transform.position,_shotArea.transform.rotation);
                Bullet bullet = newbullet.GetComponent<Bullet>();
                bullet.Shoot(_bulletSpeed);
                Destroy(newbullet, _destroyBullet);
                break;
            //tir immédiat
            case _shootTypes.instantanée :

                //insérer appel de la fonction pour baisser le point de vie de l'ennemie
                break;

        }

        
    }

    #endregion

}
