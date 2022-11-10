using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatRecoil : MonoBehaviour
{
    public Rigidbody2D rb;
    public RatJumpState jump;
    public RatEnemyController controller;
    public bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            StartCoroutine(RecoilDelay());
            isActive = false;
        }
    }

    IEnumerator RecoilDelay()
    {
        yield return new WaitForSecondsRealtime(.08f);
        rb.velocity = Vector3.zero;

        int dir = controller.isFacingRight() ? -1 : 1;

        Vector3 f = new Vector3(dir * jump.HORIZONTAL_JUMP_FORCE, .3f * jump.VERTICAL_JUMP_FORCE);
        rb.AddForce(f, ForceMode2D.Impulse);
        controller.switchState("RatChase");

        yield return null;
    }
}
