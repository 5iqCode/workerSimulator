using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;

public class TriggerPolka : MonoBehaviour
{
    [SerializeField] private GameObject _errorMessage;


    [SerializeField] private GameObject _stats;
    private GameObject _instStats;
    private BottleStats _instStatsScript;

    [SerializeField] private GameObject _messagePC;

    [SerializeField] private GameObject _messageMobile;

    private GameObject _showedMessage;

    [SerializeField] private int _idBottles;

    private bool showMessage = false;
    private LoadedInfo _loadedInfo;
    private bool _isDesktop;
    private Transform _messageCanvas;

    public List<Vector3> _positionTakedBottles;

    private PlayerItemsController _playerItemsController;

    private bool _canAddBottles = false;

    private Coroutine _Job;

    private SpawnerBottlesInPolks _spawnerBottle;

    [SerializeField] private TMP_Text _textPlayerBox;

    [SerializeField] private GameObject _triggerBottleImage;

    private AudioSource _audioSource;
    private GameObject _imageMobileButton;

    private string _language;
    private void Start()
    {
        _audioSource = GetComponentInParent<AudioSource>();


        _spawnerBottle = GameObject.Find("Spawner").GetComponent<SpawnerBottlesInPolks>();


        _playerItemsController = GameObject.Find("PlayerItemsController").GetComponent<PlayerItemsController>();

        _loadedInfo = GameObject.Find("LoadedInfo").GetComponent<LoadedInfo>();
        _language = _loadedInfo._Language;
        _isDesktop = _loadedInfo._isDesktop;
        if (_isDesktop == false)
        {
            _imageMobileButton = Resources.Load<GameObject>("mobileImages/SetBottles");
        }
        _messageCanvas = GameObject.Find("Canvas").transform;

        if(gameObject.name == "polka1")
        {
            _textPlayerBox.text = _loadedInfo.PlayerInfo._countPlayerItemsBoxBottles.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            _instStats = Instantiate(_stats, transform);
            _instStatsScript = _instStats.GetComponent<BottleStats>();
            _instStatsScript.SetStats(30 - _positionTakedBottles.Count);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player")
        {
            if (_playerItemsController.selectedItem == 1)
            {
                if (_positionTakedBottles.Count > 0)
                {
                    if (showMessage == false)
                    {
                        if (isWorked == false)
                        {
                            ShowMessage();
                            showMessage = true;

                            _canAddBottles = true;
                        }

                    }
                }
            }
            else
            {
                StopShowMessage();
            }

        }
    }
    private bool isWorked;
    private void Update()
    {
        if (_canAddBottles)
        {
            if (_isDesktop)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartAddBottlesWork();
                }
            }
            else
            {
                if(_buttonScript.IsClicked == true)
                {
                    _buttonScript.IsClicked = false;
                    StartAddBottlesWork();
                }
            }

        }
        else
        {
            if (isWorked)
            {
                StopCoroutine(_Job);
            }
        }
    }
    private void StartAddBottlesWork()
    {
        if (_Job == null)
        {
            isWorked = true;
            _Job = StartCoroutine(AddBottleCor());
            showMessage = false;
            DestroyMessage();
        }
    }

    IEnumerator AddBottleCor()
    {
        while (_positionTakedBottles.Count > 0)
        {
            if (_canAddBottles)
            {
                if (_playerItemsController.playerBoxItemCountBottles > 0)
                {
                    GameObject _point = new GameObject("PointBottle");
                    _point.transform.position = _positionTakedBottles[0];
                    GameObject[] _bottles = GameObject.FindGameObjectsWithTag("bottleInHandPlayer");


                    for (int i = _bottles.Length - 1; i >= 0; i--)
                    {
                        if (_bottles[i].name == (_playerItemsController.playerBoxItemCountBottles - 1).ToString())
                        {
                            _audioSource.Play();


                            Vector3 tempPos = _bottles[i].transform.position;

                            Destroy(_bottles[i]);

                            _bottles[i] = Instantiate(_spawnerBottle._bottles[_idBottles]);
                            _bottles[i].tag = "bottleInHandPlayer";
                            _bottles[i].transform.position = tempPos;

                            _bottles[i].transform.parent = _point.transform;
                            _playerItemsController.playerBoxItemCountBottles--;
                            _loadedInfo.PlayerInfo._countPlayerItemsBoxBottles--;

                            if (_loadedInfo.PlayerInfo._countPlayerItemsBoxBottles==0)
                            {
                                if (GameObject.FindGameObjectsWithTag("TriggerBottleImage").Length == 0)
                                {
                                    Instantiate(_triggerBottleImage);
                                }
                            }

                            _textPlayerBox.text = _playerItemsController.playerBoxItemCountBottles.ToString();

                            _bottles[i].transform.localScale *= 1.43f;

                            MovePorduct _movePorduct = _bottles[i].AddComponent<MovePorduct>();
                            _movePorduct._parent = transform;

                            _instStatsScript.SetStats(31 - _positionTakedBottles.Count);
                            break;
                        }
                    }

                    _positionTakedBottles.RemoveAt(0);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    GameObject _error = Instantiate(_errorMessage, _messageCanvas);

                    string message = "Закончились бутылки в корзине!";
                    if (_language == "en")
                    {
                        message = "There are no more bottles in the basket!";
                    }
                    _error.GetComponent<TMP_Text>().text = message;
                    break;
                }
            }
            else
            {
                break;
            }


        }
        isWorked = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            _Job = null;
            StopShowMessage();

            if (_instStats != null)
            {
                Destroy(_instStats);
            }
        }
    }
    private void StopShowMessage()
    {
        showMessage = false;
        _canAddBottles = false;
        isWorked = false;
        DestroyMessage();
    }
    private MobileButtonScript _buttonScript;
    private void ShowMessage()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Message");
        if (objs.Length > 0)
        {
            foreach (GameObject _obj in objs)
            {
                Destroy(_obj);
            }
        }

        if (_isDesktop)
        {
            _showedMessage = Instantiate(_messagePC, _messageCanvas);
            string message = "У - Заполнить полки";
            if (_language == "en")
            {
                message = "E - Fill the shelves";
            }
            _showedMessage.GetComponentInChildren<TMP_Text>().text = message;
        }
        else
        {
            _showedMessage = Instantiate(_messageMobile, _messageCanvas);
            Instantiate(_imageMobileButton, _showedMessage.transform);
            _buttonScript = _showedMessage.GetComponent<MobileButtonScript>();
        }

    }

    private void DestroyMessage()
    {
        if (_showedMessage != null)
        {
            Destroy(_showedMessage);
        }

    }
}
