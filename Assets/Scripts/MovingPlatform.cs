using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float leftLimit;
    private float rightLimit;
    private int direction;
    private float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        leftLimit = transform.position.x - 2;
        rightLimit = transform.position.x + 2;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, 0, 0);
    
        if (transform.position.x > rightLimit)
        {
            direction = -1;
        }
        if (transform.position.x < leftLimit )
        {
            direction = 1;
        }
    }
}
