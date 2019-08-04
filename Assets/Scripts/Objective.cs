using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    LevelChanger levelChanger;

    private void Awake() {
        levelChanger = FindObjectOfType<LevelChanger>();
        //There should be only one Level Changer on each sceen
        //TODO: Make a singleton
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelChanger.FadeNextLevel();
            //Start transition
            //When the transition has finised, switch the level
        }
    }

}
