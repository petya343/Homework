using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
//using Codice.Client.BaseCommands.BranchExplorer.Layout;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isOnPlatform = true;
    private float jumpForce = 8;
    private Rigidbody2D rigidBody;
    private Vector2 platformVelocity;
    private Animator animator;
    private Camera cam;
    private ScreenShaker screenShaker;
 
    private GameObject lastPlatform;

    private GameObject blood;
    private Animator bloodAnimator;

    private int lives = 3;
    private int keys = 0;
    private bool power = false;

    [SerializeField]
    private Hearts heartsUI;
    [SerializeField]
    private Keys keysUI;
    [SerializeField]
    private PowerUI powerUI;

    public Volume volume;
    public Vignette vignette;

    private IScreenShaker screenShakerService;
    private IHearts heartsUIService;

    public void SetScreenShaker(ScreenShaker shaker) => screenShakerService = shaker;
    public void SetHeartsUI(Hearts hearts) => heartsUIService = hearts;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        if (cam != null)
        {
            screenShaker = cam.GetComponent<ScreenShaker>();
        }
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        if (cam != null && screenShakerService == null)
        {
            screenShakerService = cam.GetComponent<ScreenShaker>();
        }

        if (heartsUI != null && heartsUIService == null)
        {
            heartsUIService = heartsUI;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            transform.Translate(5 * Time.deltaTime, 0, 0);
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            transform.Translate(-5 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.W) && isOnPlatform)
        {

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

        }
        rigidBody.rotation = 0f;
        animator.SetFloat("yVelocity", rigidBody.velocity.y);
        if(cameraView(transform.position).y < 0 && lives > 0)
        {
            takeDamage();
            VignetteEffect();
            if(lives > 0)
            {
               Respawn();
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
            animator.SetBool("IsJumping", false);

            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    lastPlatform = collision.gameObject;
                }
            }
        }

        if(collision.gameObject.name == "Jumping pad")
        {
            JumpingPad();
        }

        if(collision.gameObject.name == "mouse")
        {
            if (!power)
            {
                takeDamage();
                VignetteEffect();
            }
            else
            {
                Destroy(collision.gameObject);
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                powerUI.LosePower();
                power = false;

                Transform blood = collision.gameObject.transform.parent.Find("blood");
                bloodAnimator = blood.GetComponent<Animator>();

                blood.gameObject.SetActive(true);
                bloodAnimator.Play("blood", 0, 0f);
            }
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
            animator.SetBool("IsJumping", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coin"))
        {
            ++keys;
            Destroy(collider.gameObject);
            keysUI.SetKeys(keys);
        }
        if (collider.gameObject.name == "pink_power")
        {
            Destroy(collider.gameObject);
            spriteRenderer.color = new Color(1f, 0.6f, 0.9f, 1f);
            powerUI.GetPower();
            power = true;
        }

    }

    public void Respawn()
    {
        if(lastPlatform != null)
        {
            transform.position = new Vector2(lastPlatform.transform.position.x + 1f, lastPlatform.transform.position.y + 2f);
            rigidBody.velocity = Vector2.zero;
            isOnPlatform = true;                       
            animator.SetBool("IsJumping", false);
        }
    }
    public void takeDamage()
    {
        --lives;
        //if (screenShaker != null)
        //{
        //    screenShaker.ScreenShake();
        //}
        //if (heartsUI != null)
        //{
        //    heartsUI.SetLives(lives);
        //}
        screenShakerService?.ScreenShake();
        heartsUIService?.SetLives(lives);

    }

    public void JumpingPad()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * 1.25f);
    }
    public int KeysCounter()
    {
        return keys;
    }

    public int LivesCounter()
    {
        return lives;
    }

    public Vector3 cameraView(Vector3 pos)
    {
        return cam.WorldToViewportPoint(pos);
    }

    private void VignetteEffect()
    {
        if (lives == 1)
        {
            volume = FindObjectOfType<Volume>();
            if (volume != null && volume.profile.TryGet(out vignette))
            {
                vignette.active = true;
            }
        }
    }
}
