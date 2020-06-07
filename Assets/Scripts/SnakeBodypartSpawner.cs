using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Snake))]
public class SnakeBodypartSpawner : MonoBehaviour {

    public GameObject snakeBodypart;

    Snake snake;
    Vector3 oldPosition;

    void Start () {
        snake = GetComponent<Snake>();
        oldPosition = transform.position;
        snakeBodypart.GetComponent<SpriteRenderer>().color = snake.bodyColor;
    }


    void LateUpdate () {

        float distanceMoved = Vector3.Distance(transform.position, oldPosition);

        if (distanceMoved > 0.95f)
        {
            GameObject newSnakePart = Instantiate(snakeBodypart, oldPosition, Quaternion.identity, snake.bodyHolder);
            newSnakePart.GetComponent<SnakeBodypart>().AddLifeTime(Snake.timeStep * snake.lenght);
        }

        oldPosition = transform.position;
    }
}
