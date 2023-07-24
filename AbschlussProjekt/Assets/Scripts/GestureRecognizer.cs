
/*
* File: GestureRecognizer.cs
*Date: 24.07.2023
*Author: L.Ritter
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;
public class GestureRecognizer : MonoBehaviour
{
    //Deklaration der Variablen
    public XRNode controllerInput;
    public InputHelpers.Button buttonInput;
    public Transform movementSource;
    public GameObject spellLinePrefab;
    public float inputThreshold = 0.1f;
    public float regognitionThreshold = 0.9f;
    public float newPositionThresholdDistance = 0.05f;
    private bool isMoving = false;
    public bool creationMode = true;
    public string newGestureName;
    public string resultName;

    //Erlaubt editirung im Unity Inspektor
    [System.Serializable]
    //Erstellung einer eigenen Unity Event Klasse
    public class UnityStringEvent : UnityEvent<string> { }
    //Event für Zeichenerkennung
    public UnityStringEvent OnRecognized;
    //liste um neue geste einzulesen
    private List<Gesture> trainingSet = new List<Gesture>();
    //Liste für wo die Blöcke gezeichnet werden.
    private List<Vector3> positionsList = new List<Vector3>();
    //Verzeichnis für die XML dateien
    private const string GESTURES_DIRECTORY = "Gestures";
    private string gesturesPath;

    private void Start()    //wird beim start des Programms einmalig ausgeführt
    {
        //Verzeichnis für die XML dateien wird festgelegt
        gesturesPath = Path.Combine(Application.dataPath, GESTURES_DIRECTORY);

        if (!Directory.Exists(gesturesPath))    //wenn das verzeichnis nicht existiert
        {
            Directory.CreateDirectory(gesturesPath);    //erstelle verzeichnis
        }
        //finde alle kreierten XML dateien
        string[] gestureFiles = Directory.GetFiles(gesturesPath, "*.xml");
        //gebe die dateien an GestureIO
        foreach (var item in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }
    void Update() //wird jeden frame aufgerufen
    {
        //überprüfe ob der Input Knopf gedrückt wird
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controllerInput), buttonInput, out bool isPressed, inputThreshold);

        //Starte die Zeichnung
        if (!isMoving && isPressed)
        {
            StartMovement();
        }

        //Ende die Zeichnung
        else if (isMoving && !isPressed)
        {
            EndMovement();
        }

        //Update die Bewegung (der Zeichnung)
        else if (isMoving && isPressed)
        {
            UpdateMovement();
        }
    }
    void StartMovement()
    {
        Debug.Log("Start Movement");
        isMoving = true;
        positionsList.Clear();  //leere positionsList
        //aktuelle position des Kontrollers ist der erste Punkt der Zeichnung
        positionsList.Add(movementSource.position);
        //wenn ein prefab angegeben ist
        if (spellLinePrefab)
        {
            //setze den angegebenen prefab an der aktuellen position, zerstöre ihn nach 2 sekunden
            Destroy(Instantiate(spellLinePrefab, movementSource.position, Quaternion.identity), 2);
        }
    }

    void UpdateMovement()
    {
        Debug.Log("Update Movement");
        //nimm die letzte position
        Vector3 lastPosition = positionsList[positionsList.Count - 1];
        //wenn die neue position mehr als die bewegungsgrenze entfernt ist 
        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance)
        {
            //erstelle eine neue position an aktueller stelle
            positionsList.Add(movementSource.position);
            //gleich wie in zeile 89
            if (spellLinePrefab)
            {
                Destroy(Instantiate(spellLinePrefab, movementSource.position, Quaternion.identity), 2);
            }
        }
    }

    private void EndMovement()
    {
        Debug.Log("End Movement");
        isMoving = false;
        //erstelle ein point array mit der größe der position liste
        Point[] pointArray = new Point[positionsList.Count];
        //fülle das array
        for (int i = 0; i < positionsList.Count; i++)
        {
            //füge die aktuelle position der Hand zum 2D Bildschirm von Pdollar hinzu
            //dies wird benötigt da PDollar in 2D arbeitet und die hand in 3D zeichnet
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
            //die 0 steht für die anzahl an individuellen zeichnungen befor die bewegung erkannt wird
        }

        Gesture newGesture = new Gesture(pointArray);

        //wenn kreations Modus an ist, füge die zeichnung zum Trainingset hinzu
        if (creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);
            string fileName = Path.Combine(gesturesPath, newGestureName + ".xml");
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        }
        //an sonsten versuche die zeichnung zu erkennen
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + " " + result.Score); //gebe die erkannte geste aus mit dem score wie sicher es dieses zeichen ist
            resultName = result.GestureClass; //nimmt result und liest es in eine string variable rein
            //wenn der score größer als die erkennungsgrenze ist
            if (result.Score > regognitionThreshold)
            {
                OnRecognized.Invoke(result.GestureClass); //rufe die kreierte Unity String Event Klasse auf
            }
        }
    }
    public string GetResult() //getter für result der zeichenerkennung
    {
        return resultName;
    }
}