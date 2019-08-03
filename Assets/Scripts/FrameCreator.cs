using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCreator : MonoBehaviour {

    public GameObject frame;
    public int instances;
    public float startingDist;
    public float distanceIncrease;
    public float distanceExp;

    void Start() {
        for (int i = 0; i < instances; ++i)
        {
            Instantiate(frame, new Vector3(0f, 0f, distanceIncrease * i + Mathf.Pow(distanceExp, i)), Quaternion.identity);
        }
    }

    void Update() {
        
    }
}
