using System.Collections;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    [SerializeField] private Animator _animatorDoor;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _speedOfChangeSound;

    private const string Open = nameof(Open);
    private bool _isOpen;
    private bool _isRunnig;

    private void Start()
    {
        _speedOfChangeSound = 0.5f;
        _audio.volume = 0;
        _isRunnig = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isOpen = collision.TryGetComponent<ÑharacterMovement>(out ÑharacterMovement player);

        if (_isOpen)
        {            
            _animatorDoor.SetTrigger(Open);
            if (!_isRunnig)
            {
                StartCoroutine(ActivateAlarm());
            }
            _audio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isOpen = false;

        if (collision.TryGetComponent<ÑharacterMovement>(out ÑharacterMovement player))
        {
            _animatorDoor.SetTrigger(Open);
            if (!_isRunnig)
            {
                StartCoroutine(ActivateAlarm());
            }
        }        
    }

    private void Update()
    {
        Debug.Log(_isRunnig);
    }

    private IEnumerator ActivateAlarm()
    {
        _isRunnig = true;
        byte maxTargetVolume = 1;
        byte minTargetvolume = 0;

        while (CheckTerms())
        {
            if (_isOpen)
            {
                ChangeVolume(maxTargetVolume);
            }
            else
            {
                ChangeVolume(minTargetvolume);
            }
            yield return null;
        }

        if (_audio.volume == 0)
        {
            _audio.Stop();
        }
        _isRunnig = false;
    }

    private void ChangeVolume(byte number)
    {
        _audio.volume = Mathf.MoveTowards(_audio.volume, number, Time.deltaTime * _speedOfChangeSound);
    }

    private bool CheckTerms()
    {
        if (_audio.volume < 1 && _isOpen)
            return true;
        else if (_audio.volume > 0f && !_isOpen)
            return true;
        else return false;
    }
}
