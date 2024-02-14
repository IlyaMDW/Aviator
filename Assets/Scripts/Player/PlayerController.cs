using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _leftBorder = -5;
    [SerializeField] private float _rightBorder = 5;

    private PlayerInput _input;

    private Vector2 _startPosition;
    private Vector2 _prevPosition;
    private Vector2 _currentPos;
    private Vector2 _delta;
    private bool _isPressed;

    private void Awake()
    {
        _input = new PlayerInput();


        _input.Player.Press.performed += PressStarted;
        _input.Player.Press.canceled += PressCanceled;
        _input.Player.Position.performed += Position_performed;
    }

    private void PressStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _isPressed = true;
        _startPosition = _input.Player.Position.ReadValue<Vector2>();
    }

    private void PressCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _isPressed = false;
        _prevPosition = Vector2.zero;
    }

    private void Position_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_startPosition == Vector2.zero)
            return;

        if (_prevPosition != Vector2.zero)
            _prevPosition = _currentPos;
        else
            _prevPosition = _startPosition;

        _currentPos = obj.ReadValue<Vector2>();
        _delta = _currentPos - _prevPosition;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input?.Disable();
    }

    private void Update()
    {
        if (_isPressed && GameManager.IsGameActive)
        {
            Vector3 delta = new Vector3(_delta.x, 0, 0);
            transform.Translate(delta * _speed * Time.deltaTime);

            if (transform.position.x < _leftBorder)
                transform.position = new Vector3(_leftBorder, transform.position.y, 0);
            else if(transform.position.x > _rightBorder)    
                transform.position = new Vector3(_rightBorder, transform.position.y, 0);
        }
    }
}
