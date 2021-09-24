using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();

        Rotate();
        
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust while roatating
        {
            print("thrusting");
            rigidBody.AddRelativeForce(Vector3.up);
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
        if (Input.GetKey(KeyCode.A))
        {
            print("Left");
            transform.Rotate(Vector3.forward);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("right");
            transform.Rotate(-Vector3.forward);

        }
        rigidBody.freezeRotation = true; // resume physics control of rotation



    }


}
