using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(InputManager))]
public class GameController : MonoBehaviour
{
    //[System.Serializable]
    //public class SavedTile
    //{
    //    public float x, y, num;
        
    //    public SavedTile(float _x, float _y, float _num)
    //    {
    //        x = _x;
    //        y = _y;
    //        num = _num;
    //    }
    //}

    //public class SavedTiles
    //{
    //    public List<TileInfo> tiles;

    //}

    [SerializeField] private GameObject _tilePrefab;

    [SerializeField] private int _width, _height;
    [SerializeField] private float _travelTime = 0.1f;
    [SerializeField] private GameObject _lose;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _placeBGPrefab;
    [SerializeField] private ThemeManager _themeManager;

    private List<GameObject> _squereList;
    private List<int> _squereMoving;
    private GameObject _selectedTile;
    private int _selectedTileIndex;
    private Vector2 _movindDirection;
    private List<TileInfo> _savedTitles;
    private string _dataKey;

    private void Awake()
    {
        _themeManager = GameObject.Find("ThemeManager").GetComponent<ThemeManager>();
    }


    void Start()
    {
        
        //_imageBg.color = _themeManager.selectedList[13];
        CreateBoxes();
        //_squereList = new List<GameObject>();
        //_dataKey = _width.ToString() + "x" + _height.ToString();


        //CreatePlace();
        //LoadCurState();
        //LoadScore();
        //InitTiles();
        //InitTilesPreview();

        //_mainCamera.transform.position = new Vector3(_width / 2 - 0.5f +  (_width % 2 > 0 ? 0.5f : 0f), _height / 2 - 0.5f + (_height % 2 > 0 ? 0.5f : 0), -10f);
        //_mainCamera.GetComponent<Camera>().orthographicSize = Mathf.Max(_width, _height) + 1;
    }

    void CreateBoxes()
    {
        float x = -2.5f;
        float y = 1.0f;
        float vLine = y;
        float max_x = 2.5f;
        float max_y = 6.0f;
        float scale = (Mathf.Abs(x) + max_x) / _width;
        x += scale/2;
        float hLine = x;
        for (var w = 0; w < _width; w++)
        {
            for (var h = 0; h < _height; h++)
            {
                GameObject ist = Instantiate(_tilePrefab, new Vector3(x,y,0), Quaternion.identity);
                ist.GetComponent<Transform>().localScale = new Vector3(scale,scale,scale);
                ist.GetComponent<Tile>().myScale = scale;
                ist.GetComponent<Tile>().vLine = vLine;
                ist.GetComponent<Tile>().hLine = hLine;
                ist.GetComponent<Tile>().myNum = Random.Range(1, 7) >= 3 ? 2 : 4;
                ist.gameObject.name = vLine.ToString() + "x" + hLine.ToString()+"Box";
                
                vLine += scale;
                
                y += scale * 2;
                //if (y > max_y * 2)
                //{
                    
                //}
            }
            y = 1.0f;
            vLine = y;

            hLine += scale;
            x += scale;
        }
    }












    //#region Load/Save

    //private void ResetData()
    //{
    //    PlayerPrefs.DeleteKey("CurState-" + _dataKey);
    //    PlayerPrefs.DeleteKey("CurScore-" + _dataKey);
    //    PlayerPrefs.Save();
    //}

    //private void LoadPrev()
    //{
    //    if (PlayerPrefs.HasKey("PrevCurScore-" + _dataKey))
    //    {
    //        PlayerPrefs.SetString("CurScore-" + _dataKey, PlayerPrefs.GetString("PrevCurScore-" + _dataKey));
    //    }

    //    if (PlayerPrefs.HasKey("PrevCurState-" + _dataKey))
    //    {
    //        PlayerPrefs.SetString("CurState-" + _dataKey, PlayerPrefs.GetString("PrevCurState-" + _dataKey));
    //    }

    //}

