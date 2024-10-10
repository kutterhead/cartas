using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprites;//colección de sprites
    public Transform cartaMaestra;//usado como puntero

    
    public GameObject cartaMaestraPrefab;//usado para hacer copias o instancias

    Vector3 posicionIncial;
    public int filas = 4;
    public int columnas = 12;

    //public Vector3 offSetX = new Vector3( 100,0,0);
    //public Vector3 offSetY = new Vector3(0, 100, 0);

    public int setX = 100;
    public int setY = 100;

    public Canvas canvas;

    int indiceActual = 0;

    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("sheet1");//captura de los sprites
        //cartaMaestraPrefab = cartaMaestra.gameObject;




        posicionIncial = cartaMaestra.position;


        posicionIncial = new Vector3(cartaMaestra.position.x - (setX * columnas / 2), cartaMaestra.position.y, cartaMaestra.position.z);
        cartaMaestra.position = posicionIncial;

        for (int j = 0; j < filas; j++)//recorre filas de la primera columna
        {



            
            for (int i = 0; i < columnas; i++)//recorre  columnas de la misma fila
            {

                cartaMaestra.position = new Vector3(posicionIncial.x + (setX) * (i), cartaMaestra.position.y, cartaMaestra.position.z);
                //cartaMaestra.position = posicionIncial + offSetX * i;

                GameObject carta = Instantiate(cartaMaestraPrefab, cartaMaestra.position, cartaMaestra.rotation);
                


                carta.transform.SetParent(canvas.transform);

                carta.GetComponent<Image>().sprite = sprites[indiceActual];    

                Vector3 posicionMaster = new Vector3(cartaMaestra.position.x + setX, cartaMaestra.position.y, cartaMaestra.position.z);


                cartaMaestra.position = posicionMaster;


                indiceActual++;

            }
            cartaMaestra.position = new Vector3(posicionIncial.x,posicionIncial.y - (setY)*(j+1),posicionIncial.z);

        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


}
