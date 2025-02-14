using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAnim : MonoBehaviour
{
    private Animator runnerAnimator;
    private Runner runner;
    private bool isHurtCouroutineRunning = false;
    
    private void Start()
    {
        runner = GetComponent<Runner>();
        runnerAnimator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (runner.currentState == Runner.RunnerState.Idle) runnerAnimator.SetBool("isRunning", false);
        else if (runner.currentState == Runner.RunnerState.Chase) runnerAnimator.SetBool("isRunning", true);
        else if (runner.currentState == Runner.RunnerState.Hurt && !isHurtCouroutineRunning)
        {
            StartCoroutine(RunnerHurt());
        }
    }

    public IEnumerator RunnerHurt()
    {
        isHurtCouroutineRunning = true;
        runnerAnimator.SetTrigger("Hurt");
        yield return new WaitForSeconds(0.4f);
        runnerAnimator.ResetTrigger("Hurt");
        isHurtCouroutineRunning = false;


    }
}
