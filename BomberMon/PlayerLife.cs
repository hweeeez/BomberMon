using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField]
    public int numberOfLives = 3;
    [SerializeField]
    private float invulnerabilityDuration = 2;
    private bool isInvulnerable = false;
    [SerializeField]
    private GameObject playerLifeImage;
    private List<GameObject> lifeImages;
    public AudioSource hurtms;

    void Start()
    {

        GameObject playerLivesGrid = GameObject.Find("PlayerLivesGrid");
        this.lifeImages = new List<GameObject>();
        for (int lifeIndex = 0; lifeIndex < this.numberOfLives; ++lifeIndex)
        {
            GameObject lifeImage = Instantiate(playerLifeImage, playerLivesGrid.transform) as GameObject;
            this.lifeImages.Add(lifeImage);
        }
    }
    public void LoseLife()
    {
        if (!this.isInvulnerable)
        {
            this.numberOfLives--;
            GameObject lifeImage = this.lifeImages[this.lifeImages.Count - 1];
            Destroy(lifeImage);
            hurtms.Play();
            this.lifeImages.RemoveAt(this.lifeImages.Count - 1);
            if (this.numberOfLives == 0)
            {
                Destroy(this.gameObject);

            }
            this.isInvulnerable = true;
            Invoke("BecomeVulnerable", this.invulnerabilityDuration);
        }
    }
    private void BecomeVulnerable()
    {
        this.isInvulnerable = false;
    }


}
