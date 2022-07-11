using UnityEngine;
using UnityEngine.InputSystem;



public class InputManager : MonoBehaviour
{
    private InputController _input;
    private Vector2 _mousePosition;
    private Vector2 _beginPosition;
    private float _beginTime;
    private bool _pressed = false;
    private bool finishSwipe = true;
    private GameObject _hitTile;
    private bool md = false;

    
    private void Awake()
    {
        _input = new InputController();
    }

    private void Start()
    {
        _input.main.press.performed += context => OnPress();
        _input.main.release.performed += context => OnRelease();
        _input.main.posMouse.performed += context => UpdatePosition(context.ReadValue<Vector2>());
    }


    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }

    private void UpdatePosition(Vector2 context)
    {
        //Debug.Log("upd position");
        _mousePosition = context;
        
    }

    private void OnPress()
    {
        Debug.Log("Pressed");
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(_mousePosition.x, _mousePosition.y, Camera.main.gameObject.transform.position.z));
        RaycastHit hit;
        //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        //Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(_mousePosition.x, _mousePosition.y, -10)).ToString());
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Box")
        {
            _hitTile = hit.collider.gameObject;
            md = true;
        }
        
    }

    private void OnRelease()
    {
        Debug.Log("Released");
        if (!md) return;
        md = false;
        if (_hitTile != null)
        {


            Ray ray = Camera.main.ScreenPointToRay(new Vector3(_mousePosition.x, _mousePosition.y, Camera.main.gameObject.transform.position.z));
            RaycastHit hit;
            //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            //Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(_mousePosition.x, _mousePosition.y, -10)).ToString());
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Box")
            {
                if (hit.collider.gameObject != _hitTile)
                {
                    string hitEndTileName = hit.collider.gameObject.name;
                    Tile startTile = _hitTile.GetComponent<Tile>();
                    string topName = (startTile.vLine + startTile.myScale).ToString() + "x" + startTile.hLine.ToString() + "Box";
                    string bottomName = (startTile.vLine - startTile.myScale).ToString() + "x" + startTile.hLine.ToString() + "Box";
                    string leftName = startTile.vLine.ToString() + "x" + (startTile.hLine - startTile.myScale).ToString() + "Box";
                    string rightName = startTile.vLine.ToString() + "x" + (startTile.hLine + startTile.myScale).ToString() + "Box";
                    if ((hitEndTileName == topName || hitEndTileName == bottomName || hitEndTileName == leftName || hitEndTileName == rightName)
                        && startTile.myNum == hit.collider.gameObject.GetComponent<Tile>().myNum) {

                        hit.collider.gameObject.GetComponent<Tile>().myNum *= 2;

                        Tile tl = _hitTile.GetComponent<Tile>();
                        string strNextToFind = (tl.vLine + tl.myScale).ToString() + "x" + tl.hLine.ToString() + "Box";
                        GameObject boxToMove = GameObject.Find(strNextToFind);
                        GameObject topBox = _hitTile;
                        string topBoxName = _hitTile.name;
                        while (boxToMove != null)
                        {
                            topBox = boxToMove;
                            topBoxName = topBox.gameObject.name;
                            tl = boxToMove.GetComponent<Tile>();
                            strNextToFind = (tl.vLine + tl.myScale).ToString() + "x" + tl.hLine.ToString() + "Box";
                            tl.vLine -= tl.myScale;
                            boxToMove.gameObject.name = tl.vLine.ToString() + "x" + tl.hLine.ToString() + "Box";
                            tl.UpdateNum();
                            boxToMove = GameObject.Find(strNextToFind);
                        }
                        _hitTile.GetComponent<Rigidbody>().position =
                            new Vector3(_hitTile.GetComponent<Rigidbody>().position.x,
                            topBox.GetComponent<Tile>().vLine + _hitTile.GetComponent<Tile>().myScale * 2,
                            _hitTile.GetComponent<Rigidbody>().position.z);
                        _hitTile.name = topBoxName;
                        if(topBox != _hitTile)_hitTile.GetComponent<Tile>().vLine = topBox.GetComponent<Tile>().vLine + topBox.GetComponent<Tile>().myScale;
                        _hitTile.GetComponent<Tile>().ResetNum();
                        _hitTile.GetComponent<Tile>().UpdateNum();
                    }
                }
            }
            _hitTile = null;
        }

    }

    private GameObject FindCollision(Vector2 point)
    {
        GameObject collisionObject = null;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(point.x, point.y, -1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(_mousePosition.x, _mousePosition.y, -10)).ToString());
        if (hit.collider != null && hit.collider.gameObject.tag == "tile")
        {
             //Debug.Log("hit");
            collisionObject = hit.collider.gameObject;
            //_hitTile.GetComponent<Tile>().OnPressed();

        }

        return collisionObject;
    }

    private void Update()
    {

        if (_pressed&&!finishSwipe)
        {
            if(Time.time - _beginTime > 1f)
            {
                //Debug.Log("time exeption");
                //_hitTile.GetComponent<Tile>().OnPressed();
                //_pressed = false;
                finishSwipe = true;
            }
            else
            {
                Vector2 gamePosition = Camera.main.ScreenToWorldPoint(new Vector3(_mousePosition.x, _mousePosition.y, -10));
                if (Mathf.Abs(_beginPosition.x - gamePosition.x) > 0.3f || Mathf.Abs(_beginPosition.y - gamePosition.y) > 0.3f)
                {
                    //Debug.Log("distance ok");
                    Swipe();
                    
                }
            }
        }

    }

    private void Swipe()
    {

            //Debug.Log("Swipe");
            GameObject mergeTarget = FindCollision(_mousePosition);
            if (mergeTarget != null && mergeTarget != _hitTile)
            {
                //Debug.Log("find and press");
                //mergeTarget.GetComponent<Tile>().OnPressed();
                //_pressed = false;
                finishSwipe = true;
            }
            else
            {
            if (mergeTarget == null) Debug.Log("merge null");
                //Debug.Log("!find");
                //_hitTile.GetComponent<Tile>().OnPressed();
            }
    }
}
