using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelectMap : MonoBehaviour {

    struct MapDataFormat
    {
        public MissionContext[] MissionList;
        public string MapName;
    }

    public TextAsset JSONMapData;
    public MissionSelectCell[] CellPrefabs;
    Dictionary<string, MissionSelectCell> cellDictionary;
    public MissionSelectCell StartingCell;
    public List<MissionSelectCell> Graph;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitializeCellDictionary()
    {
        if (cellDictionary != null)
            return;

        cellDictionary = new Dictionary<string, MissionSelectCell>();
        foreach(MissionSelectCell prefab in CellPrefabs)
        {
            cellDictionary.Add(prefab.gameObject.name, prefab);
        }
    }

    public void LoadMap()
    {
        InitializeCellDictionary();
        Graph = new List<MissionSelectCell>();
        MapDataFormat data = Newtonsoft.Json.JsonConvert.DeserializeObject<MapDataFormat>(JSONMapData.text);
        gameObject.name = data.MapName;
        foreach(MissionContext context in data.MissionList)
        {
            context.HasBeenCleared = PlayerData.IsMissionComplete(context.MissionID);

            GameObject cellObj = GameObject.Instantiate(cellDictionary[context.Location].gameObject, transform);
            MissionSelectCell newCell = cellObj.GetComponent<MissionSelectCell>();
            newCell.MissionInfo = context;
            cellObj.name = context.MissionID;

            //TODO: Position from data
            //Placeholder: align them on z axis
            cellObj.transform.position = new Vector3(0.0f, 0.0f, 15.0f * Graph.Count);

            if (Graph.Count == 0)
                StartingCell = newCell;
            Graph.Add(newCell);
            
            //TODO: Connections from data

        }
    }
}
