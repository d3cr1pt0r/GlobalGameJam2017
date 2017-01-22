using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Pattern.asset", menuName = "LevelManager/Pattern", order = 1)]
public class Pattern : ScriptableObject
{
	[System.Serializable]
	public class ItemNode
	{
		public Enums.ItemType ItemType;
		public GameObject ItemPrefab;

		public Vector3 Position;
		public Vector3 Rotation;
		public Vector3 Scale = Vector3.one;
	}

	public List<ItemNode> ItemNodes;
	public Vector3 RootPosition;

}
