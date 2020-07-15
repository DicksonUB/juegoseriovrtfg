using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class LevelBuilder : MonoBehaviour
{
    public Room  endRoomPrefab;
    public Room startRoomPrefab;
    public FirstPersonController fpsController;
    public List<Room> oddRoomCells = new List<Room>();
    public List<Room> evenRoomCells = new List<Room>();
    public bool [,] grid = new bool[100, 100];
    //public List<Room> roomPrefabs = new List<Room>();



    List<Doorway> availableDoorways = new List<Doorway>();
    
    StartRoom startRoom;
    EndRoom endRoom;
    List<Room> placedRooms = new List<Room>();

    LayerMask roomLayerMask;
    int i;
    private void Start()
    {
        ResetLevelGenerator();
        roomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("GenerateLevel");
        

    }
    private void addToGrid(int row,int column)
    {
        if(row >= (grid.GetLength(0)-1) || column >= (grid.GetLength(0)-1))
        {
            int newSize = (int) Math.Pow(grid.GetLength(0), 2);
            grid = ResizeArray<bool>(grid,newSize, newSize);
        }
    }
    private T[,] ResizeArray<T>(T[,] original, int rows, int cols)
    {
        var newArray = new T[rows, cols];
        int minRows = Math.Min(rows, original.GetLength(0));
        int minCols = Math.Min(cols, original.GetLength(1));
        for (int i = 0; i < minRows; i++)
            for (int j = 0; j < minCols; j++)
                newArray[i, j] = original[i, j];
        return newArray;
    }

    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        //Place start room
        PlaceStartRoom();
        yield return interval;

        // Random iterations
        int iterations = UnityEngine.Random.Range((int) 4, (int) 10);
        // i = 1 because we want to start with odd rooms
        for(i = 1; i<=iterations; i++)
        {
            // Place random room from list
            PlaceRoom();
            yield return interval;
        }

        // Place end room
        PlaceEndRooms();
        yield return interval;
        Instantiate(fpsController,new Vector3(-64.28f,2.2f,38.8f),Quaternion.identity * Quaternion.Euler(0, 90f, 0));
        
        // Level generation finished
        yield return new WaitForSeconds(3);
    }
    void PlaceStartRoom()
    {
        // Instantiate room
        startRoom = Instantiate(startRoomPrefab) as StartRoom;
        // ?????????????????????????????????????????
        startRoom.transform.parent = this.transform;
        
        // Get doorways from current room and add them randomly to tthe list of available doorways
        AddDoorwayToList(startRoom, ref availableDoorways);
        placedRooms.Add(startRoom);
        // Position room
        //startRoom.transform.rotation = Quaternion.identity;
        //startRoom.transform.position = Vector3.zero;
    }
    void AddDoorwayToList(Room room, ref List<Doorway> list)
    {
        foreach(Doorway doorway in room.doorways)
        {
            //int r = Random.Range(0, list.Count);
            list.Add(doorway);
        }
    }
    void PlaceRoom()
    {
        Room currentRoom;
        if (i%2 == 0)
        {
            currentRoom = Instantiate(evenRoomCells[UnityEngine.Random.Range(0, evenRoomCells.Count)]) as Room;
        }
        else
        {
            currentRoom = Instantiate(oddRoomCells[0]) as Room;
        }
        
        currentRoom.transform.parent = this.transform;
        currentRoom.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        
        List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
        List<Doorway> currentDoorways = new List<Doorway>();
        
        AddDoorwayToList(currentRoom,ref currentDoorways);
        AddDoorwayToList(currentRoom, ref availableDoorways);
        
        bool roomPlaced = false;
        foreach (Doorway availableDoorway in allAvailableDoorways)
        {
            foreach(Doorway currentDoorway in currentDoorways)
            {
                
                PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);
                
                if (CheckRoomOverlap(currentRoom))
                {
                    continue;
                }
                roomPlaced = true;
                placedRooms.Add(currentRoom);

                //currentDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(currentDoorway);

                availableDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(availableDoorway);
                break;
            }
            if (roomPlaced)
            {
                break;
            }
        }
        /*
        if (!roomPlaced)
        {
            Destroy(currentRoom.gameObject);
            ResetLevelGenerator();
            StartCoroutine("GenerateLevel");
        }
        */
        
    }
    void PositionRoomAtDoorway(ref Room room, Doorway roomDoorway, Doorway targetDoorway)
    {

        room.transform.position = Vector3.zero;
        room.transform.rotation = Quaternion.identity;

        Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
        Vector3 roomDoorwayEuler = roomDoorway.transform.eulerAngles;
        float deltaAngle = Mathf.DeltaAngle(roomDoorwayEuler.y, targetDoorwayEuler.y);
        Quaternion currentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle,Vector3.up);
        room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler(0, 180f, 0);

        
        Vector3 roomPositionoffset = roomDoorway.transform.position - room.transform.position;
        //Debug.LogError("Positioning at doorway at: " + targetDoorway.transform.position + " (" +targetDoorway.transform.parent.position+ ")"+ ". Offset: " +roomPositionoffset + " "+ room.gameObject.name);
        room.transform.position = targetDoorway.transform.position - roomPositionoffset;
        //Debug.LogError("Result: " + "" + room.transform.position, room.gameObject);


    }
    void printDoorways()
    {
        int j = 0;
        foreach(Doorway doorway in availableDoorways)
        {
            j++;
            Debug.Log(j, doorway);
        }
        
    }
    /*
    bool CheckRoomOverlap(Room room)
    {

        Debug.Log(room.name);
        Bounds[] allBoundsCandidate = room.getRoomBounds();
        List<Collider> allColliders = new List<Collider>();

        foreach (Bounds bounds in allBoundsCandidate)
        {
            bounds.Expand(-0.1f);
            Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, room.transform.rotation, roomLayerMask);
            foreach (Collider c in colliders)
            {
                allColliders.Add(c);
            }

        } 
        
        if(allColliders.Count > 0)
        {
            foreach(Collider c in allColliders)
            {
                if (c.transform.parent.gameObject.Equals(room.gameObject) || c.transform.gameObject.Equals(room.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("Overlap detected with"+ c.transform.gameObject.name+ " child of " + c.transform.parent.gameObject.name, room.gameObject);
                    return true;
                }
            }
        }
        return false;
        
    }
    */
    
    bool CheckRoomOverlap(Room room)
    {
        Bounds[] allBoundsCandidate = room.getRoomBounds();
        List<Collider> allColliders = new List<Collider>();

        
        foreach (Room roomPlaced in placedRooms)
        {
            Bounds[] roomPlacedBounds = roomPlaced.getRoomBounds();
            foreach (Bounds bounds in roomPlacedBounds)
            {
                bounds.Expand(-0.1f);
                
                Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, roomPlaced.transform.rotation, roomLayerMask);
                foreach (Collider c in colliders)
                {
                    allColliders.Add(c);
                }
            }
        }

        if (allColliders.Count > 0)
        {
            foreach (Collider c in allColliders)
            {
                Debug.Log("Testing: " + c.transform.parent.gameObject.name + " " + c.transform.gameObject.name);
                if (c.transform.parent.gameObject.Equals(room.gameObject) || c.transform.gameObject.Equals(room.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("Overlap detected with" + c.transform.gameObject.name + " child of " + c.transform.parent.gameObject.name, room.gameObject);
                    return true;
                }
            }
        }
        return false;
    }

    void PlaceEndRooms()
    {
        endRoom = Instantiate(endRoomPrefab) as EndRoom;
        endRoom.transform.parent = this.transform;
        endRoom.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
        Doorway doorway = endRoom.doorways[0];

        bool roomPlaced = false;

        foreach(Doorway availableDoorway in allAvailableDoorways)
        {
            Room room = (Room)endRoom;
            PositionRoomAtDoorway(ref room, doorway, availableDoorway);

            if (CheckRoomOverlap(endRoom))
            {
                continue;
            }
            roomPlaced = true;
            placedRooms.Add(endRoom);
            doorway.gameObject.SetActive(false);
            availableDoorways.Remove(doorway);

            availableDoorway.gameObject.SetActive(false);
            availableDoorways.Remove(availableDoorway);

            break;
        }
        /*
        if (!roomPlaced)
        {
            Debug.Log("End room not placed");
            //ResetLevelGenerator();
            StartCoroutine("GenerateLevel");
        }*/
    }
     
    void ResetLevelGenerator()
    {
        StopCoroutine("GenerateLevel");

        foreach(Room room in placedRooms)
        {
            Destroy(room.gameObject);
        }

        // Clear lists
        placedRooms.Clear();
        availableDoorways.Clear();

        
    }

}
