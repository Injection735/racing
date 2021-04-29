using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

public class AssetsBundleParent : MonoBehaviour
{
	private static int loadIndex = 0;

	[SerializeField] private AssetReference[] _references;

	private long _timeStart = 0;

	private void Start()
	{
		_timeStart = (int) DateTimeOffset.Now.ToUnixTimeMilliseconds();

		for (int i = 0; i < _references.Length; i++)
			LoadBundle(_references[0]);
	}

	private void LoadBundle(AssetReference reference)
	{
		Addressables.InstantiateAsync(_references[0], transform).Completed += (gameObject) => OnLoaded();
	}

	private void OnLoaded()
	{
		if (++loadIndex == _references.Length)
			Debug.Log((int) DateTimeOffset.Now.ToUnixTimeMilliseconds() - _timeStart);
	}
}
