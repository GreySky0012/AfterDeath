using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tech {
    public enum Type { head, body, foot };
    public string _name;
    public abstract void TechUpdate(PlayerController player);
    public abstract void CalPlayerData(PlayerController.Data originData, PlayerController.Data data);
}