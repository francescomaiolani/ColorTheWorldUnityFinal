using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager {


    List<Resource> resources = new List<Resource>();

    public ResourceManager() {
        // QUANDO VIENE CREATO CARICA AUTOMATICAMENTE TUTTI I VALORI SALVATI DI RISORSE
        resources.Add(new Resource("gold", PlayerPrefs.GetInt("gold")));
        resources.Add(new Resource("gems", PlayerPrefs.GetInt("gems")));
        resources.Add(new Resource("level", PlayerPrefs.GetInt("level")));
        resources.Add(new Resource("experience", PlayerPrefs.GetInt("experience")));
    }
    public void AddResource(Resource resource) {
        resources.Add(resource);
    }

    public void RemoveResource(Resource resource) {
        resources.Remove(resource);
    }

    public void AddToResource(string nome, int amount) {
        Resource resource = FindResource(nome);
        resource.AddToAmount(amount);
    }

    public void SetResourceAmount(string nome, int amount) {
        Resource resource = FindResource(nome);
        resource.SetAmount(amount);
    }

    public Resource FindResource(string nome) {
        foreach (Resource r in resources) {
            if (r.GetNome() == nome)
                return r;

        }
        return null;
    }
}
