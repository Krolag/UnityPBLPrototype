using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;

public class Exporter : MonoBehaviour
{
    private List<string> origNames;
    private GameModelContainer gameModelContainer;
    
    // Preparing the GameModel class for serialization of Game Objects
    public class GameModel
    {
        [XmlAttribute("name")] public string Name;
        [XmlAttribute("model_id")] public int ModelID;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public compo components;
        [XmlArray("ChildrenModels")]
        [XmlArrayItem("ChildModel")]
        public List<GameModel> ChildGameModel;

        public GameModel()
        {
            
        }
        public GameModel(string name, int modelID, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.Name = name;
            this.ModelID = modelID;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
            this.components = new compo();
            this.ChildGameModel = new List<GameModel>();
        }
    }
    
    public class compo
    {
        [SerializeField] public bool isColliderOn;
        [SerializeField] public bool isColliderStatic;
        [SerializeField] public bool isInteractable;
        [SerializeField] public bool isTreasure;
        [SerializeField] public bool isCash;
        [SerializeField] public bool isEnemy;

        public compo()
        {
            this.isColliderOn = false;
            this.isColliderStatic = true;
            this.isInteractable = false;
            this.isTreasure = false;
            this.isCash = false;
            this.isEnemy = false;
        }
        
        public compo(bool isColliderOn, bool isColliderStatic, bool isInteractable, bool isTreasure, bool isCash, bool isEnemy)
        {
            this.isColliderOn = isColliderOn;
            this.isColliderStatic = isColliderStatic;
            this.isInteractable = isInteractable;
            this.isTreasure = isTreasure;
            this.isCash = isCash;
            this.isEnemy = isEnemy;
        }
    }

    // Preparing container for the Game Objects
    [XmlRoot("GameModelsCollection")]
    public class GameModelContainer
    {
        [XmlArray("GameModels")]
        [XmlArrayItem("GameModel")]
        public List<GameModel> GameModels;

        public GameModelContainer()
        {
            this.GameModels = new List<GameModel>();
        }
        
        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(GameModelContainer));
            using(var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
    
    [ContextMenu("export")]
    // Export function will save sml file with the scene hierarchy
    public void Export()
    {
        // store all names to get count of the objects
        origNames = new List<string>();
        gameModelContainer = new GameModelContainer();
        
        // get root objects in scene
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects( rootObjects );
  
        // iterate root objects and save transform in XML file
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[ i ];
            if (gameObject.layer == 8) 
            {
                string genName = GetNameWithoutNumber(gameObject.name);
                origNames.Add(genName);
                int id = GetIDFromName(genName);
                genName += '-' + GetNumberOfModelsNames(genName).ToString();
        
                GameModel gameModel = new GameModel(
                    genName,
                    id,
                    gameObject.transform.position,
                    gameObject.transform.rotation,
                    gameObject.transform.localScale
                );

                // Set serializable contents here @IGNACY
                gameModel.components.isColliderOn = GameObject.Find(gameObject.name)
                    .GetComponent<SerializableComponents>().isColliderOn;
                
                gameModel.components.isCash = GameObject.Find(gameObject.name)
                    .GetComponent<SerializableComponents>().isCash;
                
                gameModel.components.isColliderStatic = GameObject.Find(gameObject.name)
                    .GetComponent<SerializableComponents>().isColliderStatic;
                
                gameModel.components.isInteractable = GameObject.Find(gameObject.name)
                    .GetComponent<SerializableComponents>().isInteractable;
                
                gameModel.components.isTreasure = GameObject.Find(gameObject.name)
                    .GetComponent<SerializableComponents>().isTreasure;

                gameModel.components.isEnemy = GameObject.Find(gameObject.name)
                    .GetComponent<SerializableComponents>().isEnemy;

                ProcessChildrenOfRootObject(gameObject, gameModel, false);
            }
        }

        gameModelContainer.Save(Path.Combine(Application.dataPath, "scene.xml"));
        
        gameModelContainer.GameModels.Clear();
        origNames.Clear();
    }

    string GetNameWithoutNumber(string unityName)
    {
        string name = string.Empty;
        
        foreach (char c in unityName)
        {
            if (c == ' ') break;
            name += c;
        }
        
        return name;
    }

    int GetIDFromName(string name)
    {
        int i = 0;
        foreach (var origName in origNames)
        {
            if (origName.Equals(name))
                break;
            i++;
        }

        return i;
    }

    int GetNumberOfModelsNames(string name)
    {
        int i = 0;
        
        foreach (var origName in origNames)
        {
            if (origName.Equals(name)) i++;
        }
        
        return i;
    }

    void ProcessChildrenOfRootObject(GameObject rootObject, GameModel gameModel, bool ifProcessingChild)
    {
        if (!ifProcessingChild)
        {
            gameModelContainer.GameModels.Add(gameModel);
        }
        
        int rootChildCount = rootObject.transform.childCount;
        if (rootChildCount == 0) return;

        for (int i = 0; i < rootChildCount; i++)
        {
            GameObject child = rootObject.transform.GetChild(i).gameObject;
            // Check if object is one of the models
            if (child.layer != 8) continue;
            
            // init child
            string genName = GetNameWithoutNumber(child.name);
            origNames.Add(genName);
            int id = GetIDFromName(genName);
            genName += '-' + GetNumberOfModelsNames(genName).ToString();

            GameModel childGameModel = new GameModel(
                genName,
                id,
                child.transform.localPosition,
                child.transform.localRotation,
                child.transform.localScale
            );

            // THE SAME @IGNACY
            childGameModel.components.isCash = GameObject.Find(child.name).GetComponent<SerializableComponents>().isCash;
            childGameModel.components.isTreasure = GameObject.Find(child.name).GetComponent<SerializableComponents>().isTreasure;
            childGameModel.components.isColliderStatic = GameObject.Find(child.name).GetComponent<SerializableComponents>().isColliderStatic;
            childGameModel.components.isInteractable = GameObject.Find(child.name).GetComponent<SerializableComponents>().isInteractable;
            childGameModel.components.isEnemy = GameObject.Find(child.name).GetComponent<SerializableComponents>().isEnemy;

            gameModel.ChildGameModel.Add(childGameModel);

            // Process children
            ProcessChildrenOfRootObject(child, childGameModel, true);
        }
    }
}