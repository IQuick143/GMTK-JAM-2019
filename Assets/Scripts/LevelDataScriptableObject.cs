using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class LevelDataScriptableObject : ScriptableObject {
	public LevelData[] data;
}

public struct LevelData {
	public int nextLevel;
	public string levelName;
	public string story;
}