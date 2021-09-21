using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFront : MonoBehaviour
{
    //Destruye nuestro objeto al salir del �rea
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
