using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UIElements;
using NavMeshPlus.Components;

public class RHLevelGenerator : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] public int RHySize = 20;
    [SerializeField] public int RHxSize = 30;
    [SerializeField] public Tilemap tilemapLayer0;
    [SerializeField] public Tilemap tilemapLayer1;
    [SerializeField] public TileBase TestTile;
    [SerializeField] private NavMeshSurface navMeshSurface;
    private int RoomIndex = 0;

    public enum RHroom
    {
        Reception,
        JobInterviewRoom,
        Kitchen,
        Bathroom,
    }
    public RHroom rhRoom;
    
    
    [Header("Room 1")]
    [SerializeField] private int RH_Room1_Xcoordinate;
    [SerializeField] private int RH_Room1_Ycoordinate;
    [SerializeField] private int Room1Xsize = 10;
    [SerializeField] private int Room1Ysize = 10;
    public enum RHroom1Exits
    {
        Up,
        Down,
        Left,
        Right,
        UpDownLeftRight,
        UpDownLeft,
        UpDown,
        DownLeftRight,
        DownLeft,
        DownRight,
        LeftRight,
    }
    public RHroom1Exits rhRoom1Exits;

    [Header("Room 2")]
    [SerializeField] private int RH_Room2_Xcoordinate;
    [SerializeField] private int RH_Room2_Ycoordinate;
    [SerializeField] private int Room2Xsize = 10;
    [SerializeField] private int Room2Ysize = 10;
    public enum RHroom2Exits
    {
        Up,
        Down,
        Left,
        Right,
        UpDownLeftRight,
        UpDownLeft,
        UpDown,
        DownLeftRight,
        DownLeft,
        DownRight,
        LeftRight,
    }
    public RHroom2Exits rhRoom2Exits;

    [Header("Room 3")]
    [SerializeField] private int RH_Room3_Xcoordinate;
    [SerializeField] private int RH_Room3_Ycoordinate;
    [SerializeField] private int Room3Xsize = 10;
    [SerializeField] private int Room3Ysize = 10;
    public enum RHroom3Exits
    {
        Up,
        Down,
        Left,
        Right,
        UpDownLeftRight,
        UpDownLeft,
        UpDown,
        DownLeftRight,
        DownLeft,
        DownRight,
        LeftRight,
    }
    public RHroom3Exits rhRoom3Exits;
    
    [Header("Room 4")]
    [SerializeField] private int RH_Room4_Xcoordinate;
    [SerializeField] private int RH_Room4_Ycoordinate;
    [SerializeField] private int Room4Xsize = 10;
    [SerializeField] private int Room4Ysize = 10;
    public enum RHroom4Exits
    {
        Up,
        Down,
        Left,
        Right,
        UpDownLeftRight,
        UpDownLeft,
        UpDown,
        DownLeftRight,
        DownLeft,
        DownRight,
        LeftRight,
    }
    public RHroom4Exits rhRoom4Exits;

    [Header("Type Rooms")]
    [SerializeField] private int Receptions;
    [SerializeField] private int ReceptionsLimit;
    [SerializeField] private int JobInterviewRooms;
    [SerializeField] private int JobInterviewRoomsLimit;
    [SerializeField] private int KitchenRooms;
    [SerializeField] private int KitchenRoomsLimit;
    [SerializeField] private int Bathrooms;
    [SerializeField] private int BathroomsLimit;

    [Header("Grounds Tiles")]
    [SerializeField] private Tile RH_WallwayGroundTile;
    [SerializeField] private Tile ReceptionGroundTile;
    [SerializeField] private Tile JobInterviewRoomGroundTile;
    [SerializeField] private Tile KitchenRoomGroundTile;
    [SerializeField] private Tile BathroomGroundTile;
    [SerializeField] private Tile WhiteGround;
    [SerializeField] private Tile BrownGround;
    [SerializeField] private Tile BegeGround;

    [Header("Walls Tiles")]
    [SerializeField] private Tile RHwallTile;
    [SerializeField] private Tile ReceptionWallTile;
    [SerializeField] private Tile JobInterviewRoomWallTile;
    [SerializeField] private Tile KitchenRoomWallTile;
    [SerializeField] private Tile BathroomWallTile;
    [SerializeField] private Tile WhiteWall;
    [SerializeField] private Tile BrownWall;
    [SerializeField] private Tile BegeWall;

    [Header("Desks Tiles")]
    [SerializeField] private Tile BasicDesk;
    [SerializeField] private Tile NormalDesk;
    [SerializeField] private Tile TIDesk;
    [SerializeField] private Tile LuxuryDesk;

    [Header("Windows")]
    [SerializeField] private Tile BasicWindow;
    [SerializeField] private Tile NormalWindow;
    [SerializeField] private Tile LuxuryWindow;

    [Header("Doors")]
    [SerializeField] private Tile ReceptionDoorTile;
    [SerializeField] private Tile JobInterviewDoorTile;
    [SerializeField] private Tile KitchenDoorTile;
    [SerializeField] private Tile BathroomDoorTile;

    [Header("Decoration")]
    [SerializeField] private Tile BasicPlantVaze;
    [SerializeField] private Tile NormalPlantVaze;
    [SerializeField] private Tile LuxuryPlantVaze;
    [SerializeField] private Tile PlantWall;

    

    //Main GenerateLevelFunctions
    private void GeneratePiso(int Xsize, int Ysize, int Xcoordinate, int Ycoordinate, Tile GroundTile)
    {
        for (int x = 0; x < Xsize; x++)
        {
            for (int y = 0; y < Ysize; y++)
            {
                Vector3Int cellPosition = new Vector3Int(Xcoordinate + x, Ycoordinate + y, 0);

                tilemapLayer0.SetTile(cellPosition, GroundTile);
            }
        }
    }
    private void GenerateWalls(int Xsize, int Ysize, int Xcoordinate, int Ycoordinate, Tile WallTile)
    {
        //DownWall
        for (int x = 0; x <= Xsize; x++)
        {
            Vector3Int cellPosition = new Vector3Int(Xcoordinate + x, Ycoordinate, 0);
            tilemapLayer1.SetTile(cellPosition, WallTile);
        }
        //UpWall
        for (int x = 0; x <= Xsize; x++)
        {
            Vector3Int cellPosition = new Vector3Int(Xcoordinate + x, Ycoordinate + Ysize, 0);
            tilemapLayer1.SetTile(cellPosition, WallTile);
        }
        //LeftWall
        for (int y = 0; y <= Ysize; y++)
        {
            Vector3Int cellPosition = new Vector3Int(Xcoordinate, Ycoordinate + y, 0);
            tilemapLayer1.SetTile(cellPosition, WallTile);
        }
        //RightWall
        for (int y = 0; y <= Ysize; y++)
        {
            Vector3Int cellPosition = new Vector3Int(Xcoordinate + Xsize, Ycoordinate + y, 0);
            tilemapLayer1.SetTile(cellPosition, WallTile);
        }
    }
    private void MakeDoors(int Xcoordinate, int Ycoordinate, int Xsize, int Ysize, Tile DoorTile)
    {
        //Colocar as portas
        switch (RoomIndex)
        {
            case 1:
                switch (rhRoom1Exits)
                {
                    case RHroom1Exits.Up:

                        break;
                    case RHroom1Exits.Down:

                        break;
                    case RHroom1Exits.Left:

                        break;
                    case RHroom1Exits.Right:

                        break;
                    case RHroom1Exits.UpDownLeftRight:

                        break;
                    case RHroom1Exits.UpDownLeft:

                        break;
                    case RHroom1Exits.UpDown:

                        break;
                    case RHroom1Exits.DownLeftRight:

                        break;
                    case RHroom1Exits.DownLeft:

                        break;
                    case RHroom1Exits.DownRight:

                        break;
                    case RHroom1Exits.LeftRight:

                        break;
                }
                break;
            case 2:
                switch (rhRoom2Exits)
                {
                    case RHroom2Exits.Up:

                        break;
                    case RHroom2Exits.Down:

                        break;
                    case RHroom2Exits.Left:

                        break;
                    case RHroom2Exits.Right:

                        break;
                    case RHroom2Exits.UpDownLeftRight:

                        break;
                    case RHroom2Exits.UpDownLeft:

                        break;
                    case RHroom2Exits.UpDown:

                        break;
                    case RHroom2Exits.DownLeftRight:

                        break;
                    case RHroom2Exits.DownLeft:

                        break;
                    case RHroom2Exits.DownRight:

                        break;
                    case RHroom2Exits.LeftRight:

                        break;
                }
                break;
            case 3:
                switch (rhRoom3Exits)
                {
                    case RHroom3Exits.Up:

                        break;
                    case RHroom3Exits.Down:

                        break;
                    case RHroom3Exits.Left:

                        break;
                    case RHroom3Exits.Right:

                        break;
                    case RHroom3Exits.UpDownLeftRight:

                        break;
                    case RHroom3Exits.UpDownLeft:

                        break;
                    case RHroom3Exits.UpDown:

                        break;
                    case RHroom3Exits.DownLeftRight:

                        break;
                    case RHroom3Exits.DownLeft:

                        break;
                    case RHroom3Exits.DownRight:

                        break;
                    case RHroom3Exits.LeftRight:

                        break;
                }
                break;
            case 4:
                switch (rhRoom4Exits)
                {
                    case RHroom4Exits.Up:

                        break;
                    case RHroom4Exits.Down:

                        break;
                    case RHroom4Exits.Left:

                        break;
                    case RHroom4Exits.Right:

                        break;
                    case RHroom4Exits.UpDownLeftRight:

                        break;
                    case RHroom4Exits.UpDownLeft:

                        break;
                    case RHroom4Exits.UpDown:

                        break;
                    case RHroom4Exits.DownLeftRight:

                        break;
                    case RHroom4Exits.DownLeft:

                        break;
                    case RHroom4Exits.DownRight:

                        break;
                    case RHroom4Exits.LeftRight:

                        break;
                }
                break;
        }
    }

    public void Makefloor()
    {
        GeneratePiso(RHxSize, RHySize, 0, 0, RH_WallwayGroundTile);
        GenerateWalls(RHxSize, RHySize, 0, 0, RHwallTile);
        MakeRHroom(RH_Room1_Xcoordinate, RH_Room1_Ycoordinate, Room1Xsize, Room1Ysize);
        MakeRHroom(RH_Room2_Xcoordinate, RH_Room2_Ycoordinate, Room2Xsize, Room2Ysize);
        MakeRHroom(RH_Room3_Xcoordinate, RH_Room3_Ycoordinate, Room3Xsize, Room3Ysize);
        MakeRHroom(RH_Room4_Xcoordinate, RH_Room4_Ycoordinate, Room4Xsize, Room4Ysize);
    }
    private void MakeRHroom(int Xcoordinate, int Ycoordinate, int Xsize, int Ysize)
    {
        RoomIndex += 1;

        DecideRoom();

        switch (rhRoom)
        {
            case RHroom.Reception:
                MakeRHreception(Xcoordinate, Ycoordinate, Xsize, Ysize);
                break;
            case RHroom.JobInterviewRoom:
                MakeRHjobInterviewRoom(Xcoordinate, Ycoordinate, Xsize, Ysize);
                break;
            case RHroom.Kitchen:
                MakeRHkitchen(Xcoordinate, Ycoordinate, Xsize, Ysize);
                break;
            case RHroom.Bathroom:
                MakeBathroom(Xcoordinate, Ycoordinate, Xsize, Ysize);
                break;
        }
    }

    private void DecideRoom()
    {
        int n = Random.Range(1, 5);
        switch (n)
        {
            case 1: if (Receptions < ReceptionsLimit)
                {
                    Receptions += 1;
                    rhRoom = RHroom.Reception;
                }
                else
                {
                    DecideRoom();
                }
            break;
            case 2:
                if (JobInterviewRooms < JobInterviewRoomsLimit)
                {
                    JobInterviewRooms += 1;
                    rhRoom = RHroom.JobInterviewRoom;
                }
                else
                {
                    DecideRoom();
                }
                break;
            case 3:
                if (KitchenRooms < KitchenRoomsLimit)
                {
                    KitchenRooms += 1;
                    rhRoom = RHroom.Kitchen;

                }
                else
                {
                    DecideRoom();
                }
                break;
            case 4:
                if (Bathrooms < BathroomsLimit)
                {
                    Bathrooms += 1;
                    rhRoom = RHroom.Bathroom;

                }
                else
                {
                    DecideRoom();
                }
                break;
        }
    }

    //MakeRHroomsFunctions
    private void MakeRHreception(int Xcoordinate, int Ycoordinate, int Xsize, int Ysize)
    {
        GeneratePiso(Xsize, Ysize, Xcoordinate, Ycoordinate, ReceptionGroundTile);

        GenerateWalls(Xsize, Ysize, Xcoordinate, Ycoordinate, ReceptionWallTile);

        MakeDoors(Xsize, Ysize, Xcoordinate, Ycoordinate, ReceptionDoorTile);

    }

    private void MakeRHkitchen(int Xcoordinate, int Ycoordinate, int Xsize, int Ysize)
    {
        GeneratePiso(Xsize, Ysize, Xcoordinate, Ycoordinate, KitchenRoomGroundTile);

        GenerateWalls(Xsize, Ysize, Xcoordinate, Ycoordinate, KitchenRoomWallTile);

        MakeDoors(Xsize, Ysize, Xcoordinate, Ycoordinate, KitchenDoorTile);

    }
    private void MakeRHjobInterviewRoom(int Xcoordinate, int Ycoordinate, int Xsize, int Ysize)
    {
        GeneratePiso(Xsize, Ysize, Xcoordinate, Ycoordinate, JobInterviewRoomGroundTile);

        GenerateWalls(Xsize, Ysize, Xcoordinate, Ycoordinate, JobInterviewRoomWallTile);

        MakeDoors(Xsize, Ysize, Xcoordinate, Ycoordinate, JobInterviewDoorTile);
    }
    private void MakeBathroom(int Xcoordinate, int Ycoordinate, int Xsize, int Ysize)
    {
        GeneratePiso(Xsize, Ysize, Xcoordinate, Ycoordinate, BathroomGroundTile);

        GenerateWalls(Xsize, Ysize, Xcoordinate, Ycoordinate, BathroomWallTile);

        MakeDoors(Xsize, Ysize, Xcoordinate, Ycoordinate, BathroomDoorTile);
    }


    void Start()
    {
        Makefloor();
        navMeshSurface.BuildNavMesh();
    }

    void Update()
    {
        
    }
}
