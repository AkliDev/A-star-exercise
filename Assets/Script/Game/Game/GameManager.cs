using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public interface IGameState
{
    void OnEnter();
    void Update();
    void OnExit();
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public IGameState m_State;

    public GameObject m_StartCanvas;
    public GameObject m_PlayCanvas;

    public Slider m_MovementSlider;
    void Awake()
    {
        CreateInstance();
        m_State = new EditMode(this);
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {

    }


    void Update()
    {
        m_State.Update();
    }

    public void SwitchState(IGameState state)
    {
        m_State.OnExit();
        m_State = state;
        m_State.OnEnter();
    }
}

public class EditMode : IGameState
{
    GameManager m_RefObject;

    public EditMode(GameManager refObject)
    {
        m_RefObject = refObject;
    }

    public void OnEnter()
    {
        m_RefObject.m_StartCanvas.SetActive(true);
        m_RefObject.m_PlayCanvas.SetActive(false);
        m_RefObject.GetComponent<TileChanger>().enabled = true;
        Grid.instance.ResetSelect();
    }
    public void Update()
    {

    }
    public void OnExit()
    {
        m_RefObject.m_StartCanvas.SetActive(false);
        m_RefObject.GetComponent<TileChanger>().enabled = false;
    }
}

public class MoveMode : IGameState
{
    GameManager m_RefObject;

    public MoveMode(GameManager refObject)
    {
        m_RefObject = refObject;
    }

    public void OnEnter()
    {
        m_RefObject.m_PlayCanvas.SetActive(true);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject hax = Grid.instance.GetHaxClicked();
            if (hax != null)
            {
                HaxNode haxNode = hax.GetComponent<HaxNode>();

                m_RefObject.SwitchState(new ShowReachAble(m_RefObject, haxNode));
            }
        }
    }
    public void OnExit()
    {

    }
}

public class ShowReachAble : IGameState
{
    GameManager m_RefObject;
    HaxNode m_SelectedNode;
    HaxNode[] m_Reachable;

    public ShowReachAble(GameManager refObject, HaxNode node)
    {
        m_RefObject = refObject;
        m_SelectedNode = node;
    }
    public void OnEnter()
    {
        m_RefObject.m_MovementSlider.onValueChanged.AddListener(UpdateReach);
        m_Reachable = Grid.instance.ShowReachable(m_SelectedNode, (int)m_RefObject.m_MovementSlider.value);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject hax = Grid.instance.GetHaxClicked();
            if (hax != null)
            {
                HaxNode haxNode = hax.GetComponent<HaxNode>();
                if (ArrayUtility.Contains(m_Reachable, haxNode))
                {

                    m_RefObject.SwitchState(new ShowPath(m_RefObject, m_SelectedNode, haxNode));
                }
            }
        }
    }

    private void UpdateReach(float value)
    {
        m_Reachable = Grid.instance.ShowReachable(m_SelectedNode, (int)value);
    }

    public void OnExit()
    {
        m_RefObject.m_MovementSlider.onValueChanged.RemoveListener(UpdateReach);
    }
}

public class ShowPath : IGameState
{
    GameManager m_RefObject;
    HaxNode m_MoveToHax;
    HaxNode[] m_Path;
    HaxNode PrePos;

    public ShowPath(GameManager refObject, HaxNode prePos, HaxNode moveToHax)
    {
        m_RefObject = refObject;
        m_MoveToHax = moveToHax;
        PrePos = prePos;
    }

    public void OnEnter()
    {
        Grid.instance.ShowPath(PrePos, m_MoveToHax);
        m_RefObject.SwitchState(new MoveMode(m_RefObject));
    }
    public void Update()
    {

    }
    public void OnExit()
    {

    }
}