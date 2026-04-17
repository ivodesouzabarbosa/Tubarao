using UnityEngine;
using UnityEngine.InputSystem;
public class MouseControl : MonoBehaviour
{
    [SerializeField] Vector2 _moveInput;

    public Transform _target;
    public Transform _followTarget;

    [SerializeField] float _followSpeed = 5f;

    [Header("Rotação")]
    [SerializeField] float _tiltAmount = 0.5f;
    [SerializeField] float _tiltSpeed = 6f;
    [SerializeField] float _maxTilt = 60f;

    Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = _followTarget.position;
    }

    private void Update()
    {
        // Move target
        Vector3 mousePos = new Vector3(_moveInput.x, _moveInput.y, 10f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        _target.position = worldPos;

        // Follow suave
        _followTarget.position = Vector3.Lerp(
            _followTarget.position,
            _target.position,
            _followSpeed * Time.deltaTime
        );

        // Velocidade real do movimento
        Vector3 velocity = (_followTarget.position - _lastPosition) / Time.deltaTime;

        // Flip X
        if (velocity.x != 0)
        {
            Vector3 scale = _followTarget.localScale;
            scale.x = Mathf.Sign(velocity.x) * Mathf.Abs(scale.x);
            _followTarget.localScale = scale;
        }

        // Direção baseada no flip
        float direction = Mathf.Sign(_followTarget.localScale.x);

        // Rotação baseada na velocidade vertical
        float tilt = Mathf.Clamp(
            velocity.y * _tiltAmount,
            -_maxTilt,
            _maxTilt
        );

        Quaternion targetRotation = Quaternion.Euler(0, 0, tilt * direction);

        _followTarget.rotation = Quaternion.Lerp(
            _followTarget.rotation,
            targetRotation,
            _tiltSpeed * Time.deltaTime
        );

        _lastPosition = _followTarget.position;
    }

    public void MoveMouse(InputAction.CallbackContext value)
    {
        _moveInput = value.ReadValue<Vector2>();
    }
}