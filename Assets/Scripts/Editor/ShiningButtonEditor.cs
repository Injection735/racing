using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(ShiningButton))]
public class ShiningButtonEditor : ButtonEditor
{
	private SerializedProperty m_InteractableProperty;

	protected override void OnEnable()
	{
		m_InteractableProperty = serializedObject.FindProperty("m_Interactable");
	}

	public override void OnInspectorGUI()
	{
		ShiningButton targetMenuButton = (ShiningButton) target;

		// Show default inspector property editor
		DrawDefaultInspector();
	}
}
