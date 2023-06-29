using UnityEngine;
 
 public class GlideController : MonoBehaviour {
     public float speed;
 
     private Vector3 destination;
     
     void Update () {
         // If the object is not at the target destination
         if (destination != gameObject.transform.position) {
            IncrementPosition ();
         }
         else
         {
            if(gameObject.tag == "SetLater")
            {
                //gameObject.transform.parent = gameObject.transform.parent.transform.parent.transform.parent;
                gameObject.tag = "MiniFire";
                if(destination.x <= -18)
                {
                    gameObject.tag = "Untagged";
                }
                else
                {
                    ParentUtils.MoveToBlankObject("fires", gameObject);
                }
            }
            
         }
     }
 
     void IncrementPosition ()
     {
         // Calculate the next position
         float delta = speed * Time.deltaTime;
         Vector3 currentPosition = gameObject.transform.position;
         Vector3 nextPosition = Vector3.MoveTowards (currentPosition, destination, delta);
 
         // Move the object to the next position
         gameObject.transform.position = nextPosition;
     }
 
     // Set the destination to cause the object to smoothly glide to the specified location
     public void SetDestination (Vector3 value) {
         destination = value;
         if(gameObject.name == "MiniFire"){
            gameObject.transform.parent = gameObject.transform.parent.transform.parent.transform.parent;
         }
     }
 }