using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{

    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 20f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip winSound;

    //Particles
    [SerializeField] ParticleSystem boostParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem winParticles;
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

            RespondToThrustInput();
            RespondToRotateInput();

        }
        
    }

    void OnCollisionEnter(Collision collision){

        if (state != State.Alive){

            return; //ignore collisions when dead

        }

        switch(collision.gameObject.tag){

            case "Friendly":

            break;

            case "Finish":
                audioSource.Stop();
                audioSource.PlayOneShot(winSound);
                winParticles.Play();
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);
            break;

            default:
                audioSource.Stop();
                audioSource.PlayOneShot(deathSound);
                deathParticles.Play();
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
            break;
        }

    }

    private void RespondToRotateInput(){

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

    private void RespondToThrustInput(){

        if (Input.GetKey(KeyCode.Space)){
            ApplyThrust();
        }
        else{
            audioSource.Stop();
            boostParticles.Stop();
        } 

    }

    private void ApplyThrust(){

        float thrustSpeed = Time.deltaTime * rcsThrust;

        //si no fuera relative, no giraria en up local sino global
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);

            if (!audioSource.isPlaying){
                audioSource.PlayOneShot(mainEngine);
                boostParticles.Play();
            }

    }

    private void LoadNextScene(){
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel(){
        SceneManager.LoadScene(0);
    }

}
