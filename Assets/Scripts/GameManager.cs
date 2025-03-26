using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player1;

    [SerializeField]
    private GameObject _player2;

    [SerializeField]
    private GameObject _winTextBackground;

    public UnityEvent OnGameWin;

    // Could handle all of this in the player class
    // You want the functionality to belong to the player
    // The player should be the one to determine how it turns off
    private TimerSystem _player1Timer;
    private TimerSystem _player2Timer;
    private TagSystem _player1TagSystem;
    private TagSystem _player2TagSystem;
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;
    private Rigidbody _player1Rigidbody;
    private Rigidbody _player2Rigidbody;

    private bool _gameWon = false;

    private void Start()
    {
        // Getting all the components that we need for our game to manage
        // Being extra careful while getting and assigning everything
        // Also could make a struct to handle this
        if (_player1)
        {
            // If try get component fails display error
            // Assigning the variable by reference
            // If it finds the component assign it if not throw error
            if (!_player1.TryGetComponent(out _player1Timer))
                Debug.LogError("GameManager: Could not get Player1Timer");
            if (!_player1.TryGetComponent(out _player1TagSystem))
                Debug.LogError("GameManager: Could not get Player1TagSystem");
            if (!_player1.TryGetComponent(out _player1Controller))
                Debug.LogError("GameManager: Could not get Player1Controller");
            if (!_player1.TryGetComponent(out _player1Rigidbody))
                Debug.LogError("GameManager: Could not get Player1Rigidbody");
            //if (!_player1.TryGetComponent(out _player1CameraFollow))
            //    Debug.LogError("GameManager: Could not get Player1CameraFollow");

        }
        else
            Debug.LogError("GameManager: Player1 not assigned!");

        if (_player2)
        {
            // If try get component fails display error
            // Assigning the variable by reference
            // If it finds the component assign it if not throw error
            if (!_player2.TryGetComponent(out _player2Timer))
                Debug.LogError("GameManager: Could not get Player2Timer");
            if (!_player2.TryGetComponent(out _player2TagSystem))
                Debug.LogError("GameManager: Could not get Player2TagSystem");
            if (!_player2.TryGetComponent(out _player2Controller))
                Debug.LogError("GameManager: Could not get Player2Controller");
            if (!_player2.TryGetComponent(out _player2Rigidbody))
                Debug.LogError("GameManager: Could not get Player2Rigidbody");
        }
        else
            Debug.LogError("GameManager: Player2 not assigner!");

        // If the UI does not work give a warning
        if (!_winTextBackground)
            Debug.LogWarning("GameManager: Win Text Background not assigned!");
    }

    private void Update()
    {
        // We want both of the player's timers to be valid
        // If either timer is not assigned, do nothing
        if (!(_player1Timer && _player2Timer))
            return;

        // If the game has already been won, do nothing
        if (_gameWon)
            return;

        // Check if either timer is finished and win the game if so
        if (_player1Timer.TimeRemaining <= 0)
            Win("Player 1 Wins!");
        else if (_player2Timer.TimeRemaining <= 0)
            Win("Player 2 Wins!");
    }

    private void Win(string winText)
    {
        // Enable Win Screen UI and set text to winText
        if (_winTextBackground)
        {
            // Setting the winTextBackground image to be active
            // For game objects use SetActive, enabled is for components
            _winTextBackground.SetActive(true);
            // GetComponentInChildren goes through the component and gets the first component in that child that matches the parameters
            TextMeshProUGUI text = _winTextBackground.GetComponentInChildren<TextMeshProUGUI>(true);
            // If we did get text make it equal to winText
            if (text)
            {
                text.text = winText;
            }
        }

        // Turn off player controllers and tag system and timer
        // Turns off the components
        if (_player1Timer)
            _player1Timer.enabled = false;
        if (_player1TagSystem)
            _player1TagSystem.enabled = false;
        if (_player1Controller)
            _player1Controller.enabled = false;
        // Rigidbody does not have enabled so use isKinematic
        // Kinematic means it will stop everything in place
        if (_player1Rigidbody)
            _player1Rigidbody.isKinematic = true;

        if (_player2Timer)
            _player2Timer.enabled = false;
        if (_player2TagSystem)
            _player2TagSystem.enabled = false;
        if (_player2Controller)
            _player2Controller.enabled = false;
        // Rigidbody does not have enabled so use isKinematic
        // Kinematic means it will stop everything in place
        if (_player2Rigidbody)
            _player2Rigidbody.isKinematic = true;

        // Only call this function one time instead of every frame
        _gameWon = true;

        // Take all the functions plugged into the event and call them
        OnGameWin.Invoke();
        // Need a reference to the event and pass in the function itself
        // If you wanted to remove an event you would use RemoveListener
        //OnGameWin.AddListener(ResetGame);
    }

    public void ResetGame()
    {
        // Reload the active scene
        // build index tells what scene it is
        // add plus 1 to the build index to move scenes
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
