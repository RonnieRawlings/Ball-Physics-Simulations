// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForce : MonoBehaviour
{
    // Power behind the bounce on collision.
    [SerializeField] private float bounceForce;

    // Chosen audio clip.
    [SerializeField] private AudioClip audioClip;

    // Camera audio source.
    private AudioSource cameraAudio;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get rigidbody comp & collision contact.
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        Vector2 collisionNormal = collision.contacts[0].normal;

        // Calculate a random force change.
        float bounceChange = Random.Range(1f, 4);
        float finalForce = bounceForce * bounceChange;

        // Add force in opposite direction.
        rb.AddForce(collisionNormal * finalForce, ForceMode2D.Impulse);

        // Increase scale of obj.
        IncreaseScale();

        // Plays audio.
        cameraAudio.PlayOneShot(audioClip);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy circle if ball exited.
        if (collision.CompareTag("Exit")) { Destroy(collision.transform.parent.gameObject); }
    }

    /// <summary> method <c>IncreaseScale</c> exponentially increases the objects size. </summary>
    public void IncreaseScale()
    {
        transform.localScale *= 1.01f;
    }

    // Called once on script initlization.
    void Awake()
    {
        // Finds camera audio comp.
        cameraAudio = Camera.main.GetComponent<AudioSource>();
    }
}
