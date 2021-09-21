using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRandom : MonoBehaviour
{
    private Rigidbody rb; //Cuerpo rígido
    public float tumble;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Damos velocidad angular aleatoria a nuestro objeto
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
