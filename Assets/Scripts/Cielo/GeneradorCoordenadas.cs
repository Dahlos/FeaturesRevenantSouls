using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneradorCoordenadas : MonoBehaviour
{
    // matriz of float
    private string[,] matriz;
    private int filas;
    private int columnas;
    public GameObject prefab;
    public GameObject prefabEstrellaMayor;
    public GameObject prefabEstrellaMenor;
    public GameObject prefabVertice;
    public GameObject startsContainer;

    public GameObject coordinatesContainer;

    // public ArrayList estrellas;
    public Estrella[] estrellas;
    public int cantidadEstrellasMayor;
    public int cantidadEstrellasMenor;


    void Start()
    {
        print("GeneradorCoordenadas");
        columnas = 5;
        filas = 4;
        cantidadEstrellasMayor = 12;
        cantidadEstrellasMenor = 8;
        // estrellas = new ArrayList();
        estrellas = new Estrella[cantidadEstrellasMayor + cantidadEstrellasMenor];
        GenerarMatriz(columnas, filas);
    }

    public void Generate()
    {
        DeleteAll();
        StartCoroutine(GenerarMatriz(columnas, filas));
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Generar"))
        {
            DeleteAll();
            StartCoroutine(GenerarMatriz(columnas, filas));
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

    private IEnumerator GenerarMatriz(int columnas, int filas)
    {
        matriz = new string[columnas, filas];
        var indiceX = 0;
        var counterId = 0;
        for (var x = 0f; indiceX < columnas; x += 0.2f)
        {
            var indiceY = 0;
            for (var y = 0f; indiceY < filas; y += 0.25f)
            {
                matriz[indiceX, indiceY] = "(" + x + "," + y + ")";
                float auxX = x;
                float auxY = y;
                if (x == 0f) auxX += .1f;
                if (x >= .8f) auxX -= .1f;
                if (y == 0f) auxY += .125f;
                if (y >= .75f) auxY -= .125f;

                Cuadrante cuadrante = new Cuadrante(new Vector2(auxX, auxY), new Vector2(auxX + .2f, auxY),
                    new Vector2(auxX, auxY + .25f), new Vector2(auxX + .2f, auxY + .25f));
                indiceY += 1;

                Instantiate(prefab,
                    Camera.main.ViewportToWorldPoint(new Vector3(auxX, auxY, Camera.main.nearClipPlane)),
                    Quaternion.identity, coordinatesContainer.transform);
                Instantiate(prefab,
                    Camera.main.ViewportToWorldPoint(new Vector3(auxX + .2f, auxY, Camera.main.nearClipPlane)),
                    Quaternion.identity, coordinatesContainer.transform);
                Instantiate(prefab,
                    Camera.main.ViewportToWorldPoint(new Vector3(auxX, auxY + .25f, Camera.main.nearClipPlane)),
                    Quaternion.identity, coordinatesContainer.transform);
                Instantiate(prefab,
                    Camera.main.ViewportToWorldPoint(new Vector3(auxX + .2f, auxY + .25f, Camera.main.nearClipPlane)),
                    Quaternion.identity, coordinatesContainer.transform);

                GameObject prefabRandom = ObtenerPrefabRandomStart();
                Vector3 posicionRandom = GenerarPosicionEstrellaRandom(cuadrante);
                GameObject estrellaCreada = Instantiate(prefabRandom,
                    posicionRandom, Quaternion.identity,
                    startsContainer.transform);
                int numberRandomIntentos = Random.Range(1, 7);
                // StarColorMap.GetStartByNumberTry(numberRandomIntentos);
                estrellaCreada.GetComponent<SpriteRenderer>().color =
                    StarColorMap.GetStarByNumberTry(numberRandomIntentos).color;
                print(numberRandomIntentos);
                GuardarEstrellaPlayerPrefs(counterId, prefabRandom, posicionRandom, numberRandomIntentos);
                VerificarDistanciaAOtrasEstrellas(estrellaCreada, cuadrante);
                counterId++;
                yield return new WaitForSeconds(0.025f);
            }

            indiceX++;
        }

        string playerToJson = JsonHelper.ToJson(estrellas, true);
        PlayerPrefs.SetString("estrellas", playerToJson);
    }

    private void GuardarEstrellaPlayerPrefs(int id, GameObject prefabRandom, Vector3 posicionRandom, int intentos = 0)
    {
        var tipoEstrella = ObtenerTipoEstrellaPorGameObject(prefabRandom.name);

        var estrella = new Estrella(id, posicionRandom, tipoEstrella, intentos);
        // string playerToJson = JsonUtility.ToJson(estrella);
        // print(playerToJson);
        estrellas[id] = estrella;
    }

    private TipoEstrella ObtenerTipoEstrellaPorGameObject(string prefabName)
    {
        if (prefabName.Contains("Mayor"))
            return TipoEstrella.Mayor;
        else
            return TipoEstrella.Menor;
    }

    private void VerificarDistanciaAOtrasEstrellas(GameObject estrellaCreada, Cuadrante cuadrante)
    {
        foreach (Transform estrella in startsContainer.transform)
        {
            if (Vector2.Distance(estrella.position, estrellaCreada.transform.position) < 1.5f)
            {
                print("Estrella " + estrella.gameObject.name + " esta muy cerca de " + estrellaCreada.name);
                estrellaCreada.transform.position = GenerarPosicionEstrellaRandom(cuadrante);
            }
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
        float xRandomPosition = Random.Range(cuadrante.TopLeft.x, cuadrante.TopRight.x);
        float yRandomPosition = Random.Range(cuadrante.BottomRight.y, cuadrante.TopLeft.y);
        return Camera.main.ViewportToWorldPoint(
            new Vector3(xRandomPosition, yRandomPosition, Camera.main.nearClipPlane));
    }
}

[Serializable]
public class Estrella
{
    public Vector2 posicion;
    public int id;
    public TipoEstrella tipoEstrella;
    public int intentos;

    public Estrella(int id, Vector2 posicion, TipoEstrella tipoEstrella, int intentos = 0)
    {
        this.posicion = posicion;
        // this.posX = posicion.x;
        // this.posY = posicion.y;
        this.id = id;
        this.tipoEstrella = tipoEstrella;
        this.intentos = intentos;
    }
}

[Serializable]
public enum TipoEstrella
{
    Mayor,
    Menor
}

[Serializable]
public class Cuadrante
{
    public Vector2 BottomLeft { get; }
    public Vector2 BottomRight { get; }
    public Vector2 TopLeft { get; }
    public Vector2 TopRight { get; }

    public Cuadrante(Vector2 bottomLeft, Vector2 bottomRight, Vector2 topLeft, Vector2 topRight)
    {
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
        TopLeft = topLeft;
        TopRight = topRight;
    }

    public override string ToString()
    {
        return "(" + BottomLeft + "," + BottomRight + "," + TopLeft + "," + TopRight + ")";
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}