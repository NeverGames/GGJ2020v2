using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolSelect : MonoBehaviour
{
    [SerializeField]
    private Sprite[] toolSprites;

    [SerializeField]
    private Image toolImage;
    public int toolID;
    public int selectID;

    [SerializeField]
    private AudioClip[] breakingSoundEffects;

    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();

    }



    public void PickTool(int idToSelect)
    {
        selectID = idToSelect;
       
        switch (selectID)
        {
            case 1:
                Debug.Log("The Hammer is needed to repair the broken part");
                toolImage.sprite = toolSprites[selectID - 1];
                toolID = 1;
                audio.PlayOneShot(breakingSoundEffects[0]);
                //Get broken part ID so you can display which tool is needed over the correct part.
                break;
            case 2:
                Debug.Log("The Spanner is needed to repair the broken part");
                toolImage.sprite = toolSprites[selectID - 1];
                toolID = 2;
                audio.PlayOneShot(breakingSoundEffects[1]);
                //Get broken part ID so you can display which tool is needed over the correct part.
                break;
            case 3:
                Debug.Log("The Screw Driver is needed to repair the broken part");
                toolID = 3;
                toolImage.sprite = toolSprites[selectID - 1];
                audio.PlayOneShot(breakingSoundEffects[2]);
                //Get broken part ID so you can display which tool is needed over the correct part.
                break;
        }
    }
}
