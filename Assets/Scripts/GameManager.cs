using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool simulationRunning = false;
    [SerializeField] float stepDuration = 1f;

    [SerializeField] Grid gameGrid;

    float nextStep;

    // Update is called once per frame
    void Update()
    {
        if (!simulationRunning) return;
        
        if(Time.time > nextStep)
        {
            gameGrid.CountAliveNeighborsOnGrid();
            gameGrid.ApplyRules();

            nextStep = Time.time + stepDuration;
        }
        
    }

    public void StartSimulation()
    {
        simulationRunning = true;
    }
}
