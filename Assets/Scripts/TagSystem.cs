using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSystem : MonoBehaviour
{
    [SerializeField]
    private bool _startTagged = false;

    [SerializeField]
    private float _tagImmunityDuration = 1.0f;

    [SerializeField]
    private GameObject _tagParticlesPrefab;

    private bool _tagged = false;
    private bool _tagImmune = false;
    private PlayerCameraFollow _followingPlayer;

    public bool Tagged { get { return _tagged; } }

    public bool Tag()
    {
        // If already tagged, do nothing
        if (Tagged)
            return false;

        // If immune to tag, do nothing
        if (_tagImmune) return false;

        // Whenever tagged
        if (TryGetComponent(out TrailRenderer renderer))
            renderer.emitting = true;

        _tagged = true;
        SpawnParticles();
        return true;
    }

    private void SwapCameraFollow()
    {
        //if (Tagged)
        //{
        //    _followingPlayer.
        //}
    }
    private void SpawnParticles()
    {
        // How to get rotation as a vector 3
        //Vector3 rot = gameObject.transform.rotation.eulerAngles;

        // Stores the object as a component
        //GameObject obj = Instantiate

        // Guard clause
        if (!_tagParticlesPrefab)
            return;
        Instantiate(_tagParticlesPrefab, gameObject.transform.position, gameObject.transform.rotation);

        // Do not have to worry about destroying
       // Destroy(obj);
    }
    private void SetTagImmuneFalse()
    {
        _tagImmune = false;
    }

    private void Start()
    {
        _tagged = _startTagged;
        // How to pause the game
       // Debug.Break();
        // Whenever tagged
        if (TryGetComponent(out TrailRenderer renderer))
            renderer.emitting = _startTagged;
    }

    // Blue function means that this is a built in unity function
    // It will be called whenever collision occurs
    private void OnCollisionEnter(Collision collision)
    {
        // If we are not tagged do nothing
        if (!Tagged) return;

        // if nothing is found and tagsystem is null then nothing is executed
        // if we did not use an if statement it could be null and we get null reference exceptions
        if (collision.gameObject.TryGetComponent(out TagSystem tagsystem))
        {
            // Tag the other player
            // Only untag of tag is succesful
           if (tagsystem.Tag())
           {
                _tagged = false;
                _tagImmune = true;
                if (TryGetComponent(out TrailRenderer renderer))
                    renderer.emitting = false;
                // nameof gets the name of the function
                Invoke(nameof(SetTagImmuneFalse), _tagImmunityDuration);
           }
        }
    }
}
