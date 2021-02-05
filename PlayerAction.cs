using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProgrammingPractice
{
    public class PlayerAction : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float rayDistance;
        public float pierceDistance;
        GameObject _thisGameObject;
        public Camera mainCam;
        GameObject triggeredObject;
        Vector2 mousePosition;
        Vector2 direction;

        Vector2 move;
        bool onTrigger = false;

        void OnTriggerEnter2D(Collider2D collider)
        {
            triggeredObject = collider.gameObject;
            onTrigger = true;
            
        }
        void OnTriggerExit2D()
        {
            onTrigger = false;
        }

        void Awake()
        {
            _thisGameObject = this.gameObject;

        }
        void Update()
        {
            mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePosition - rb.position;
            //float angle = Mathf.Atan2(direction.y, direction.x);
            if(onTrigger && Input.GetButtonDown("Fire1")){
            InteractCheck();
            }
            if(Input.GetButtonDown("Fire2")){
                RaycastShoot();
            }
        
        }
        void InteractCheck(){
          if(onTrigger && Input.GetButtonDown("Fire1"))
            {
                var interactable = triggeredObject.GetComponent<IInteract>();
                if(interactable == null) return;
                interactable.Interact();
            }  
        }
        void RaycastShoot(){
                RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, rayDistance);
                if(hit)
                {
                    print("rayCast Hit: " + hit.collider.name);

                    //Trying to make a raycast off of the hit object to check if an object is behind
                    //to pierce and deal damage
                    RaycastHit2D pierceHit = Physics2D.Raycast(hit.rigidbody.position, direction, pierceDistance);
                    if(pierceHit){
                    print("rayCastPierce Hit: " + pierceHit.collider.name);
                    }
                } 
        }
    }
}
