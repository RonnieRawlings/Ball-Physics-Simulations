// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    // Simulations in the scene & starting sim points.
    public List<GameObject> sceneSimulations;
    public List<GameObject> simPoints;

    // Change in added points Y.
    private float yPosChange = -7.5f;

    /// <summary> method <c>ResetAllSimulations</c> resets the scene, in-turn reseting all simulations that were running. </summary>
    public void ResetAllSimulations()
    {
        // Reloads active scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    /// <summary> method <c>AddASimulation</c> adds another simulation into the scene. </summary>
    public void AddASimulation()
    {
        // Instantiates new simulation.
        Object newSimPrefab = Resources.Load("Prefabs/Simulations/BallEscape");
        GameObject newSim = Instantiate(newSimPrefab) as GameObject;

        // Adds sim to running simulations.
        sceneSimulations.Add(newSim);

        // Sets new positions for each sim in scene.
        SetSimulationPositions();
    }

    bool isOther;

    /// <summary> method <c>SetSimulationPositions</c> moves the simulation positions around so they all fit. </summary> 
    public void SetSimulationPositions()
    {
        // Only add a new sim point if both starters have been used.
        if (sceneSimulations.Count > simPoints.Count)
        {
            // Determines which X cord to use.
            float xPos;
            if (!isOther) { xPos = simPoints[0].transform.position.x; isOther = true; }
            else { xPos = simPoints[1].transform.position.x; isOther = false; }

            // Adds new sim point.
            simPoints.Add(new GameObject("SimPoint " + sceneSimulations.Count));

            // Moves position to next.
            Vector2 newPosition;
            if ((simPoints.Count - 2) % 2 == 1) // Check if the count is odd.
            {
                // It is odd, so change the Y.
                newPosition = new Vector2(xPos, simPoints[simPoints.Count - 2].transform.position.y + yPosChange);
            }
            else
            {
                // Even so keep Y the same.
                newPosition = new Vector2(xPos, simPoints[simPoints.Count - 2].transform.position.y);
            }

            // Change to final position.
            simPoints[simPoints.Count - 1].transform.position = newPosition;

            // Increases max cam scroll.
            IncreaseMaxCameraScroll();
        }

        // Moves each sim at relevant position.
        for (int s = 0; s < sceneSimulations.Count; s++)
        {
            sceneSimulations[s].transform.position = simPoints[s].transform.position;
        }
    }

    /// <summary> method <c>IncreaseMaxCameraScroll</c> increases the max Y the camera can move, found by max simulation. </summary>
    public void IncreaseMaxCameraScroll()
    {
        // Finds cam comp, adjusts maxY.
        CameraScroll mainCamScroll = Camera.main.GetComponent<CameraScroll>();
        mainCamScroll.MaxY = simPoints[simPoints.Count - 1].transform.position.y;
    }
}
