using UnityEngine;

namespace AMAK.Entities
{
	public abstract class Agent : ActiveEntity
	{
		public virtual void OnPerceive()
		{
		}

		public virtual void OnDecide()
		{
		}

		public virtual void OnAct()
		{
		}

		public virtual void OnDecideAndAct()
		{
			OnDecide();
			OnAct();
		}

		private void OnValidate()
		{
			if (entitiesContainer != null && entitiesContainer is not MultiAgentSystem)
			{
				Debug.LogError("The EntitiesContainer of an Agent must be a Multi-Agent System");
			}
		}
	}
}