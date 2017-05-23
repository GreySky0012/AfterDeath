using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechManager {
    private HeadTech _head;
    private FootTech _foot;
    private BodyTech _body;

    public TechManager(string[] techs)
    {
        for (int i = 0; i < 3; i++)
        {
            ProduceTech((Tech.Type)i, techs[i]);
        }
    }

    public ArrayList GetTechs()
    {
        ArrayList techs = new ArrayList();
        if(_head != null)
            techs.Add(_head);
        if (_body != null)
            techs.Add(_body);
        if (_foot != null)
            techs.Add(_foot);
        return techs;
    }

    public string[] ToString()
    {
        string[] strings = new string[4];
        if (_head != null)
            strings[0] = _head._name;
        else
            strings[0] = "none";
        if (_body != null)
            strings[1] = _body._name;
        else
            strings[1] = "none";
        if (_foot != null)
            strings[3] = _foot._name;
        else
            strings[3] = "none";
        return strings;
    }

    public void ProduceTech(Tech.Type type, string name)
    {
        if (name == "none")
            return;
        switch (type)
        {
            case Tech.Type.head:
                _head = TechFactory.ProduceHead(name);
                break;
            case Tech.Type.body:
                _body = TechFactory.ProduceBody(name);
                break;
            case Tech.Type.foot:
                _foot = TechFactory.ProduceFoot(name);
                break;
            default:
                Debug.LogError("The part of tech is wrong in Class TechManager Function ProduceTech");
                break;
        }
    }

    public void TechsUpdate(PlayerController player)
    {
        foreach(Tech t in GetTechs()){
            t.TechUpdate(player);
        }
    }

    public void CalPlayerDate(PlayerController.Data originDate, PlayerController.Data data)
    {
        foreach (Tech t in GetTechs())
        {
            t.CalPlayerData(originDate, data);
        }
    }
}
