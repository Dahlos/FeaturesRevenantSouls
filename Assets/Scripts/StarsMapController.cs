using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StarsMapController : MonoBehaviour
{
    // Start is called before the first frame update
    public int numeroFilas = 4;

    public int numeroColumnas = 5;

    public int maximoJefes = 12;
    public int counterJefes = 0;

    public int maximoSubjefes = 8;
    public int counterSubjefes = 0;

    // matriz of vector2
    private readonly Vector2[,] _matrizCoordenadas =
    {
        {
            new Vector2(0.08f, .08f), new Vector2(0.30f, .08f), new Vector2(0.52f, .08f), new Vector2(0.72f, .08f),
            new Vector2(0.93f, .08f)
        },
        {
            new Vector2(0.08f, 0.30f), new Vector2(0.30f, 0.30f), new Vector2(0.52f, 0.30f), new Vector2(0.72f, 0.30f),
            new Vector2(.93f, 0.30f)
        },
        {
            new Vector2(0.08f, 0.52f), new Vector2(0.30f, 0.52f), new Vector2(0.52f, 0.52f), new Vector2(0.72f, 0.52f),
            new Vector2(.93f, 0.52f)
        },
        {
            new Vector2(0.08f, 0.72f), new Vector2(0.30f, 0.72f), new Vector2(0.52f, 0.72f), new Vector2(0.72f, 0.72f),
            new Vector2(.93f, 0.72f)
        }
        // {
        //     new Vector2(0.08f, .93f), new Vector2(0.30f, .93f), new Vector2(0.52f, .93f), new Vector2(0.72f, .93f),
        //     new Vector2(.93f, .93f)
        // }
    };

    public GameObject prefab;

    void Start()
    {
        GenerarCoordenadas();
    }

    private void GenerarCoordenadas()
    {
        while (counterJefes < maximoJefes || counterSubjefes < maximoSubjefes)
        {
            for (var i = 0; i < numeroFilas; i++)
            {
                for (var j = 0; j < numeroColumnas; j++)
                {
                    // print(_matrizCoordenadas[i, j]);
                    if (Camera.main is { })
                    {
                        int randomZone = Random.Range(0, 2);
                        print(randomZone);
                        GameObject zona = Instantiate(prefab,
                            Camera.main.ViewportToWorldPoint(new Vector3(_matrizCoordenadas[i, j].x,
                                _matrizCoordenadas[i, j].y, Camera.main.nearClipPlane)), Quaternion.identity);
                        Color32 color = zona.GetComponent<SpriteRenderer>().color;
                        
                        if (randomZone == 1 && counterJefes < maximoJefes)
                        {
                            counterJefes++;
                            color = Color.red;
                        }
                        else if (randomZone == 0 && counterSubjefes < maximoSubjefes)
                        {
                            counterSubjefes++;
                            color = Color.blue;
                        }
                        
                        zona.GetComponent<SpriteRenderer>().color = color;
                        print("counterJefes " + counterJefes);
                        print("counterSubjefes " + counterSubjefes);
                        // color = randomZone == 1 ? Color.blue : Color.red;
                    }
                }
            }
        }
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Generar Sectores"))
            GenerarCoordenadas();

        if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
            Debug.Log("Clicked the button with text");
    }

    private void GenerarFila(int i)
    {
    }

    // Update is called once per frame
    void Update()
    {
        // var inicio = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
        // print(inicio);
        // // 
        // var fin = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
        // print(fin);
    }
}