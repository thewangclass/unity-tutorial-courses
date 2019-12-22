using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    [SerializeField] private float horsePower = 0;
    [SerializeField] private float turnSpeed = 25.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
    [SerializeField] private GameObject centerOfMass;

    // Speed UI
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] float speed;

    // RPM UI
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float rpm;

    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Player Controllers
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (isOnGround())
        {
            // Move the vehicle forward based on vertical input
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);

            // Turn vehicle right/left based on horizontal input
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            // Speed UI
            speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f); // 3.6 for kph
            speedometerText.SetText("Speed: " + speed + " mph");

            // RPM UI
            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);
        }
    }

    bool isOnGround()
    {
        wheelsOnGround = 0;

        foreach(WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if(wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
