using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

public class ParkingController : MonoBehaviour
{
    private bool isRecording = false;
    private float currentTime;
        
    [SerializeField]
    public Transform transform;
    
    // Start is called before the first frame update
    void Start()
    {
        string filePath = "Assets/data/myfile.txt";
        StreamWriter writer = new StreamWriter(filePath);
        string myString = "Hello, world!";
        writer.Write(myString);
        writer.Close();

        
        currentTime = Time.time;

        
        Matrix<double> matrix = Matrix<double>.Build.Dense(3, 3, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        Debug.Log("Matrix: ");
        Debug.Log(matrix.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !isRecording && (Time.time - currentTime > 1 ) ){
            currentTime = Time.time;
            Debug.Log("start record: ");
        } else if (Input.GetKey(KeyCode.Space) && isRecording && (Time.time - currentTime > 1 ) ){
            currentTime = Time.time;
            Debug.Log("stop record: ");
        }


    }
}
