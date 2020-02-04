using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform b_RespawnPoint, r_RespawnPoint;



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.GetComponent<MovementInput>() != null)
        {
            MovementInput m = other.GetComponent<MovementInput>();
            RespawnAction(m.playerNo, other.transform.gameObject);
            Debug.Log("respawn");
        }
    }

    void RespawnAction (int no, GameObject player)
    {
        CharacterController c = player.GetComponent<CharacterController>();
        c.enabled = false;
        
        Debug.Log(player.name + no);
        if (no == 1)
            player.transform.position = r_RespawnPoint.position;
        else if(no == 2)
            player.transform.position = b_RespawnPoint.position;

        c.enabled = true;
    }
}
