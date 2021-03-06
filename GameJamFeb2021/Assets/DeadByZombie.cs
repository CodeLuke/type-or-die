using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadByZombie : MonoBehaviour
{
    public GameObject deathText;

    private void Start()
    {
        deathText.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Enemy"))
        {
            deathText.SetActive(true);
            Destroy(gameObject);
        }
    }
}
