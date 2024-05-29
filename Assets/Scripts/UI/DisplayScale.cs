// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScale : MonoBehaviour
{
    // Ball to track.
    [SerializeField] private Transform ballTransform;

    // Update is called once per frame
    void Update()
    {
        // Update text to show current ball scale.
        GetComponent<TextMeshProUGUI>().text = "Ball Size: " + ballTransform.localScale.x.ToString("F2");
    }
}
