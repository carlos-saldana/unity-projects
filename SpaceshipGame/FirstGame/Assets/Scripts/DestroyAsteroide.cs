using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroide : MonoBehaviour
{

    //Exportamos propiedades de nuestros scripts
    public GameController gameController;
    public Player jugador;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        GameObject JugadorObject = GameObject.FindWithTag("Player");
        jugador = JugadorObject.GetComponent<Player>();
    }
    void OnTriggerEnter(Collider other)
    {
        //El asteroide no se destruye con la frontera, solo desaparece
        if (other.CompareTag("Frontera")) return;
        //Destruimos el objeto con el que hace contacto (rayo o nave)
        Destroy(other.gameObject);
        Destroy(gameObject); //Destruimos nuestro asteroide

        if (other.CompareTag("Player"))
        {
            gameController.GameOver(); //Llamamos la función GameOver
            jugador.ClosePort(); //Llamamos la función ClosePort
        }
    }
}
