using UnityEngine;

public class AidKitItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.TryGetComponent<IHeallable>(out IHeallable healable))
        {
            AidKitItemSO healItem = _itemSO as AidKitItemSO;
            healable.Heal(healItem.hillPoint);
            Destroy(gameObject);
        }
    }
}