    //private void SavePrev(string key)
    //{
    //    PlayerPrefs.SetString("Prev" + key + _dataKey, PlayerPrefs.GetString(key + _dataKey));
    //    PlayerPrefs.Save();
    //}

    //private void LoadScore()
    //{
    //    if (PlayerPrefs.HasKey("CurScore-" + _dataKey))
    //    {
    //        _score.text = PlayerPrefs.GetString("CurScore-" + _dataKey);
    //    }
    //    else
    //    {
    //        _score.text = "0";
    //    }
    //    if (PlayerPrefs.HasKey("HighScore-" + _dataKey))
    //    {
    //        _highScore.text = PlayerPrefs.GetString("HighScore-" + _dataKey);
    //    }
    //}


    //private void SaveScore()
    //{
    //    SavePrev("CurScore-");
    //    PlayerPrefs.SetString("CurScore-" + _dataKey, _score.text);

    //    if (System.Convert.ToInt32(_score.text) > System.Convert.ToInt32(_highScore.text))
    //    {
    //        _highScore.text = _score.text;
    //        PlayerPrefs.SetString("HighScore-" + _dataKey, _score.text);
    //    }
    //    PlayerPrefs.Save();
    //}

    //private void LoadCurState()
    //{
    //    if (PlayerPrefs.HasKey("CurState-" + _dataKey))
    //    {
    //        string state = PlayerPrefs.GetString("CurState-" + _dataKey);
    //        Debug.Log(state);
    //        SavedTiles tiles = JsonUtility.FromJson<SavedTiles>(state);
    //        _savedTitles = tiles.tiles;
    //    }
    //    else
    //    {
    //        _savedTitles = new List<TileInfo>();
    //    }
    //}

    //private void SaveCurState()
    //{
    //    SavePrev("CurState-");
    //    SaveScore();

    //    SavedTiles tiles = new SavedTiles();
    //    tiles.tiles = new List<TileInfo>();
    //    for (int ii = 0; ii < _squereList.Count; ii++)
    //    {
    //        //_squereList[ii].ResetState();
    //        tiles.tiles.Add(_squereList[ii]._tile);
    //    }

    //    //tiles.tiles = _squereList;
    //    string state = JsonUtility.ToJson(tiles);
    //    tiles.tiles.Clear();
    //    Debug.Log(state);
    //    PlayerPrefs.SetString("CurState-" + _dataKey, state);
    //    PlayerPrefs.Save();


    //}

    //#endregion

    //private void InitTilesPreview()
    //{
    //    int count = 1;
    //    GameObject spawnedTile = null;
    //    for (int ii = 0; ii < _width; ii++)
    //    {
    //        for (int jj = 0; jj < _height; jj++)
    //        {

    //            int num = (int)Mathf.Pow(2, count);
    //            if (ii % 2 > 0 || jj % 2 > 0) count++;
    //            spawnedTile = Instantiate(_squerePrefab, new Vector3(ii, jj), Quaternion.identity);
    //            //spawnedTile.Init(ii, jj, num);
    //        }
    //    }
    //}

    //private void InitTiles()
    //{
    //    int count = 0;
    //    GameObject spawnedTile = null;
    //    for (int ii = 0; ii < _width; ii++)
    //    {
    //        for (int jj = 0; jj < _height; jj++)
    //        {
    //            int num = Random.Range(1, 7) >= 3 ? 2 : 4;
    //            if (_savedTitles.Count != _width * _height)
    //            {
    //                spawnedTile = Instantiate(_squerePrefab, new Vector3(ii, jj), Quaternion.identity);
    //                //spawnedTile.Init(ii, jj, num);
    //            }
    //            else
    //            {
    //                TileInfo tile = _savedTitles[count++];
    //                spawnedTile = Instantiate(_squerePrefab, new Vector3(tile.x, tile.y), Quaternion.identity);
    //                spawnedTile.Init(tile.x, tile.y, tile.number);
    //            }
    //            _squereList.Add(spawnedTile);
    //        }
    //    }
    //    CheckLoose();
    //    SaveCurState();
    //}

