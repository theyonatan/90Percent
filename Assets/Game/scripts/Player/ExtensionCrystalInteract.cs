public class ExtensionCrystalInteract : ExtensionInteractier
{
    private void Awake()
    {
        InteractableType = typeof(Crystal);
    }

    private void Start()
    {
        base.Start();
        UnsubscribeFromDefaultInteract();
        _inputDirector.OnFirePressed += OnPressedInteract;
    }

    private void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        _inputDirector.OnFirePressed -= OnPressedInteract;
    }
}
