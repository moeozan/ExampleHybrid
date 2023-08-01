using Firebase.Firestore;

[FirestoreData]
public class UserData
{
    [FirestoreProperty] public string Email { get; set; }
    [FirestoreProperty] public string Password { get; set; }
    [FirestoreProperty] public string DisplayName { get; set; }
    [FirestoreProperty] public float Gold { get; set; } = 500;
    [FirestoreProperty] public Inventory UserInventory { get; set; } = new Inventory();
    [FirestoreProperty] public Talents Talents { get; set; } = new Talents();

    public UserData() { }

    public UserData(string email, string password, string displayName)
    {
        Email = email;
        Password = password;
        DisplayName = displayName;
        //UserInventory = new Inventory();
        Talents = new Talents();
    }

    public UserData(string displayName)
    {
        DisplayName = displayName;
        //UserInventory = new Inventory();
        Talents = new Talents();
    }
}
