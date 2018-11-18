using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyProjectile : MonoBehaviour {

    [SerializeField]
    private float _speed;
    [SerializeField]
    private string _guns = "Guns";

    private Vector3 _target;
    private bool _shot = false;

    public int Damage;

    private void OnDisable()
    {
        _shot = false;
    }

    public void Shoot(Vector3 target)
    {
        _shot = true;
        _target = target;
    }

    private void Update()
    {
        if (_shot == true)
        {
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target, step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((!other.gameObject.GetComponent<Enemy>()) && (other.gameObject.tag != _guns) && (!other.isTrigger))
        {
            if (other.gameObject.GetComponent<Objective>())//attacks player and tower
            {
                other.gameObject.GetComponent<Objective>().TakeDamage(Damage);
                this.gameObject.SetActive(false);
            }
            else if (other.gameObject.GetComponent<Player>())
            {
                other.gameObject.GetComponent<Player>().TakeDamage(Damage);
                this.gameObject.SetActive(false);
            }
            else if (other.gameObject.GetComponent<Tower>())
            {
                other.gameObject.GetComponent<Tower>().TakeDamage(Damage);
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
