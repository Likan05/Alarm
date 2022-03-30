using System.Collections;
using UnityEngine;

public class CheckDoorOpening : MonoBehaviour
{
    [SerializeField] private Animator _animatorDoor;
    [SerializeField] private AudioSource _audio;

    private bool _isExit;

    private void Start()
    {        
        _audio.volume = 0;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterControllerPlayer>(out CharacterControllerPlayer player))
        {
            _isExit = false;
            _animatorDoor.SetTrigger("Open");
            StartCoroutine(UpperAlarmVolume());
            _audio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterControllerPlayer>(out CharacterControllerPlayer player))
        {
            _isExit = true;
            _animatorDoor.SetTrigger("Open");
            StartCoroutine(LowerAlarmVolume());
        }
    }

    private IEnumerator UpperAlarmVolume()
    {
        while (_audio.volume < 1)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, 1, Time.deltaTime * 0.5f);
            if (_isExit)
            {
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator LowerAlarmVolume()
    {
        while (_audio.volume > 0.0f)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, 0, Time.deltaTime * 0.5f);
            yield return null;
        }

        if (_audio.volume == 0)
        {
            _audio.Stop();
        }
    }
}
