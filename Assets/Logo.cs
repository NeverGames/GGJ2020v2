using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {

        StartCoroutine(Delay());
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

}
