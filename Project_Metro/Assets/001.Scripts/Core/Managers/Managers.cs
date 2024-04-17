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
    private TriggerManager _trigger;

    private GameManager _game;

    public static ObjectManager Object { get { return Instance?._object; } }
    public static GameManager Game { get { return Instance?._game; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static InputManager Input { get { return Instance?._input; } }
    public static ObstacleManager Obstacle { get { return Instance?._obstacle; } }
    public static TriggerManager Trigger { get { return Instance?._trigger; } }



    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    private static void Init()
    {
        Instance._object = new ObjectManager();
        Instance._resource = new ResourceManager();
        Instance._ui = new UIManager();
        Instance._pool = new PoolManager();
        Instance._input = new InputManager();
        Instance._obstacle = new ObstacleManager();
        Instance._trigger = new TriggerManager();
        Instance._game = GameManager.Instance;
    }
}
