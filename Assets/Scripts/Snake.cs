using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;
using System.Collections.Specialized;
using System;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using System.Globalization;
using UnityEngine.UI;
using System.Diagnostics;

public class Snake : NetworkBehaviour
{
    public static float timeStep = 0.1f;
    static int numberOfPlayers = 0;

    [SyncVar]
    public int lenght = 1;

    [HideInInspector]
    public Transform bodyHolder;

    public Color playerOneColor;
    public Color playerTwoColor;
    public Color bodyColor;

    public Vector3[] startPositions;


    void Awake()
    {
        
        bodyHolder = new GameObject("Snake").transform;
        transform.parent = bodyHolder;

        if (numberOfPlayers == 0)
            GetComponent<SpriteRenderer>().color = playerOneColor;
            
        if (numberOfPlayers == 1)
            GetComponent<SpriteRenderer>().color = playerTwoColor;


        if (numberOfPlayers > 2)
        {
            Destroy(gameObject);
            return;
        }
        
        transform.position = startPositions[numberOfPlayers];
        numberOfPlayers++;
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<SnakeMovement>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var snakeParts = bodyHolder.GetComponentsInChildren<SnakeBodypart>();
        
        foreach (var part in snakeParts)  
            part.enabled = false;

        gameObject.SetActive(false);
        numberOfPlayers = 0;
        SceneManager.LoadScene("Menu");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Apple")
        {
            lenght += 3;
            var snakeParts = bodyHolder.GetComponentsInChildren<SnakeBodypart>();
            foreach (var part in snakeParts)   
                part.AddLifeTime(3 * timeStep);     
            
            Destroy(other.gameObject);
        }
    }
}
