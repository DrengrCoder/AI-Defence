using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private int _range = 50;
    [SerializeField]
    private float _fireRate = 0.3f;
    private float _nextFire = 0.0f;

    [SerializeField]
    private EndGameStats _stats;

    [SerializeField]
    private float _fowardZ = 0.0f;
    [SerializeField]
    private float _backZ = 0.0f;
    private float _currentZ = 0.0f;

    [SerializeField]
    private ParticleSystem _muzzle;
    [SerializeField]
    private LineRenderer _lazer;

    [SerializeField]
    private AudioSource _sound;

    private void Start()
    {
        _currentZ = _fowardZ;
        _lazer.enabled = false;
    }

    private void Shoot()
    {
        _muzzle.Play();
        _sound.Play();

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(_lazer.gameObject.transform.position, fwd * _range, Color.green);

        //RayCast
        Ray ray = new Ray(_lazer.gameObject.transform.position, fwd * _range);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hit.distance <= _range)
            {
                if (!hitObject.GetComponent<Player>())
                {
                    if (hitObject.GetComponent<Enemy>())//attacks player and tower
                    {
                        _stats.Hits = _stats.Hits + 1;
                        _stats.DamageDealt = _stats.DamageDealt + _damage;

                        bool killed = hitObject.GetComponent<Enemy>().TakeDamage(_damage);

                        if (hitObject.GetComponent<MeleeEnemy>())
                        {
                            hitObject.GetComponent<MeleeEnemy>().Enrage();
                        }

                        if (killed == true)
                        {
                            _stats.Kills = _stats.Kills + 1;
                        }
                    }
                }
                _lazer.SetPosition(1, hit.point);//Lazer endpoint
            }
        }
        else
        {
            _lazer.SetPosition(1, ray.GetPoint(_range));//Lazer endpoint
        }

        //Lazer show
        _lazer.SetPosition(0, ray.origin);
        StartCoroutine(FireLazer());

    }

    IEnumerator FireLazer()
    {
        _lazer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        _lazer.enabled = false;
    }

    private void Update()
    {
        if (_nextFire >= _fireRate)
        {
            if (Input.GetButton("Fire1") == true)
            {
                Shoot();

                _nextFire = 0.0f;
                _currentZ = _currentZ - 0.1f;
                if (_currentZ < _backZ)
                {
                    _currentZ = _backZ;
                }
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _currentZ);
            }
        }
        else
        {
            _currentZ = _currentZ + (0.1f * Time.deltaTime);
            if (_currentZ > _fowardZ)
            {
                _currentZ = _fowardZ;
            }
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _currentZ);
                _nextFire = _nextFire + Time.deltaTime;
        }
    }

}
