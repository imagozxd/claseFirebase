using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    [SerializeField] private string userID;
    private DatabaseReference reference;

    [SerializeField] private User customUser;


    private void Awake()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
    }

    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        //Invoke(nameof(GetUserInfo), 1f);
    }

    public void CreateUser()
    {
        User newUser = new User("Pedro", "Piedrito", 9781235);
        string json = JsonUtility.ToJson(newUser);

        reference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }
    public void CreateCustomUser()
    {
        string json = JsonUtility.ToJson(customUser);

        reference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    public IEnumerator GetFirstName(Action<string> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).Child("firstName").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if(userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallBack?.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator GetLastName(Action<string> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).Child("lastName").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallBack?.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator GetCustomAttribute(string field,Action<string> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).Child("lastName").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallBack?.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator GetCodeID(Action<int> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).Child(nameof(User.codeID)).GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            //(int) -> Casting
            //int.Parse -> Parsing
            //https://teamtreehouse.com/community/when-should-i-use-int-and-intparse-whats-the-difference
            onCallBack?.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }

    public void GetUserInfo()
    {
        StartCoroutine(GetFirstName(PrintData));
        StartCoroutine(GetLastName(PrintData));
        StartCoroutine(GetCodeID(PrintData));
    }

    private void PrintData(string name)
    {
        Debug.Log(name);
    }

    private void PrintData(int code)
    {
        Debug.Log(code);
    }
}

[System.Serializable]
public class User
{
    public string firstName;
    public string lastName;
    public int codeID;

    public User(string firstName, string lastName, int codeID)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.codeID = codeID;
    }
}