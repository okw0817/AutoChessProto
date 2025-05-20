
public class WorldManager : ManagerBase<WorldManager>, IState<WorldState>
{
    #region Members : Private
    private WorldState state;
    #endregion

    #region Members : Property
    public WorldState State { 
        get => state;
        set
        {
            if (value == state)
                return;

            state = value;
            ChangeState(value);
        }
    }
    #endregion

    #region Methods : Mono
    private void Start()
    {
        State = WorldState.Init;
    }
    #endregion

    #region Methods : Override
    public override void Init()
    {
        var initializers = GetComponentsInChildren<IInitializer>(true);

        var worldInitializer = (IInitializer)this; //Initialize �������̽��� ����� Ŭ������ �ʱ�ȭ.
        foreach (var initializer in initializers)
        {
            if (initializer == worldInitializer)
                continue;
                
            initializer.Init();
        }

        State = WorldState.Login;
    }
    #endregion

    #region Methods : Private
    public void ChangeState(WorldState state) //IState�� ����Ͽ� ���°� ���� �� ���� �ൿ�� �ٲ�.
    {
        switch(state)
        {
            case WorldState.Init:
                Init();
                break;
            case WorldState.Login:
                {
                    var command = new UIPageCommand(UIPageString.Login, true);
                    command.SetData(UIDataType.Title.ToString(), "Login Page");
                    command.SetData(UIDataType.Content.ToString(), "Login Content");
                    command.SetCallback(UIDataType.Callback.ToString(), () => print("Login Success"));
                    command.Excute();

                    State = WorldState.Main;
                }
                break;
            case WorldState.Main:
                {
                    var command = new SceneCommand(SceneName.Main, true);
                    command.Excute();

                    //var command2 = new UIPageCommand(UIPageString.Login, false);
                    //command2.Excute();
                }
                break;
        }
    }
    #endregion
}
