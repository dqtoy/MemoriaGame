﻿using UnityEngine;
using System.Collections;

public enum PowerDoor
{
    Any = 0, // Use this transition to represent a non-existing transition in your system
    Tati = 1,
    Tica = 2,
    Tico = 3,
    Timi = 4,
    Topi = 5,
}
public class PowerOff : MonoBehaviour {

    public PowerDoor character = PowerDoor.Any;


    protected bool Locked = true;
    [HideInInspector]
    public bool used = false;


    protected UIButton button;

    Door[] doors;

    bool firstRun = true;

    void Start(){

        button = GetComponent<UIButton> ();
        button.isEnabled = false;

        doors = GameObject.FindObjectsOfType<Door> ();


        for (int i = 0; i < doors.Length; ++i) {
            if (character == doors [i].character) {
                doors[i].onCheckTruePair.Add (new Signal ("setOnPower", gameObject));
                Debug.Log ("SetOnPower add");


                break;
            }
        }

        ManagerPowers.Instance.onPowerTrue.Add (new Signal ("onNotUse", gameObject));
        ManagerPowers.Instance.onPowerFalse.Add (new Signal ("onUse", gameObject));

        ManagerPause.SubscribeOnPauseGame(onPaused);
        ManagerPause.SubscribeOnResumeGame( onResume);

        firstRun = false;
    }

    void OnEnable(){
        if (!firstRun) {

            ManagerPause.SubscribeOnPauseGame(onPaused);
            ManagerPause.SubscribeOnResumeGame( onResume);
        }
    }

    void OnDisable(){

        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);
    }

    [Signal]
    public void setOnPower(){
        Debug.Log ("SetOnPower Padre");

        Locked = false;
        button.isEnabled = true;
    }
    public void setOffPower(){

        if (!isPaused) {
            button.isEnabled = false;
            used = true;
        }


    }

    #region Used
    [Signal]
    void onUse(){
        if(!Locked && !used)
            button.isEnabled = true;

    }
    [Signal]
    void onNotUse(){
        button.isEnabled = false;

    }
    #endregion

    #region Paused
    bool isPaused = false;
    void onPaused(){
        isPaused = true;

    }
    void onResume(){
        isPaused = false;

    }
    #endregion

}
