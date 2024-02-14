using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _direction = 1;

    private void Update()
    {
        if (GameManager.IsGameActive)
        {
            transform.Translate(transform.up * _speed * Time.deltaTime * _direction);
        }
    }
}
