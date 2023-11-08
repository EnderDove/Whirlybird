using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Controls _playerInput;
    public float MovementInput { get; private set; }

    public void OnEnable()
    {
        _playerInput ??= new Controls();
        _playerInput.Player.Movement.performed += _movement => MovementInput = _movement.ReadValue<float>();
        _playerInput.Enable();
    }
}
