using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraHolder;
    public float baseSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float sprintSpeed = 5f;
    public float mouseSensitivity = 100f;

    private float speedBoost = 1f;
    private Vector3 velocity;
    private float xRotation = 0f;

    public AudioSource walkAudio; 
    public float stepDelay = 0.5f; 
    private float stepTimer = 0f;

    
    
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = 0.223308f;
            controller.center = new Vector3(0, 1f, 0);
        }
        else
        {
            controller.height = 2f;
            controller.center = new Vector3(0, 0.223308f / 2, 0);
        }


        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        speedBoost = Input.GetButton("Fire3") ? sprintSpeed : 1f;
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * (baseSpeed + speedBoost) * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (controller.isGrounded && move.magnitude > 0) 
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepDelay)
            {
                if (!walkAudio.isPlaying) 
                {
                    walkAudio.Play();
                }
                stepTimer = 0f;
            }
        }
        else
        {
            walkAudio.Stop(); 
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
