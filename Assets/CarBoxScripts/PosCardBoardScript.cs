using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCardBoardScript : MonoBehaviour
{
    [SerializeField] private List<Transform> _posPoint;

    private PlayerItemsController _playerItemsController;

    public SpawnerCarWork _spawnerCarWork;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_playerItemsController._isTakedBoxOnCar)
        {
            if (other.gameObject.name == "Player")
            {
                CarBoxScript _obj = other.gameObject.GetComponentInChildren<CarBoxScript>();
                _obj.IsEndedBox = true;
                _obj.DropBox();

                Destroy(_obj.GetComponent<Rigidbody>());
                Destroy(_obj.GetComponent<BoxCollider>());

                MovePorduct _movePorduct = _obj.gameObject.AddComponent<MovePorduct>();
                _movePorduct._parent = transform;

                Transform _pos = _posPoint[Random.Range(0, _posPoint.Count)];
                _movePorduct.transform.parent = _pos;
                _movePorduct._parent = transform;

                _posPoint.Remove(_pos);

                _playerItemsController.TakeNothing();

                _spawnerCarWork.AddProgress();

                _audioSource.pitch = Random.Range(1f, 2f);
                _audioSource.Play();
            }
        }

    }
}
