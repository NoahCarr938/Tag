using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedPowerUp : MonoBehaviour
{
    [SerializeField]
    private float _boostedSpeed = 40.0f;

    private PlayerController _playerController;

    private void Update()
    {
        _boostedSpeed = _playerController.GetMaxSpeed;
    }
}
