using UnityEngine;
using System.Collections;

public class MusicClass : MonoBehaviour
{

    private static MusicClass instance = null;
    public static MusicClass Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // any other methods you need
}