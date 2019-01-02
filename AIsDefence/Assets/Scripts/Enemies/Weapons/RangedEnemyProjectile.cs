using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyProjectile : MonoBehaviour {

    [SerializeField]
    private float _speed;
    [SerializeField]
    private string _guns = "Guns";
    [SerializeField]
    private Vector3 _target;
    [SerializeField]
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

            if (transform.position == _target)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((!other.gameObject.GetComponent<Enemy>()) && (!other.isTrigger))
        {
            if (other.gameObject.GetComponent<Objective>())//attacks player and tower
            {
                other.gameObject.GetComponent<Objective>().TakeDamage(Damage);
                gameObject.SetActive(false);
            }
            else if (other.gameObject.GetComponent<Player>())
            {
                other.gameObject.GetComponent<Player>().TakeDamage(Damage);
                gameObject.SetActive(false);
            }
            else if (other.gameObject.transform.parent.GetComponent<Tower>())
            {
                other.gameObject.transform.parent.GetComponent<Tower>().TakeDamage(Damage);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
