using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    Vector3 direction = new Vector3(0, 0, 0);
    int x = 0;
    int y = 0;

    void Start()
    {
        InvokeRepeating("StepUpdate", 0, Snake.timeStep);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
            if(y != -1)
            {
                x = 0;
                y = 1;
            }


        if (Input.GetKeyDown(KeyCode.DownArrow))
            if (y != 1)
            {
                x = 0;
                y = -1;
            }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            if (x != 1)
            {
                x = -1;
                y = 0;
            }

        if (Input.GetKeyDown(KeyCode.RightArrow))
            if (x != 1)
            {
                x = 1;
                y = 0;
            }

        direction = new Vector3(x, y, 0);
    }


    void StepUpdate()
    {
        transform.position += direction;
    }
}
