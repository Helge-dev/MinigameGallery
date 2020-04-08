using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * USED TO TEST SAFE TO REMOVE
 */
public class test : MonoBehaviour
{
    List<string> firstPlace = new List<string>();
    List<string> secondPlace = new List<string>();
    public void StartNextScene() => CommonCommands.NextGame(firstPlace, secondPlace);
    List<InputManager> controllers = new List<InputManager>(); //List of components with players inputs
    void Awake()
    {
        //Put InputManagers of GameObjects with "GameController" Tag into a array
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("GameController"))
        {
            controllers.Add(o.GetComponent<InputManager>()); 
        }
        Debug.Log("Amount of Controllers Registered = " + controllers.Count);
    }
}
