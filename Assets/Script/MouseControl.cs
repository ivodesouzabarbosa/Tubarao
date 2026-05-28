using DG.Tweening;
using System;
using System.Collections;
using Unity.Mathematics;
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

    [SerializeField] float veloctyG;
    [SerializeField] bool _chekMove;
    [SerializeField] Animator _anim;

    Vector3 _lastPosition;
    [SerializeField] float deadZone = 150f;
    public GameObject _boca;
    public int _levelTuba;
    public Color[] _corHitTuba;
    public SpriteRenderer[] _spriteTuba;

    [SerializeField] bool _moveMouseCheck;
    public Transform _targetM;
    public Transform _targetR;
    [SerializeField] float _timeR;

    private void Start()
    {
        _lastPosition = _followTarget.position;
        _moveMouseCheck = true;
        //_anim = GetComponent<Animator>();
    }
    private void Update()
    {
        // Move target com desaceleração no centro
        Vector3 mousePos = new Vector3(_moveInput.x, _moveInput.y, 10f);

        // Centro da tela
        Vector2 centro = new Vector2(Screen.width / 2, Screen.height / 2);

        // Distância até o centro
        float distancia = Vector2.Distance(new Vector2(mousePos.x, mousePos.y), centro);

        // Fator (0 no centro ? 1 longe)
        float fator = Mathf.Clamp01(distancia / deadZone);

        // deixa mais suave (curva)
        fator = fator * fator;

        if (_moveMouseCheck)
        {
            _target = _targetM;
            // Converte para mundo
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Movimento suave
            _target.position = Vector3.Lerp(_target.position, worldPos, fator);
        }
        else
        {
            _target= _targetR;
            _followSpeed = 2f;
            Invoke("TimeRec", _timeR);
        }
            // Follow suave
            _followTarget.position = Vector3.Lerp(
                _followTarget.position,
                _target.position,
                _followSpeed * Time.deltaTime
            );

        // Velocidade real do movimento
        Vector3 velocity = (_followTarget.position - _lastPosition) / Time.deltaTime;


        veloctyG = MathF.Abs(velocity.x+ velocity.y);

      
        if (veloctyG >= 4)
        {
            _chekMove = true;
           // _anim.SetBool("MoveCheck", true);
        }
        else {
            _chekMove = false;
          //  _anim.SetBool("MoveCheck", false);//
        }
        _anim.SetBool("MoveCheck", _chekMove);
        // Flip X
        if (_moveMouseCheck && velocity.x != 0)
        {
            Vector3 scale = _followTarget.localScale;
            scale.x = Mathf.Sign(velocity.x) * Mathf.Abs(scale.x);
            _followTarget.localScale = scale;
        }

        // Direção baseada no flip
        float direction = Mathf.Sign(_followTarget.localScale.x);

        if (_moveMouseCheck)
        {
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
        }

        _lastPosition = _followTarget.position;
    
    }

    void TimeRec()
    {
        _moveMouseCheck = true;
    }

    public void MoveMouse(InputAction.CallbackContext value)
    {
        _moveInput = value.ReadValue<Vector2>();
    }

    public void HitTuba()
    {
        _moveMouseCheck=false;
        StartCoroutine(HitTubaraoCor());
    }

    private IEnumerator HitTubaraoCor()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < _spriteTuba.Length; j++)
            {
                _spriteTuba[j].DOColor(_corHitTuba[0], .15f);
            }

            yield return new WaitForSeconds(.15f);

            for (int j = 0; j < _spriteTuba.Length; j++)
            {
                _spriteTuba[j].DOColor(_corHitTuba[1], .15f);
            }
            yield return new WaitForSeconds(.15f);
        }
    }


}