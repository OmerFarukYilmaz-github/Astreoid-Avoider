using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astreoid : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShipHealtManager.instance.Crash();
        }
    }

    public void OnBecameInvisible()
    {
        ScoreSystem.instance.AddScore();
        Destroy(gameObject);
    }
}
