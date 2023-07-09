using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] GameObject food;
    [SerializeField] List<Sprite> sprites;

    private bool makingNom = false;

    void Update()
    {
        if (transform.childCount == 0)
        {
            if (!makingNom)
            {
                makingNom = true;
                StartCoroutine(SpawnFoodRoutine());
            }
        }
    }

    IEnumerator SpawnFoodRoutine()
    {
        var newFood = Instantiate(food);
        SpriteRenderer spriteRenderer = newFood.GetComponent<SpriteRenderer>();
        var index = Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[index];
        newFood.transform.parent = this.transform;
        newFood.transform.position = this.transform.position;

        yield return new WaitForSeconds(20f);
        makingNom = false;
    }
}
