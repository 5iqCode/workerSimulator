using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardInGameScene : MonoBehaviour
{
    private LoadedInfo _loadedInfo;

    [SerializeField] private Sprite[] _sprites;

    [SerializeField] private TMP_Text _text1;
    [SerializeField] private TMP_Text _text2;
    public LBData _lbData;

    private List<GameObject> _masObjs = new List<GameObject>();

    private void Start()
    {
        _loadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

        if (_loadedInfo._Language == "en")
        {
            _text1.text = "The Board of Honor";
            _text2.text = "* The best players in the world are on this board!!!";
        }
        if (_loadedInfo.PlayerInfo._canUseLeaders)
        {
            _loadedInfo.ClickLiaders();
        }
    }
    public void LoadedLeaderbordsInWindow(string data)
    {

        foreach (MarkParentImage _parImg in GetComponentsInChildren<MarkParentImage>())
        {
            _masObjs.Add(_parImg.gameObject);
        }

        JsonLB jsonLB = JsonUtility.FromJson<JsonLB>(data);

        LBData lbData = new LBData()
        {
            technoName = jsonLB.technoName,
            isDefault = jsonLB.isDefault,
            isInvertSortOrder = jsonLB.isInvertSortOrder,
            decimalOffset = jsonLB.decimalOffset,
            type = jsonLB.type,
            entries = jsonLB.entries,
            players = new LBPlayerData[jsonLB.names.Length],
            thisPlayer = null
        };

        for (int i = 0; i < jsonLB.names.Length; i++)
        {
            lbData.players[i] = new LBPlayerData();
            lbData.players[i].name = jsonLB.names[i];
            lbData.players[i].rank = jsonLB.ranks[i];
            lbData.players[i].score = jsonLB.scores[i];
            lbData.players[i].photo = jsonLB.photos[i];
            lbData.players[i].uniqueID = jsonLB.uniqueIDs[i];
        }
        _lbData = lbData;

        UpdateInfoInWindow();
    }

    private void UpdateInfoInWindow()
    {
        for (int i = 0; i < 3; i++)
        {
            TMP_Text[] _texts = _masObjs[i].GetComponentsInChildren<TMP_Text>();

            foreach (TMP_Text _text in _texts)
            {
                if (_text.name == "Name")
                {
                    _text.text = _lbData.players[i].name;
                }
                else
                {
                    _text.text = _lbData.players[i].score.ToString();
                }
            }
            foreach (Image image in GetComponentsInChildren<Image>())
            {
                if(image.name == "PhotoPers")
                {
                    image.sprite = _sprites[Random.Range(0, 9)];
                }
            }
        }

        Destroy(this);
    }
}
