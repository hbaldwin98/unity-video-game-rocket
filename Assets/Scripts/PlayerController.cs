using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float thrustSpeed = 10;
    [SerializeField] float rotationSpeed = 10;

    [Header("Audio")]
    [SerializeField] AudioSource rocketBoost;
    
    
    Rigidbody _rigidBody;
    float _verticalMovement = 0f;
    float _rotationalMovement;
    bool _disableControls = false;
    
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
        if (!_disableControls)
        {
            Fly();
            PlayRocketSound();
        }
        else
        {
            rocketBoost.Stop();
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
        if ((!rocketBoost.isPlaying || rocketBoost.volume < 0.1f) && _verticalMovement > 0) 
        {
            rocketBoost.volume = 0.1f;
            rocketBoost.Play();
        }
        
        if (_verticalMovement == 0)
        {
            rocketBoost.volume -= 0.0025f;
            if (rocketBoost.volume == 0)
            {
                rocketBoost.Stop();
            }
        }
    }

    public void DisableControls()
    {
        _disableControls = true;
    }
}
