using System;
using System.Collections.Generic;
using AMAK.Entities;
using UnityEngine;

namespace AMAK
{
	public class MultiAgentSystem : EntitiesContainer
	{
		public enum ExecutionPolicy
		{
			OnePhase,
			TwoPhases
		}

		[SerializeField] private ExecutionPolicy executionPolicy = ExecutionPolicy.OnePhase;


		protected override void OnCycle()
		{
			switch (executionPolicy)
			{
				case ExecutionPolicy.OnePhase:
					Entities.ForEach(entity =>
					{
						var agent = (Agent) entity;
						agent.OnPerceive();
						agent.OnDecideAndAct();
					});
					break;
				case ExecutionPolicy.TwoPhases:
					Entities.ForEach(agent => (agent as Agent)?.OnPerceive());
					Entities.ForEach(agent => (agent as Agent)?.OnDecideAndAct());
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}