
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RotateMoneyInfo : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;

    [SerializeField] private Image _color;


    private void Start()
    {

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        GetComponent<Canvas>().worldCamera = _mainCamera;
    }
    private void FixedUpdate()
    {
        if (transform.rotation != _mainCamera.transform.rotation)
        {
            transform.rotation = _mainCamera.transform.rotation;
        }
    }

    public void ChangeStats(float value)
    {
        _text.text = value.ToString();
        _slider.value = value;

        if (value > 25000)
        {
            _color.color = Color.red;
        }
        else if (value >15000)
        {
            _color.color = Color.yellow;
        } else 
        {
            _color.color = Color.green;
        }
    }
}
