using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public Room  endRoomPrefab;
    public List<Room> oddRoomCells = new List<Room>();
    public List<Room> evenRoomCells = new List<Room>();
    //public List<Room> roomPrefabs = new List<Room>();

    

    List<Doorway> availableDoorways = new List<Doorway>();
    
    public StartRoom startRoom;
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

    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        //Place start room
        PlaceStartRoom();
        yield return interval;

        // Random iterations
        int iterations = Random.Range((int) 10, (int) 20);
        Debug.Log(iterations);
        // i = 1 because we want to start with odd rooms
        for(i = 1; i<=iterations; i++)
        {
            // Place random room from list
            PlaceRoom();
            yield return interval;
        }

        // Place end room
        //PlaceEndRoom();
        yield return interval;

        // Level generation finished
        yield return new WaitForSeconds(3);

    }
    void PlaceStartRoom()
    {
        // Instantiate room
        //startRoom = Instantiate(startRoomPrefab) as StartRoom;
        // ?????????????????????????????????????????
        startRoom.transform.parent = this.transform;
        
        // Get doorways from current room and add them randomly to tthe list of available doorways
        AddDoorwayToList(startRoom, ref availableDoorways);

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
            currentRoom = Instantiate(evenRoomCells[Random.Range(0, evenRoomCells.Count)]) as Room;
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
        Debug.LogError("ye" + availableDoorways.Count + " " + currentDoorways.Count, currentRoom);
        foreach (Doorway availableDoorway in allAvailableDoorways)
        {
            foreach(Doorway currentDoorway in currentDoorways)
            {
                
                PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);
                /*
                if (CheckRoomOverlap(currentRoom))
                {
                    continue;
                }*/
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
        Debug.LogError("Positioning at doorway at: " + targetDoorway.transform.position + " (" +targetDoorway.transform.parent.position+ ")"+ ". Offset: " +roomPositionoffset + " "+ room.gameObject.name);
        room.transform.position = targetDoorway.transform.position - roomPositionoffset;
        Debug.LogError("Result: " + "" + room.transform.position, room.gameObject);


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

    bool CheckRoomOverlap(Room room)
    {
        Bounds bounds = room.getRoomBounds();
        bounds.Expand(-0.1f);
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, room.transform.rotation, roomLayerMask);

        if(colliders.Length > 0)
        {
            foreach(Collider c in colliders)
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

    void PlaceEndRoom()
    {

    }
     
    void ResetLevelGenerator()
    {
        StopCoroutine("GenerateLevel");

        
        if (endRoom)
        {
            Destroy(endRoom.gameObject);
        }
        foreach(Room room in placedRooms)
        {
            Destroy(room.gameObject);
        }

        // Clear lists
        placedRooms.Clear();
        availableDoorways.Clear();

        
    }

}
