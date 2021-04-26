using UnityEngine.Events;
using UnityEngine.UI;

public abstract class DataProperty
{
	protected DataType _type;
	public int Value { get; protected set; } = 0;

	public System.Action<int, DataType> OnClickObserver { get; set; } = delegate { };

	public DataProperty(DataType type)
	{
		_type = type;
	}

	public void SubscribeButton(Button minusButton, Button plusButton)
	{
		plusButton.onClick.AddListener(() => Change(true));
		minusButton.onClick.AddListener(() => Change(false));
	}

	public abstract void Change(bool isAdd);
}
