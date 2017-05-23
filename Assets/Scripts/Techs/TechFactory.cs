using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TechFactory {
    public static HeadTech ProduceHead(string name)
    {
        if(!Enum.IsDefined(typeof(HeadTech.HeadTechs),name)){
            Debug.LogError("There is no tech with name:"+name+" in type:Head");
            return null;
        }
        switch((HeadTech.HeadTechs)Enum.Parse(typeof(HeadTech.HeadTechs),name)){
            default:
                Debug.LogError("The tech named " + name + " can't be made in Class TechFactory Function ProduceHead");
                return null;
        }
    }

    public static BodyTech ProduceBody(string name)
    {
        if (!Enum.IsDefined(typeof(BodyTech.BodyTechs), name))
        {
            Debug.LogError("There is no tech with name:" + name + " in type:Body");
            return null;
        }
        switch ((BodyTech.BodyTechs)Enum.Parse(typeof(BodyTech.BodyTechs), name))
        {
            default:
                Debug.LogError("The tech named " + name + " can't be made in Class TechFactory Function ProduceBody");
                return null;
        }
    }

    public static FootTech ProduceFoot(string name)
    {
        if (!Enum.IsDefined(typeof(FootTech.FootTechs), name))
        {
            Debug.LogError("There is no tech with name:" + name + " in type:Foot");
            return null;
        }
        switch ((FootTech.FootTechs)Enum.Parse(typeof(FootTech.FootTechs), name))
        {
            case FootTech.FootTechs.doubleJump:
                return new DoubleJump();
            default:
                Debug.LogError("The tech named " + name + " can't be made in Class TechFactory Function ProduceFoot");
                return null;
        }
    }
}
