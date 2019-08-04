using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class LevelDataScriptableObject : ScriptableObject {
	public LevelData this[int i] {
		get {
			return data[i];
		}
	}
	public LevelData[] data;
}

[System.Serializable]
public struct LevelData {
	public string levelName;
	public string story;
}