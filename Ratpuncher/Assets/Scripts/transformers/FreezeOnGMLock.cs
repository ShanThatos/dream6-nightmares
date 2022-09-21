using UnityEngine;

public class FreezeOnGMLock : MonoBehaviour {
    private bool locked = false;
    private Rigidbody2D rb;
    private RigidbodyConstraints2D previousConstraints;
    private Vector2 previousVelocity;
    private Animator animator;
    private float previousAnimatorSpeed;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        GameManager.AddMovementLockedObject(this);

        previousConstraints = rb.constraints;
        if (animator != null) 
            previousAnimatorSpeed = animator.speed;
    }

    void OnDestroy() {
        GameManager.RemoveMovementLockedObject(this);
    }

    public void Lock() {
        if (locked) return;
        locked = true;
        previousConstraints = rb.constraints;
        previousVelocity = rb.velocity;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (animator != null) {
            previousAnimatorSpeed = animator.speed;
            animator.speed = 0;
        }
    }

    public void Unlock() {
        if (!locked) return;
        locked = false;
        rb.constraints = previousConstraints;
        rb.velocity = previousVelocity;
        if (animator != null)
            animator.speed = previousAnimatorSpeed;
    }
}
