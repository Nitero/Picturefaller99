﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class PictureJSON
{
    public string[] nature = new string[255];
    public string[] food = new string[255];
    public string[] city = new string[255];

}



public class ImageLoader
{


    public string url = "http://localhost:8000/nature_255/nature_9.jpg";
    public Renderer thisRenderer;

    private static PictureJSON loadJson()
    {
        PictureJSON pictureJSON;
        string path = Application.dataPath + "/StreamingAssets/pictures_all.json";
        string contents = File.ReadAllText(path);
        pictureJSON = JsonUtility.FromJson<PictureJSON>(contents);

        Debug.Log(pictureJSON.nature[0]);
        return pictureJSON;
    }


    // automatically called when game started
    public Sprite[] nature;
    public Sprite[] city;
    public Sprite[] food;
    public ImageLoader(Sprite[] nature, Sprite[] city, Sprite[] food)
    {
        this.nature = nature;
        this.city = city;
        this.food = food;
    }

    private void load(string url, int pos, Sprite[] array)
    {
        Debug.Log("Loading ....");
        WWW wwwLoader = new WWW(url);   // create WWW object pointing to the url
                                        // start loading whatever in that url ( delay happens here )
        while (!wwwLoader.isDone) { }
        Debug.Log("Loaded");
        // set white
        Sprite pic = Sprite.Create(wwwLoader.texture, new Rect(0.0f, 0.0f, wwwLoader.texture.width, wwwLoader.texture.height), new Vector2(0.5f, 0.5f));

        array[pos] = pic;  // set loaded image
    }


    public void loadPictures()
    {
        PictureJSON pictureJSON = loadJson();

        for (int i = 0; i < pictureJSON.nature.Length; i++)
        {
            Debug.Log(i);
           load(pictureJSON.nature[i], i, nature);
            //load(pictureJSON.city[i], i, city);
            //load(pictureJSON.food[i], i, food);
        }



    }




}