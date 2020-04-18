using System.Collections.Generic;
using UnityEngine;
/*
 * USED TO TEST SAFE TO REMOVE
 */
public class test : MonoBehaviour
{
    List<int> firstPlace = new List<int>();
    List<int> secondPlace = new List<int>();
    private void Update()
    {
        foreach (InputManager im in DataStorage.GetSetControllers.Values)
            if (im.GetButtonSouthPressed)
                CommonCommands.NextGame(firstPlace, secondPlace);
    }
}
