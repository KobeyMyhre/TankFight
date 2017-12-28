using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TankPowerGun : MonoBehaviour {


    [HideInInspector]
    TankScore score;
   
    TankController controller;
    
    Statics.Player playerNum; // Who Shot The Bullet
    
    //Current PowerUp
    public Stack<PowerUpGun> myPowerUp;

    //Effects for PowerUp
    public GameObject Effect;
    public GameObject barrel;
    public TextMesh outOfAmmoText;
    public Image ammoFill;
    public string ImageName;
    //PowerUps TriggerDelay
    float bulletDelay;

    // Use this for initialization
    void Start ()
    {
        bulletDelay = 0;
        outOfAmmoText.gameObject.SetActive(false);
        myPowerUp = new Stack<PowerUpGun>();
        controller = GetComponent<TankController>();
        score = GetComponent<TankScore>();
        ammoFill = GameObject.Find(ImageName).GetComponent<Image>();
        //Assign who this is
        switch (controller.playerNum)
        {
            case 1:
                playerNum = Statics.Player.one;
                break;
            case 2:
                playerNum = Statics.Player.two;
                break;
            case 3:
                playerNum = Statics.Player.three;
                break;
            case 4:
                playerNum = Statics.Player.four;
                break;
        }
        
    }
    public void turnOffEffect()
    {
        Effect.SetActive(false);
    }
    // Update is called once per frame
    void Update ()
    {
       
		if(controller.autoFire && bulletDelay <= 0)
        {
            if(myPowerUp.Count > 0)
            {
                if(!myPowerUp.Peek().displayAmmoText())
                {
                    Effect.SetActive(true);
                    Invoke("turnOffEffect", .2f);
                    myPowerUp.Peek().shoot(score, playerNum, barrel.transform);
                    bulletDelay = myPowerUp.Peek().fireDelay;
                    ammoFill.fillAmount = ((float)myPowerUp.Peek().currentSpecialAmmo / (float)myPowerUp.Peek().maxSpecialAmmo);
                }
                else
                {
                    outOfAmmoText.gameObject.SetActive(true);
                }
                
            }
           
        }
        else if(bulletDelay > 0)
        {
            bulletDelay -= Time.deltaTime;
        }
        else
        {
            outOfAmmoText.gameObject.SetActive(false);
        }
	}
}
