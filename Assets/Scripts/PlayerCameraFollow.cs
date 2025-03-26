using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _smoothTime;
    // Vector3.zero is short for Vector3(0, 0, 0)
    private Vector3 _currentVelocity = Vector3.zero;

    private TagSystem _player1TagSystem;
    private TagSystem _player2TagSystem;

    // Awake is called when the instance of the script is loaded
    private void Start()
    {
        _offset = transform.position - _target.position;
    }

    // Called every frame if the behaviour is enabled
    private void FixedUpdate()
    {
        Vector3 targetPosition = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }
}
