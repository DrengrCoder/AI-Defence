using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _lifeTime = 10f;

    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        float step = _speed * Time.deltaTime;
        transform.Translate(0, _speed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((!other.gameObject.GetComponent<Player>()) && (!other.isTrigger))
        {
            if (other.gameObject.GetComponent<Enemy>())//attacks player and tower
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
