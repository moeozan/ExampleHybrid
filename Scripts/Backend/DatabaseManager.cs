using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
    private FirebaseAuth auth;
    public static DatabaseManager instance;

    public UserData Data { get; private set; }
    
    private CostMenu costMenu;
    private PropertyManager propertyManager;
    private EnemyPooling enemyPooling;
    private EnemyDetector enemyDetector;
    private TargetHandler targetHandler;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        auth = FirebaseAuth.DefaultInstance;
        GetUserData();
        StartCoroutine(WaitForDataInitialize());
    }

    public IEnumerator WaitForDataInitialize()
    {
        yield return new WaitUntil(() => Data != null);
        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            Debug.Log("GamePlay Scene Active");
            propertyManager = FindObjectOfType<PropertyManager>();
            enemyPooling = FindObjectOfType<EnemyPooling>();
            targetHandler = FindObjectOfType<TargetHandler>();
            enemyDetector = FindObjectOfType<EnemyDetector>();
            enemyPooling.enabled = true;
            propertyManager.enabled = true;
            targetHandler.enabled = true;
            enemyDetector.enabled = true;
        }
    }

    private DocumentReference CheckCurrentUser()
    {
        FirebaseFirestore _firestore = FirebaseFirestore.DefaultInstance;
#if UNITY_EDITOR
        CollectionReference collectionRef = _firestore.Collection("TestUsers");
#elif PLATFORM_ANDROID
        CollectionReference collectionRef = _firestore.Collection("Users");
#endif
        DocumentReference documentRef = collectionRef.Document(auth.CurrentUser.DisplayName);
        return documentRef;
    }

    public void SaveUser(UserData userData)
    {
        SaveUserToFireStore(userData);
    }

    private void SaveUserToFireStore(UserData data)
    {
        FirebaseFirestore _firestore = FirebaseFirestore.DefaultInstance;
#if UNITY_EDITOR
        CollectionReference collectionRef = _firestore.Collection("TestUsers");
#elif PLATFORM_ANDROID
        CollectionReference collectionRef = _firestore.Collection("Users");
#endif
        DocumentReference documentRef = collectionRef.Document(data.DisplayName);
        documentRef.SetAsync(data).ContinueWithOnMainThread((task) =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Player Added with ID : " + documentRef.Id);
            }
            else if (task.IsFaulted)
            {
                Debug.Log("Error : " + task.Exception.InnerException.ToString());
            }
        });
        
    }

    public void SetInventory(Inventory inventory)
    {
        DocumentReference documentRef = CheckCurrentUser();
        var data = new UserData
        {
            UserInventory = inventory
        };

        Dictionary<string, object> updates = new ()
        {
            { "UserInventory", inventory }
        };

        documentRef.UpdateAsync(updates).ContinueWithOnMainThread((task) =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Inventory Updated for : " + documentRef.Id);
            }
            else if (task.IsFaulted)
            {
                Debug.Log("Error : " + task.Exception.InnerException.ToString());
            }
        });

        
    }

    public void UpdateTalents(Talents talents)
    {
        SetTalents(talents);
    }
    private void SetTalents(Talents talents)
    {
        DocumentReference documentRef = CheckCurrentUser();
        Dictionary<string, object> updates = new ()
        {
            { "Talents", talents }
        };

        documentRef.UpdateAsync(updates).ContinueWithOnMainThread((task) =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Talents Updated for : " + documentRef.Id);
            }
            else if (task.IsFaulted)
            {
                Debug.Log("Error : " + task.Exception.InnerException.ToString());
            }
        });
    }
    
    private void GetUserData()
    {
        DocumentReference documentRef = CheckCurrentUser();
        documentRef.GetSnapshotAsync().ContinueWithOnMainThread((task) =>
        {
            if (task.IsCompleted)
            {
                Data = task.Result.ConvertTo<UserData>();
            }
        });
    }
    public Talents GetTalents()
    {
        GetUserData();
        return Data.Talents;
    }

    public float GetGold()
    {
        GetUserData();
        return Data.Gold;
    }

    public Inventory GetInventoryInformation()
    {
        GetUserData();
        return Data.UserInventory;
    }

    public void AddGold(float amount)
    {
        SetGold(amount);
    }
    private void SetGold(float amount)
    {
        float temp = GetGold() + amount;
        temp = GameUtilities.FloatHandler(temp);
        DocumentReference documentRef = CheckCurrentUser();
        Dictionary<string, object> updates = new()
        {
            { "Gold", temp }
        };
        documentRef.UpdateAsync(updates).ContinueWithOnMainThread((task) =>
        {
            if (task.IsCompleted)
            {
                if (costMenu == null)
                {
                    costMenu = FindObjectOfType<CostMenu>();
                }
                else
                {
                    costMenu.SetGoldText(temp);
                }
            }
        });
        
    }
}
