using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMaster : MonoBehaviour {
	[SerializeField]
	private Shader shader;
	private Material material;

	[SerializeField]
	private Color WireColour = Color.red;

	[SerializeField]
	private MeshRenderer[] renderers = null;

	// Start is called before the first frame update
	void Awake() {
		this.material = new Material(shader);
		this.material.SetColor("_WireColour", WireColour);
	}

	void Start() {
		for (int i = 0; i < renderers.Length; i++) {
			renderers[i].material = this.material;
		}
	}

	public void SetState(bool state) {
		this.material.SetFloat("_ON", state?1f:0f);
	}
}
