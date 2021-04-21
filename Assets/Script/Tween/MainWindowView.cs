﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainWindowView : MonoBehaviour
{
	[SerializeField] private Button _buttonOpenPopup;

	[SerializeField] private PopupView _popupView;

	[SerializeField] private Text _changeText;

	private void Start()
	{
		_buttonOpenPopup.onClick.AddListener(_popupView.ShowPopup);
	}

	private void OnDestroy()
	{
		_buttonOpenPopup.onClick.RemoveAllListeners();
	}

	private void ChangeText()
	{
		_changeText.DOText("New text", 1.0f).SetEase(Ease.Linear);
	}

}
