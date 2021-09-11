using UnityEngine;

public class TitleScreenUIModel : ViewModelBase
{
    [SerializeField, FieldToProperty("LevelNumber")] // Don't worry about this FieldToPropertyAttribute for now
    private string levelNumber;

    [Bindable]
    public string LevelNumber
    {
        get => levelNumber;
        set => Set(ref levelNumber, value);
    }
}