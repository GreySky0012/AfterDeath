using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the abstract tech class
/// </summary>
public abstract class Tech {
    public enum Type { head, body, foot };
    public string _name;
    //called in the player update function to comply some active skills
    public abstract void TechUpdate(PlayerController player);
    //called in the player update to cal the date of the player
    public abstract void CalPlayerData(PlayerController.Data originData, PlayerController.Data data);
}