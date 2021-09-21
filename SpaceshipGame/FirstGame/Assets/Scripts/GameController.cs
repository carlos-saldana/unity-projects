using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject warning;  //Representa a nuestro asteroide
    public Vector3 spawnValues; //Vector de 3 posiciones
    public int cont;            //Cantidad de asteroides a crear
    public float spawnWait;     //Tiempo entre la creación de asteroides
    public float startWave;     //Tiempo de inicio para la oleada de asteroides
    public float waveWait;      //Tiempo de espera para cada oleada de asteroides

    public Text gameOverText;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gameOverText.gameObject.SetActive(false);

        StartCoroutine(SpawnWaves());
        
    }

    //IEnumerable function nos permite tener ciclos for
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWave);

        while (true)
        {


            for (int i = 0; i < cont; i++)
            {
                //Creamos la oleada de asteroides en distintas posiciones
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

                //Quaternion.identity hace que nuestro objeto no tenga rotación, pues ya posee una
                Instantiate(warning, spawnPosition, Quaternion.identity); //Llamamos a nuestro GameObject

                //Tiempo entre la creación de asteroides
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            //Cerramos el ciclo while (termina la oleada de asteroides)
            if (gameOver)
            {
                break;
            }
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
    }
}
