using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    // getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Animator Animator { get { return _animator; } }
    public Rigidbody Rb { get { return _rb; } }
    public GroundCheckSensor GroundSensor { get { return _groundCheckSensor; } }
    public bool FacingLeft { get { return _facingLeft; } set { _facingLeft = value; } }
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } }
    public float SpeedH { get { return _speedH; } set { _speedH = value; } }
    public float SpeedV { get { return _speedV; } set { _speedV = value; } }
    public float MaxSpeedH { get { return _maxSpeedH; } set { _maxSpeedH = value; } }
    public float MaxSpeedV { get { return _maxSpeedV; } set { _maxSpeedV = value; } }
    public float AttackDuration { get { return _attackDuration; } set { _attackDuration = value; } }
    public float AttackStartTime { get { return _attackStartTime; } set { _attackStartTime = value; } }
    public float AttackTime { get { return _attackTime; } set { _attackTime = value; } }
    public float AttackBuffer { get { return _attackBuffer; } set { _attackBuffer = value; } }
    public bool Dead { get { return _dead; } }

    [Header("Movement")]
    [SerializeField] private float _maxSpeedH = 1f;
    [SerializeField] private float _maxSpeedV = 1f;

    private Rigidbody _rb;
    private GroundCheckSensor _groundCheckSensor;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerHealth _playerHealth;

    private float _speedH = 0f;
    private float _speedV = 0f;
    private bool _facingLeft = false;

    // Attacks
    private float _attackDuration = 0.75f;
    private float _attackStartTime = 0f;
    private float _attackTime = 0f;
    private float _attackBuffer = 0f;

    private bool _dead = false;

    public GameObject _snowballPrefab;
    public GameObject _snowballSpawnL;
    public GameObject _snowballSpawnR;

    private void Awake()
    {
        // initialize reference variables
        _rb = GetComponent<Rigidbody>();
        _groundCheckSensor = GetComponentInChildren<GroundCheckSensor>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerHealth.OnPlayerDeath += OnPlayerDeath;

        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void OnDisable()
    {
        _playerHealth.OnPlayerDeath -= OnPlayerDeath;
    }

    private void Update()
    {
        _currentState.UpdateStates();
    }
    public void DelayedThrowSnowball(float delayTime, int damage)
    {
        StartCoroutine(SpawnSnowballHelper(delayTime, damage));
    }

    private void SpawnSnowball(int damage)
    {
        if (_snowballPrefab && _snowballSpawnL && _snowballSpawnR)
        {
            GameObject snowball = Instantiate(_snowballPrefab, _facingLeft ? _snowballSpawnL.transform.position : _snowballSpawnR.transform.position, Quaternion.Euler(45, 0, 0));
            SnowballBehavior snowballBehavior = snowball.GetComponent<SnowballBehavior>();
            snowballBehavior.moveLeft = _facingLeft;
            snowballBehavior.despawnTime = 3f;
            snowballBehavior.friendly = true;
            snowballBehavior.damage = damage;
        }
    }

    private IEnumerator SpawnSnowballHelper(float delayTime, int damage)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
        SpawnSnowball(damage);
    }

    private void OnPlayerDeath()
    {
        _dead = true;
    }
}
