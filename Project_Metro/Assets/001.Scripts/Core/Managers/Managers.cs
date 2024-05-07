using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    private ObjectManager _object;
    private ResourceManager _resource;
    private UIManager _ui;
    private PoolManager _pool;
    private InputManager _input;
    private ObstacleManager _obstacle;
    private DataManager _data;
    private MonsterManager _monster;
    private BossManager _boss;
    private SceneManager _scene;
    private ScreenManager _screen;
    private CoroutineManager _coroutine;
    private DialogManager _dialog;
    private EventManager _event;

    private GameManager _game;

    private static bool init = false;

    public static ObjectManager Object { get { return Instance?._object; } }
    public static GameManager Game { get { return Instance?._game; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static InputManager Input { get { return Instance?._input; } }
    public static ObstacleManager Obstacle { get { return Instance?._obstacle; } }
    public static DataManager Data { get { return Instance?._data; } }
    public static MonsterManager Monster { get {  return Instance?._monster; } }
    public static BossManager Boss { get {  return Instance?._boss; } }
    public static SceneManager Scene { get { return Instance?._scene; } }
    public static ScreenManager Screen { get { return Instance?._screen; } }
    public static CoroutineManager Routine { get { return Instance?._coroutine; } }
    public static DialogManager Dialog { get { return Instance?._dialog; } }
    public static EventManager Event { get { return Instance?._event; } }

    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        if (init) return;
        Instance._object = new ObjectManager();
        Instance._resource = new ResourceManager();
        Instance._ui = new UIManager();
        Instance._pool = new PoolManager();
        Instance._input = new InputManager();
        Instance._obstacle = new ObstacleManager();
        Instance._data = new DataManager();
        Instance._monster = new MonsterManager();
        Instance._boss = new BossManager();
        Instance._scene = new SceneManager();
        Instance._screen = new ScreenManager();
        Instance._dialog = new DialogManager();
        Instance._event = new EventManager();

        Instance._coroutine = CoroutineManager.Instance;
        Instance._game = GameManager.Instance;
        init = true;
    }
}
