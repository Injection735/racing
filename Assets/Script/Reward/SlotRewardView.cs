using UnityEngine;
using UnityEngine.UI;

public class SlotRewardView : MonoBehaviour
{
	[SerializeField] private Image _selectBackground;
	[SerializeField] private Image _iconCurrency;
	[SerializeField] private Text _countReward;

	public void SetData(Reward reward, bool isSelect)
	{
		_iconCurrency.sprite = reward.IconCurrency;
		_countReward.text = reward.CountCurrency.ToString();
		_selectBackground.gameObject.SetActive(isSelect);
	}
}
