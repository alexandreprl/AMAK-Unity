using System;
using UnityEngine;

namespace AMAK.Entities
{
	public abstract class ActiveEntity : Entity
	{
		
		[SerializeField]
		protected EntitiesContainer entitiesContainer;

		public EntitiesContainer EntitiesContainer
		{
			get => entitiesContainer;
			set => entitiesContainer = value;
		}

		public void Awake()
		{
		}

		private void Start()
		{
			
			if (entitiesContainer == null)
			{
				Debug.LogError("EntitiesContainer reference not set", gameObject);
			}
			if (entitiesContainer != null)
				entitiesContainer.Add(this);
		}

		private void OnDisable()
		{
			if (entitiesContainer != null)
				entitiesContainer.Remove(this);
		}
		
		public virtual void Cycle()
		{
		}
		
		private void OnValidate()
		{
			if (entitiesContainer != null && entitiesContainer is not Environment)
			{
				Debug.LogError("The EntitiesContainer of an active entities must be an Environment");
			}
		}

		public virtual void Setup()
		{
			
		}
		
		public virtual void Cleanup()
		{
			
		}
	}
}