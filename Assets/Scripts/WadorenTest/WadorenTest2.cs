using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WadorenTest2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private KeyCode key = KeyCode.A;
    private float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(key) || Input.GetKey(key))
        {
            rb.velocity = new Vector2(speed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0f);
        }
    }
}
