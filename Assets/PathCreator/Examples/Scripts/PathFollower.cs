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
        public float lerpFraction;
        public float slerpFraction;

        private bool follow = true;
        float distanceTravelled;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                if (follow)
                {
                    transform.position = transform.position + (pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction) - transform.position) * lerpFraction;
                    //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction), slerpFraction);
                }
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        public void toggleFollow(bool shouldFollow)
        {
            follow = shouldFollow;

            if (shouldFollow)
            {
                distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
            }
        }
    }
}