using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCarta : MonoBehaviour
{
    // Start is called before the first frame update
    public gameManager manager;
    public int indiceCarta = 0;


    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void compruebamanager()
    {

        manager.compruebaCarta(indiceCarta);

    }


}
