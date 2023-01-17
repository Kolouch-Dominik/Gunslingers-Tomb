using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [field: SerializeField] public GameObject LayoutRoom { get; set; }
    [field: SerializeField] public bool IncludeShop { get; set; }
    [field: SerializeField] public int MinDistanceToShop { get; set; }
    [field: SerializeField] public int MaxDistanceToShop { get; set; }
    [field: SerializeField] public Color StartColor { get; set; }
    [field: SerializeField] public Color EndColor { get; set; }
    [field: SerializeField] public Color ShopColor { get; set; }
    [field: SerializeField] public Color GunRoomColor { get; set; }
    [field: SerializeField] public int DistanceToEnd { get; set; }
    [field: SerializeField] public Transform GeneratorPoint { get; set; }
    [field: SerializeField] public Direction SelectedDirection { get; set; }

    [field: SerializeField] public bool IncludeGunRoom { get; set; }
    [field: SerializeField] public int MinDistanceToGun { get; set; }
    [field: SerializeField] public int MaxDistanceToGun { get; set; }


    public enum Direction { Up, Down, Left, Right }

    private float xRoomOffset = 18f, yRoomOffset = 10f;

    private GameObject EndRoom { get; set; }
    private GameObject ShopRoom { get; set; }
    private GameObject GunRoom { get; set; }

    [field: SerializeField] public LayerMask RoomLayerNumber { get; set; }

    private List<GameObject> layoutRoomObjects = new List<GameObject>();


    [field: SerializeField] public RoomCenter CenterStart { get; set; }
    [field: SerializeField] public RoomCenter CenterEnd { get; set; }
    [field: SerializeField] public RoomCenter CenterShop { get; set; }
    [field: SerializeField] public RoomCenter CenterGunRoom { get; set; }
    [field: SerializeField] public List<RoomCenter> PotentialCenters { get; set; }
    [field: SerializeField] public RoomPrefabs Rooms { get; set; }
    [field: SerializeField] public List<GameObject> GeneratedOutlines { get; set; } = new List<GameObject>();


    void Start()
    {
        Instantiate(LayoutRoom, GeneratorPoint.position, GeneratorPoint.rotation).GetComponent<SpriteRenderer>().color = StartColor;

        SelectedDirection = (Direction)Random.Range(0, 4);

        MoveGenerationPoint();

        for (var i = 0; i < DistanceToEnd; i++)
        {
            var newRoom = Instantiate(LayoutRoom, GeneratorPoint.position, GeneratorPoint.rotation);

            layoutRoomObjects.Add(newRoom);

            if (i + 1 == DistanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = EndColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
                EndRoom = newRoom;
            }

            SelectedDirection = (Direction)Random.Range(0, 4);

            while (Physics2D.OverlapCircle(GeneratorPoint.position, 0.2f, RoomLayerNumber))
            {
                MoveGenerationPoint();
            }
        }

        if(IncludeShop)
        {

            int shopSelector = Random.Range(MinDistanceToShop, MaxDistanceToShop + 1);
            ShopRoom = layoutRoomObjects[shopSelector];
            layoutRoomObjects.RemoveAt(shopSelector);

            ShopRoom.GetComponent<SpriteRenderer>().color = ShopColor;
        }
        if (IncludeGunRoom)
        {

            int grSelector = Random.Range(MinDistanceToGun, MaxDistanceToGun/* +1 - postupnì se snižuje poèet možných místnosti a zaène tam padat vyjímka*/);
            GunRoom = layoutRoomObjects[grSelector];
            layoutRoomObjects.RemoveAt(grSelector);

            GunRoom.GetComponent<SpriteRenderer>().color = GunRoomColor;
        }

        CreateRoomOutline(Vector3.zero);
        foreach (var room in layoutRoomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(EndRoom.transform.position);

        if (IncludeShop) CreateRoomOutline(ShopRoom.transform.position);
        if (IncludeGunRoom) CreateRoomOutline(GunRoom.transform.position);

        foreach (var outline in GeneratedOutlines)
        {
            bool generateCenter = true;


            if (outline.transform.position == Vector3.zero)
            {
                Instantiate(CenterStart, outline.transform.position, transform.rotation).Room =
                    outline.GetComponent<Room>();
                generateCenter = false;
            }

            if (outline.transform.position == EndRoom.transform.position)
            {
                Instantiate(CenterEnd, outline.transform.position, transform.rotation).Room =
                    outline.GetComponent<Room>();
                generateCenter = false;
            }

            if(IncludeShop)
            {
                if (outline.transform.position == ShopRoom.transform.position)
                {
                    Instantiate(CenterShop, outline.transform.position, transform.rotation).Room =
                        outline.GetComponent<Room>();
                    generateCenter = false;
                }
            }

            if(IncludeGunRoom)
            {
                if (outline.transform.position == GunRoom.transform.position)
                {
                    Instantiate(CenterGunRoom, outline.transform.position, transform.rotation).Room =
                        outline.GetComponent<Room>();
                    generateCenter = false;
                }
            }

            if (generateCenter)
            {

                var centerSelect = Random.Range(0, PotentialCenters.Count);

                Instantiate(PotentialCenters[centerSelect], outline.transform.position, transform.rotation).Room =
                    outline.GetComponent<Room>();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

#endif
    }

    public void MoveGenerationPoint()
    {
        GeneratorPoint.position += SelectedDirection switch
        {
            Direction.Up => new Vector3(0f, yRoomOffset, 0f),
            Direction.Down => new Vector3(0f, -yRoomOffset, 0f),
            Direction.Left => new Vector3(-xRoomOffset, 0f, 0f),
            Direction.Right => new Vector3(xRoomOffset, 0f, 0f),
            _ => GeneratorPoint.position
        };
    }


    public void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yRoomOffset, 0f), .2f, RoomLayerNumber);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yRoomOffset, 0f), .2f, RoomLayerNumber);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xRoomOffset, 0f, 0f), .2f, RoomLayerNumber);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xRoomOffset, 0f, 0f), .2f, RoomLayerNumber);

        var directionCount = 0;
        if (roomAbove)
        {
            directionCount++;
        }

        if (roomBelow)
        {
            directionCount++;
        }

        if (roomLeft)
        {
            directionCount++;
        }

        if (roomRight)
        {
            directionCount++;
        }

        switch (directionCount)
        {
            case 0:
                break;
            case 1:
                if (roomAbove)
                    GeneratedOutlines.Add(Instantiate(Rooms.singleU, roomPosition, transform.rotation));
                else if (roomBelow)
                    GeneratedOutlines.Add(Instantiate(Rooms.singleD, roomPosition, transform.rotation));
                else if (roomLeft)
                    GeneratedOutlines.Add(Instantiate(Rooms.singleL, roomPosition, transform.rotation));
                else
                    GeneratedOutlines.Add(Instantiate(Rooms.singleR, roomPosition, transform.rotation));
                break;

            case 2:
                if (roomAbove && roomBelow)
                    GeneratedOutlines.Add(Instantiate(Rooms.doubleUD, roomPosition, transform.rotation));
                else if (roomLeft && roomRight)
                    GeneratedOutlines.Add(Instantiate(Rooms.doubleLR, roomPosition, transform.rotation));
                else if (roomBelow && roomLeft)
                    GeneratedOutlines.Add(Instantiate(Rooms.doubleLD, roomPosition, transform.rotation));
                else if (roomLeft && roomAbove)
                    GeneratedOutlines.Add(Instantiate(Rooms.doubleLU, roomPosition, transform.rotation));
                else if (roomBelow && roomRight)
                    GeneratedOutlines.Add(Instantiate(Rooms.doubleRD, roomPosition, transform.rotation));
                else
                    GeneratedOutlines.Add(Instantiate(Rooms.doubleUR, roomPosition, transform.rotation));
                break;

            case 3:
                if (roomLeft && roomBelow && roomRight)
                    GeneratedOutlines.Add(Instantiate(Rooms.tripleLRD, roomPosition, transform.rotation));
                else if (roomLeft && roomBelow && roomAbove)
                    GeneratedOutlines.Add(Instantiate(Rooms.tripleLUD, roomPosition, transform.rotation));
                else if (roomLeft && roomAbove && roomRight)
                    GeneratedOutlines.Add(Instantiate(Rooms.tripleLUR, roomPosition, transform.rotation));
                else
                    GeneratedOutlines.Add(Instantiate(Rooms.tripleURD, roomPosition, transform.rotation));
                break;

            case 4:
                if (roomLeft && roomBelow && roomRight && roomAbove)
                    GeneratedOutlines.Add(Instantiate(Rooms.fourway, roomPosition, transform.rotation));
                break;
        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleU,
        singleD,
        singleR,
        singleL,
        doubleLD,
        doubleLR,
        tripleLRD,
        doubleLU,
        tripleLUD,
        tripleLUR,
        fourway,
        doubleRD,
        doubleUD,
        doubleUR,
        tripleURD;
}