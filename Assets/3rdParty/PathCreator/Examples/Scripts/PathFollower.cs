using UnityEngine;
using DG.Tweening;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
        float playTime;
        float playTimeTwo;
        bool switchX;
        bool switchedX;
        int rand;
        int randTransitionTime;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                GetBotInitPositions();

            }
            rand = Random.Range(0, 3);
            randTransitionTime = Random.Range(1, 7);
        }

        void Update()
        {
            if (!GameManager.instance.gameStart) return;
            if (!switchedX && !switchX && pathCreator != null && GameManager.instance.gameStart && !GetComponent<PlayerCollisions>().botDeath)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).y, pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).z);
                GetComponent<PlayerMovementTwo>().anim.SetBool("run", true);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            }

            playTime += Time.deltaTime;
            if (!switchedX && playTime >= randTransitionTime && pathCreator != null && GameManager.instance.gameStart && !GetComponent<PlayerCollisions>().botDeath)
            {
                switchX = true;
                distanceTravelled += speed * Time.deltaTime;
                if (rand == 0)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).x - 3f, pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).y, pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).z), Time.deltaTime * 1.5f);
                if (rand == 1)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).x + 3f, pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).y, pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).z), Time.deltaTime * 1.5f);
                if (rand == 2)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).x, pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).y, pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).z), Time.deltaTime * 1f);
                GetComponent<PlayerMovementTwo>().anim.SetBool("run", true);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
        public void GetBotInitPositions()
        {
            if (GetComponent<PlayerCollisions>().botID == 0)
            {
                transform.position = pathCreator.path.GetPointAtDistance(10f);             
                distanceTravelled = 10f;
                transform.position = new Vector3(2.253009f, transform.position.y, transform.position.z);
            }
            else if (GetComponent<PlayerCollisions>().botID == 1)
            {
                transform.position = pathCreator.path.GetPointAtDistance(16f);         
                distanceTravelled = 16f;
                transform.position = new Vector3(0.21f, transform.position.y, transform.position.z);
            }
            else if (GetComponent<PlayerCollisions>().botID == 2)
            {
                transform.position = pathCreator.path.GetPointAtDistance(20f);
                distanceTravelled = 20f;
                transform.position = new Vector3(2.86f, transform.position.y, transform.position.z);
            }
            else if (GetComponent<PlayerCollisions>().botID == 3)
            {
                transform.position = pathCreator.path.GetPointAtDistance(11f);
                distanceTravelled = 11f;
                transform.position = new Vector3(-1.65f, transform.position.y, transform.position.z);
            }
            else if (GetComponent<PlayerCollisions>().botID == 4)
            {
                transform.position = pathCreator.path.GetPointAtDistance(21f);
                distanceTravelled = 21f;
                transform.position = new Vector3(-1.65f, transform.position.y, transform.position.z);
            }
            else if (GetComponent<PlayerCollisions>().botID == 5)
            {
                transform.position = pathCreator.path.GetPointAtDistance(28f);
                distanceTravelled = 28f;
                transform.position = new Vector3(1.56f, transform.position.y, transform.position.z);
            }
        }
    }
}