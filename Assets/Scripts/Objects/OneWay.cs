using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWay : MonoBehaviour {
	[SerializeField]
	private GameObject platform = null;
	[SerializeField]
	private GameObject backPlate = null;

	[SerializeField]
	private BoxCollider2D trigger = null;
	private BoxCollider2D realCollider = null;
	private ContactFilter2D filter;

	[SerializeField]
	private float moveTime = 0.1f;
	private float state = 0f;

	void Start() {
		this.realCollider = platform.GetComponent<BoxCollider2D>();
		this.filter = new ContactFilter2D();
		this.filter.useLayerMask = true;
		this.filter.layerMask = ~LayerMask.GetMask("OneWay", "Wall");
	}

	void FixedUpdate() {
		bool open = IsIntersected();
		this.realCollider.enabled = !open;
		if (!open && state > 0f) {
			state -= Time.fixedDeltaTime / moveTime;
			if (state <= 0f) state = 0f;
		}
		if (open && state < 1f) {
			state += Time.fixedDeltaTime / moveTime;
			if (state >= 1f) state = 1f;
		}
		this.platform.transform.localPosition = new Vector3(0, 0, 0.25f + state);
	}

	private bool IsIntersected() {
		var results = new Collider2D[16];
		this.trigger.OverlapCollider(this.filter, results);

		for (int i = 0; i < results.Length; i++) {
			if (results[i] != null) {
				if (!results[i].isTrigger) {
					return true;
				}
			}
		}
		return false;
	}
}
