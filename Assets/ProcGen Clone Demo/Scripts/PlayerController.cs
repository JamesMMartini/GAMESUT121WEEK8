using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 8;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        float newX = 0.0f;
        float newY = 0.0f;
        if (xInput > 0.05f || xInput < -0.05f)
            newX = xInput;

        if (yInput > 0.05f || yInput < -0.05f)
            newY = yInput;

        Debug.Log(newX + " " + newY);

        rb.velocity = new Vector2(newX * speed, newY * speed);
    }
}
