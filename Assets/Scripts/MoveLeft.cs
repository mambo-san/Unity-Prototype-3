using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 0;
    private float normalSpeed = 25;
    private float fastSpeed = 50;
    private PlayerControl playerControlScript;
    private float leftBound = -10;
    private int distanceTraveled = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlScript.hasGameStarted())
        {
            if (playerControlScript.jumpCount == 0 && Input.GetKey(KeyCode.W))
            {
                speed = fastSpeed;
            }
            else if (playerControlScript.jumpCount == 0 && !Input.GetKey(KeyCode.W))
            {
                speed = normalSpeed;
            }
            else
            {
                //Keep the current speed (as you should not be able to change speed mid air)
            }

            if (playerControlScript.isGameOver() == false)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                distanceTraveled += ((int)speed / 10);
                PrintScore();
            }
            if (gameObject.CompareTag("Obstacle") && transform.position.x < leftBound)
            {
                Destroy(gameObject);
            }
        }
    }

    void PrintScore()
    {
        Debug.Log("Score: " + distanceTraveled);
    }
}
