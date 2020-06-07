using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class AppleSpawner : NetworkBehaviour
{
    public int maxApples = 5;
    public GameObject apple;
    public int x = 80;        
    public int y = 42;

	public override void OnStartServer() {
        InvokeRepeating("SpawnApples", 0, 5); 
    }
	

	void SpawnApples()
    {
        for (int i = 0; i < maxApples - GetCurrentAmountOfApples(); i++)
            SpawnApple();
    }


    private void SpawnApple()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0);
        GameObject newApple = Instantiate(apple, spawnPos, transform.rotation, transform);
        NetworkServer.Spawn(newApple);
    }


    private static int GetCurrentAmountOfApples()
    {
        return GameObject.FindGameObjectsWithTag("Apple").Length;
    }
}
