using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    public Rigidbody2D rb;
    public Animator animator;
    public AIDestinationSetter destinationSetter;
    private Vector2 movement;


    void Start()
    {
        int target = Random.Range(0, 1);
        if (target == 0)
        {
            destinationSetter.target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            Debug.Log(GameObject.FindGameObjectsWithTag("Player")[0]);
        }
        else
        {
            destinationSetter.target = GameObject.FindGameObjectsWithTag("NPC")[0].transform;
            Debug.Log(GameObject.FindGameObjectsWithTag("NPC")[0]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
    }

    void FixedUpdate()
    {
        
    }
}
