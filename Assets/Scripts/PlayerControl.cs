using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    
    public AudioClip jumpSound;
    public AudioClip crashSound;
    
    public float jumpForce = 10;
    public float gravityModifier;
    
    public int jumpCount = 0;

    private bool gameStared;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;

        Invoke("StartGame", 5);
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        if (gameStared)
        {
            if (jumpCount < 2 && !gameOver && Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 0.75f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameStared)
        {
            //Player is able to jump again
            if (collision.gameObject.CompareTag("Ground"))
            {
                jumpCount = 0;
                dirtParticle.Play();
            }//Game Over - Crashed into obstacle
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                jumpCount = 0;
                gameOver = true;
                Debug.Log("Game Over!");
                playerAnim.SetBool("Death_b", true);
                explosionParticle.Play();
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSound);
            }
        }
    }


    public bool isGameOver()
    {
        return gameOver;
    }

    private void StartGame()
    {
        gameStared = true;
        playerAnim.SetFloat("Speed_f", 1);
    }

    public bool hasGameStarted()
    {
        return gameStared;
    }
}
