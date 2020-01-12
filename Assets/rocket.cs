using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){

        ProcessInput();

    }

    private void ProcessInput(){

        if (Input.GetKey(KeyCode.Space)){

            //si no fuera relative, no giraria en up local sino global
            rigidBody.AddRelativeForce(Vector3.up);

        }

        if (Input.GetKey(KeyCode.A)){
            
        }
        else if (Input.GetKey(KeyCode.D)){
            
        }
    }
}
