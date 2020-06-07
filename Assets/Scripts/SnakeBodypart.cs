using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodypart : MonoBehaviour {

    float lifeTimeRemaining = 0;

    private void Update()
    {
        ManualLifeTimeCounter();
    }

    private void ManualLifeTimeCounter()
    {
        lifeTimeRemaining -= Time.deltaTime;

        if (lifeTimeRemaining < 0)
            Destroy(gameObject);
    }

    public void AddLifeTime(float time)
    {
        lifeTimeRemaining += time;
    }
}
