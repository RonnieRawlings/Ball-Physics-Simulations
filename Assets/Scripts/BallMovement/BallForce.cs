// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForce : MonoBehaviour
{
    // Power behind the bounce on collision.
    [SerializeField] private float bounceForce;

    // Chosen audio clips.
    [SerializeField] private AudioClip[] audioClips;
    private int currentClip;

    // Camera audio source.
    private AudioSource audioSource;

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

        // Resets to start if at end.
        if (currentClip == audioClips.Length) { currentClip = 0; }

        // Increments int, plays audio.
        audioSource.PlayOneShot(audioClips[currentClip]);
        currentClip++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy circle if ball exited.
        if (collision.CompareTag("Exit"))
        {
            Transform parent = collision.transform.parent.parent;
            int nextSiblingIndex = collision.transform.parent.GetSiblingIndex() + 1;

            // Only tries to enable collider if has a higher sibling.
            if (nextSiblingIndex < parent.childCount)
            {
                // Enable sibling collider.
                parent.GetChild(nextSiblingIndex).GetComponent<PolygonCollider2D>().enabled = true;
            }

            // Destroy escaped obj.
            Destroy(collision.transform.parent.gameObject);
        }
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
        audioSource = GetComponent<AudioSource>();
    }
}