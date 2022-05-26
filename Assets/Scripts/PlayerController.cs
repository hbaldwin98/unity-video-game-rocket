using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float thrustSpeed;
    [SerializeField] float rotationSpeed;

    [Header("Audio")]
    [SerializeField] AudioClip rocketBoost;

    [Header("Particles")] 
    [SerializeField] ParticleSystem rocketJetParticles;

    Rigidbody _rigidBody;
    AudioSource _audioSource;
    float _verticalMovement;
    float _rotationalMovement;
    bool _disableControls;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        if (_disableControls) return;
        
        Fly();
        ProcessSound();
        ProcessParticles();
    }
    
    void ProcessInput()
    {
        _verticalMovement = Input.GetAxisRaw("Jump");
        _rotationalMovement = Input.GetAxis("Horizontal");
    }

    void Fly()
    {
        _rigidBody.freezeRotation = true;
        Vector3 rotation = new Vector3(0, 0, rotationSpeed * -_rotationalMovement * Time.fixedDeltaTime);
        transform.Rotate(rotation);
        _rigidBody.freezeRotation = false;
        _rigidBody.AddRelativeForce(Vector3.up * (thrustSpeed * _verticalMovement * Time.fixedDeltaTime));
    }


    void ProcessSound()
    {
        // if we are not rocket boosting
        // lower the sound until it hits 0
        // than stop it
        if (_verticalMovement == 0)
        {
            _audioSource.volume -= 0.0025f;
            if (_audioSource.volume <= Mathf.Epsilon)
            {
                _audioSource.Stop();
            }

            return;
        }

        if (_audioSource.isPlaying && _audioSource.volume == 0.1f) return; 
        // plays the rocket boost sound
        _audioSource.clip = rocketBoost;
        _audioSource.volume = 0.1f;
        _audioSource.Play();
        
    }

    void ProcessParticles()
    {
        // stop the particles of our movement is zero
        if (_verticalMovement == 0)
        {
            rocketJetParticles.Stop();
            return;
        }
        
        rocketJetParticles.Play();
    }

    public void DisableControls()
    {
        // disables all controls and stops the particles
        // from emitting as they will continue emitting otherwise
        _disableControls = true;
        rocketJetParticles.Stop();
    }
}
