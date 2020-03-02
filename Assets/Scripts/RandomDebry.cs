using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDebry : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;

    private float speed = 2;
    GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * gameController.speed * Time.deltaTime);
        if (transform.position.z >= startPoint.GetChild(4).transform.position.z)
            transform.position = SpawnSpot();
    }

    Vector3 SpawnSpot ()
    {
        Vector3 spot = Vector3.zero;
        int rng = Random.Range(0, 2);
        float rngX;
        if (rng == 0)
            rngX = Random.Range(startPoint.GetChild(0).transform.position.x, startPoint.GetChild(1).transform.position.x);
        else
            rngX = Random.Range(startPoint.GetChild(2).transform.position.x, startPoint.GetChild(3).transform.position.x);

        spot = new Vector3(rngX, transform.position.y, startPoint.transform.position.z);

        return spot;
    }


}
