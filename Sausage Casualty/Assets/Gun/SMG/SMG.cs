using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SMG : Gun
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PrimaryFire();
        }
    }

    protected override void PrimaryFire() //virtual fns need to be overridden in the child
    {
        if (shootDelayTimer <= 0)
        {            
            //delay gun from shooting again
            shootDelayTimer = gunData.primaryFireDelay;

            if (primaryFireIsShooting || primaryFireHold)
            {
                primaryFireIsShooting = false;

                Vector3 dir = Quaternion.AngleAxis(Random.Range(-gunData.spread, gunData.spread), Vector3.up) * cam.transform.forward;
                dir = Quaternion.AngleAxis(Random.Range(-gunData.spread, gunData.spread), Vector3.right) * dir;
               
                StartCoroutine(BurstWait(dir));
                StopCoroutine(BurstWait(dir));     
            }          
        }
    }

    protected IEnumerator BurstWait(Vector3 dir)
    {
        //bool running = true;

        for (int i = 0; i < 4; i++)
        {
            ray = new Ray(cam.transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, gunData.range))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green, 0.05f);
                //print(ammoInClip);
            }
            ammoInClip--;
            if (ammoInClip <= 0) ammoInClip = gunData.ammoPerClip;


            //particles
            muzzleFlash.Play();
            TrailRenderer trail = Instantiate(bulletTrail, shootPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, dir, hit));

            yield return new WaitForSecondsRealtime(0.1f);
        }
        //running = false;

    }
}
