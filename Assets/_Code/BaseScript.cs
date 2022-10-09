using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public GameManager.PlayerSideEnum enumside;
    public GameManager.PlayerAttribute PlayerAttribute = null;

    private void Update()
    {
        if (PlayerAttribute != null)
        {
            return;
        }

        if (enumside == GameManager.PlayerSideEnum.Left)
        {
            PlayerAttribute = GameManager.Instance?.PlayerLeft;
        }
        else
        {
            PlayerAttribute = GameManager.Instance?.PlayerRight;
        }
    }
} 