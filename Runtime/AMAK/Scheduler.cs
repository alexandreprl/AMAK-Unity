using System;
using AMAK;
using UnityEngine;

public class Scheduler : MonoBehaviour
{
	[SerializeField] private MultiAgentSystem multiAgentSystem;
	[SerializeField] private float cyclesPerSecond = 1f;

	public float CyclesPerSecond
	{
		get => cyclesPerSecond;
		set => cyclesPerSecond = Math.Max(0f, value);
	}
	private float _nextCycleTime;
	private bool _isRunning;

	private void Awake()
	{
		if (multiAgentSystem == null)
		{
			Debug.LogWarning("MultiAgentSystem reference not set. Trying to find one in the scene...", gameObject);
			multiAgentSystem = FindFirstObjectByType<MultiAgentSystem>();
		}
	}

	private void Start()
	{
		Play();
	}

	private void Update()
	{
		if (Time.time >= _nextCycleTime && _isRunning && cyclesPerSecond> 0f)
		{
			multiAgentSystem.Cycle();
			_nextCycleTime = Time.time + 1f / cyclesPerSecond;
		}
	}

	public void Play()
	{
		_isRunning = true;
		_nextCycleTime = Time.time;
	}

	public void Stop()
	{
		_isRunning = false;
	}
}