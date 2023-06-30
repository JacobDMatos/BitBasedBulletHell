using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI timer;
	public AudioClip pickupSFX;

	private float seconds;
	private float minutes;
	private int score = 0;
	private int row;
	private int column;

    void Start()
    {
		
    }

	void Update()
    {
		minutes = (int)(Time.time / 60.0f);
		seconds = (int)(Time.time % 60.0f);
		timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
	}

	void FixedUpdate()
	{
		float hor = 0;
		float ver = 0;
		if (Input.GetKey("a"))
			hor = -1;
		if (Input.GetKey("d"))
			hor = 1;
		if (Input.GetKey("w"))
			ver = 1;
		if (Input.GetKey("s"))
			ver = -1;
		Vector3 vel = new Vector3(hor, 0, ver);
		GetComponent<Rigidbody>().position += vel * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("Pickup"))
		{
			other.gameObject.SetActive(false);
			AudioSource.PlayClipAtPoint(pickupSFX, new Vector3(5, 10, 5));
			score++;
			SetScore();
			SetPickupSpawn(other);
			other.gameObject.SetActive(true);
		}
	}

	void SetScore()
    {
		scoreText.text = "Score: " + score.ToString();
    }

	void SetPickupSpawn(Collider other)
    {
		System.Random r = new System.Random();
		row = r.Next(0, 8);
		column = r.Next(0, 8);
		other.transform.position = new Vector3(row + 1, 0.125f, column + 1);
    }
}
