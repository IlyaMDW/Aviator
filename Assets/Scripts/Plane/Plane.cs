using UnityEngine;

public abstract class Plane : MonoBehaviour, IAttackable, IDamagable
{
    [SerializeField] protected PlaneType _type;
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected PlaneGun _gun;
    [SerializeField] protected float _damage = 1;
    [SerializeField] protected float _maxHP = 12;

    protected float _hp;

    public virtual float HP { get; set; }
    public PlaneType Type => _type;

    public virtual void Initialize()
    {
        _hp = _maxHP;
    }
    private void OnEnable()
    {
        EventBus.restarted += OnRestarted;
    }

    private void OnDisable()
    {
        EventBus.restarted -= OnRestarted;
    }

    protected abstract void OnRestarted();

    public virtual float GiveDamage()
    {
        return _damage;
    }

    public virtual void TakeDamage(float damage)
    {
        HP -= damage;
    }
}
