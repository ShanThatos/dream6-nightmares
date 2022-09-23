using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementAdjuster : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public PlayerMovement playerMovement;

    [Space(3)]

    public TMP_Dropdown selector;
    public TMP_InputField inputField;

    List<string> vars;

    bool suppressChange = false;

    // Start is called before the first frame update
    void Start()
    {
        vars = new List<string>();

        
        vars.Add("Air Speed");
        vars.Add("Gravity");
        vars.Add("Ground Speed");
        vars.Add("Jump Power");
        vars.Add("Dash Power");
        vars.Add("Dash Duration");
        vars.Add("Dash Cooldown");
        vars.Add("Pound Delay");

        selector.ClearOptions();
        selector.AddOptions(vars);

        OnDropdownChange();
    }

    public void OnSetNewVal()
    {
        if (suppressChange)
        {
            return;
        }

        float val = float.Parse(inputField.text);
        int index = vars.IndexOf(selector.captionText.text);
        set(index, val);
    }

    public void OnDropdownChange()
    {
        int index = vars.IndexOf(selector.captionText.text);
        suppressChange = true;
        inputField.text = get(index).ToString();
        suppressChange = false;
    }

    public void CopyAll()
    {
        System.Text.StringBuilder str = new System.Text.StringBuilder();

        foreach(int i in System.Linq.Enumerable.Range(0, vars.Count))
        {
            str.Append(vars[i]);
            str.Append(": ");
            str.Append(get(i).ToString());
            str.Append("\n");
        }

        GUIUtility.systemCopyBuffer = str.ToString();
    }
    public float get(int index)
    {
        switch (index)
        {
            case 0:
                return playerRB.gravityScale;
            case 1:
                return playerMovement.maxGroundMoveSpeed;
            case 2:
                return playerMovement.maxAirMoveSpeed;
            case 3:
                return playerMovement.jumpForce;
            case 4:
                return playerMovement.dashForce;
            case 5:
                return playerMovement.dashDuration;
            case 6:
                return playerMovement.dashCooldown;
            case 7:
                return playerMovement.groundPoundDelay;
            default:
                return float.NaN;
        }

    }

    void set(int index, float val)
    {
        switch (index)
        {
            case 0:
                playerRB.gravityScale = val;
                break;
            case 1:
                playerMovement.maxGroundMoveSpeed = val;
                break;
            case 2:
                playerMovement.maxAirMoveSpeed = val;
                break;
            case 3:
                playerMovement.jumpForce = val;
                break;
            case 4:
                playerMovement.dashForce = val;
                break;
            case 5:
                playerMovement.dashDuration = val;
                break;
            case 6:
                playerMovement.dashCooldown = val;
                break;
            case 7:
                playerMovement.groundPoundDelay = val;
                break;
            default:
                Debug.LogWarning("Invalid dropdown index!");
                break;
        }
    }
}
