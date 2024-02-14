using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float _yPos;
    [SerializeField] private float _xMinPos;
    [SerializeField] private float _xMaxPos;
    [SerializeField] private float _coolDownSpawn = 10;

    [SerializeField] private Item[] _items;

    private List<ItemChance> _itemChances;
    private int weight;

    private void Awake()
    {
        CalculateWeight();
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
            yield return new WaitForSeconds(_coolDownSpawn);
            Item item = GetRandomItem();
            item.Initialize(GetRandomPosition());
        }
    }

    private Item GetRandomItem()
    {
        int randWeight = Random.Range(0, weight);
        int index = 0;
        for (int i = 0; i < _itemChances.Count; i++)
        {
            if (_itemChances[i].Weight > randWeight)
            {
                index = i;
                break;
            }
        }
        Item newItem = Instantiate(_items[index]);
        return newItem;
    }

    private void CalculateWeight()
    {
        _itemChances = new List<ItemChance>();
        weight = 0;
        for (int i = 0; i < _items.Length; i++)
        {
            weight += _items[i].ItemSO.chanceDrop;

            ItemChance newItem = new ItemChance();
            newItem.Weight = weight;
            newItem.Id = i;
            _itemChances.Add(newItem);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 pos = new Vector3(Random.Range(_xMinPos, _xMaxPos), _yPos, 0);
        return pos;
    }
}
