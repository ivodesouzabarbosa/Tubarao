using UnityEngine;
using UnityEngine.InputSystem;
public class MouseControl : MonoBehaviour
{
    [SerializeField] Vector2 _moveInput;

    public Transform _target;

    public Transform _followTarget;

    [SerializeField] float _followSpeed = 5f;


    private void Update()
    {
        // Move o target principal
        Vector3 mousePos = new Vector3(_moveInput.x, _moveInput.y, 10f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        _target.position = worldPos;

        // Outro objeto seguir lentamente
        _followTarget.position = Vector3.Lerp(
            _followTarget.position,
            _target.position,
            _followSpeed * Time.deltaTime
        );
    }

    public void MoveMouse(InputAction.CallbackContext value)
    {
        _moveInput = value.ReadValue<Vector2>();
    }
}
