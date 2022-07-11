using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float vLine = 0.0f;
    public float hLine = 0.0f;
    public float myScale;
    public int myNum;
    private int myNumToDraw;

    public Material myMaterial;
    public GameObject myGhost;
    public GameObject prefGhost;
    public TextMeshProUGUI text;

    [SerializeField] private ThemeManager _themeManager;

    private void Awake()
    {
        _themeManager = GameObject.Find("ThemeManager").GetComponent<ThemeManager>();
    }

    private void Start()
    {
        UpdateColor();
    }


    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (myNum > myNumToDraw)
        {
            int stepToAdd = (myNum - myNumToDraw) / 20;
            if (stepToAdd == 0) stepToAdd = 1;
            myNumToDraw = Mathf.Clamp(myNumToDraw + stepToAdd, myNumToDraw, myNum);
            UpdateNum();
            UpdateColor();
        }
    }

    void UpdateColor()
    {
        text.color = _themeManager.GetColor(myNum);
    }


    public void UpdateNum()
    {
        text.text = myNumToDraw.ToString();// + "\n" + vLine.ToString();//+gameObject.name.ToString();
    }

    public void ResetNum()
    {
        myNum = Random.Range(1, 7) >= 3 ? 2 : 4;
        myNumToDraw = myNum;
        UpdateNum();
        UpdateColor();
    }
    //private bool selected = false;
    //private bool selectedOther = false;
    //public TileInfo _tile;

    //[SerializeField] private GameObject _highlits;
    //[SerializeField] private GameObject _highlitsOther;
    //[SerializeField] private TextMeshPro _textMesh;
    //[SerializeField] private GameController _controller;
    //[SerializeField] private ThemeManager _themeManager;

    //public void Init(float _column,float _line, int _number)
    //{
    //    _tile.number = _number;
    //    _tile.x = _column;
    //    _tile.y = _line;
    //    _textMesh.text = _tile.number.ToString();
    //    this.GetComponent<SpriteRenderer>().color = _themeManager.selectedList[Mathf.Clamp(Mathf.RoundToInt(Mathf.Log(_tile.number, 2)) - 1, 0, 11)];
    //}

    //private void Awake()
    //{
    //    _tile = new TileInfo();
    //    _controller = GameObject.Find("GameController").GetComponent<GameController>();
    //    _themeManager = GameObject.Find("ThemeManager").GetComponent<ThemeManager>();
    //}

    //private void Update()
    //{
    //    _tile.x = this.transform.position.x;
    //    _tile.y = this.transform.position.y;
    //}

    //public void OnPressed()
    //{
    //    if (selectedOther)
    //    {
    //        _controller.SelectOther(this);
    //    }
    //    else
    //    {
    //            selected = _controller.Select(this);
    //        if (selected)
    //        {
    //            //_highlits.SetActive(true);
    //            this.transform.localScale = new Vector3(1f, 1f, 1f);
    //        }
    //    }
    //}

    //public void ResetState()
    //{
    //    //_highlits.SetActive(false);
    //    //_highlitsOther.SetActive(false);
    //    this.transform.localScale = new Vector3(0.95f, 0.95f, 1f);
    //    selected = false;
    //    selectedOther = false;
    //    _tile.x = this.transform.position.x;
    //    _tile.y = this.transform.position.y;
    //}

    //public void UpdateNumber()
    //{
    //    _tile.number *= 2;
    //    _textMesh.text = _tile.number.ToString();
    //    this.GetComponent<SpriteRenderer>().color = _themeManager.selectedList[Mathf.Clamp(Mathf.RoundToInt(Mathf.Log(_tile.number, 2)) - 1, 0, 11)];
    //}

    //public void HihgtliteLikeOther()
    //{
    //    selectedOther = true;
    //    //_highlitsOther.SetActive(true);
    //}

    //public bool CheckSelected()
    //{
    //    return selected;
    //}

    //public int GetNumber()
    //{
    //    return _tile.number;
    //}
    //public float GetColumn()
    //{
    //    return _tile.x;
    //}

    //public float GetLine()
    //{
    //    return _tile.y;
    //}

    //private void OnDestroy()
    //{
    //    Destroy(gameObject);
    //    Destroy(_textMesh);
    //    Destroy(_highlits);
    //    Destroy(_highlitsOther);
    //}
}
