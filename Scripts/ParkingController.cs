using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


using UnityEngine.AI;
public class ParkingController : MonoBehaviour
{
    private bool isRecording = false;
    private float currentTime;
    
    private List<Transform> targets;
    private List<float> data;
    
    [SerializeField]
    public Transform transform;
    public Transform carTransform;
    
    [SerializeField] NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        agent.enabled = true;
        agent.SetDestination(transform.position);
    }


    void saveTraj()
    {

        float startTime = data[0];
            
        var numData = data.Count;
        Matrix<double> matrixX = Matrix<double>.Build.Dense(numData, 5);
        Matrix<double> matrixY = Matrix<double>.Build.Dense(numData, 5);
        Matrix<double> matrixRot = Matrix<double>.Build.Dense(numData, 5);
        
        Matrix<double> targetX = Matrix<double>.Build.Dense(numData, 1);
        Matrix<double> targetY = Matrix<double>.Build.Dense(numData, 1);
        Matrix<double> targetRot = Matrix<double>.Build.Dense(numData, 1);

        for (int i = 0; i < numData; i++)
        {
            Transform trans = targets[i];
            targetX[i, 0] = trans.position.x - transform.position.x;
            targetY[i, 0] = trans.position.y - transform.position.y;
            targetRot[i, 0] = trans.rotation.z - transform.rotation.z;
            
            float time = data[i] - startTime;
            for (int p = 0; p < 5; p++) {
                matrixX[i, p] = Mathf.Pow(time, p);
                matrixY[i, p] = Mathf.Pow(time, p);
                matrixRot[i, p] = Mathf.Pow(time, p);
            }
        }

        var weightsX = (matrixX.Transpose()*matrixX).Inverse() * matrixX.Transpose()*targetX;
        var weightsY = (matrixY.Transpose()*matrixY).Inverse() * matrixY.Transpose()*targetY;
        var weightsRot = (matrixRot.Transpose()*matrixRot).Inverse() * matrixRot.Transpose()*targetRot;
        
        string filePath = "Assets/data/myfile.txt";
        StreamWriter writer = new StreamWriter(filePath);
        string myString = "Hello, world!";
        writer.Write(myString);
        writer.Close();
        
    }
    

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !isRecording && (Time.time - currentTime > 1 ) )
        {
            isRecording = true;
            currentTime = Time.time;
            targets = new List<Transform>();
            data = new List<float>();
            Debug.Log("start record: ");
        } else if (Input.GetKey(KeyCode.Space) && isRecording && (Time.time - currentTime > 1 ) ){
            currentTime = Time.time;
            isRecording = false;
            saveTraj();
            Debug.Log("stop record: ");
        }

        if (isRecording)
        {
            targets.Add(carTransform);
            data.Add(Time.time);
        }


    }
}
