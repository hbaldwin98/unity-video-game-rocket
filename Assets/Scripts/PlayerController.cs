using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float thrustSpeed = 10;
    [SerializeField] float rotationSpeed = 10;

    [Header("Audio")]
    [SerializeField] AudioClip rocketBoost;

    Rigidbody _rigidBody;
    AudioSource _audioSource;
    float _verticalMovement = 0f;
    float _rotationalMovement;
    bool _disableControls = false;
    
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
        if (!_disableControls)
        {
            Fly();
            PlayRocketSound();
        }
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
        _audioSource.clip = rocketBoost;
        if ((!_audioSource.isPlaying || _audioSource.volume < 0.1f) && _verticalMovement > 0) 
        {
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

    public void DisableControls()
    {
        _disableControls = true;
    }
}
