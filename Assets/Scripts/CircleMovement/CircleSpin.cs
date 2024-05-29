// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpin : MonoBehaviour
{
    // Rotation speed of obj.
    [SerializeField] private float rotationSpeed;

    // Determines rot direction.
    [SerializeField] private int direction; 

    /// <summary> method <c>SpinSelf</c> rotates the object at given rotSpeed. </summary>
    public void SpinSelf(float rotSpeed, int direction)
    {
        transform.Rotate(0, 0, direction * rotSpeed * Time.deltaTime);
    }

    // Called once every frame. 
    void Update()
    {
        // Rotate self at given speed.
        SpinSelf(rotationSpeed, direction);
    }
}
