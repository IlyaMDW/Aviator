using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _yPos;
    [SerializeField] private float _xMinPos;
    [SerializeField] private float _xMaxPos;
    [SerializeField] private float _coolDownSpawn = 3;
    [SerializeField] private EnemyPlane _enemyPlanePrafab;
    [SerializeField] private Bullet _bulletPrafab;

    private ObjectPool<EnemyPlane> _enemyPlanePool;
    private ObjectPool<Bullet> _enemyBulletsPool;

    private void Awake()
    {
        _enemyPlanePool = new ObjectPool<EnemyPlane>(_enemyPlanePrafab, 5, "Enemies");
        _enemyBulletsPool = new ObjectPool<Bullet>(_bulletPrafab, 20, "EnemieBullets");
    }

    public void Initialize()
    {
        StartCoroutine(Spawning());
    }
    private void OnEnable()
    {
        EventBus.playerDestroyed += OnGameOvered;
    }

    private void OnDisable()
    {
        EventBus.playerDestroyed -= OnGameOvered;
    }

    private void OnGameOvered(int arg1, DataProvider provider)
    {
        StopCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            EnemyPlane enemy = _enemyPlanePool.Get();
            enemy.Initialize(_enemyBulletsPool, GetRandomPosition(), _enemyPlanePool);
            yield return new WaitForSeconds(_coolDownSpawn);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 pos = new Vector3(Random.Range(_xMinPos, _xMaxPos), _yPos, 0);
        return pos;
    }
}
