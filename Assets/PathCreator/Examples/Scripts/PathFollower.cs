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

        private Vector3 lastPosition;
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
                Vector3 newPosition = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                if (follow)
                {
                    float multiplier = 1.0f;
                    if (lastPosition != null)
                    {
                        if (lastPosition == newPosition)
                        {
                            newPosition = new Vector3(newPosition.x + 0.1f, newPosition.y, transform.position.z);
                            multiplier = 2f;
                        }
                    }

                    transform.position = transform.position + (newPosition - transform.position) * lerpFraction * multiplier;
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                    //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction), slerpFraction);
                }
                lastPosition = newPosition;
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