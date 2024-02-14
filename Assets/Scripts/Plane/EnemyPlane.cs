using UnityEngine;

public class EnemyPlane : Plane
{
    [SerializeField] private int _scorePerDeath;

    private ObjectPool<EnemyPlane> _pool;
    public void Initialize(ObjectPool<Bullet> pool, Vector3 pos, ObjectPool<EnemyPlane> poolEnemies)
    {
        Initialize();
        _hp = _maxHP;
        _pool = poolEnemies;
        transform.position = pos;
        _gun.Initialize(pool, this);
    }

    private void Update()
    {
        if (transform.position.y < -15)
            _pool.Realease(this);
    }

    protected override void OnRestarted()
    {
        _pool.Realease(this);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if(HP <= 0)
        {
            _pool.Realease(this);
            EventBus.OnEnemyDestroed(_scorePerDeath);
        }
    }
}
