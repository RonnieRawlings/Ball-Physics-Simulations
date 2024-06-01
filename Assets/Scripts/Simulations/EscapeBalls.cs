// Author - Ronnie Rawlings.

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBalls : MonoBehaviour
{
    // Amount of balls spawned each call.
    [SerializeField] private int totalBallsSpawned;

    /// <summary> method <c>SpawnNewBalls</c> spawns new balls at circle middle upon 1 ball exit. Also at increased size. </summary>
    /// <param name="escapedBall">Ball that exited scale.</param>
    public void SpawnNewBalls(GameObject escapedBall)
    {
        // Loads ball as object from resources.
        Object ballPrefab = Resources.Load("Prefabs/Sprites/Ball");

        // Instantiates ball game objs.
        GameObject newBall;
        for (int i = 0; i < totalBallsSpawned; i++)
        {
            // Adds new ball obj, increases scale.
            newBall = Instantiate(ballPrefab, transform) as GameObject;
            newBall.transform.localScale = escapedBall.transform.localScale * 1.2f;

            // Add audio clips.
            newBall.GetComponent<BallForce>().AudioClips = escapedBall.GetComponent
                <BallForce>().AudioClips;

            // Allows more balls to spawned upon exit.
            newBall.GetComponent<BallForce>().SpawnNewBalls = true;
        }
    }
}