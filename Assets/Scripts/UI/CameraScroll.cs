// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScroll : MonoBehaviour
{
    public Scrollbar scrollbar;
    private float initialY;

    void Start()
    {
        initialY = transform.position.y;
        scrollbar.onValueChanged.AddListener(ScrollCamera);
    }

    void ScrollCamera(float value)
    {
        Vector3 newPosition = transform.position;
        newPosition.y = initialY - value * 10; // Adjust the multiplier as needed
        transform.position = newPosition;
    }
}
