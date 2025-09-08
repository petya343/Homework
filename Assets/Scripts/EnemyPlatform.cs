using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatform : MonoBehaviour
{
    private GameObject mouse;
    private MouseMovement mMovement;

    private void Start()
    {
        Transform mouseTransform = transform.GetChild(0);
        mouse = mouseTransform.gameObject;
        mMovement = mouse.GetComponent<MouseMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player" && mouse != null)
        {
            mMovement.BecomeAggressive();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && mouse != null)
        {
            mMovement.StopAggressing();
        }
    }
}
