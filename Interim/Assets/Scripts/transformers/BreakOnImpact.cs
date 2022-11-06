using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnImpact : MonoBehaviour
{

    public GameObject[] fragments;
    public float launchForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        collision.GetContacts(contacts);
        Vector2 hit = contacts[0].point;


        foreach (GameObject obj in fragments)
        {
            obj.SetActive(true);
            obj.transform.SetParent(null, true);

            Vector2 launch = hit - (Vector2) obj.transform.position;
            launch.Normalize();
            launch.x *= Random.Range(launchForce * .8f, launchForce * 1.2f);
            launch.y *= Random.Range(launchForce * .8f, launchForce * 1.2f);
            launch.y += launchForce;

            if (obj.TryGetComponent(out Rigidbody2D rb))
            {
                rb.AddForce(launch, ForceMode2D.Impulse);
                rb.angularVelocity = Random.Range(-90, 90);
            }
            else
            {
                Debug.Log("RB not found");
            }
            Destroy(obj, 10f);
        }

        Destroy(gameObject);

    }
}
