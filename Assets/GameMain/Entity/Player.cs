using UnityEngine;

namespace SchoolRPG.GameMain
{
    public class Player : EntityBase
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.position = GetCurrentWorldPos(2, 2);
        }

        private const float moveDist = 0.05f;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W) && ValidatePos(transform.position + moveDist * Vector3.up))
                transform.position += moveDist * Vector3.up;
            if (Input.GetKey(KeyCode.A) && ValidatePos(transform.position + moveDist * Vector3.left))
                transform.position += moveDist * Vector3.left;
            if (Input.GetKey(KeyCode.S) && ValidatePos(transform.position + moveDist * Vector3.down))
                transform.position += moveDist * Vector3.down;
            if (Input.GetKey(KeyCode.D) && ValidatePos(transform.position + moveDist * Vector3.right))
                transform.position += moveDist * Vector3.right;

            if (Input.GetKeyDown(KeyCode.J))
                TryShoot();
        }

        protected override void Shoot()
        {
            
        }
    }
}
