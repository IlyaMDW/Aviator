using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _upBorder = 15;
    [SerializeField] private PlaneType _type;

    private ObjectPool<Bullet> _pool;
    private IAttackable _source;

    public void Initialize(Vector3 position, ObjectPool<Bullet> pool, IAttackable source)
    {
        _source = source;
        _pool = pool;
        transform.position = position;
    }

    private void OnEnable()
    {
        EventBus.restarted += OnRestarted;
    }

    private void OnDisable()
    {
        EventBus.restarted -= OnRestarted;
    }

    private void OnRestarted()
    {
        _pool.Realease(this);
    }

    private void Update()
    {
        if (transform.position.y > _upBorder)
            _pool.Realease(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            if((damagable is Plane) && (damagable as Plane).Type != _type)
            {
                ApplyDamage(damagable);
            }
            //if (damagable is PlayerPlane)
            //{
            //    if ((damagable as PlayerPlane).Type == _type)
            //        return;
            //    ApplyDamage(damagable);
            //}
            //else if(damagable is EnemyPlane)
            //{
            //    if ((damagable as EnemyPlane).Type == _type)
            //        return;
            //    ApplyDamage(damagable);
            //}
        }
    }

    private void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage(_source.GiveDamage());
        _pool.Realease(this);
    }
}
