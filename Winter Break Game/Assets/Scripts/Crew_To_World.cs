using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crew_To_World : MonoBehaviour
{
    [SerializeField] private Crew_Stats crewStats;
    [SerializeField] private UnityEvent startEvent;
    private Renderer crewRenderer;

    private void Awake()
    {
        crewRenderer = GetComponent<Renderer>();
        crewStats.OnStart(this.gameObject);
    }

    public void SetCrewColor()
    {
        crewRenderer.material.color = crewStats.crewColor;
    }
}
