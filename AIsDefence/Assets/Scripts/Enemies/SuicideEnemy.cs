using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemy : Enemy {

    [SerializeField]
    private ParticleSystem _explode;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == EnemyTarget)//ignores players and towers
        {
            Attack(other.gameObject);
        }
    }

    private void Attack(GameObject target)
    {
        if (target.GetComponent<Objective>())
        {
            target.GetComponent<Objective>().TakeDamage(Damage);
        }

        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
        _explode.Play();
        AttackSound.Play();

        for (int i = 0; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(3.0f);

        for (int i = 0; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(true);
        }
        CreditsOnDeath = 0;
        Death();
    }

}
