using UnityEngine;

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
        PlayRocketSound();
        ShowRocketParticles();
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


    void PlayRocketSound()
    {
        if ((!_audioSource.isPlaying || _audioSource.volume < 0.1f) && _verticalMovement > 0) 
        {
            _audioSource.clip = rocketBoost;

            _audioSource.volume = 0.1f;
            _audioSource.Play();
        }
        
        if (_verticalMovement == 0)
        {
            _audioSource.volume -= 0.0025f;
            if (_audioSource.volume == 0)
            {
                _audioSource.Stop();
            }
        }
    }

    void ShowRocketParticles()
    {
        if (_verticalMovement > 0)
        {
            rocketJetParticles.Play();
        }
        else
        {
            rocketJetParticles.Stop();
        }
    }

    public void DisableControls()
    {
        _disableControls = true;
        rocketJetParticles.Stop();
    }
}
