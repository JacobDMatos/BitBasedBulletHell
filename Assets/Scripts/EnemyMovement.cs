using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public GameObject enemy;
    public AudioClip deathSFX;
    public GameObject gameOver;

    private float z;
    private float x;
    private Vector3 dir;
    private Vector3 vel;
    private float closeDelay;
    private bool isPlayerAlive = true;
    private bool isMoving = false;

    void Start()
    {
        gameOver.SetActive(false);
        InvokeRepeating("MilestoneCheck", 30.0f, 30.0f);
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Time.time >= 2.0f)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, speed);
                isMoving = true;
            }
        }

        if (Time.time >= 90.0f)
        {
            CancelInvoke("MilestoneCheck");
        }

        if (!isPlayerAlive)
        {
            closeDelay += Time.deltaTime;
            if (closeDelay > 5.0f)
            {
                Application.Quit();
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        vel = GetComponent<Rigidbody>().velocity;

        if (other.gameObject.CompareTag("LeftWall"))
        {
            z = vel.z;
            dir = new Vector3(1, 0, z).normalized;
            GetComponent<Rigidbody>().velocity = dir * speed;
        } else if (other.gameObject.CompareTag("RightWall"))
        {
            z = vel.z;
            dir = new Vector3(-1, 0, z).normalized;
            GetComponent<Rigidbody>().velocity = dir * speed;
        } else if (other.gameObject.CompareTag("TopWall"))
        {
            x = vel.x;
            dir = new Vector3(x, 0, -1).normalized;
            GetComponent<Rigidbody>().velocity = dir * speed;
        } else if (other.gameObject.CompareTag("BottomWall"))
        {
            x = vel.x;
            dir = new Vector3(x, 0, 1).normalized;
            GetComponent<Rigidbody>().velocity = dir * speed;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(deathSFX, new Vector3(5, 10, 5));
            gameOver.SetActive(true);
            isPlayerAlive = false;
        }
    }

    void MilestoneCheck()
    {
        Instantiate(enemy, new Vector3(Random.Range(1.0f, 9.0f), 0.25f, Random.Range(1.0f, 9.0f)), Quaternion.identity);
    }
}