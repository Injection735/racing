using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainWindowView : MonoBehaviour
{
	[SerializeField] private Button _buttonOpenPopup;
	[SerializeField] private ShiningButton _shiningButton;
	[SerializeField] private Button _appearShiningButton;

	[SerializeField] private PopupView _popupView;

	[SerializeField] private Text _changeText;

	private void Start()
	{
		_buttonOpenPopup.onClick.AddListener(_popupView.ShowPopup);
		_appearShiningButton.onClick.AddListener(_shiningButton.Appear);
	}

	private void OnDestroy()
	{
		_buttonOpenPopup.onClick.RemoveAllListeners();
		_appearShiningButton.onClick.RemoveAllListeners();
	}

	private void ChangeText()
	{
		_changeText.DOText("New text", 1.0f).SetEase(Ease.Linear);
	}

}
