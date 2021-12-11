using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneradorCoordenadas : MonoBehaviour
{
    // matriz of float
    public string[,] matriz;
    private int filas;
    private int columnas;
    public GameObject prefab;
    public GameObject prefabEstrellaMayor;
    public GameObject prefabEstrellaMenor;
    public GameObject startsContainer;
    public GameObject coordinatesContainer;

    public int cantidadEstrellasMayor;
    public int cantidadEstrellasMenor;

    void Start()
    {
        print("GeneradorCoordenadas");
        columnas = 5;
        filas = 4;
        cantidadEstrellasMayor = 12;
        cantidadEstrellasMenor = 8;
        GenerarMatriz(columnas, filas);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Generar"))
        {
            DeleteAll();
            GenerarMatriz(columnas, filas);
        }
    }

    void DeleteAll()
    {
        foreach (Transform child in startsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in coordinatesContainer.transform)
        {
            Destroy(child.gameObject);
        }

        cantidadEstrellasMayor = 12;
        cantidadEstrellasMenor = 8;
    }

    private void GenerarMatriz(int columnas, int filas)
    {
        matriz = new string[columnas, filas];
        var indiceX = 0;
        var indiceY = 0;
        for (float x = 0f; indiceX < columnas; x += 0.2f)
        {
            indiceY = 0;
            for (float y = 0f; indiceY < filas; y += 0.25f)
            {
                // print(indiceX + " - " + indiceY);
                matriz[indiceX, indiceY] = "(" + x + "," + y + ")";
                Cuadrante cuadrante = new Cuadrante(new Vector2(x, y), new Vector2(x + .2f, y),
                    new Vector2(x, y + .25f), new Vector2(x + .2f, y + .25f));
                indiceY += 1;

                Instantiate(prefab, Camera.main.ViewportToWorldPoint(new Vector3(x, y, Camera.main.nearClipPlane)),
                    Quaternion.identity, coordinatesContainer.transform);
                Instantiate(prefab,
                    Camera.main.ViewportToWorldPoint(new Vector3(x + .2f, y, Camera.main.nearClipPlane)),
                    Quaternion.identity, coordinatesContainer.transform);
                Instantiate(prefab,
                    Camera.main.ViewportToWorldPoint(new Vector3(x, y + .25f, Camera.main.nearClipPlane)),
                    Quaternion.identity, coordinatesContainer.transform);
                Instantiate(prefab,
                    Camera.main.ViewportToWorldPoint(new Vector3(x + .2f, y + .25f, Camera.main.nearClipPlane)),
                    Quaternion.identity, coordinatesContainer.transform);

                Instantiate(ObtenerPrefabRandomStart(), GenerarPosicionEstrellaRandom(cuadrante), Quaternion.identity,
                    startsContainer.transform);
            }

            indiceX++;
        }
    }

    private GameObject ObtenerPrefabRandomStart()
    {
        var prefabs = new List<GameObject>
        {
            prefabEstrellaMayor,
            prefabEstrellaMenor
        };
        int indicePrefab = Random.Range(0, prefabs.Count);
        switch (indicePrefab)
        {
            case 0 when cantidadEstrellasMayor > 0:
                cantidadEstrellasMayor -= 1;
                return prefabs[indicePrefab];
            case 1 when cantidadEstrellasMayor == 0:
                cantidadEstrellasMenor -= 1;
                return prefabs[1];
            case 1 when cantidadEstrellasMenor > 0:
                cantidadEstrellasMenor -= 1;
                return prefabs[indicePrefab];
            case 1 when cantidadEstrellasMenor == 0:
                cantidadEstrellasMayor -= 1;
                return prefabs[0];
            default:
                return prefabs[indicePrefab];
        }
    }

    private Vector3 GenerarPosicionEstrellaRandom(Cuadrante cuadrante)
    {
        float maxBorderX = cuadrante.BottomLeft.x;
        float maxBorderY = cuadrante.BottomLeft.y;
        float xRandomPosition = Random.Range(cuadrante.BottomLeft.x, cuadrante.BottomRight.x);
        float yRandomPosition = Random.Range(cuadrante.BottomLeft.y, cuadrante.TopLeft.y);
        // Vector2 posicionEstrella = new Vector2(xRandomPosition, yRandomPosition);
        // posicionEstrella = VerificarSiEstaCercaDelBorde(cuadrante, posicionEstrella);
        return Camera.main.ViewportToWorldPoint(
            new Vector3(xRandomPosition, yRandomPosition, Camera.main.nearClipPlane));
        // return Camera.main.ViewportToWorldPoint(new Vector3(posicionEstrella.x, posicionEstrella.y,
        //     Camera.main.nearClipPlane));
    }

    private Vector2 VerificarSiEstaCercaDelBorde(Cuadrante cuadrante, Vector2 posicionEstrella)
    {
        Vector3 fixedPosition = posicionEstrella;
        // var distanciaAlBorde = 0.05f;
        // if (posicionEstrella.x >= cuadrante.BottomLeft.x + distanciaAlBorde)
        // {
        //     print("Esta cerca del borde izquierdo");
        //     fixedPosition.x = cuadrante.BottomLeft.x + distanciaAlBorde;
        // }
        //
        // if (posicionEstrella.x >= cuadrante.BottomRight.x - distanciaAlBorde)
        // {
        //     fixedPosition.x = cuadrante.BottomRight.x - distanciaAlBorde;
        //     print("Esta cerca del borde derecho");
        // }
        //
        // if (posicionEstrella.y >= cuadrante.BottomLeft.y + distanciaAlBorde)
        // {
        //     fixedPosition.y = cuadrante.BottomLeft.y + distanciaAlBorde;
        //     print("Esta cerca del borde inferior");
        // }
        //
        // if (posicionEstrella.y >= cuadrante.TopLeft.y - distanciaAlBorde)
        // {
        //     fixedPosition.y = cuadrante.TopLeft.y - distanciaAlBorde;
        //     print("Esta cerca del borde superior");
        // }

        return fixedPosition;
    }
}

public class Cuadrante
{
    public Vector2 BottomLeft { get; }
    public Vector2 BottomRight { get; }
    public Vector2 TopLeft { get; }
    public Vector2 TopRight { get; }

    public Cuadrante(Vector2 bottomLeft, Vector2 bottomRight, Vector2 topLeft, Vector2 topRight)
    {
        this.BottomLeft = bottomLeft;
        this.BottomRight = bottomRight;
        this.TopLeft = topLeft;
        this.TopRight = topRight;
    }

    public override string ToString()
    {
        return "(" + BottomLeft + "," + BottomRight + "," + TopLeft + "," + TopRight + ")";
    }
}