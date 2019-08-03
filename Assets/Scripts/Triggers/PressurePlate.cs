using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Trigger
{
    private Vector3 moveDown = new Vector3(0f, -0.125f, 0f);
    private Transform model;

    private void Start()
    {
        model = transform.Find("Model");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            model.position += moveDown;
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            model.position -= moveDown;
            active = false;
        }
    }


}
