using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Serialize Field makes it to where private variables show up in the inspector
    // Acceleration is how fast you approach your maximum speed
    [SerializeField]
    private bool _playerOne = true;

    [SerializeField]
    private float _acceleration = 10.0f;

    [SerializeField]
    private float _maxSpeed = 20.0f;

    [SerializeField]
    private float _jumpPower = 10.0f;

    [SerializeField]
    private GameObject _jumpParticlesPrefab;

    private Rigidbody _rigidbody;
    private float _moveInput;

    public float GetMaxSpeed { get { return _maxSpeed; } }
    public bool IsPlayerOne { get { return _playerOne; } }

    private void SpawnJumpParticles()
    {
        // Guard clause
        if (!_jumpParticlesPrefab)
            return;
        Instantiate(_jumpParticlesPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // A check for which player we are
        if (_playerOne)
        {
            // Using the legacy input system
            // Getting the value of the horizontal axis
            // GetAxisRaw gets rid of any scale movement
            // Storing input for the horizontal axis into a movement variable
            _moveInput = Input.GetAxisRaw("Player1Horizontal");
            if (Input.GetKeyDown(KeyCode.W))
                Jump();

            if (Input.GetKeyDown(KeyCode.S))
                Fall();
        }
        else 
        {
            _moveInput = Input.GetAxisRaw("Player2Horizontal");
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Jump();

            if (Input.GetKeyDown(KeyCode.DownArrow))
                Fall();
        }
    }

    // Update is called once per frame
    // FixedUpdate runs at a fixed time step, it will usually run slower than the framerate
    void FixedUpdate()
    {
        Vector3 deltaMovement = new Vector3();
        deltaMovement.x = _moveInput * _acceleration;
        // Getting the rigidbody component and adding force.
        // Class Time is static and is used to access deltaTime or in this case fixedDeltaTime
        _rigidbody.AddForce(deltaMovement * Time.fixedDeltaTime, ForceMode.VelocityChange);

        // clamp the velocity
        Vector3 newVelocity = _rigidbody.velocity;
        newVelocity.x = Mathf.Clamp(newVelocity.x, -_maxSpeed, _maxSpeed);
        _rigidbody.velocity = newVelocity;
    }

    void Jump()
    {
        // An impulse only happens once, so we do not need to care about deltaTime
        _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        SpawnJumpParticles();
    }

    void Fall()
    {
        _rigidbody.AddForce(Vector3.down * _jumpPower, ForceMode.Impulse);
    }
}
