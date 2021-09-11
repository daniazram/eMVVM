using UnityEngine;

public class TitleScreenUIModel : ViewModelBase
{
    [SerializeField, FieldToProperty("LevelNumber")]
    private string levelNumber;

    [Bindable]
    public string LevelNumber
    {
        get => levelNumber;
        set => Set(ref levelNumber, value);
    }
}