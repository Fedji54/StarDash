using Lean.Pool;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _rooms;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private bool _testRoom;
    [SerializeField] private int _roomIndex;
    [SerializeField] private bool _linearSpawn;
    private int _curRoomIndex;

    public void EnableGenerator()
    {
        SpawnFirstRoom();
    }

    private void SpawnFirstRoom()
    {
        GameObject roomGO = _rooms[0];
        LeanPool.Spawn(roomGO, _spawnPoint.position, Quaternion.identity).GetComponent<Room>().Setup(this);
    }

    public void SpawnRoom(Transform upPosition)
    {
        if (_testRoom)
        {
            GameObject roomGO = _rooms[_roomIndex];
            Room room = roomGO.GetComponent<Room>();
            Vector3 spawnPosition = new(0f, upPosition.position.y + (room.Size / 2f), 0f);
            LeanPool.Spawn(roomGO, spawnPosition, Quaternion.identity).GetComponent<Room>().Setup(this);
        }
        else if (_linearSpawn)
        {
            if (_curRoomIndex < _rooms.Length - 1)
            {
                _curRoomIndex++;
            }
            else
            {
                _curRoomIndex = 1;
            }
            GameObject roomGO = _rooms[_curRoomIndex];
            Room room = roomGO.GetComponent<Room>();
            Vector3 spawnPosition = new(0f, upPosition.position.y + (room.Size / 2f), 0f);
            LeanPool.Spawn(roomGO, spawnPosition, Quaternion.identity).GetComponent<Room>().Setup(this);
        }
        else
        {
            GameObject roomGO = _rooms[Random.Range(1, _rooms.Length)];
            Room room = roomGO.GetComponent<Room>();
            Vector3 spawnPosition = new(0f, upPosition.position.y + (room.Size / 2f), 0f);
            LeanPool.Spawn(roomGO, spawnPosition, Quaternion.identity).GetComponent<Room>().Setup(this);
        }
    }
}