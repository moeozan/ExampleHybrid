using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Firestore;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FirebaseLoginManager : MonoBehaviour
{
    public static FirebaseLoginManager instance;
    public TMP_Text informationText;

    //Firebase variables
    [Header("Firebase")]
    private DependencyStatus dependencyStatus;
    private FirebaseAuth auth;
    private FirebaseUser User;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject DBManager;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        instance = this;
    }

    public void QuickLogin()
    {
        StartCoroutine(Login("moe@moe.com", "784478"));
    }
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
        SceneManager.LoadSceneAsync(1);
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //informationText.text = "Logging";
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(() => LoginTask.IsCompleted);
        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            //informationText.text = message;
            Debug.Log(message);
        }
        else
        {
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.Email, User.DisplayName);
            //informationText.text = "Logged In";
            //informationText.text = "";
            if (DBManager == null || canvas == null)
            {
                SceneManager.LoadScene("Menu");
                yield break;
            }
            DBManager.SetActive(true);
            canvas.SetActive(true);
        }
    }

    private IEnumerator Register(string _email, string _password, string userName)
    {
        if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            informationText.text = "Password Does Not Match!";
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(() => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                informationText.text = message;
            }
            else
            {
                string message = string.Empty;
                if (RegisterTask.Result != null)
                {
                    UserProfile profile = new()
                    {
                        DisplayName = userName
                    };

                    User = RegisterTask.Result.User;

                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        message = $"Failed to register task with {ProfileTask.Exception}";
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        informationText.text += "\nUsername Set Failed!";
                    }
                    else
                    {
                        UserData userdata = new(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text);
                        DatabaseManager.instance.SaveUser(userdata);
                        informationText.text = "Register Success";
                    }
                }
            }
        }
    }
}