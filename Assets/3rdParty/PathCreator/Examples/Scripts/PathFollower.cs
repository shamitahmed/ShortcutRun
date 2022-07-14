using UnityEngine;

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

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                GetBotInitPositions();

            }
           
        }

        void Update()
        {
            if (pathCreator != null && GameManager.instance.gameStart && !GetComponent<PlayerCollisions>().botDeath)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                GetComponent<PlayerMovement>().anim.SetBool("run", true);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
        void GetBotInitPositions()
        {
            if (GetComponent<PlayerCollisions>().botID == 0)
            {
                transform.position = pathCreator.path.GetPointAtDistance(14f);
                transform.position = new Vector3(2.253009f, transform.position.y, transform.position.z);
                distanceTravelled = 14f;
            }
            else if (GetComponent<PlayerCollisions>().botID == 1)
            {
                transform.position = pathCreator.path.GetPointAtDistance(19f);
                transform.position = new Vector3(-1.65f, transform.position.y, transform.position.z);
                distanceTravelled = 19f;

            }
        }
    }
}