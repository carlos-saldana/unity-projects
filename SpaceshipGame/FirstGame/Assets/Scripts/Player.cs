using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Comunicación serial
using System;
using System.IO.Ports;

[System.Serializable]
public class Frontera
{
    public float xMin, xMax, zMin, zMax;
}

public class Player : MonoBehaviour
{
    //Declaramos el nombre del puerto
    static SerialPort puerto;

    int value;
    bool fire;

    private Rigidbody rb; //Guarda la información de nuestro objeto
    float move_x, move_z; //Variables para nuestros ejes
    Vector3 input;        //Vector de entrada para la posición
    public float speed;   //Variable pública para la velocidad (aparece en Unity)

    public Frontera limites;

    //---------- Disparos ----------
    public GameObject shot;     //GameObject - aquí va "bolt" (disparo)
    public Transform shotSpawn; //Marco de referencia

    //Variables que permitirán establecer tiempos para cada disparo
    public float fireRate;
    private float nextFire;
    //------------------------------

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la comunicación serial
        puerto = new SerialPort("COM8", 115200);
        puerto.Open();          //Abrimos el puerto serial
        puerto.ReadTimeout = 1; //Tiempo de espera para verificar la comunicación serial

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move_x = Input.GetAxis("Horizontal");
        move_z = Input.GetAxis("Vertical");

        //Verificamos si nuestro puerto serial está abierto
        if (puerto.IsOpen)
        {
            try
            {
                string s;
                s = puerto.ReadLine();          //Guardamos lo que llega por Arduino
                Int32.TryParse(s, out value);   //Transformamos el string a int

                if (value == 1)
                {
                    fire = true;
                }
                else if (value == 0)
                {
                    fire = false;
                }

            }
            catch (System.Exception)
            {

            }
        }

        //---------- DISPAROS ----------
        //Fire1 hace referencia al clic izquierdo del mouse o ctrl
        if (fire && Time.time > nextFire)
        //if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            //Establecemos un tiempo para cada disparo
            nextFire = Time.time + fireRate;
            //Llamamos al objeto cada vez que hacemos clic izquierdo o ctrl
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
        //------------------------------

    }

    //Damos movimiento a nuestro objeto
    void FixedUpdate()
    {
        input = new Vector3(move_x, 0.0f, move_z);
        //Damos velocidad a nuestro objeto
        rb.velocity = input * speed;

        rb.position = new Vector3(Mathf.Clamp(rb.position.x, limites.xMin, limites.xMax), 0f, Mathf.Clamp(rb.position.z, limites.zMin, limites.zMax));
    }

    //Cerramos el puerto serial al terminar el juego
    public void ClosePort()
    {
        puerto.Close();
    }
}
