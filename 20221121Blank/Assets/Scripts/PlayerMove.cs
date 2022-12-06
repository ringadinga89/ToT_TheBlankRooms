using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float walkSpeed, runSpeed;
    private float speed;

   
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;

    private Vector3 move, velocity;
    private bool onGround;

    [SerializeField] private float maxStamina, runCost, jumpCost;
    private float stamina;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        onGround = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        bool running = Input.GetButton("Fire3") && stamina > 0;
        speed = running ? runSpeed : walkSpeed;

        if(running)
        {
            stamina -= runCost * Time.deltaTime;

        }

        else
        {
            stamina += Time.deltaTime;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        move = transform.right * h + transform.forward * v;
        controller.Move(move.normalized * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        if (onGround && velocity.y < 0)
        {
            velocity.y = -2f;

            if(Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
                stamina -= jumpCost;
            }

        }
        controller.Move(velocity * Time.deltaTime);

        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        if (stamina <= 0)
        {
            running = false;
        }
    }

    public float GetStamina() => stamina;
    public float GetMaxStamina() => maxStamina;

}
