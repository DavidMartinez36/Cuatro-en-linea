using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tablero : MonoBehaviour
{
    private bool turnoJugador1;
    int ancho = 10;// con estas dos variables 
    int alto = 10;//  se da el tamaño del tablero
    public GameObject ganador,reset;
    public GameObject pieza;
    private GameObject[,] mEsfera;
    public Color baseColor;// se asigna el color base de las piezas
    public Color player1;// color del jugador1
    public Color player2;// color del jugador2
    public bool clik = true; // varible para detener el juego
    public float contraReloj = 20f; //tiempo del turno
    public float velocidad = 1f; // velocidad del tiempo

    public void Start()
    {
        /* se crea el tablero */
        mEsfera = new GameObject[ancho, alto];
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {

                GameObject esfera = GameObject.Instantiate(pieza) as GameObject;
                Vector3 position = new Vector3(i, j, 0);
                esfera.transform.position = position;
                esfera.GetComponent<Renderer>().material.color = baseColor;
                mEsfera[i, j] = esfera;

            }
        }
    }

    public void Update()
    {   /*loob principal del juego, la variable click puede ser falsa si se llega 
        al limite de tiempo (20seg) o se cumple la condicion de victoria */
        if (clik == true)
        {


            contraReloj -= Time.deltaTime;

            if (contraReloj == 0.0f)
            {
                transform.position += new Vector3(velocidad * Time.deltaTime, 0.0f, 0.0f);


            }
            if (contraReloj <= 0.0f)
            {
                clik = false;
            }


            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SeleccionarFicha(mPosition);
        }
    }

    public void SeleccionarFicha(Vector3 position)
    {
        int i = (int)(position.x + 0.5f);
        int j = (int)(position.y + 0.5f);
        /* se asigna el boton del raton como control el cual genera la pieza, y la bloquea 
         ademas se resetea el tiempo */

        if (Input.GetButtonDown("Fire1"))
        {

            contraReloj = 20f;

            if (i >= 0 && j >= 0 && i < ancho && j < alto)
            {
                GameObject esfera = mEsfera[i, j];
                if (esfera.GetComponent<Renderer>().material.color == baseColor)
                {
                    Color colorAUsar = Color.clear;
                    if (turnoJugador1)
                    
                        colorAUsar = player1;
                    
                    else
                  
                         colorAUsar = player2;
                        esfera.GetComponent<Renderer>().material.color = colorAUsar;
                        turnoJugador1 = !turnoJugador1;
                        RevisionX(i, j, colorAUsar);
                        RevisionY(i, j, colorAUsar);
                        RevisionDiagonal1(i, j, colorAUsar);
                        RevisionDiagonal2(i, j, colorAUsar);
                    
                }
            }
        }

    }

    public void RevisionX(int x, int y, Color colorAVerificar)
    /*verifica que las piezas sercanas sean del mismo color
    para que se cunpla la condicion de victoria */
    {
        int contador = 0;
        for (int i = x - 3; i <= x + 3; i++)
        {
            if (i < 0 || i >= ancho)
                continue;

            GameObject esfera = mEsfera[i, y];

            if (esfera.GetComponent<Renderer>().material.color == colorAVerificar)
            {
                contador++;
                if (contador == 4)
                {
                    /*cuando se cunple la condicion de victoria se imprime ganador, 
                     * se activa el GameObject ganador y la variable click */
                    Debug.Log("ganador");
                    ganador.SetActive(true);
                    reset.SetActive(true);
                    clik = false;

                }
            }
            else
                contador = 0;
        }


    }

    public void RevisionY(int x, int y, Color colorAVerificar)
    {
        int contador = 0;
        for (int j = y - 3; j <= y + 3; j++)
        {
            if (j < 0 || j >= alto)
                continue;

            GameObject esfera = mEsfera[x, j];
            if (esfera.GetComponent<Renderer>().material.color == colorAVerificar)
            {
                contador++;
                if (contador == 4)
                {
                    Debug.Log("ganador");
                    ganador.SetActive(true);
                    reset.SetActive(true);

                    clik = false;
                }
            }
            else
                contador = 0;
        }
    }

    public void RevisionDiagonal1(int x, int y, Color colorAVerificar)
    {
        int contador = 0;
        int j = y - 3;

        for (int i = x - 3; i <= x + 3; i++)
        {
            if (j < 10 && j >= 0 && i < 10 && i >= 0)
            {

                GameObject esfera = mEsfera[i, j];


                if (esfera.GetComponent<Renderer>().material.color == colorAVerificar)
                {
                    contador++;


                    if (contador == 4)
                    {

                        Debug.Log("Ganador");
                        ganador.SetActive(true);
                        reset.SetActive(true);

                        clik = false;


                    }
                }
                else
                {
                    contador = 0;
                }

            }
            j++;
        }


    }


    public void RevisionDiagonal2(int x, int y, Color colorAVerificar)
    {

        int contador = 0;
        int j = y + 3;



        for (int i = x - 3; i <= x + 3; i++)
        {
            if (j < 10 && j >= 0 && i < 10 && i >= 0)
            {

                GameObject esfera = mEsfera[i, j];


                if (esfera.GetComponent<Renderer>().material.color == colorAVerificar)
                {
                    contador++;

                    if (contador == 4)
                    {
                        Debug.Log("Ganador");
                        ganador.SetActive(true);
                        reset.SetActive(true);

                        clik = false;

                    }
                }
                else
                {
                    contador = 0;
                }
            }
            j--;
        }
    }
    public void Reset()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
