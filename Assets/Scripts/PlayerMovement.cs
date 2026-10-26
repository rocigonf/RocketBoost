using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;

    [Header("Movement Settings")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 100f;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem thrustParticleSystem;
    [SerializeField] private ParticleSystem thrustParticleSystemLeft;
    [SerializeField] private ParticleSystem thrustParticleSystemRight;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip thrustAudioClip;

    Rigidbody rb;
    AudioSource audioSource;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void ProcessRotation()
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate

        float rotationValue = rotation.ReadValue<float>();

        if (rotationValue < 0)
        {
            RotateRight();
        }
        else if (rotationValue > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotation();
        }
    }

    private void StopRotation()
    {
        thrustParticleSystemLeft.Stop();
        thrustParticleSystemRight.Stop();
    }

    private void RotateLeft()
    {
        transform.Rotate(Vector3.back * rotationForce);
        if (!thrustParticleSystemLeft.isPlaying)
        {
            thrustParticleSystemLeft.Play();
            thrustParticleSystemRight.Stop();
        }
    }

    private void RotateRight()
    {
        transform.Rotate(Vector3.forward * rotationForce);
        if (!thrustParticleSystemRight.isPlaying)
        {
            thrustParticleSystemRight.Play();
            thrustParticleSystemLeft.Stop();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * moveForce);

        if (!thrustParticleSystem.isPlaying)
        {
            thrustParticleSystem.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudioClip);
        }
    }

    /// <summary>
    /// Stops the thrusting action by stopping the particle system and audio source
    /// </summary>
    private void StopThrusting()
    {
        thrustParticleSystem.Stop();
        audioSource.Stop();
    }
}
