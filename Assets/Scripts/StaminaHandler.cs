using UnityEngine;

public class StaminaHandler : ScriptableObject
{
    [SerializeField] private float moveCost = .001f;
    [SerializeField] private float jumpCost = .10f;
    [SerializeField] private float meowCost = .01f;
    [SerializeField] private float pushCost = .01f;
    [SerializeField] private float gainByStayingStill = 0.2f;

    public void Increase(float value)
    {
        GameplayScreen.Instance.MeterCatStamina = Mathf.Clamp01(GameplayScreen.Instance.MeterCatStamina + value);
    }

    public void Decrease(float value)
    {
        GameplayScreen.Instance.MeterCatStamina = Mathf.Clamp01(GameplayScreen.Instance.MeterCatStamina - value);
    }

    public void DecreaseByMovementCost()
    {
        Decrease(moveCost);
    }

    public void DecreaseByJumpCost()
    {
        Decrease(jumpCost);
    }

    public void DecreaseByMeowCost()
    {
        Decrease(meowCost);
    }
    public void DecreaseByPushCost()
    {
        Decrease(pushCost);
    }

    public void IncreaseWhenStill()
    {
        Increase(gainByStayingStill);
    }
}
