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

    public string playerName1;
    public string playerName2;
    
    IEnumerator Upload()
    {

        WWWForm form = new WWWForm();
        form.AddField("lost", name);
        form.AddField("player1", playerName1);
        form.AddField("player2", playerName2);
        form.AddField("match_duration", Time.time.ToString("f2"));

        UnityWebRequest www = UnityWebRequest.Post("http://ptsv2.com/t/3v5do-1590155793/post", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
    
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
        playerName1 = "player_server";
        playerName2 = "player_client";

        if (!isLocalPlayer)
        {
            GetComponent<SnakeMovement>().enabled = false;
        }

        if (this.isLocalPlayer)
        {
            name = playerName1;
        }
        else
        {
            name = playerName2;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var snakeParts = bodyHolder.GetComponentsInChildren<SnakeBodypart>();

        StartCoroutine(Upload());

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
