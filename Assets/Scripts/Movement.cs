using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody myRigidbody;
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationSpeed = 100f;

    [SerializeField] ParticleSystem mainThrust;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MainThrust();
        SideThrust();
    }

    void MainThrust()
    {
        //up input
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //add force relative to the position of the rocket
            myRigidbody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);

            //particle effect
            mainThrust.Play();
        }
        else
        {
            mainThrust.Stop();
        }
    }

    void SideThrust()
    {
        Vector3 angularForce = Vector3.forward * rotationSpeed * Time.deltaTime;
        
        //left input
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody.AddRelativeTorque(angularForce);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.AddRelativeTorque(angularForce * -1.0f);
        }
    }

 
}
