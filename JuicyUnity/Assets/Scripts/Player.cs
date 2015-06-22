using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
    [System.Serializable]
    public class CustomData
    {
        public GameObject graphics;
        public GameObject hook;
    }
    public CustomData data;
    public enum HealthStates
    {
        Life,
        Death
    }
    public HealthStates healthState;

    public float rotateSpeed;

    public Dot currentDot;
    [HideInInspector]
    public Dot triggeredDot;
    private float rotateToAngle;
    private bool dragCalled;
    private bool allowHook;
    private BoxCollider boxCollider;

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
    }

    void Update()
    {
        if (Game.Instance.state != Game.GameStates.Game)
            return;
        if (healthState != HealthStates.Life)
            return;
        Rotate();

        if (triggeredDot == null)
            allowHook = false;
        if (allowHook)
        {
            float distance = Vector3.Distance(triggeredDot.transform.position, data.hook.transform.position);
            if (distance <= 0.1f)
            {
                allowHook = false;
                ConnectToDot();
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
    }

    /// <summary>
    /// Меняем сторону вращения
    /// </summary>
    /// <param name="side">true - право</param>
    public void ChangeRotation(bool side)
    {
        if (side)
        {
            if (rotateSpeed < 0)
                rotateSpeed *= -1f;
        }
        else
        {
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
        float difference = current.x - press.x;        
        float dragTrashold = 10f;
        if (Mathf.Abs(difference) < dragTrashold || dragCalled)
            return;
        
        if (difference > 0)
            ChangeRotation(true);

        if (difference < 0)
            ChangeRotation(false);

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
        transform.position = triggeredDot.transform.position;
        InvertChildrensTransform();
        boxCollider.center = new Vector3(
            boxCollider.center.x,
            boxCollider.center.y * -1f,
            boxCollider.center.z
            );
        currentDot.Hide();
        currentDot = triggeredDot;
        currentDot.StartHeat();
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