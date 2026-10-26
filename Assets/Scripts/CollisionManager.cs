using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip landingAudioClip;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private ParticleSystem landingParticleSystem;

    AudioSource audioSource;

    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("Launchpad Collision Detected");
                break;
            case "LandingPad":
                ManageCollision(landingAudioClip);
                //landingParticleSystem.Play();
                break;
            case "Explosion":
                ManageCollision(explosionAudioClip);
                //explosionParticleSystem.Play();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Manages the collision by disabling player control, 
    /// stopping ongoing audio, playing the specified audio clip
    /// </summary>
    /// <param name="audio"></param>
    private void ManageCollision(AudioClip audio)
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(audio);
        GetComponent<PlayerMovement>().enabled = false;
    }
}
