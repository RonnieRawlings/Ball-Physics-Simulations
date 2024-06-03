// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForce : MonoBehaviour
{
    // Chosen audio clips.
    [SerializeField] private AudioClip[] audioClips;
    private int currentClip;

    // Camera audio source.
    private AudioSource audioSource;

    // Should increase scale.
    [SerializeField] private bool increaseScale, spawnNewBalls;

    #region Properties

    public bool SpawnNewBalls
    {
        set { spawnNewBalls = value; }
    }

    public AudioClip[] AudioClips
    {
        get { return audioClips; }
        set { audioClips = value; }
    }

    #endregion

    // Added force around a moving circle.
    private float tangentialForce = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get rigidbody comp & collision contact.
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        Vector2 collisionNormal = collision.contacts[0].normal;

        // Calculate the reflected direction.
        Vector2 incomingDirection = rb.velocity.normalized;
        Vector2 reflectDirection = Vector2.Reflect(incomingDirection, collisionNormal);

        float bounceChange = Random.Range(0.7f, 2f);

        // Add force in reflected direction.
        rb.AddForce(reflectDirection * bounceChange, ForceMode2D.Impulse);

        // Add tangential force.
        Vector2 tangentialDirection = Vector2.Perpendicular(collisionNormal);
        //rb.AddForce(tangentialDirection * (tangentialForce * bounceChange), ForceMode2D.Impulse);

        // Increase scale of obj.
        if (increaseScale) { IncreaseScale(); }

        // Resets to start if at end.
        if (currentClip == audioClips.Length) { currentClip = 0; }

        // Increments int, plays audio.
        audioSource.PlayOneShot(audioClips[currentClip]);
        currentClip++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collison is destroy, remove obj from scene.
        if (collision.CompareTag("Destroy")) { Destroy(this.gameObject); return; }

        // Destroy circle if ball exited.
        if (collision.CompareTag("Exit"))
        {
            // Spawns new balls, removes self.
            if (spawnNewBalls) 
            {
                // Calls add balls routine.
                EscapeBalls addMoreBalls = transform.parent.GetComponent<EscapeBalls>();
                StartCoroutine(SpawnAndDestroy(addMoreBalls));
                
                // Ends method early.
                return;
            }

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

    /// <summary> coroutine <c>SpawnAndDestroy</c> waits until the spawn routine completes, then destroies object. </summary>
    /// <param name="addMoreBalls">EscapeBalls Script</param>
    public IEnumerator SpawnAndDestroy(EscapeBalls addMoreBalls)
    {
        // Waits until routine completes.
        yield return StartCoroutine(addMoreBalls.SpawnNewBalls(this.gameObject));

        // Removes self, ends routine.
        Destroy(this.gameObject);
    }

    /// <summary> method <c>IncreaseScale</c> exponentially increases the objects size. </summary>
    public void IncreaseScale()
    {
        transform.localScale *= 1.01f;
    }

    // Called once on script initlization.
    void Awake()
    {
        // Finds audio comp.
        audioSource = GetComponent<AudioSource>();
    }

    // Max velocity.
    private float maxVelocity = 7f;

    void FixedUpdate()
    {
        // Gets rigidbody comp.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Limits velocity if above limit.
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }
}