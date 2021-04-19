using UnityEngine;
using UnityEngine.UI;

public class ContainerSlotRewardView : MonoBehaviour
{
	[SerializeField] private Image _selectBackground;
	[SerializeField] private Image _iconCurrency;
	[SerializeField] private Text _textDays;
	[SerializeField] private Text _countReward;

	public void SetData(Reward reward, int countDay, bool isSelect)
	{
		_iconCurrency.sprite = reward.IconCurrency;
		_textDays.text = $"Day {countDay}";
		_countReward.text = reward.CountCurrency.ToString();
		_selectBackground.gameObject.SetActive(isSelect);
	}
}
