using UnityEngine;

public abstract class Actuator : MonoBehaviour {
	[SerializeField]
	protected Trigger trigger;

	public void Start() {
		this.trigger.AddActuator(this);
	}

	public abstract void OnActivate();
	public abstract void OnDeactivate();
}
