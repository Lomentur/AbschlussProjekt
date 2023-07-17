using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;
public class GestureRecognizer : MonoBehaviour
{
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;
    public Transform movementSource;
    public float newPositionThresholdDistance = 0.05f;
    public GameObject spellLinePrefab;
    public bool creationMode = true;
    public string newGestureName;

    public float regognitionThreshold = 0.9f;

    //allows for editing of Unity event from editor
    [System.Serializable]
    //create class for custom Unity events
    public class UnityStringEvent : UnityEvent<string> { }
    //create event - onRecognized
    public UnityStringEvent OnRecognized;

    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving = false;
    private List<Vector3> positionsList = new List<Vector3>();

    //represents the name of the directory for XML files
    private const string GESTURES_DIRECTORY = "Gestures";
    private string gesturesPath;

    private void Start()
    {
        //set path for XML files in the Projekt
        gesturesPath = Path.Combine(Application.dataPath, GESTURES_DIRECTORY);
        //if directory doesn't exist, create it
        if (!Directory.Exists(gesturesPath))
        {
            Directory.CreateDirectory(gesturesPath);
        }
        //find all xml files (the gestures)
        string[] gestureFiles = Directory.GetFiles(gesturesPath, "*.xml");
        foreach (var item in gestureFiles)
        {
            //feed the xml files into GestureIO
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }
    void Update()
    {
        //check if input Button is Pressed
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        //Start the Movement
        if (!isMoving && isPressed)
        {
            StartMovement();
        }

        //Ending the Movement
        else if (isMoving && !isPressed)
        {
            EndMovement();
        }

        //Updating the Movement
        else if (isMoving && isPressed)
        {
            UpdateMovement();
        }
    }

    void StartMovement()
    {
        Debug.Log("Start Movement");
        isMoving = true;
        //clear position List
        positionsList.Clear();
        //add Current Position as first point
        positionsList.Add(movementSource.position);
        //if a prefab is loaded in
        if (spellLinePrefab)
        {
            //create a new prefab at current position, destroy it after 3 seconds
            Destroy(Instantiate(spellLinePrefab, movementSource.position, Quaternion.identity), 3);
        }
    }
    private void EndMovement()
    {
        Debug.Log("End Movement");
        isMoving = false;
        //create point array with the size of the position list
        Point[] pointArray = new Point[positionsList.Count];

        //fill array
        for (int i = 0; i < positionsList.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            //the 0 stands for the number of strokes before its registered
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray);

        //add a gesture to the training set
        if (creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Path.Combine(gesturesPath, newGestureName + ".xml");
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        }
        //recognize a gesture
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + result.Score);
            if (result.Score > regognitionThreshold)
            {
                OnRecognized.Invoke(result.GestureClass);
            }
        }
    }



    void UpdateMovement()
    {
        Debug.Log("Update Movement");
        //get the last position
        Vector3 lastPosition = positionsList[positionsList.Count - 1];
        //if the new Position is more than threshold distance away, make new position 
        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance)
        {
            positionsList.Add(movementSource.position);
            if (spellLinePrefab)
            {
                //create a new prefab at current position, destroy it after 3 seconds
                Destroy(Instantiate(spellLinePrefab, movementSource.position, Quaternion.identity), 3);
            }
        }
    }
}