    //private void CreatePlace()
    //{
    //    GameObject bg = Instantiate(_placeBGPrefab, new Vector3(_width / 2 - 0.5f + (_width % 2 > 0 ? 0.5f : 0), _height / 2 - 0.5f + (_height % 2 > 0 ? 0.5f : 0)), Quaternion.identity);
    //    bg.transform.localScale = new Vector3(_width + 0.2f, _height + 0.2f);
    //    bg.GetComponent<SpriteRenderer>().color = _themeManager.selectedList[13];

    //    for (int ii = 0; ii < _width; ii++)
    //    {
    //        for (int jj = 0; jj < _height; jj++)
    //        {
    //            Instantiate(_slotPrefab, new Vector3(ii, jj), Quaternion.identity);
    //        }
    //    }
    //}

    //private void Update()
    //{

    //}

    //public void RevertTurn()
    //{

    //    for (int ii = 0; ii < _squereList.Count; ii++)
    //    {
    //        Destroy(_squereList[ii]);
    //    }
    //    _squereList.Clear();

    //    LoadPrev();
    //    LoadCurState();
    //    LoadScore();

    //    InitTiles();


    //}


    //private void MoveTileInit(float _column, float _line, Vector2 _direction)
    //{
    //    _squereMoving = new List<int>();
    //    for (int ii = 0; ii < _squereList.Count; ii++)
    //    {
    //        Tile tempSquere = _squereList[ii];
    //        if (_direction == Vector2.up)
    //        {
    //            if (tempSquere.GetColumn() == _column && tempSquere.GetLine() < _line)
    //            {
    //                _squereMoving.Add(ii);
    //            }
    //        }
    //        else
    //            if (_direction == Vector2.down)
    //        {
    //            if (tempSquere.GetColumn() == _column && tempSquere.GetLine() > _line)
    //            {
    //                _squereMoving.Add(ii);
    //            }
    //        }
    //        else
    //            if (_direction == Vector2.right)
    //        {
    //            if (tempSquere.GetColumn() < _column && tempSquere.GetLine() == _line)
    //            {
    //                _squereMoving.Add(ii);
    //            }
    //        }
    //        else
    //            if (_direction == Vector2.left)
    //        {
    //            if (tempSquere.GetColumn() > _column && tempSquere.GetLine() == _line)
    //            {
    //                _squereMoving.Add(ii);
    //            }
    //        }
    //        tempSquere.ResetState();
    //    }
    //    float x = _column;
    //    float y = _line;
    //    if (_direction == Vector2.up)
    //    {
    //        y = 0;
    //    }
    //    else
    //        if (_direction == Vector2.down)
    //    {
    //        y = _height - 1;
    //    }
    //    else
    //        if (_direction == Vector2.left)
    //    {
    //        x = _width - 1;
    //    }
    //    else
    //        if (_direction == Vector2.right)
    //    {
    //        x = 0;
    //    }

    //    var spawnedTile = Instantiate(_squerePrefab, new Vector3(x, y), Quaternion.identity);
    //    spawnedTile.Init(x, y, Random.Range(1, 7) >= 3 ? 2 : 4);
    //    spawnedTile.transform.localScale = Vector3.zero;
    //    spawnedTile.transform.DOScale(new Vector3(0.95f, 0.95f, 1f), _travelTime);
    //    _squereList.Add(spawnedTile);

    //    _movindDirection = _direction;
    //    // moving = true;
    //    MoveTile();
    //}

