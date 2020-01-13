using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{

    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 20f;
    Rigidbody rigidBody;
    AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){

        Thrust();
        Rotate();

    }

    private void Rotate(){

        float rotationSpeed = Time.deltaTime * rcsThrust;

        // taking care of the rotation
        rigidBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A)){
            
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D)){
            
            transform.Rotate(-Vector3.forward * rotationSpeed);

        }

        rigidBody.freezeRotation = false;
    }

    private void Thrust(){

        float thrustSpeed = Time.deltaTime * rcsThrust;

        if (Input.GetKey(KeyCode.Space)){

            //si no fuera relative, no giraria en up local sino global
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);

            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();

    }

}
