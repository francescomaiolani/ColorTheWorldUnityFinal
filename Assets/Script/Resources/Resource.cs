using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource {

    string nome;
    int amount;

    public Resource(string nome, int amount)  {
        this.nome = nome;
        this.amount = amount;
    }

    public string GetNome() {
        return nome;
    }

    public int GetAmount() {
        return amount;
    }

    public void AddToAmount(int amountToAdd) {
        amount += amountToAdd;
    }

    public void SetAmount(int quantita) {
        amount = quantita;
    }

    public void SubtractToAmount(int amountToAdd)
    {
        amount -= amountToAdd;
        amount = (int)Mathf.Clamp(amount, 0, Mathf.Infinity);
    }


}
