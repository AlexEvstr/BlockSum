using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneObstacleCollision : MonoBehaviour
{
    [SerializeField] private GameObject[] _badEffects;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private LifeSystem _lifeSystem;
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private GameAudioManager _gameAudioManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balloon"))
        {
            GameObject badEffect = Instantiate(_badEffects[1]);
            badEffect.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
            Destroy(badEffect, 1f);
            _lifeSystem.TakeDamage();
            _cameraShake.TriggerShake();
            _gameAudioManager.PlayMinusLife();
        }

        else if (collision.gameObject.CompareTag("Trash"))
        {
            GameObject badEffect = Instantiate(_badEffects[0]);
            badEffect.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
            Destroy(badEffect, 1f);
            _lifeSystem.TakeDamage();
            _cameraShake.TriggerShake();
            _gameAudioManager.PlayMinusLife();
        }

        else if (collision.gameObject.CompareTag("Meteorite"))
        {
            ContactPoint2D contactPoint = collision.GetContact(0);
            Vector2 contactPosition = contactPoint.point;
            GameObject explosion = Instantiate(_explosion);
            explosion.transform.position = contactPosition;
            Destroy(explosion, 0.25f);
            _lifeSystem.TakeDamage();
            _cameraShake.TriggerShake();
            _gameAudioManager.PlayMinusLife();
        }
    }
}