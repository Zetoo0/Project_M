using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOfInterest : MonoBehaviour
{
    public static event Action<string> OnPointsOfInteresEntered;

    [SerializeField] private string poiName;

    public string PoiName { get { return poiName; } }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnPointsOfInteresEntered != null)
            OnPointsOfInteresEntered(this.poiName);
    }


}
