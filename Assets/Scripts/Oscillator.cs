using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;   
    [SerializeField] float period = 2f;

    float movementFactor;

    //[SerializeField] [Range(0,1)] float movementFactor;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (period <= Mathf.Epsilon) { return; }

        const float tau = Mathf.PI * 2; //constant that represents the whole circle

        float cycles = Time.time / period; // time to go between lapses

        movementFactor = (Mathf.Sin(tau * cycles) + 1) / 2; // recalculate to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;

        Vector3 newPos = startingPosition + offset;
        transform.position = newPos;
    }
}
