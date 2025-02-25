using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimHandle : MonoBehaviour
{

    // Hidden Animator is used to invoke animation events 
    private Animator animator;
    private Animator hiddenAnimator;
    private Player player;
    private string currentAnimationPlaying;
    private bool canSetAttackTriggerAgain = true;
    private bool canSetHurtTriggerAgain = true;
    private void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
        hiddenAnimator = GetComponent<Animator>();
       
    }

    private void Update()
    {
        if (player.currentState == Player.PlayerState.Idle)
        {
            animator.SetBool("isRunning", false);
            currentAnimationPlaying = "Idle";
        }

        if (player.currentState == Player.PlayerState.Run)
        {
            animator.SetBool("isRunning", true);
            currentAnimationPlaying = "Run";
        }

        if (player.currentState == Player.PlayerState.Attack && canSetAttackTriggerAgain)
        {
            animator.SetTrigger("Attack");
            canSetAttackTriggerAgain = false;
            currentAnimationPlaying = "Attack";
            StartCoroutine(CanPlayAttackAnimAgain());
        }

        if (player.currentState == Player.PlayerState.Hurt && canSetHurtTriggerAgain)
        {
            animator.SetTrigger("Hurt");
           
            canSetHurtTriggerAgain = false;
            currentAnimationPlaying = "Hurt";
            StartCoroutine(CanPlayHurtAnimAgain());
        }
    }

    public void SetDeathTrigger()
    {
        animator.SetTrigger("Death");
        hiddenAnimator.SetTrigger("Death");
    }


    public string GetCurrentAnimationPlaying()
    {
        return currentAnimationPlaying;
    }


    private IEnumerator CanPlayAttackAnimAgain()
    {
        yield return new WaitForSeconds(player.attackCooldown);
        canSetAttackTriggerAgain = true;
        animator.ResetTrigger("Attack");
    }

    public IEnumerator CanPlayHurtAnimAgain()
    {
        yield return new WaitForSeconds(player.hurtCooldown);
        canSetHurtTriggerAgain = true;
        animator.ResetTrigger("Hurt");
    }
}
