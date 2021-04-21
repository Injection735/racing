using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShiningButton : Button
{
	private const int LOOP_COUNT = 200;

	[SerializeField] private ShiningStar _star;
	[SerializeField] private float _rotation;
	[SerializeField] private float _buttonAppearDuration;
	[SerializeField] private float _rotationTimeOffset;
	[SerializeField] private float _scaleInDuration;
	[SerializeField] private float _scaleBackDuration;

	public void Appear()
	{
		var sequence = DOTween.Sequence();

		sequence.Insert(0.0f, transform.DORotate(new Vector3(0, 0, _rotation), _scaleInDuration / LOOP_COUNT - _rotationTimeOffset / LOOP_COUNT).SetEase(Ease.Linear).SetLoops(LOOP_COUNT, LoopType.Incremental));
		sequence.Insert(0.0f, transform.DOScale(new Vector3(1.3f, 1.3f, 1), _scaleInDuration));
		
		sequence.Insert(_scaleInDuration - _rotationTimeOffset, transform.DORotate(new Vector3(0, 0, 0), _rotationTimeOffset).SetEase(Ease.Linear));

		sequence.Insert(_scaleInDuration, transform.DOScale(new Vector3(1f, 1f, 1), _scaleBackDuration));
		sequence.OnComplete(() =>
		{
			sequence = null;
			_star.StartAnimation();
		});
	}
}
