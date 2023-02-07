using UnityEngine;
using UnityEngine.Events;

public class GameActionHandler : MonoBehaviour
{
    //LISTENER
    public GameAction gameActionObj;
    public UnityEvent onRaiseEvent;

    private void Start()
    {
        //game action script has a UnityAction called raise that is public
        //when you invoke it. It will can a function
        //you are subscribing the raise UnityAction to the Raise function
        //whenever Raise is called it will also call whatever is subscribed
        gameActionObj.actionEvent += EventInoker;
    }

    private void EventInoker()
    {
        onRaiseEvent.Invoke();
    }
}