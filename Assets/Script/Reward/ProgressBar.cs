using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
	[SerializeField] private RectTransform bar;

	private float _currentValue;
	private float _total;

	public float CurrentValue
	{
		get => _currentValue;
		set
		{
			_currentValue = value;
			UpdateBar();
		}
	}

	public float Total
	{
		get => _total;
		set
		{
			_total = value;
			UpdateBar();
		}
	}

	private void UpdateBar()
	{
		var position = bar.localPosition;
		position.x = -bar.rect.width + (_currentValue / _total) * bar.rect.width;
		bar.localPosition = position;
	}
}
