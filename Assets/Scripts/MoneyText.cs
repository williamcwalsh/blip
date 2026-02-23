using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] public Ship ship;
    [SerializeField] private TextMeshProUGUI moneyText;

    void Start(){

    }
    void Update()
    {
        moneyText.text = ship.money.ToString();
    }
}