using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private int jumpCounter = 0;
    
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    
    public float jumpForce = 10;
    public float gravityModifier;
    
    public bool gameOver = false;
    public bool doubleSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        PlayerMovement();

        RushAbility();
    }

    void PlayerMovement()
    {
        // While Space is pressed, game is not over and jump counter less than 2, jump
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && jumpCounter < 2)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", -1, 0.0f);  // Play jump animation and restart it while double jump
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            jumpCounter++;
        }
    }

    void RushAbility()
    {
        // While LeftShift is pressed, double speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if player collides with ground, continue the game and dirt particles
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCounter = 0;
            dirtParticle.Play();
        }
        // if player collides with obstacle, explode and stop the game
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
