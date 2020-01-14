using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{

    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 20f;
    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending};
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){

        if (state == State.Alive){

            Thrust();
            Rotate();

        }
        

    }

    void OnCollisionEnter(Collision collision){

        switch(collision.gameObject.tag){

            case "Friendly":

            break;

            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);
            break;

            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
            break;
        }

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

    private void LoadNextScene(){
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel(){
        SceneManager.LoadScene(0);
    }

}
