using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Requires 
[RequireComponent(typeof(TagSystem))]
public class TimerSystem : MonoBehaviour
{
    [SerializeField]
    private float _startingTime = 30.0f;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    private float _timeRemaining;
    private TagSystem _tagSystem;

    public float TimeRemaining { get { return _timeRemaining; } }

    private void Start()
    {
        _tagSystem = GetComponent<TagSystem>();
        _timeRemaining = _startingTime;
        if (_timerText)
        {
            // ToString lets you specify what the text should look like
            _timerText.text = _timeRemaining.ToString("0.0");
        }
    }

    private void Update()
    {
        // If not tagged do not lower timer
        if (!_tagSystem.Tagged) return;

        // If tagged
        _timeRemaining -= Time.deltaTime;
        _timeRemaining = Mathf.Clamp(_timeRemaining, 0, _startingTime);

        if (_timerText)
        {
            _timerText.text = _timeRemaining.ToString();
        }
    }
}
