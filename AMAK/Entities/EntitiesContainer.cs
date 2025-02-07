using System;
using System.Collections.Generic;
using UnityEngine;

namespace AMAK.Entities
{
	public abstract class EntitiesContainer : MonoBehaviour
	{
		private readonly List<ActiveEntity> _entities = new();
		private readonly List<ActiveEntity> _entitiesPendingAddition = new();
		private readonly List<ActiveEntity> _entitiesPendingRemoval = new();
		public Action OnBeforeCycle { get; set; }
		public Action OnAfterCycle { get; set; }
		public Action OnAgentsListUpdated { get; set; }
		public List<ActiveEntity> Entities => _entities;

		private void UpdateEntitiesList(bool beforeCycle)
		{
			if (_entitiesPendingRemoval.Count == 0 && _entitiesPendingAddition.Count == 0)
				return;
			_entitiesPendingRemoval.ForEach(agent =>
			{
				_entities.Remove(agent);
				agent.Cleanup();
			});

			_entitiesPendingRemoval.Clear();

			_entities.AddRange(_entitiesPendingAddition);
			if (beforeCycle)
				_entitiesPendingAddition.ForEach(e => e.Setup());
			_entitiesPendingAddition.Clear();
			OnAgentsListUpdated?.Invoke();
		}

		public void Cycle()
		{
			Debug.Log("Cycle");
			UpdateEntitiesList(true);
			OnBeforeCycle?.Invoke();
			OnCycle();
			OnAfterCycle?.Invoke();
			UpdateEntitiesList(false);
		}

		protected virtual void OnCycle()
		{
			Entities.ForEach(e => e.Cycle());
		}

		public void Add(ActiveEntity entity)
		{
			_entitiesPendingAddition.Add(entity);
		}

		public void Remove(ActiveEntity entity)
		{
			_entitiesPendingRemoval.Add(entity);
		}
	}
}