using UnityEngine;

public abstract class Actuator : MonoBehaviour {
	[SerializeField]
	protected Trigger trigger;

	void Awake() {
		this.trigger.AddActuator(this);
	}

	public abstract void OnActivate();
	public abstract void OnDeactivate();
}
