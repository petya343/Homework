using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float speed = 0.5f;
    private bool isAggressive = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        if(isAggressive)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            if (direction.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x < 0) 
            {
                spriteRenderer.flipX = false;
            }
                transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
        
    }

    public void BecomeAggressive()
    {
        isAggressive = true;
        animator.SetBool("isAggressive", true);
    }

    public void StopAggressing()
    {
        isAggressive = false;
        animator.SetBool("isAggressive", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            isAggressive = false;
        }
    }

}
