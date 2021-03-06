using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Shooting : MonoBehaviour
{
    public enum ShootState
    {
        WAITING, AIMING, SHOOTING, RELOADING
    }
    
    
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    private Rigidbody2D firePointRB;

    private string availableTargets;
    private ShootState state = ShootState.WAITING;
    public float fireRate;
    public float bulletForce = 20f;

    private bool allowFire = true;

    public AudioSource gunShotSource;
    public AudioClip gunShotAudio;



    private void Start()
    {
        firePointRB = firePoint.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("NPC"))
        {
            availableTargets = "uplmn";
        }
        else if (gameObject.CompareTag("Player"))
        {
            availableTargets = "rqazc";
        }
        
        foreach (char key in availableTargets.ToCharArray())
        {
            if (Input.GetKeyDown(key.ToString()))
            {
                state = ShootState.AIMING;
                // for all the enemies
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    // if enemy matches button pressed
                    if (enemy.GetComponent<Enemy>().button == key.ToString())
                    {
                        // Look towards enemy
                        Vector2 lookDir = enemy.GetComponent<Rigidbody2D>().position - firePointRB.position;
                        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                        firePoint.rotation = Quaternion.Euler(Vector3.forward * angle);
                        state = ShootState.SHOOTING;
                    }
                }
                
                if (state == ShootState.AIMING)
                {
                    float angle = 0;
                    if (gameObject.CompareTag("NPC"))
                    {
                        angle = 60 - (30 * availableTargets.IndexOf(key)) - 90f;
                    }
                    else if (gameObject.CompareTag("Player"))
                    {
                        angle = 120 + (30 * availableTargets.IndexOf(key)) - 90f;
                    }

                    firePoint.rotation=Quaternion.Euler(Vector3.forward * angle);
                    state = ShootState.SHOOTING;
                }
                
            }
        }
    }

    private void FixedUpdate()
    {
        if (state == ShootState.SHOOTING && allowFire)
        {
            StartCoroutine(Shoot());
            state = ShootState.WAITING;
        }
    }

    IEnumerator Shoot()
    {
        allowFire = false;
        gunShotSource.PlayOneShot(gunShotAudio);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        animator.SetTrigger("shot");
        yield return new WaitForSeconds(1/fireRate);
        allowFire = true;
    }

    void findEnemy()
    {
        
    }
}
