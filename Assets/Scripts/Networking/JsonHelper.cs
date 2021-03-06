using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    
    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.result;
    }

   

    [System.Serializable]
    class Wrapper<T>
    {
        public List<T> result;
    }

    /*public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.result;
    }



    [System.Serializable]
    class Wrapper<T>
    {
        public T[] result;
    } 
    */

}
