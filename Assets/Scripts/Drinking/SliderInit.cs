using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderInit : MonoBehaviour
{
    [SerializeField] private Vector2 borders;
    [SerializeField] private GameObject point;
    [SerializeField] private float pointSpeed;
    [SerializeField] private int numberOfSlots;
    [SerializeField] private GameObject[] slots;
    [SerializeField] private DrinkBar drinkBar;
    [SerializeField] private GameObject spaceImage;
    public Animator playerAnimator;
    public Animator enemyAnimator;

    float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            float xSlotPos = Random.Range(borders.x, borders.y);
            slots[i].SetActive(true);
            slots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xSlotPos, 0f);
        }

        DrinkBar.instance.SetMaxAmount(10);
    }

    // Update is called once per frame
    void Update()
    {
        // Move point on slider
        var pointPosition = point.GetComponent<RectTransform>();
        if (pointPosition.anchoredPosition.x >= 160f)
            direction = -1;
        else if (pointPosition.anchoredPosition.x <= -160f)
            direction = 1;

        pointPosition.anchoredPosition += new Vector2(pointSpeed * Time.deltaTime * direction, 0f);

        playerAnimator.SetBool("IsDrinking", false);

        // Check if player pressed space
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (pointPosition.anchoredPosition.x > slots[i].GetComponent<RectTransform>().anchoredPosition.x - slots[i].GetComponent<RectTransform>().rect.width / 2 &&
                pointPosition.anchoredPosition.x < slots[i].GetComponent<RectTransform>().anchoredPosition.x + slots[i].GetComponent<RectTransform>().rect.width / 2)
            {
                spaceImage.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space) && !playerAnimator.GetBool("IsFalling"))
                {
                    drinkBar.Drink(1);
                    playerAnimator.SetBool("IsDrinking",true);
                }
            }
            else
                spaceImage.SetActive(false);
        }

        if (drinkBar.drinkLeft <= 0)
        {
            enemyAnimator.SetBool("IsFalling", true);
            playerAnimator.transform.parent.GetComponent<Lean>().Winner(true);
        }
    }
}
