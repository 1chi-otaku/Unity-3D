using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float forceFactor = 5.0f;

    private InputAction moveAction;
    private Rigidbody rb;
    private AudioSource hitlSound;


    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        hitlSound = GetComponent<AudioSource>();
        GameState.Subscribe(nameof(GameState.effectsVolume), OnVolumeChanged);
        GameState.Subscribe(nameof(GameState.isMuted), OnMuteAllChanged);
        OnVolumeChanged();
    }

    private void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 correctedForward = Camera.main.transform.forward;
        correctedForward.y = 0f;
        correctedForward.Normalize();
        Vector3 forceValue = forceFactor * (Camera.main.transform.right * moveValue.x +
           correctedForward * moveValue.y);
        rb.AddForce(forceValue);
      

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))

        {
            if (!hitlSound.isPlaying)
            {
                hitlSound.Play();
            }
        }
    }

    private void OnVolumeChanged()
    {
        hitlSound.volume = GameState.effectsVolume;
    }
    private void OnMuteAllChanged()
    {
        hitlSound.volume = GameState.isMuted ? 0.0f : GameState.effectsVolume;
    }

    private void OnDestroy()
    {

        GameState.UnSubscribe(nameof(GameState.effectsVolume), OnVolumeChanged);
        GameState.UnSubscribe(nameof(GameState.isMuted), OnMuteAllChanged);
    }





}