using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveHitbox : MonoBehaviour
{

    [Tooltip("Max size, assumes circular (1:1 ratio)")]
    public float maxScale;
    
    [Tooltip("Time to reach max size")]
    public float timeToMaxScale;

    [Tooltip("Time to remain after hitting max size")]
    public float timeToFade;

    float startScale;

    SpriteRenderer spriteRenderer;
    float startAlpha;

    float currentLifetime;
    // Start is called before the first frame update
    void Start()
    {  
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        startAlpha = spriteRenderer.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLifetime <= timeToMaxScale)
        {
            float nextScale = Mathf.Lerp(startScale, maxScale, currentLifetime / timeToMaxScale);
            transform.localScale = new Vector2(nextScale, nextScale);
        }
        else if(currentLifetime < timeToMaxScale + timeToFade)
        {
            float nextAlpha = Mathf.Lerp(startAlpha, 0, (currentLifetime - timeToMaxScale) / (timeToFade));
            Color c = spriteRenderer.color;
            c.a = nextAlpha;
            spriteRenderer.color = c;
        }
        else
        {
            Destroy(gameObject, .1f);
        }

        currentLifetime += Time.deltaTime;
    }

    void DoDamage()
    {

    }
}
