using UnityEngine;

[System.Serializable]
public class RoulettePieceData
{
    public Sprite icon;
    public string description;

    [Range(1, 100)] public int chance = 100;
}
