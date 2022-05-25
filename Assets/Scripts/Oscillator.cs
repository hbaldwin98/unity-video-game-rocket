using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    
    float _movementFactor;
    Vector3 _startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (period <= Mathf.Epsilon) return;
        
        const float tau = Mathf.PI * 2f; // constant value of 6.283
        float cycles = Time.time / period; // number of cycles since start of game
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1
        
        _movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1
        
        
        Vector3 offset = movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
