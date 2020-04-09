using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

[System.Serializable]
public class WeightData{
    public int[] layersSize;
    public int epoch;
    //public int score;
    public float fitness;
    public List<float> weights = new List<float>();
    public WeightData(float _score, int _epoch, int[] _layersSize){fitness = _score; epoch = _epoch; layersSize = _layersSize;}

    
    [System.NonSerialized] int count = 0;
    public void  push(float f){weights.Add(f);}
    public float pull() => weights[count++];
    
}


public class NeuralNetwork : MonoBehaviour
{
    public enum ActivationFunction{
        sigmoid, tanh, custom
    }

    

#region "Variables"

    [Header("Activation Function")]
    [SerializeField] ActivationFunction functionType;
    //[SerializeField] AnimationCurve function;

    [Header("Layers")]
    [SerializeField] int[] layersSize;
    [SerializeField] List<VectorN> nodes;
    [SerializeField] List< List<VectorN> > weights = new List< List<VectorN> >();

    int outputCount;
    public WeightData weightData;
    [Range(0.0f,1.0f)]
    public float randomRate;
    [Range(0.0f,1.0f)]
    public float resetRate;

#endregion

#region "Initialization"

    public void setInput(float[] input){
        if(input.Length != (nodes[0].Length-1))
            throw new System.Exception("input dimensions must agree");
        for(int i = 0; i < input.Length; i++)
            nodes[0][i] = input[i];
    }    

    public float[] getOuput(){
        float[] output = new float[outputCount];
        for(int i = 0; i < outputCount; i++)
            output[i] = nodes[outputCount][i];        

        return output;
    }
    
    void initNodes()
    {
        nodes = new List<VectorN>();
        for(int i = 0; i < layersSize.Length; i++)
            nodes.Add(new VectorN(layersSize[i]+1));
        for(int i = 0; i < layersSize.Length-1; i++)
            nodes[i][nodes[i].Length-1] = 1.0f;

        outputCount = nodes.Count - 1;
    }

    void initWeights(WeightData data){
        if(data == null){
            randomWeights();
            return;
        }

        weights =  new List< List<VectorN> >();
        for(int i = 1; i < nodes.Count; i++){
            weights.Add(new List<VectorN>(nodes[i].Length));
            for(int j = 0; j < nodes[i].Length; j++){
                weights[i-1].Add( new VectorN(nodes[i-1].Length) );
                for(int k = 0; k < weights[i-1][j].Length; k++)
                    weights[i-1][j][k] = data.pull();
            }   
        }

        /*for(int i = 0; i < weights.Count; i++)      
            for(int j = 0; j < weights[i].Count; j++)
                for(int k = 0; k < weights[i][j].Length; k++)
                    weights[i][j][k] = data.pull();
        */
    }

    void randomize(float probability){
        for(int i = 0; i < weights.Count; i++)      
            for(int j = 0; j < weights[i].Count; j++)
                for(int k = 0; k < weights[i][j].Length; k++)
                    if(Random.Range(0.0f,1.0f) <= probability )
                        weights[i][j][k] = Random.Range(-1.0f,1.0f);

    }


    void randomWeights(){
        weights =  new List< List<VectorN> >();
        for(int i = 1; i < nodes.Count; i++){
            weights.Add(new List<VectorN>(nodes[i].Length));
            for(int j = 0; j < nodes[i].Length; j++){
                weights[i-1].Add( new VectorN(nodes[i-1].Length) );
                for(int k = 0; k < weights[i-1][j].Length; k++)
                    weights[i-1][j][k] = Random.Range(-1.0f,1.0f);
            }   
        }
    }

#endregion

#region "NeuralNetwork related"

    float activationFunction(float x){
        return Mathf.SmoothStep(-1.0f,1.0f,x);
    }

    void eval(){
        for(int i = 1; i < nodes.Count; i++){
            for(int j = 0; j < nodes[i].Length-1; j++){
                nodes[i][j] = activationFunction(VectorN.Dot(nodes[i-1], weights[i-1][j]));
            }
        }
    }  
    
#endregion

#region "Unity Routines"

    void Start()
    {
        initNodes();
        
        
        initWeights(LoadFile());
        randomize(randomRate);

        if(Random.Range(0.0f,1.0f) <= resetRate){
            randomWeights();
            weightData.epoch = 0;
        }
        
    }


    void Update()
    {
        eval();
    }

    
#endregion

#region "Save/Load"

    public void SaveFile(float score)
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        WeightData data = new WeightData(score, weightData.epoch+1,layersSize);

        for(int i = 0; i < weights.Count; i++)      
            for(int j = 0; j < weights[i].Count; j++)
                for(int k = 0; k < weights[i][j].Length; k++)
                    data.push(weights[i][j][k]);
                
                    

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Saved at " + destination);
    }

    public WeightData LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(destination)) file = File.OpenRead(destination);
        else {
            //throw new System.Exception("File not found");
            return null;
        }
        

        BinaryFormatter bf = new BinaryFormatter();
        WeightData data = (WeightData) bf.Deserialize(file);
        file.Close();

        weightData = data;

        if(!data.layersSize.SequenceEqual(layersSize))
            throw new System.Exception("Incompatible Layer sizes");


        return data;
    }

#endregion

}
