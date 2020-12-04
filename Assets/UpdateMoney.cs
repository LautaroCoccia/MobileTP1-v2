using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpdateMoney : MonoBehaviour
{

    public Player _player;

    public TextMeshProUGUI TMP;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        TMP.text = "$" + _player.Dinero.ToString();
    }
}
