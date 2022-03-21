using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedMaps
{
    public bool _00;
    public bool _11;
    public bool _21;
    private List<bool> maps;

    public UnlockedMaps(bool _00, bool _11, bool _21)
    {
        maps.Clear();
        this._00 = _00;
        this._11 = _11;
        this._21 = _21;
        AddMaps(_00, _11, _21);
    }

    private void AddMaps(bool _00, bool _11, bool _21)
    {
        maps.Add(_00);
        maps.Add(_11);
        maps.Add(_21);
    }

    public void GetUnlockedMaps() 
    {


        foreach(var map in this.maps)
        {

        }
    }


}
