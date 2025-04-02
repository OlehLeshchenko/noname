// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//     public float speed = 5f;
//     public float sensitivity = 2f;
//     private CharacterController controller;
//     private Vector3 velocity;
//     private float rotationX = 0f;

//     void Start()
//     {
//         controller = GetComponent<CharacterController>();
//         // Cursor.lockState = CursorLockMode.Locked; // Блокируем курсор
//     }

//     void Update()
//     {
//         // Движение
//         float moveX = Input.GetAxis("Horizontal");
//         float moveZ = Input.GetAxis("Vertical");
//         Vector3 move = transform.right * moveX + transform.forward * moveZ;
//         controller.Move(move * speed * Time.deltaTime);

//         // Гравитация
//         if (controller.isGrounded && velocity.y < 0)
//         {
//             velocity.y = -2f;
//         }
//         velocity.y += Physics.gravity.y * Time.deltaTime;
//         controller.Move(velocity * Time.deltaTime);

//         // Вращение камеры (мышь)
//         float mouseX = Input.GetAxis("Mouse X") * sensitivity;
//         float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
//         rotationX -= mouseY;
//         rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Ограничение обзора
//         Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
//         transform.Rotate(Vector3.up * mouseX);
//     }
// }