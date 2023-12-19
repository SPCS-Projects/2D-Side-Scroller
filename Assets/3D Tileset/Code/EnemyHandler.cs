using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {
    bool goRight;
    float speed;
    float interval;
    float lastSwitchTime;

    // Use this for initialization
    void Start () {
        goRight = false;
        speed = 1.5f;
        interval = 5f;
        lastSwitchTime = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (goRight == true)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(speed * -1 * Time.deltaTime, 0, 0);
        }
        if (Time.time > lastSwitchTime + interval)
        {
            lastSwitchTime = Time.time;
            if (goRight == true)
            {
                goRight = false;
            }
            else
            {
                goRight = true;
            }
        }
    }
}
