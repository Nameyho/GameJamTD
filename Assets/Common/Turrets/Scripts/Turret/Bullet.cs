using UnityEngine;


public class Bullet : MonoBehaviour
{

    #region private

    private Transform _transform;
    private Rigidbody _Rigidbody;

    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private SphereCollider _collider;

    [Header("movement")]
    [SerializeField]
    private float _bulletSpeed;

    [Header("Tweaking")]

    [SerializeField]
    private float _radius;



    private Bullet _bullet;

    private float _damage;
    public float damage
    {
        get => _damage;
        set => _damage = value;
    }

    public enum TypeBullet
    {
        classique = 0,
        explosive = 1
    }

    public TypeBullet _bulletType;
    #endregion


    #region  Unity APi
    private void Awake()
    {

        _transform = transform;
        _Rigidbody = GetComponent<Rigidbody>();
        _bullet = GetComponent<Bullet>();

    }

    private void FixedUpdate()
    {

        Vector3 velocity = transform.forward * _bulletSpeed;
        Vector3 movementStep = velocity * Time.fixedDeltaTime;
        Vector3 NewPos = _transform.position + movementStep;
        _Rigidbody.MovePosition(NewPos);

    }
    public void Shoot(float speed)
    {
        _bulletSpeed = speed;

    }

    private void Start() {

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        switch (_bulletType)
        {
            case TypeBullet.classique:
                GetComponentInChildren<GameObject>().SetActive(true);
                other.GetComponent<EnemyHealth>().ReceiveDamages(_damage);
                Destroy(gameObject);
                break;

            case TypeBullet.explosive:


                Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _radius);

                foreach (var hitCollider in hitColliders)
                {

                    if (hitCollider.CompareTag("Mob"))
                    {
                        hitCollider.GetComponent<EnemyHealth>().ReceiveDamages(_damage);
                    }
                }

                Instantiate(_explosion, _transform.position, _transform.rotation);
                Destroy(gameObject);


                break;
        }

    }

    #endregion
}

