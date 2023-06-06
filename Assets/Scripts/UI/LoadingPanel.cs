public class LoadingPanel : Panel
{
    public static LoadingPanel instance;
    
    private void Awake() => instance = this;
}
