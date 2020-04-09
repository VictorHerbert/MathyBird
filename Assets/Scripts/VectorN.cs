using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class VectorN {
 
    public float[] values;
 
 
    public VectorN(int size){
        this.values = new float[size];
    }

    public int Length{
        get => values.Length;
    }
 
    public float this[int i]
    {
        get
        {
            return values[i];
        }
        set
        {
            values[i] = value;
        }
    }
 
 
    public void print(){
        for ( int i = 0; i < values.Length; i++){
            Debug.Log(i+" -> "+values[i]);
        }
    }
 
    public float norme(){
        return Dot(this,this);
    }
   
    public static float Dot(VectorN v1, VectorN v2){
        if ( v1.Length == v2.Length ){
            float sum = 0;
            for ( int i = 0; i < v1.Length; i++){
                sum += v1[i]*v2[i];
            }
            return sum;
        }
        else{
            throw new System.Exception("VectorN dimensions must agree");
        }
    }
 
    public static Vector3 CrossProduct(Vector3 u, Vector3 v){ //Dimension 3
        return new Vector3(u.y*v.z-u.z*v.y,u.z*v.x-u.x*v.z,u.x*v.y-u.y*v.x);
    }
 
    public static bool Orthogonal(VectorN v1, VectorN v2){
        return VectorN.Dot(v1,v2) == 0;
    }
 
    /*-$-$-$-$-$-$-$-$-OPERATORS-$-$-$-$-$-$-$-$-*/
 
    public static VectorN operator +(VectorN v1, VectorN v2){
        if ( v1.Length == v2.Length ) {
            VectorN v = new VectorN(v1.Length);
            for ( int i = 0; i < v1.Length; i++){
                v[i] = v1[i] + v2[i];
            }
            return v;
        }
        else{
            throw new System.Exception("VectorN dimensions must agree");
        }
    }
 
    public static VectorN operator -(VectorN v1, VectorN v2){
        if ( v1.Length == v2.Length ) {
            VectorN v = new VectorN(v1.Length);
            for ( int i = 0; i < v1.Length; i++){
                v[i] = v1[i] - v2[i];
            }
            return v;
        }
        else{
            throw new System.Exception("VectorN dimensions must agree");
        }
    }
 
    public static VectorN operator *(VectorN v1, float f){
        VectorN v = new VectorN(v1.Length);
        for ( int i = 0; i < v1.Length; i++){
            v[i] = v1[i]*f;
        }
        return v;
    }
    public static VectorN operator *(float f, VectorN v1){
        VectorN v = new VectorN(v1.Length);
        for ( int i = 0; i < v1.Length; i++){
            v[i] = v1[i]*f;
        }
        return v;
    }
   
    public static VectorN operator /(VectorN v1, float f){
        if ( f != 0 ){
            VectorN v = new VectorN(v1.Length);
            for ( int i = 0; i < v1.Length; i++){
                v[i] = v1[i]/f;
            }
            return v;
        }
        else{
            throw new System.Exception("Division by zero encountered in VectorN");
        }
    }
 
    public static bool operator == (VectorN v1, VectorN v2){
        if ( v1.Length == v2.Length){
            for ( int i = 0; i < v1.Length; i++){
                if ( v1[i] != v2[i] ){
                    return false;
                }
            }
            return true;
        }
        else{
            return false;
        }
    }
 
    public static bool operator != (VectorN v1, VectorN v2){
        if ( v1.Length == v2.Length){
            for ( int i = 0; i < v1.Length; i++){
                if ( v1[i] != v2[i] ){
                    return true;
                }
            }
            return false;
        }
        else{
            return true;
        }
    }
 
}