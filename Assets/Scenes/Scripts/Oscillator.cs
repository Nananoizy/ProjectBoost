using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(0f, -9f, 0f);

    [SerializeField] float period = 9f;
    float movementFactor;

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float cycles = Time.time / period; //grows continiously from 0

        const float tau = Mathf.PI * 2f;
        float rawSinValue = Mathf.Sin(cycles * tau);

        movementFactor = rawSinValue / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
