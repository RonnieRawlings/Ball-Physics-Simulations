// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoWillWin : MonoBehaviour
{
    // Has the simulation finished, win settings triggered.
    private bool simOver = false;

    /// <summary> method <c>CheckForRemainingBalls</c> checks the parent for remaining balls, if none this ball has won. </summary>
    public void CheckForRemainingBalls()
    {
        // Finds all balls in the simulation.
        BallForce[] totalBallsRemaining = transform.parent.GetComponentsInChildren<BallForce>();

        // This is the only ball remanining, trigger win.
        if (totalBallsRemaining.Length <= 1)
        {
            simOver = true;
            StartCoroutine(TriggerWinSettings());
        }    
    }

    /// <summary> coroutine <C>TriggerWinSettings</C> this obj has won the simulation, pause force & increase size rapidly. </summary>
    public IEnumerator TriggerWinSettings()
    {
        // Prevents ball movement.
        GetComponent<BallForce>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());

        // Move to center.
        transform.position = transform.parent.position;

        // While scale not reached, increase scale.
        int maxScale = 30;
        while (transform.localScale.x < maxScale)
        {   
            transform.localScale *= 1.3f;
            yield return new WaitForSeconds(0.05f);
        }      
    }


    // Update is called once per frame
    void Update()
    {
        if (!simOver) { CheckForRemainingBalls(); }
    }
}
