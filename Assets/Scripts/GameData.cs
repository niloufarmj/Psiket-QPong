using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    public float aiSpeed;
    public float ballSpeed;
    public int Qbits;
    public bool isPvp;
    public bool soundOn;
}
