using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed; //Variable velocidad
    private Rigidbody rb; //Interacci�n con el cuerpo r�gido

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Desplazamiento en el eje z
        rb.velocity = transform.forward * speed;
        
    }
}
