// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScroll : MonoBehaviour
{
    // Scrollbar UI comp.
    [SerializeField] private Scrollbar scrollbar;

    // Camera Y bounds.
    private float minY, maxY;

    #region Properties

    public float MaxY
    {
        set { maxY = value; }
    }

    #endregion

    /// <summary>
    /// method <c>ScrollCamera</c> moves the camera along the Y acordding to scrollbar value.
    /// </summary>
    /// <param name="value">Current ScrollBar pos value.</param>
    void ScrollCamera(float value)
    {
        Debug.Log(maxY);

        // Gets current pos.
        Vector3 newPosition = transform.position;

        // Lerps between min & max Y.
        newPosition.y = minY + value * (maxY - minY);

        // Sets new pos.
        transform.position = newPosition;
    }

    // Called once before first update.
    void Start()
    {
        // Starting bounds.
        minY = transform.position.y;
        maxY = 0;

        // Adds listner to scrollbar.
        scrollbar.onValueChanged.AddListener(ScrollCamera);
    }  
}
