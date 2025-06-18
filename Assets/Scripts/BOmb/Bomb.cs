using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter çalýþtý!");


        Debug.Log("a!");
        Destroy(gameObject);
        Debug.Log("y");
        PelletCollector.Instance.BombHit();
        Debug.Log("d");


    }
}