using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("Movement")] float thrustSpeed = 10;
    [SerializeField, Header("Movement")] float rotationSpeed = 10;

    Rigidbody _rigidBody;
    [SerializeField, Header("Audio")] AudioSource audioSource;
    float _verticalMovement = 0f;
    float _rotationalMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

    }

    void FixedUpdate()
    {
        Fly();
        PlayRocketSound();
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
        if ((!audioSource.isPlaying || audioSource.volume < 0.1f) && _verticalMovement > 0) 
        {
            audioSource.volume = 0.1f;
            audioSource.Play();
        }
        
        if (_verticalMovement == 0)
        {
            audioSource.volume -= 0.0025f;
            if (audioSource.volume == 0)
            {
                audioSource.Stop();
            }
        }
    }
}
