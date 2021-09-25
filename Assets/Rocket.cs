using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f; // rcs stands for Reaction Control System which is used in rockets !
    [SerializeField] float mainThrust = 1000f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending};
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } //god condition, ignore collision when dead

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f); //parameterize time
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f); //parameterize time

                break;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); //Allow for more than two lvls
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0); 
    }

    private void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) //can thrust while roatating
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!audioSource.isPlaying) //so it doesnt layer
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);

        }
        rigidBody.freezeRotation = true; // resume physics control of rotation



    }


}
