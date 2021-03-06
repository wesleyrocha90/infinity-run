﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject moveSound;
    public GameObject hitSound;
    public GameObject hitParticle;
    public Text textHealth;

    private float health = 3f;
    private Rigidbody2D rb;

    // Events
    public delegate void PlayerDead(GameObject player);
    public static event PlayerDead OnPlayerDead;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        textHealth.text = health.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown("w") && transform.position.y < 3)
        {
            Instantiate(moveSound, gameObject.transform.position, Quaternion.identity);
            rb.MovePosition(transform.position + new Vector3(0, 3, 0));
        }
        if (Input.GetKeyDown("s") && transform.position.y > -3)
        {
            Instantiate(moveSound, gameObject.transform.position, Quaternion.identity);
            rb.MovePosition(transform.position + new Vector3(0, -3, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        textHealth.text = (--health).ToString();
        Instantiate(hitSound, collider.gameObject.transform.position, Quaternion.identity);
        Instantiate(hitParticle, collider.gameObject.transform.position, Quaternion.identity);
        Destroy(collider.gameObject);
        if (health <= 0)
        {
            OnPlayerDead(gameObject);
        }
    }
}
