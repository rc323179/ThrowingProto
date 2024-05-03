using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public float displaySpeed;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = displaySpeed;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.AddForce(new Vector2(horizontal*speed, vertical*speed), ForceMode2D.Impulse);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        

        if(horizontal == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (vertical == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
