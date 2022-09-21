using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementAdjuster : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public PlayerMovement playerMovement;

    [Space(3)]

    public TMP_InputField GravityField;
    public TMP_InputField JumpField;
    public TMP_InputField AirSpeedField;
    public TMP_InputField DashField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeGravity()
    {
        float grav = playerRB.gravityScale;
        float.TryParse(GravityField.text, out grav);
        Debug.Log(grav);

        playerRB.gravityScale = grav;
    }

    public void OnChangeJump()
    {
        float val = playerMovement.jumpForce;
        float.TryParse(JumpField.text, out val);

        playerMovement.jumpForce = val;
    }

    public void OnChangeAirMove()
    {
        float val = playerMovement.maxAirMoveSpeed;
        float.TryParse(AirSpeedField.text, out val);

        playerMovement.maxAirMoveSpeed = val;
    }

    public void OnChangeDash()
    {
        float val = playerMovement.dashForce;
        float.TryParse(DashField.text, out val);

        playerMovement.dashForce = val;
    }
}
