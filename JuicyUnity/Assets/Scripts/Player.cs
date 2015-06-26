using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
    public enum RotatingSides
    {
        Right,
        Left
    }
    public enum InputTypes
    {
        LeftRight,
        UpDown,
        InvLeftRight,
        InvUpDown,
        DoubleTap
    }
    [System.Serializable]
    public class CustomData
    {
        public GameObject graphics;
        public GameObject hook;
        public float doubleTapTime = 0.25f;
    }
    public CustomData data;
    public InputTypes inputType;
    private RotatingSides rotatingSide;
    public enum HealthStates
    {
        Life,
        Death
    }
    public HealthStates healthState;

    public float rotateSpeed;
    public float magnetSpeed = 10f;

    public Dot currentDot;
    [HideInInspector]
    public Dot triggeredDot;
    private float rotateToAngle;
    private bool dragCalled;
    private bool allowHook;
    private BoxCollider boxCollider;
    private float lastTapTime;
    private int doubleTapCounter = 0;
    private Vector3 magnetPosition;
    private Vector3 magnetVelo;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }
    private static Player _instance;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rotateToAngle = transform.eulerAngles.z * -1f;
        currentDot.StartHeat();
        magnetPosition = currentDot.transform.position;
    }

    void Update()
    {
        if (Game.Instance.state != Game.GameStates.Game)
            return;
        if (healthState != HealthStates.Life)
            return;

        Rotate();
        CheckForHook();
        magnetPosition = Vector3.SmoothDamp(transform.position, currentDot.transform.position, ref magnetVelo, magnetSpeed * Time.deltaTime);
        transform.position = magnetPosition;
    }

    void CheckForHook()
    {
        if (triggeredDot == null)
            allowHook = false;
        if (allowHook)
        {
            float distance = Vector3.Distance(triggeredDot.transform.position, data.hook.transform.position);
            Vector3 dotSide = data.hook.transform.InverseTransformPoint(triggeredDot.transform.position);
            float trashold = 0.1f;
            if (data.hook.transform.localPosition.y < 0f)
                dotSide.x *= -1f;
            switch (rotatingSide)
            {
                case RotatingSides.Right:
                    if (dotSide.x > 0f) //точка с правой стороны
                    {
                        if (distance <= trashold)
                        {
                            allowHook = false;
                            ConnectToDot();
                        }
                    }
                    else
                    {
                        allowHook = false;
                        ConnectToDot();
                    }
                    break;
                case RotatingSides.Left:
                    if (dotSide.x < 0f) //точка с левой стороны
                    {
                        if (distance <= trashold)
                        {
                            allowHook = false;
                            ConnectToDot();
                        }
                    }
                    else
                    {
                        allowHook = false;
                        ConnectToDot();
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Вращаем игрока
    /// </summary>
    void Rotate()
    {
        rotateToAngle += rotateSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(
                                transform.localRotation.eulerAngles.x,
                                transform.localRotation.eulerAngles.y,
                                rotateToAngle * -1f);
        if (Mathf.Abs(rotateToAngle) > 360)
            if (rotateToAngle > 0)
                rotateToAngle = rotateToAngle - 360f;
            else
                rotateToAngle = 360 + rotateToAngle;
    }

    /// <summary>
    /// Меняем сторону вращения
    /// </summary>
    /// <param name="side">true - право</param>
    public void ChangeRotation(bool side)
    {
        if (side)
        {
            rotatingSide = RotatingSides.Right;
            if (rotateSpeed < 0)
                rotateSpeed *= -1f;
        }
        else
        {
            rotatingSide = RotatingSides.Left;
            if (rotateSpeed > 0)
                rotateSpeed *= -1f; 
        }  
    }

    /// <summary>
    /// Ведем пальцем по экрану
    /// </summary>
    /// <param name="bed"></param>
    public void Drag(BaseEventData bed)
    {
        PointerEventData data = (PointerEventData)bed;
        Vector2 press = data.pressPosition; 
        Vector2 current = data.position;
        Vector2 difference = new Vector2() { 
            x = current.x - press.x,
            y = current.y - press.y
        };        
        float dragTrashold = 20f;

        switch (inputType)
        {
            case InputTypes.LeftRight:
                if (Mathf.Abs(difference.x) < dragTrashold || dragCalled)
                    return;
                if (difference.x > 0)
                    ChangeRotation(true);
                else
                    ChangeRotation(false);
                break;
            case InputTypes.UpDown:
                if (Mathf.Abs(difference.y) < dragTrashold || dragCalled)
                    return;
                if (difference.y > 0)
                    ChangeRotation(true);
                else
                    ChangeRotation(false);
                break;
            case InputTypes.InvLeftRight:
                if (Mathf.Abs(difference.x) < dragTrashold || dragCalled)
                    return;
                if (transform.localEulerAngles.z >= 90 && transform.localEulerAngles.z < 270)
                {
                    if (difference.x > 0)
                    {
                        if(this.data.graphics.transform.localPosition.y > 0)
                            ChangeRotation(false);
                        else
                            ChangeRotation(true);
                    }
                    else
                    {
                        if (this.data.graphics.transform.localPosition.y > 0)
                            ChangeRotation(true);
                        else
                            ChangeRotation(false);
                    }
                }
                else
                {
                    if (difference.x > 0)
                    {
                        if (this.data.graphics.transform.localPosition.y > 0)
                            ChangeRotation(true);
                        else
                            ChangeRotation(false);
                    }
                    else
                    {
                        if (this.data.graphics.transform.localPosition.y > 0)
                            ChangeRotation(false);
                        else
                            ChangeRotation(true);
                    }
                }
                break;
            case InputTypes.InvUpDown:
                if (Mathf.Abs(difference.y) < dragTrashold || dragCalled)
                    return;
                if (!(transform.localEulerAngles.z >= 0 && transform.localEulerAngles.z < 180))
                {
                    if (difference.y > 0)
                    {
                        if (this.data.graphics.transform.localPosition.y > 0)
                            ChangeRotation(false);
                        else
                            ChangeRotation(true);
                    }
                    else
                    {
                        if (this.data.graphics.transform.localPosition.y > 0)
                            ChangeRotation(true);
                        else
                            ChangeRotation(false);
                    }
                }
                else
                {
                    if (difference.y > 0)
                    {
                        if (this.data.graphics.transform.localPosition.y > 0)
                            ChangeRotation(true);
                        else
                            ChangeRotation(false);
                    }
                    else
                    {
                        if (this.data.graphics.transform.localPosition.y > 0)
                            ChangeRotation(false);
                        else
                            ChangeRotation(true);
                    }
                }
                break;
        }

        dragCalled = true;
    }

    /// <summary>
    /// Отпустили пальчик
    /// </summary>
    /// <param name="bed"></param>
    public void PointerUp(BaseEventData bed)
    {
        dragCalled = false;
    }

    /// <summary>
    /// Нажали пальчиком
    /// </summary>
    /// <param name="bed"></param>
    public void PointerDown(BaseEventData bed)
    {
        ConnectToDotRequest();
        doubleTapCounter++;
        if (doubleTapCounter >= 2 && Mathf.Abs(Time.time - lastTapTime) <= data.doubleTapTime)
        {
            if (inputType == InputTypes.DoubleTap)
            {
                if (rotatingSide == RotatingSides.Left)
                    ChangeRotation(true);
                else
                    ChangeRotation(false);
            }
            doubleTapCounter = 0;            
        }
        lastTapTime = Time.time;
    }

    /// <summary>
    /// пуступил запрос на зацепку к точке
    /// </summary>
    void ConnectToDotRequest()
    {
        if (triggeredDot != null)
            allowHook = true;
    }

    void ConnectToDot()
    {
        Game.Instance.IncScore();
        transform.position = data.hook.transform.position;
        InvertChildrensTransform();
        boxCollider.center = new Vector3(
            boxCollider.center.x,
            boxCollider.center.y * -1f,
            boxCollider.center.z
            );
        currentDot.Hide();
        currentDot = triggeredDot;
        currentDot.StartHeat();
        magnetPosition = currentDot.transform.position;
    }

    /// <summary>
    /// Отражаем по Y координаты
    /// </summary>
    void InvertChildrensTransform()
    {
        data.graphics.transform.localPosition = new Vector3(
            data.graphics.transform.localPosition.x,
            data.graphics.transform.localPosition.y * -1f,
            data.graphics.transform.localPosition.z
            );
        data.hook.transform.localPosition = new Vector3(
            data.hook.transform.localPosition.x,
            data.hook.transform.localPosition.y * -1f,
            data.hook.transform.localPosition.z
            );
    }

    /// <summary>
    /// Умираем
    /// </summary>
    public void Die()
    {
        if (healthState == HealthStates.Death)
            return;
        healthState = HealthStates.Death;
        GetComponent<Rigidbody>().isKinematic = false;
        boxCollider.enabled = false;
        Game.Instance.GameOver();
    }
}