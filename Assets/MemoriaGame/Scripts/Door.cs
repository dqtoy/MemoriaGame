﻿//
// Door.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Door : MonoBehaviour
{
    public int IDpair = -1;
    public string NameDoor = "Hola";
    public bool isPairOpen { get; private set;}

    [HideInInspector]
    public Vector2 posMaxtrix = new Vector2(-1,-1);

    #region Signal list
    [HideInInspector]
    public List<Signal> onOpen = new List<Signal>();
    [HideInInspector]
    public List<Signal> onClose = new List<Signal>();
    [HideInInspector]
    public List<Signal> onCheckTruePair = new List<Signal>();
    #endregion
   
    void Awake(){
    
        NameDoor = gameObject.name;
    }

    public void Touch(){

        ManagerDoors.Instance.TouchMe(this);
    }

    public void Open(){

        foreach (Signal sig in onOpen) {
        
            sig.Invoke ();
        }
    }
    public void Close(){

        foreach (Signal sig in onClose) {

            sig.Invoke ();
        }
    }
    /// <summary>
    /// Si los dos pares  fueron iguales
    /// </summary>
    public void CheckTruePair(){
        isPairOpen = true;
        foreach (Signal sig in onCheckTruePair) {

            sig.Invoke ();
        }
    }

}

