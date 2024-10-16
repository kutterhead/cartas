using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprites;//colecci�n de sprites
    public GameObject[] cartasFisicas;//son los objetos instanciados de las cartas
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

    int indiceActualAdivina = 0;//el �ndice de la carta que hay que adivinar
    

    [SerializeField]
    int indiceActual = 0;


    public Image cartaAdivina;//la carta que se muestra para adivinar
    [SerializeField]
    int[] indicesAdivinados;

    public byte vidas = 3;
    void Start()
    {

       

        sprites = Resources.LoadAll<Sprite>("sheet1");//captura de los sprites
        //cartaMaestraPrefab = cartaMaestra.gameObject;

        System.Array.Resize(ref cartasFisicas, sprites.Length - 2);
        //indiceActualAdivina = Random.Range(0, sprites.Length-2);
        //cartaAdivina.sprite = sprites[indiceActualAdivina];



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
                carta.GetComponent<scriptCarta>().indiceCarta = indiceActual;

                Vector3 posicionMaster = new Vector3(cartaMaestra.position.x + setX, cartaMaestra.position.y, cartaMaestra.position.z);


                cartaMaestra.position = posicionMaster;

                cartasFisicas[indiceActual] = carta;
                indiceActual++;

            }
            cartaMaestra.position = new Vector3(posicionIncial.x,posicionIncial.y - (setY)*(j+1),posicionIncial.z);

        }



        mezclaCartas();

        StartCoroutine(tirarNuevaCarta());
        cartaMaestra.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void compruebaCarta(int indiceCartaAtual)
   {
        if (indiceCartaAtual == indiceActualAdivina)
        {

            print("La carta SI es correcta.");
            //si acierta se tira otra carta
            System.Array.Resize(ref indicesAdivinados, indicesAdivinados.Length+1);
            indicesAdivinados[indicesAdivinados.Length - 1] = indiceActualAdivina;

            //la carta ha sido acertada y se pone la imagen de carta boca abajo
            cartasFisicas[indiceActualAdivina].GetComponent<Image>().sprite = sprites[sprites.Length-1];
            cartasFisicas[indiceActualAdivina].GetComponent<Button>().enabled = false;

            StartCoroutine(tirarNuevaCarta());

        }
        else
        {
            print("La carta NO es correcta.");
            vidas--;
            if (vidas < 1)
            {
                print("Game Over, no quedan vidas.");

            }
            else {

                print("Vidas restantes:" + vidas);
            }           
            
        }

   }
    void voltearCartasArriba(){

        for (int i = 0; i < cartasFisicas.Length; i++)//este bucle se recorre todas las cartas
        {
            int tempIdex =  cartasFisicas[i].GetComponent<scriptCarta>().indiceCarta;
            cartasFisicas[i].GetComponent<Image>().sprite = sprites[tempIdex];
            cartasFisicas[indiceActualAdivina].GetComponent<Button>().enabled = false;
            for (int j = 0; j < indicesAdivinados.Length; j++)
            {
                if (tempIdex == indicesAdivinados[j])
                {

                    cartasFisicas[i].GetComponent<Image>().sprite = sprites[sprites.Length-1];
                    //cartasFisicas[indiceActualAdivina].GetComponent<Button>().enabled = false;
                }
            }


        }


    }
    void voltearCartasAbajo()
    {
        for (int i = 0; i < cartasFisicas.Length; i++)//este bucle se recorre todas las cartas
        {
            cartasFisicas[i].GetComponent<Image>().sprite = sprites[sprites.Length - 1];
            cartasFisicas[indiceActualAdivina].GetComponent<Button>().enabled = true;
        }

        }

        IEnumerator tirarNuevaCarta()
    {

        voltearCartasArriba();
        yield return new WaitForSeconds(1f);

        bool cartaUnicaNueva = false;
        while (cartaUnicaNueva==false) {
            indiceActualAdivina = Random.Range(0, sprites.Length - 2);
            cartaUnicaNueva = true;
            for (int i = 0; i< indicesAdivinados.Length;i++)
            {
                if (indiceActualAdivina== indicesAdivinados[i])
                {

                    cartaUnicaNueva = false;

                }

            }

        }
        
       

        cartaAdivina.sprite = sprites[indiceActualAdivina];
        yield return new WaitForSeconds(3f);
        voltearCartasAbajo();


    }
    public void mezclaCartas()
    {
        for (int i = 0; i < 10; i++)
        {
            //escogemos indice random para mezclar
            int randomIndex = Random.Range(0, cartasFisicas.Length);
            Vector3 posAux = cartasFisicas[i].transform.position;

            cartasFisicas[i].transform.position = cartasFisicas[randomIndex].transform.position;
            cartasFisicas[randomIndex].transform.position = posAux;

        }

    }

}
