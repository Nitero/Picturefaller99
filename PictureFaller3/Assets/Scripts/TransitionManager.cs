﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Image screenFadeImg;
    [SerializeField] private float screenFadeSpd = 0.25f;
    [SerializeField] private float screenWhiteDur = 0.5f;

    // TODO: Move player slowmoTimer here !!!

    private ChunkManager chunkManager;
    private CameraManager cameraManager; 
    private bool hitWall;

    void Start()
    {
        chunkManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<ChunkManager>();
        cameraManager = Camera.main.GetComponent<CameraManager>();

        setFadeAlpha(0);
    }


    void Update()
    {
        
    }


    public void hitPicWall()
    {
        if(!hitWall)
        {
            hitWall = true;
            StartCoroutine(fadeScreenWhiteOver(1, screenFadeSpd));
        }
    }




    private void setFadeAlpha(float am)
    {
        var col = screenFadeImg.color;
        col.a = am;
        screenFadeImg.color = col;
    }


    private IEnumerator fadeScreenBackOver(float aValue, float aTime)
    {
        float alpha = screenFadeImg.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            setFadeAlpha(Mathf.Lerp(alpha, aValue, t));
            yield return null;
        }
    }


        private IEnumerator fadeScreenWhiteOver(float aValue, float aTime)
    {
        float alpha = screenFadeImg.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            setFadeAlpha(Mathf.Lerp(alpha, aValue, t));
            yield return null;
        }


        yield return new WaitForSeconds(screenWhiteDur);


        // Screen is now completely white
        StartCoroutine(fadeScreenBackOver(0, screenFadeSpd));

        chunkManager.resetChunksAndWall();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().reroute();

        cameraManager.setNormalCam();

        hitWall = false;

    }

}
