using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson; 
using MongoDB.Driver;
using UnityEngine;

public class DatabaseScript
{
    // Start is called before the first frame update
    public string connectionString = "mongodb+srv://htwberlin:hWFPy6V6vjmFb4zI@cluster0.hvjaogp.mongodb.net/?retryWrites=true&w=majority";
    public string databaseName = "HTWBerlin";
    private string collectionName;
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;

    public DatabaseScript(string collectionName)
    {
        this.collectionName = collectionName;
    }

    public void Start()
    {
        try
        {
            // Connect to the MongoDB server
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
            Debug.Log(collectionName);
            collection = database.GetCollection<BsonDocument>(collectionName);

            Debug.Log("Successfully connected to MongoDB");
        }
        catch (MongoException e)
        {
            Debug.LogError("Error connecting to MongoDB: " + e.Message);
        }
    }

    public bool CheckLogin(string email, string password)
    {   
        Debug.Log(email +" "+ password);
        // Build a filter to search for the entered username
        var filter = Builders<BsonDocument>.Filter.Eq("email", email);
        Debug.Log(filter);
        // Search the collection for the username
        var result = collection.Find(filter).FirstOrDefault();

        // If the username is found, check the password
        if (result != null)
        {
            string savedPassword = result["password"].AsString;

            // Compare the entered password with the password in the database
            if (password == savedPassword)
            {
                Debug.Log("Login successful!");
                return true;
            }
            else
            {
                Debug.Log("Incorrect password.");
            }
            
        }
        else
        {
            Debug.Log("Username not found.");
        }

        return false;
    }

    public bool CheckIfUserExist(string username)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("username", username);
        // Search the collection for the username
        var result = collection.Find(filter).FirstOrDefault();
        if (result == null)
        {
           Debug.Log("user available!!!");
            return false;
        }
        else
        {
            Debug.Log("user exist!!!");
            return true;
        }
        
    }
    public bool CheckIfEmailExist(string email)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("email", email);
        // Search the collection for the username
        var result = collection.Find(filter).FirstOrDefault();
        if (result == null)
        {
            Debug.Log("email available!!!");
            return false;
        }
        else
        {
            Debug.Log("email exist!!!");
            return true;
        }
        
    }

    public string GetUsername(string email)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("email", email);
        var result = collection.Find(filter).FirstOrDefault();
        if (result != null)
        {
            return result["username"].AsString;
        }

        return null;
    }
    
    public void CreateUser(string username, string password, string email)
    {
        var document = new BsonDocument
        {
            { "username", username },
            { "password", password },
            { "email", email }
        };
        try
        {
            collection.InsertOne(document);
            Debug.Log("User created successfully.");
        }
        catch (MongoException e)
        {
            Debug.LogError("Error creating user: " + e.Message);
        }
    }
    
    public List<BsonDocument> GetAllDataInDatabase()
    {
        var filter = Builders<BsonDocument>.Filter.Empty;
        return collection.Find(filter).ToList();
    }

    public List<BsonDocument> GetCreateBy(string query)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("create_by",query);
        return collection.Find(filter).ToList();
    }
    
    public List<BsonDocument> GetJoinBy(string query)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("join_by",query);
        return collection.Find(filter).ToList();
    }

    public List<BsonDocument> GetMyJobs(string username)
    {
        List<BsonDocument> myjobs =  new List<BsonDocument>();
        var results = GetAllDataInDatabase();
        
        Debug.Log("results from DB" + results.Count);
        
        foreach (var result in results)
        {
            string create_by = result["create_by"].AsString;

            if (result["join_by"] != null)
            { 
                BsonArray array = result["join_by"].AsBsonArray;
            

            foreach (var value in array)
            {
                Debug.Log("results from DB" + results.Count);
                
                if (create_by == username || value.AsString == username)
                
                {
                    myjobs.Add(result);
                    
                }
            }
            }
            
        }
        return myjobs;
    }

}