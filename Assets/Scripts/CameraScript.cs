using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform fpvPosition;
    private InputAction lookAction;

    private Vector3 c;
    private bool fpv = true;
    private float mX, mY; 
    private float sensitivityH = 10, sensitivityV = 5, sensitivityW = 0.1f;
    private float fpvRange = 0.6f;
    private float maxDistance = 5f;


    void Start()
    {
        c = this.transform.position - player.transform.position;
        mX = this.transform.eulerAngles.y;
        mY = this.transform.eulerAngles.x;
        lookAction = InputSystem.actions.FindAction("Look");
        GameState.Subscribe(OnSensitivityChanged, nameof(GameState.sensitivityX), nameof(GameState.sensitivityY));
    }
    private void Update()
    {
        if (fpv) {

            Vector2 mouseWheel = Input.mouseScrollDelta * Time.timeScale;
            if (mouseWheel.y != 0)
            {
                if (c.magnitude > maxDistance)
                {
                    c = c.normalized * maxDistance;
                }
                if (c.magnitude > fpvRange)
                {
                    c = c * (1 - mouseWheel.y * sensitivityW);
                    if (c.magnitude < fpvRange)
                    {
                        c = c * 0.01f;
                        GameState.isFpv = true;
   
                    }
                }
                else
                {
                    if (mouseWheel.y < 0)
                    {
                        c = c / c.magnitude * fpvRange * 1.01f;
                        GameState.isFpv = false;
                    }

                }
            }
            Vector2 lookValue = lookAction.ReadValue<Vector2>() * Time.deltaTime;
            mX += lookValue.x * sensitivityH;
            float my = -lookValue.y * sensitivityV;
            if(0 <= mY + my && mY + my <= 75){
                mY += my;
            }
           
            this.transform.eulerAngles = new Vector3(mY,mX,0);
            
        }
        if (Input.GetKeyDown(KeyCode.Tab) && Time.timeScale != 0)
        {
            fpv = !fpv;

            if (!fpv)
            {
                this.transform.position = fpvPosition.position;
                this.transform.rotation = fpvPosition.rotation;
            }
            
        }
    }

    private void OnSensitivityChanged()
    {
        // [0..1] ---> [1, 10, 20]
        
        sensitivityH = Mathf.Lerp(1, 20, GameState.sensitivityX);
        sensitivityV = Mathf.Lerp(1, 20, GameState.sensitivityY);
    }



    private void OnDestroy()
    {
        GameState.UnSubscribe(OnSensitivityChanged, nameof(GameState.sensitivityX), nameof(GameState.sensitivityY));
    }
    void LateUpdate()
    {
        if (fpv)
        {
            this.transform.position = Quaternion.Euler(0, mX,0) * c + player.transform.position;
        }
       
    }
}
