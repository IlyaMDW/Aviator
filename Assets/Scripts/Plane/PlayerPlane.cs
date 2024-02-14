using UnityEngine;

public class PlayerPlane : Plane, IHeallable
{
    [SerializeField] private DataProvider _dataProvider;
    [SerializeField] private ScoreCounter _scoreCounter;

    public override float HP
    {
        get { return _hp; }
        set { _hp = value;
            EventBus.OnHpChanged(_hp);
        }
    }
    public void Initialize(DataProvider dataProvider)
    {
        base.Initialize();
        _dataProvider = dataProvider;
        _scoreCounter.Initialze(dataProvider);
        _gun.Initialize(new ObjectPool<Bullet>(_bulletPrefab, 5, "playerBullets"), this);
    }

    public void Heal(float hp)
    {
        HP += hp;
        if(HP > _maxHP )
        {
            HP = _maxHP;
        }
    }
    protected override void OnRestarted()
    {
        HP = _maxHP;
        _scoreCounter.CurrentScore = 0;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (HP <= 0)
        {
            GameManager.IsGameActive = false;
            EventBus.OnPlayerDestroyed(_scoreCounter.CurrentScore, _dataProvider);
        }
    }
}
