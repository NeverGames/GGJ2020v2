using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obtainable : MonoBehaviour
{
    public int toolID;
    public GameObject benchTool;
    public GameObject playerTool1;
    public GameObject playerTool2;
    public bool hasTool = true;

    public virtual void GrabObjectPlayer1( MovementInput player)
    {
        if (player.isHolding && hasTool || !player.isHolding && !hasTool)
            return;
        // not have a tool and tool there
        if (!player.isHolding && hasTool)
        {
            player.isHolding = true;
            player.toolSelected = toolID;
            hasTool = false;
            benchTool.SetActive(false);
            playerTool1.SetActive(true);
            
        }
        else if (player.isHolding && !hasTool && player.toolSelected == toolID)
        {
            player.isHolding = false;
            player.toolSelected = 0;
            hasTool = true;
            benchTool.SetActive(true);
            playerTool1.SetActive(false);
            
        }
    }


    public virtual void GrabObjectPlayer2( MovementInput player)
    {
        if (player.isHolding && hasTool || !player.isHolding && !hasTool)
            return;

        if (!player.isHolding && hasTool)
        {
            player.isHolding = true;
            player.toolSelected = toolID;
            hasTool = false;
            benchTool.SetActive(false);
            playerTool2.SetActive(true);

        }
        else if (player.isHolding && !hasTool && player.toolSelected == toolID)
        {
            player.isHolding = false;
            player.toolSelected = 0;
            hasTool = true;
            benchTool.SetActive(true);
            playerTool2.SetActive(false);
        }
    }
}
