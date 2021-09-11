# eMVVM
Easy and simple MVVM for Unity UI,  highly inspired by [Unity Weld](https://github.com/Real-Serious-Games/Unity-Weld) and [Fruit Ninja 2's Tech Talk](https://www.youtube.com/watch?v=IDGmxSBt3D4).
It enables simple binding between Unity's UI elements and your game's logic, which results in less cross referencing of components and ultimately much less clutter to worry about.

As this project is in it's early stages, it provide following features for now. (More features will be added soon)
- One way data binding. e.g. bind simple UI elements to simple C# primitive types (int, float, string etc)
- Custom editor to make the binding easier
- Property attribute that let's you change C# properties through inspector 

**Note:** current version has only been tested with UnityEngine.UI.Text and C# strings, though it would work with other C# primitive types but I haven't tested other UI elements to bind those types.

# Getting Started
It's super simple to get started with this package, simply download the package from [release](https://github.com/daniazram/eMVVM/releases) section and import it.
The implemention could be visualized as Game Logic -> ViewModel -> View -> ViewModel -> Game Logic, the game logic interacts with the view model which  changes the view and changes in view could be communicated to view model which can alert the game logic.

So you just have to create view models that will interact with your game logic and update the UI through bindings. To better understand let's look at an example.

### Example
Let's say in a mobile game you want to show the current level on title screen, you can use eMVVM in following way.
- Create a ViewModel named `TitleScreenUIModel.cs` (which inherits from `ViewModelBase.cs`) with a property named `LevelNumber : string`
- Now back in Unity Editor do following
  - Create a canvas (if not already created) and add an empty transform to act as view model holder, name it `TitleScreenUI` and stretch it to fit the whole canvas.
  - Create a simple text (or TMPro text), place it appropriately in the UI. Name it `text-levelNumber`.
  - Attach `TitleScreenUIModel` component to the  `TitleScreenUI` object.
  - Attach `DataBinder` component to the `text-levelNumber`. (`DataBinder` is already available for you)
  - On `DataBinder` component set the `Target Property` to `UnityEngine.UI.Text.text` and `Source Value` to `TitleScreenUIModel.LevelNumber`. This will bind the text field in UI to the property in view model, now whenever that property will change we will see that in UI text.  (You do this through Unity Editor as can be seen in one of the screenshots below)
- Use your game logic to update the `TitleScreenUIModel`'s `LevelNumber` property and chagnes will automatically be reflected in the UI through binding you just did in the previous step.
  
### Implementation Details
Few points to note from the example listed above.
- For a property to be binded from Unity Editor you must have to assign `[Bindable]` attribute to that property. Like `LevelNumber` property is using `[Bindable]` attribute.
- Every Bindable property has to use the speical `Set` method defined in `ViewModelBase.cs`. Like  `LevelNumber` property is using this method in it's setter.

``` 
public class ViewModelBase : MonoBehaviour, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void Set<T>(ref T target, T newValue, [CallerMemberName]string propertyName = "")
    {
        target = newValue;
        PropertyChanged?.Invoke(target, new PropertyChangedEventArgs(propertyName));
    }
}
```

Couple of screenshots to further assist in understanding the working.

**Sample scene setup and binding.**
![binding](https://user-images.githubusercontent.com/12896256/132950021-725b2e43-88d8-45dc-8101-bc1129470852.png)

**Custom View Model from the above example**
![viewModel](https://user-images.githubusercontent.com/12896256/132950026-6eb4058a-5178-4901-bb58-2b836c5b5fb7.PNG)

# FieldToPropertyAttribute
Since binding works with the properties but Unity doesn't exopse properties to inspector which means we can't update view model property through editor. But with this attribute we can encapsulate fields through properties and bind those fields to property through this attribute which will call setter on the property whenever that field is updated through editor.

![attrib](https://user-images.githubusercontent.com/12896256/132950895-6194fd70-e548-431a-8e23-b43a74d2fc06.PNG)

![attrib-gif](https://user-images.githubusercontent.com/12896256/132950905-6dd38bab-6a48-4228-8a93-c9bfcf742fdd.gif)


