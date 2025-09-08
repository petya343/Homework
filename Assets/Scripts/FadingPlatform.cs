using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatform : MonoBehaviour
{
    private bool isOnPlatform = false;
    private float timePassed;
    private float timeToFade = 1f;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timePassed >= timeToFade)
        {
            Destroy(gameObject);
        }
        if (isOnPlatform)
        {
            timePassed += Time.fixedDeltaTime;
            transform.Translate(0, -0.5f * Time.fixedDeltaTime, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            isOnPlatform = true;
        }
    }
}
