using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string button;

    public ScoreManager scoreManager;

    public AudioSource deathSource;
    public AudioClip deathClip;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet") && gameObject.CompareTag("Enemy"))
        {
            scoreManager.score++;
            Destroy(other.gameObject);
            deathSource.PlayOneShot(deathClip);
            gameObject.tag = "Untagged";
            GetComponentInChildren<EnemyGFX>().isDead = true;
            gameObject.GetComponent<AIPath>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
