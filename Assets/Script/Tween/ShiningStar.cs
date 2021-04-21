using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class ShiningStar : MonoBehaviour
{
	[SerializeField] private float _rotationDuration;
	[SerializeField] private float _rotation;
	[SerializeField] private float _scaleInDuration;
	[SerializeField] private float _scaleBackDuration;

	public void StartAnimation()
	{
		var sequence = DOTween.Sequence();

		sequence.Insert(0.0f, transform.DORotate(new Vector3(0, 0, _rotation), _rotationDuration).SetEase(Ease.Linear).SetLoops(200, LoopType.Incremental));
		sequence.Insert(0.0f, transform.DOScale(new Vector3(1.3f, 1.3f, 1), _scaleInDuration));
		sequence.Insert(_scaleInDuration, transform.DOScale(new Vector3(0, 0, 1), _scaleBackDuration));
		sequence.OnComplete(() =>
		{
			sequence = null;
		});
	}


	private void SimpleTween()
	{
	}
}
