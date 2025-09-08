using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private Transform target;        
    public float smoothSpeed = 0.125f;
    private Movement movement;
    
    void Start()
    {
        target = GameObject.Find("Player").transform;
        movement = target.GetComponent<Movement>();
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position; 
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        if (smoothed.y > transform.position.y)
        {
            transform.position = new Vector3(smoothed.x, smoothed.y, transform.position.z);
        }
        else if(smoothed.y > 0)
        {
            transform.position = new Vector3(smoothed.x, transform.position.y, transform.position.z);
        }
        
    }
}
