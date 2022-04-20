using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TargetScript : MonoBehaviour
{
    private Transform m_player;
    private Transform m_myTransform;
    private Vector3 v_positionMove;

    public Image fill;

    private Targets m_myTargets;
    public BoxCollider[] targetColliders;

    private float f_chergeTime = 1f;
    private bool  b_selected,b_unSelected = false;
    private float f_currentTIme = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        m_myTargets = GameObject.FindGameObjectWithTag("target").GetComponent<Targets>();

        m_myTransform = transform;

        for (int i = 0; i < m_myTargets.targetColliders.Length; i++) // Add all colliders from main array
        {
            targetColliders[i] = m_myTargets.targetColliders[i];
        }

        v_positionMove = new Vector3 (m_myTransform.position.x, m_player.position.y, m_myTransform.position.z); // Update pos
    }

    private void FixedUpdate()
    {
        if (b_unSelected == false) {

            if (b_selected == true)
            {
                if (f_currentTIme < f_chergeTime) //Fill
                {
                    f_currentTIme += Time.fixedDeltaTime / f_chergeTime;
                    fill.fillAmount = f_currentTIme;
                }
                else if (f_currentTIme >= f_chergeTime)
                {
                    StartCoroutine(PlayerMove());
                    b_selected = false;
                }
            }
        }
        else 
        {
            if (f_currentTIme > 0) // Only run if f_currentTime is 0.1 or more
            {
                f_currentTIme -= Time.fixedDeltaTime / 0.5f;
                fill.fillAmount = f_currentTIme;
            }
        }
    }

    public void PointEnter()
    {
        b_unSelected = false;
        b_selected = true;
    }

    public void PointExit()
    {
        b_unSelected = true;
        b_selected = false;
    }

    private IEnumerator PlayerMove()
    {
        foreach (BoxCollider myCollider in targetColliders) // off colliders
        {
            myCollider.enabled = false;
        }
        m_player.DOMove(v_positionMove, 1.5f).SetEase(Ease.InSine);

        yield return new WaitForSeconds(1.5f);

        foreach (BoxCollider myCollider in targetColliders) // On colliders
        {
            myCollider.enabled = true;
        }
    }
}
