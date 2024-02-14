using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected ItemSO _itemSO;

    public ItemSO ItemSO => _itemSO;

    public void Initialize(Vector3 pos)
    {
        transform.position = pos;
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
        Destroy(gameObject);
    }

    private void Update()
    {
        if(transform.position.y <= -10)
            Destroy(gameObject);
    }
}