    //public void MoveTile()
    //{
    //    for (int ii = 0; ii < _squereMoving.Count; ii++)
    //    {
    //        Tile tempSquere = _squereList[_squereMoving[ii]];
    //        tempSquere.transform.DOMove(tempSquere.transform.position + new Vector3(_movindDirection.x, _movindDirection.y, 0), _travelTime);
    //    }
    //    // moving = false;
    //    _squereMoving.Clear();
    //    Invoke("CheckLoose", _travelTime + 0.02f);
    //    Invoke("SaveCurState", _travelTime + 0.02f);

    //}

    //public void RestartGame()
    //{
    //    for (int ii = 0; ii < _squereList.Count; ii++)
    //    {
    //        Destroy(_squereList[ii]);
    //    }

    //    _score.text = "0";
    //    ResetData();
    //    _squereList.Clear();

    //    LoadCurState();
    //    InitTiles();

    //    _lose.SetActive(false);
    //}

    //public void LoseGame()
    //{
    //    for (int ii = 0; ii < _squereList.Count; ii++)
    //    {
    //        _squereList[ii].GetComponent<BoxCollider2D>().enabled = false;
    //    }
    //    Debug.Log("lose game");
    //    _lose.SetActive(true);
    //}

    //public void CheckLoose()
    //{
    //    //Debug.Log("Check lose");
    //    for (int ii = 0; ii < _squereList.Count; ii++)
    //    {
    //        Vector2 pos1 = _squereList[ii].transform.position;
    //        for (int jj = 0; jj < _squereList.Count; jj++)
    //        {
    //            if (ii == jj) continue;
    //            Vector2 pos2 = _squereList[jj].transform.position;
    //            if ((Mathf.Abs(pos1.x - pos2.x) == 1 && Mathf.Abs(pos1.y - pos2.y) == 0) || (Mathf.Abs(pos1.x - pos2.x) == 0 && Mathf.Abs(pos1.y - pos2.y) == 1))
    //            {
    //                if (_squereList[ii].GetNumber() == _squereList[jj].GetNumber())
    //                    return;
    //            }
    //        }
    //    }
    //    //Debug.Log("Lose");
    //    LoseGame();
    //}


    //public bool Select(Tile _selected)
    //{
    //    bool findSelected = false;
    //    var selPos = _selected.GetComponent<Transform>().position;
    //    for (int ii = 0; ii < _squereList.Count; ii++)
    //    {
    //        if (_squereList[ii].CheckSelected())
    //        {
    //            findSelected = true;
    //        }
    //        _squereList[ii].ResetState();
    //    }
    //    if (findSelected) return false;

    //    for (int ii = 0; ii < _squereList.Count; ii++)
    //    {
    //        Tile tempSquere = _squereList[ii];
    //        var pos = tempSquere.GetComponent<Transform>().position;
    //        if ((Mathf.Abs(pos.x - selPos.x) == 1 && Mathf.Abs(pos.y - selPos.y) == 0) || (Mathf.Abs(pos.x - selPos.x) == 0 && Mathf.Abs(pos.y - selPos.y) == 1))
    //        {
    //            if (tempSquere.GetNumber() == _selected.GetNumber())
    //                tempSquere.HihgtliteLikeOther();
    //        }
    //        if (pos == selPos)
    //        {
    //            _selectedTileIndex = ii;
    //        }
    //    }
    //    _selectedTile = _selected;

    //    return true;
    //}

    //public void SelectOther(Tile _otherSelected)
    //{
    //    Vector2 _direction = new Vector2(_otherSelected.transform.position.x - _selectedTile.transform.position.x, _otherSelected.transform.position.y - _selectedTile.transform.position.y);
    //    _otherSelected.UpdateNumber();
    //    _otherSelected.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), 0.1f);
    //    int score = System.Convert.ToInt32(_score.text);
    //    score += _otherSelected.GetNumber();
    //    _score.text = score.ToString();
    //    _squereList.RemoveAt(_selectedTileIndex);
    //    Destroy(_selectedTile.gameObject);
    //    MoveTileInit(_otherSelected.transform.position.x, _otherSelected.transform.position.y, _direction);

    //}



}
