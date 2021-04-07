using Company.Project.Features.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Company.Project.Features.Abilities
{
	public class AbilityItemView : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private Text _itemText;
		
		public void SetupItem(IItem item, Action<IItem> Use)
		{
			_itemText.text = item.Info.Title;
			_button.onClick.AddListener(() => Use.Invoke(item));
		}
	}
}
