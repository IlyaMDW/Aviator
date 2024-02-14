using System.Collections;
using UnityEngine;

public class PlaneGun : MonoBehaviour
{
    [SerializeField] private float _coolDownGun = 1f;

    private ObjectPool<Bullet> _bulletsPool;
    private IAttackable _source;

    public void Initialize(ObjectPool<Bullet> pool, IAttackable source)
    {
        _source = source;
        _bulletsPool = pool;
        StartCoroutine(Shooting());
    }

    public void GameOvered()
    {
        StopCoroutine(Shooting());
    }

    public void Restart()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            Bullet newBullet = _bulletsPool.Get();
            newBullet.Initialize(transform.position, _bulletsPool, _source);
            yield return new WaitForSeconds(_coolDownGun);
        }
    }
}
