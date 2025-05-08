using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class DataTests
{
    GameObject loginObject;
    LoginHandler loginHandler;
    MockFileDataHandler mockDataHandler;
    TextMeshProUGUI mockMenuText;
    GameObject loginPanel;
    GameObject mainMenuPanel;

    [SetUp]
    public void GlobalSetup()
    {
        if (GameManager.instance == null)
        {
            new GameObject("GameManager").AddComponent<GameManager>();
        }
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        loginObject = new GameObject("LoginHandler");
        loginHandler = loginObject.AddComponent<LoginHandler>();

        mockDataHandler = new MockFileDataHandler();

        // Inject the mock data handler into the LoginHandler
        loginHandler.SetDataHandler(mockDataHandler);

        mockMenuText = new GameObject("MenuText").AddComponent<TextMeshProUGUI>();
        SetPrivateField(loginHandler, "_menuText", mockMenuText);

        loginPanel = new GameObject("LoginPanel");
        mainMenuPanel = new GameObject("MainMenuPanel");
        SetPrivateField(loginHandler, "_loginPanel", loginPanel);
        SetPrivateField(loginHandler, "_mainMenuPanel", mainMenuPanel);

        loginPanel.SetActive(true);
        mainMenuPanel.SetActive(false);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(loginObject);
        Object.Destroy(mockMenuText.gameObject);
        Object.Destroy(loginPanel);
        Object.Destroy(mainMenuPanel);
        yield return null;
    }

    private void SetPrivateField<T>(object obj, string fieldName, T value)
    {
        var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field == null)
            throw new System.ArgumentException($"Field '{fieldName}' not found on {obj.GetType().Name}");
        field.SetValue(obj, value);
    }

    [Test]
    public void LoginButton_Fails_With_Incorrect_Credentials()
    {
        loginHandler.SetUsername("TestUser");
        loginHandler.SetPassword("WrongPassword");
        mockDataHandler.SetMockData(new UserData("TestUser", "CorrectPassword"));

        loginHandler.LoginButton();

        Assert.AreEqual("Account not found or password incorrect.", mockMenuText.text);
        Assert.AreEqual(Color.red, mockMenuText.color);
        Assert.IsTrue(loginPanel.activeSelf);
        Assert.IsFalse(mainMenuPanel.activeSelf);
    }

    [Test]
    public void LoginButton_Succeeds_With_Correct_Credentials()
    {
        loginHandler.SetUsername("TestUser");
        loginHandler.SetPassword("CorrectPassword");
        mockDataHandler.SetMockData(new UserData("TestUser", "CorrectPassword"));

        loginHandler.LoginButton();

        Assert.AreEqual("TestUser", GameManager.instance.currentUsername);
        Assert.IsFalse(loginPanel.activeSelf);
        Assert.IsTrue(mainMenuPanel.activeSelf);
    }

    [Test]
    public void CreateAccountButton_Fails_If_Username_Is_Empty()
    {
        loginHandler.SetUsername("");
        loginHandler.SetPassword("ValidPassword");

        loginHandler.CreateAccountButton();

        Assert.AreEqual("Invalid username.", mockMenuText.text);
        Assert.AreEqual(Color.red, mockMenuText.color);
        Assert.IsFalse(mockDataHandler.WasSaveCalled);
    }

    [Test]
    public void CreateAccountButton_Fails_If_Password_Is_Empty()
    {
        loginHandler.SetUsername("NewUser");
        loginHandler.SetPassword("");

        loginHandler.CreateAccountButton();

        Assert.AreEqual("Invalid Password.", mockMenuText.text);
        Assert.AreEqual(Color.red, mockMenuText.color);
        Assert.IsFalse(mockDataHandler.WasSaveCalled);
    }

    [Test]
    public void CreateAccountButton_Fails_If_Username_Taken()
    {
        loginHandler.SetUsername("ExistingUser");
        loginHandler.SetPassword("NewPassword");
        mockDataHandler.SetMockData(new UserData("ExistingUser", "OldPassword"));

        loginHandler.CreateAccountButton();

        Assert.AreEqual("User already taken.", mockMenuText.text);
        Assert.AreEqual(Color.red, mockMenuText.color);
        Assert.IsFalse(mockDataHandler.WasSaveCalled);
    }

    [Test]
    public void CreateAccountButton_Succeeds_With_Valid_Credentials()
    {
        loginHandler.SetUsername("NewUser");
        loginHandler.SetPassword("SecurePassword");
        mockDataHandler.SetMockData(null);

        loginHandler.CreateAccountButton();

        Assert.AreEqual("Account created. Please login with your credentials.", mockMenuText.text);
        Assert.AreEqual(Color.green, mockMenuText.color);
        Assert.IsTrue(mockDataHandler.WasSaveCalled);
    }

    // 👇 Mock class nested inside the test script
    private class MockFileDataHandler : FileDataHandler
    {
        private UserData mockData;
        public bool WasSaveCalled { get; private set; }

        public MockFileDataHandler() : base("mock_path", null) { }

        public void SetMockData(UserData data)
        {
            mockData = data;
            WasSaveCalled = false;
        }

        public override UserData Load() {
            Debug.Log("Mock Load called");
            return mockData;
        }

        public override void Save(UserData data)
        {
            WasSaveCalled = true;
            mockData = data;
        }

        public override void ChangeFilename(string newFilename)
        {
            // No-op
        }
    }
}
