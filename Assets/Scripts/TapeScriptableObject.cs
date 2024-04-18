using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Walkman/TapeInfo", order = 1)]
public class TapeScriptableObject : ScriptableObject
{
    public string songName;
    public int startingMillisecond; 
}