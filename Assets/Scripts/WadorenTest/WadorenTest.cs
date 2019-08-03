using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WadorenTest : MonoBehaviour
{
    private Rigidbody2D rb;
    private KeyCode key = KeyCode.A;
    private float direction = 1f; //-1 or 1
    private float speed = 5f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(key) || Input.GetKey(key))
        {
            rb.velocity = new Vector2(direction * speed, 0f);
        }
        if (Input.GetKeyUp(key))
        {
            //Character turns around
            direction = -direction;
            rb.velocity = Vector2.zero;
        }
    }
}
