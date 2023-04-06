using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameConfig _config;
    [SerializeField] private UnitController _unitController;
    [SerializeField] private PlayerControl _playerControl;

    [Space]
    [Header("UI")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _replayBtn;
    [SerializeField] private Button _rforceReplayBtn;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _uiPanel;


    public int _targetLevel = 1;
    
    private void Awake()
    {
        _startPanel.SetActive(true);
        _uiPanel.SetActive(false);
        _losePanel.SetActive(false);
        _playBtn.onClick.AddListener(StartGame);
        _replayBtn.onClick.AddListener(RestartGame);
        _rforceReplayBtn.onClick.AddListener(RestartGame);
    }


    public void RestartGame()
    {
        _playerControl.UpdateUnitData(_config._playerData);

        _unitController.ClearAllUnits();
        _uiPanel.SetActive(true);
        _startPanel.SetActive(false);
        _losePanel.SetActive(false);
        _playerControl.gameObject.SetActive(true);
        _playerControl.ForseUpdate();
        _unitController.PrepareEnemys(_config._existedLevels.Find(x => x._levelNumber == _targetLevel));
    }


    [ContextMenu("test")]
    public void StartGame()
    {
        _playerControl.UpdateUnitData(_config._playerData);
        
        _unitController.ClearAllUnits();
        _uiPanel.SetActive(true);
        _startPanel.SetActive(false);
        _losePanel.SetActive(false);
        _playerControl.gameObject.SetActive(true);
        _unitController.PrepareEnemys(_config._existedLevels.Find(x => x._levelNumber == _targetLevel));
    }

    public void GameOver()
    {
        _unitController.ClearAllUnits();
        _losePanel.gameObject.SetActive(true);
    }
}
