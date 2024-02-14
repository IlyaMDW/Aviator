using System;

public static class EventBus
{
    public static event Action<float> hpChanged;
    public static void OnHpChanged(float val)
    {
        hpChanged?.Invoke(val);
    }

    public static event Action<int> enemyDestroyed;
    public static void OnEnemyDestroed(int val)
    {
        enemyDestroyed?.Invoke(val);
    }

    public static event Action<int, DataProvider> playerDestroyed;
    public static void OnPlayerDestroyed(int val, DataProvider dataProvider)
    {
        playerDestroyed?.Invoke(val, dataProvider);
    }

    public static event Action restarted;
    public static void OnRestarted()
    { 
        restarted?.Invoke();
    }
}
