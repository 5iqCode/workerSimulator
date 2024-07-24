using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LBData
{
    public string technoName;
    public string entries;
    public bool isDefault;
    public bool isInvertSortOrder;
    public int decimalOffset;
    public string type;
    public LBPlayerData[] players;
    public LBThisPlayerData thisPlayer;
}

public class LBPlayerData
{
    public int rank;
    public string name;
    public int score;
    public string photo;
    public string uniqueID;
}

public class LBThisPlayerData
{
    public int rank;
    public int score;
}
public class JsonLB
{
    public string technoName;
    public bool isDefault;
    public bool isInvertSortOrder;
    public int decimalOffset;
    public string type;
    public string entries;
    public int[] ranks;
    public string[] photos;
    public string[] names;
    public int[] scores;
    public string[] uniqueIDs;
}


public class leaderBoardController : MonoBehaviour
{
    private LoadedInfo _loadedInfo;
    [SerializeField] private GameObject[] _masObjs;
    [SerializeField] private TMP_Text _valuePlayer;
    public LBData _lbData;

    [SerializeField] private Sprite[] _sprites;
    public void LoadedLeaderbordsInWindow(string data)
    {
        _loadedInfo = GameObject.FindGameObjectWithTag("LoadedInfo").GetComponent<LoadedInfo>();

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
        for (int i = 0; i < 5; i++)
        {
            TMP_Text[] _texts = _masObjs[i].GetComponentsInChildren<TMP_Text>();

            foreach (TMP_Text _text in _texts)
            {
                if (_text.name == "Name")
                {
                    _text.text = _lbData.players[i].name;
                }
                else if (_text.name == "Place")
                {
                    _text.text = (_lbData.players[i].rank).ToString();
                }
                else
                {
                    _text.text = _lbData.players[i].score.ToString();
                }
            }

            if (i < 3)
            {
                foreach (Image _image in _masObjs[i].GetComponentsInChildren<Image>())
                {
                    if (_image.name == "PlayerPhoto")
                    {
                        _image.sprite = _sprites[Random.Range(0, 9)];
                        break;
                    }
                }
            }

        }
        
        _valuePlayer.text = _loadedInfo.PlayerInfo._AllMoneyCount.ToString();

    }


    public void OnClickClose()
    {
        Destroy(gameObject);
    }
}
