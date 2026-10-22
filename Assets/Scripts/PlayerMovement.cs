using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 100f;

    [SerializeField] private ParticleSystem thrustParticleSystem;
    [SerializeField] private ParticleSystem thrustParticleSystemLeft;
    [SerializeField] private ParticleSystem thrustParticleSystemRight;

    Rigidbody rb;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        // TODO: Activate Sound

        rb.AddRelativeForce(Vector3.up * moveForce);

        if (!thrustParticleSystem.isPlaying)
        {
            thrustParticleSystem.Play();
        }
    }

    private void StopThrusting()
    {
        // TODO: Deactivate Particles, Deactivate Sound
        thrustParticleSystem.Stop();
    }
}
